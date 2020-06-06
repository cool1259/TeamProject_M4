using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[NetworkSettings(channel =0, sendInterval = 0.01666f)]
public class player_control : NetworkBehaviour
{


    //시작하면 브금부터끄자 이미지도
    private AudioSource main_tema;
    private GameObject degi_image;

    public float rotSpeed = 100f;
    public float speed;      // 캐릭터 움직임 스피드.
    public float jumpSpeed; // 캐릭터 점프 힘.
    public float gravity;    // 캐릭터에게 작용하는 중력.

    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.

    public float minX = -45.0f;
    public float maxX = 45.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;

    
    Vector3 mousePos;
    private Animator anim;

    Rigidbody rb;
   
    int jpct=0;

    private bool skilling = false;

    public Transform cm;
    public Transform minimap;
    private Vector3 offset;

 
    private Vector3 plus;

    private Vector3[] startingpoint = new Vector3[3];
  
    private int rnd_index;


    public GameObject AIM_CAMVAS;
    
    //1인칭
    public Camera dote_camera;
    public GameObject dote_mode;
    private int Viewpoint=3;

    GameObject HUSUABI;

    private Transform spine; // 아바타의 상체
    private Transform hips; // 아바타의 상체
                            //public GameObject spine_object;

    private Camera mapcamera;
    private bool mapon = false;
    public GameObject my_direction;

    private AudioSource foot_sound;
    private AudioSource run_sound;

    public Text kill_text;
    public Text n_kill_text;
    public Text winner_text;
    private Text endM;
    private Text endM2;
    private Text endM4;
    private GameObject endM3;

    int heal_size=0;


   

    [SerializeField] private Color gameEndColor;
    [SerializeField] private float gameEndDensity;

    public Transform kill_cal;

   // [SyncVar]
    public GameObject Time_text;

    public GameObject field_prefab;
    private int field_rnd_index; //자기장위치
    private int field_rnd_index2;

    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        main_tema = GameObject.Find("Tema_song").GetComponent<AudioSource>();
        main_tema.mute = true;
        degi_image = GameObject.Find("DEGI_IMAGE");
        if(degi_image)
        degi_image.SetActive(false);


        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
        speed = 10.0f;
        jumpSpeed = 8.0f;
        gravity = 20.0f;

        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();

        /*
        AIM_CAMVAS = GameObject.FindGameObjectWithTag("AIM_CANVAS");

       AIM_CAMVAS.transform.parent = this.transform.GetChild(5);
        cm = GameObject.FindGameObjectWithTag("aa").transform;
        cm.position = new Vector3(transform.position.x - 0.04998779f, transform.position.y + 2.48999f, transform.position.z + -4.31f);
        cm.parent = this.transform.GetChild(5);
        */

        AIM_CAMVAS = GameObject.Find("equip");
        AIM_CAMVAS.transform.position = transform.position;
        AIM_CAMVAS.transform.parent = this.transform.GetChild(5);
        // //minimap = GameObject.Find("direc").transform;
       //  minimap.position = transform.position;
       // minimap.parent = this.transform.GetChild(6);

        // minimap = GameObject.FindGameObjectWithTag("Minimap_camera").transform;
        // minimap.parent = this.transform;





        rnd_index = Random.Range(0,3); //0,1,2


        startingpoint[0] = new Vector3(0, 0f, 0);
        startingpoint[1] = new Vector3(-145.29f, 1.7f, -5.29f);
        startingpoint[2] = new Vector3(150.6548f, 1.763f, -1.62f);

       




        HUSUABI = GameObject.Find("HUSUABI");
        
        spine = anim.GetBoneTransform(HumanBodyBones.Spine);
        hips= anim.GetBoneTransform(HumanBodyBones.Hips);

      mapcamera = GameObject.Find("mapcamera").GetComponent<Camera>();
        my_direction = GameObject.Find("My_direction");
        my_direction.SetActive(false);

        foot_sound = GameObject.Find("foot_sound").GetComponent<AudioSource>();
        run_sound = GameObject.Find("run_sound").GetComponent<AudioSource>();

        kill_text = GameObject.Find("killtext").GetComponent<Text>();
        n_kill_text = GameObject.Find("n_kill").GetComponent<Text>();
        winner_text = GameObject.Find("WINNER_TEXT").GetComponent<Text>();
        endM = GameObject.Find("endM").GetComponent<Text>();
        endM2 = GameObject.Find("endM2").GetComponent<Text>();
        endM3= GameObject.Find("endbutton");
        endM3.SetActive(false);
        endM4 = GameObject.Find("endM4").GetComponent<Text>();

        kill_cal = GameObject.Find("kill_cal").GetComponent<Transform>();

        var a = GameObject.Find("자기장");
        if (a)
            winner_text.text = "100초뒤 자기장이 가동됩니다.";
        else
            winner_text.text = "20초뒤 자기장이 가동됩니다.";



        if (isServer)
            {
           // Time_text = GameObject.Find("time_text");
           // Time_text.GetComponent<TIME>().tt -= Time.time;
            StartCoroutine(start_multi_game());
          }

      

    }

    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        

        
        HUSUABI.transform.position = transform.position+new Vector3(0,2f,0);

       
        Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
                                                 // Cursor.visible = false;//마우스 커서 보이기


        Debug.Log(controller.Move(MoveDir * Time.deltaTime)); //이게있어야뛰네;;



        if (Input.GetKey(KeyCode.C) || Water.iswater == true)
        {
            anim.speed = 0.5f;
            speed = 2.5f;
        }

        else if (Input.GetKey(KeyCode.LeftShift)&& !Input.GetMouseButton(0) &&!Input.GetMouseButtonDown(0))
        {
          //  anim.speed = 2f;
            speed = 7f;
            anim.SetBool("realrun", true);
        }
        else
        {
            anim.speed = 1.25f;
            speed = 5f;
            anim.SetBool("realrun", false);
        }


        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            //Debug.Log("gun");

            anim.SetBool("SHOOT", true);
            anim.SetBool("realrun", false);
            speed = 1f;
        }

        else
        {
            anim.SetBool("SHOOT", false);
            
            if (controller.velocity == new Vector3(0, 0, 0))
            {
                anim.SetBool("isRun", false);
                if(anim.GetBool("realrun")==false)
                anim.speed = 5;

            }
            else
            {
                anim.SetBool("isRun", true);

            }
        }


         if (Input.GetKey(KeyCode.Alpha5))
        {
            anim.SetBool("bomb", true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            anim.SetBool("bomb", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
           if (Viewpoint == 3)
            {
                dote_mode.SetActive(true);
                dote_camera.depth = 10;
                AIM_CAMVAS.SetActive(false);
                Viewpoint = 1;
            }
            else
            {
              dote_mode.SetActive(false);
             dote_camera.depth = 1;
                AIM_CAMVAS.SetActive(true);
                Viewpoint = 3;

            }
      

        }

       if (Input.GetKey(KeyCode.M))
        {
            my_direction.SetActive(true);
            mapcamera.depth = 11;
      

        }
        if (Input.GetKey(KeyCode.N))
        {
            my_direction.SetActive(false);
            mapcamera.depth = -5;
         

        }



        //걷더나 뛰는 모션 때 발소리
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("walk") && Water.iswater == false)
        {
            if (!foot_sound.isPlaying)
            {
                run_sound.mute = true;
                foot_sound.mute = false;
                foot_sound.Play();
               
            }
            
        }
  
      else if (anim.GetCurrentAnimatorStateInfo(0).IsName("run") && Water.iswater == false)
        {

            if (!run_sound.isPlaying)
            {
                foot_sound.mute = true;
                run_sound.mute = false;
                run_sound.Play();
        
                
            }

        }
        else
        {
            foot_sound.mute = true;
            run_sound.mute = true;
        }
        /*
                 // 마우스 입력
                 rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
                 rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

                 //반동
                 if (Input.GetMouseButton(0))
                     rotationY += 10f * Time.deltaTime;

                rotationY = Mathf.Clamp(rotationY, minY, maxY);
                 transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                 */





        // var dir = cube2.transform.position - spine.position;
        //var q = Quaternion.LookRotation(dir);
        //anim.SetBoneLocalRotation(HumanBodyBones.Spine, q);





        // 현재 캐릭터가 땅에 있는가?
        if (controller.isGrounded )
        {
            // 위, 아래 움직임 셋팅. 
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
            MoveDir = transform.TransformDirection(MoveDir);

            // 스피드 증가.
            MoveDir *= speed;

            // 캐릭터 점프

            if (Input.GetButton("Jump"))
            {

               
                    MoveDir.y = jumpSpeed;
                //    anim.SetBool("jump", true);
                //    StartCoroutine(CheckAnimationState());
                
            }

        }
            // 캐릭터에 중력 적용.
            MoveDir.y -= gravity * Time.deltaTime;



        // 캐릭터 움직임.
            controller.Move(MoveDir * Time.deltaTime);

        
    }
    /*
    IEnumerator CheckAnimationState()
    {

        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))

        {
            //전환 중일 때 실행되는 부분
            yield return null;




        }

        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.1f)
        {
            //애니메이션 재생 중 실행되는 부분
            yield return null;

            
        }

        //애니메이션 완료 후 실행되는 부분

        anim.SetBool("jump", false);
    }
    */
    private void LateUpdate()
    {
        //  spine.LookAt(cube2.transform.position); //플레이어의 상체부분이 타겟 위치 보기 
        //spine.rotation = spine.rotation * Quaternion.Euler(cube2.transform.position - spine.position); // 타겟으로 회전

        //  var dir = new Vector3(100, -40, -100); ;
        //  var q = Quaternion.LookRotation(dir);
        //  anim.SetBoneLocalRotation(HumanBodyBones.UpperChest, q);
      

    

        if (!isLocalPlayer)
            return;
      
        // 마우스 입력
        rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        //반동
        if (Input.GetMouseButton(0))
            rotationY += 10f * Time.deltaTime;

        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);


       
        spine.transform.localEulerAngles = new Vector3 (0,0, rotationY);

        if(kill_text.text.Equals("적을 처치하였습니다"))
            StartCoroutine(killtext_reset());
        if (winner_text.text.Equals("경기가 종료 되었습니다"))
        {

        
           
         
            //혼자남았을때 내가 살아있어야 이긴거지
            if(gameObject.GetComponent<health>().currentHealth>0)
                StartCoroutine(gameEnd());

        // gameObject.GetComponent<health>().enabled = false;
        }


    }
    public override void OnStartLocalPlayer()
    {
      //  GameObject.FindGameObjectWithTag("LOCAL_CAMERA").GetComponent<CameraFollow>().target =gameObject.transform;
    }
    //  if (isLocalPlayer)
    //     {
    //       GameObject.FindGameObjectWithTag("LOCAL_CAMERA").GetComponent<CameraFollow>().target = transform; ;
    //
    //  }

        public void gamelose()
    {
         StartCoroutine(gameEnd_lose());
        
    }
    public void gamewin()
    { 
        StartCoroutine(gameEnd());

    }
    IEnumerator killtext_reset()
    {
        yield return new WaitForSeconds(2.0f);
        kill_text.text = "";
        n_kill_text.text = "";
        winner_text.text = "";
    }
    IEnumerator gameEnd()
    {
        yield return new WaitForSeconds(10f);
        winner_text.text = "";
        RenderSettings.fogStartDistance = -300;
        RenderSettings.fogColor = gameEndColor;
        RenderSettings.fogDensity = gameEndDensity;
        endM.text = "이겼다 야호";

        var a = GameObject.Find("자기장");
        var game = GameObject.Find("GameManager");
        var remain = game.GetComponent<GameManager>();
        if (a)
        {
            winner_text.text = "";
            endM2.text = 1 + "등!" + "\n\n" + "킬수: " + kill_cal.transform.position.x ;
            endM4.text = "명중률: 84%\n\n" + "회복량: " + heal_size;
            
        }

        else { 
            endM2.text = 1+ "등!" +"\n\n" + "킬수: 1";
        endM4.text = "회복량: " + heal_size;
    }


    endM3.SetActive(true);

        if (gameObject.GetComponent<gun_sound>())
            gameObject.GetComponent<gun_sound>().enabled = false;
        Time.timeScale = 0;
      //  StartCoroutine(realEnd());
    }

    IEnumerator gameEnd_lose()
    {
        yield return new WaitForSeconds(0.5f);
        winner_text.text = "";
        RenderSettings.fogStartDistance = -300;
        RenderSettings.fogColor = gameEndColor;
        RenderSettings.fogDensity = gameEndDensity;
        endM.text = "패배";

        var a = GameObject.Find("자기장");
        var game = GameObject.Find("GameManager");
        var remain = game.GetComponent<GameManager>();
        if (a)
        {
            endM2.text = remain.bot_num + "등" + "\n\n" + "킬수: " + kill_cal.transform.position.x;
            endM4.text = "명중률: 84%\n\n" + "회복량: " + heal_size;
        }
        else
        {
            endM2.text = remain.player_num + 1 + "등" + "\n\n" + "킬수: 0";
            endM4.text =  "회복량: " + heal_size;
        }


        endM3.SetActive(true);
       //4mon9// Time.timeScale = 0;

        //  StartCoroutine(realEnd());
    }
    public void exit()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("HEAL_PACK"))
        {
            
       if(GetComponent<health>().currentHealth > 0)
            {
                heal_size += 100 - (int)GetComponent<health>().currentHealth;
                GetComponent<health>().currentHealth = 100;
              
                Destroy(other.gameObject);
            }

           

        }

    }
    IEnumerator start_multi_game()
    {
        yield return new WaitForSeconds(20f);
        var a = GameObject.Find("자기장");
        if (!a)
            Cmdfield();
    }
    [Command]
    void Cmdfield()
    {
        Transform aa=GameObject.Find("temp_field").GetComponent<Transform>();
        field_rnd_index = Random.Range(-300, 300); //0,1,2
        field_rnd_index2 = Random.Range(-300, 300); //0,1,2
        aa.position= new Vector3(field_rnd_index, 0, field_rnd_index2);
     
        GameObject field = Instantiate(field_prefab,aa );

   
        NetworkServer.Spawn(field);

     // winner_text.text = "자기장기 가동 되었습니다.";
     // StartCoroutine(killtext_reset());
    }
}