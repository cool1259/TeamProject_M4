using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool iswater=false;

    [SerializeField] private Color waterColor; //물색
    [SerializeField] private float waterfogDensity;  //물탁함
    private Color Origin_color;
    private float Origin_Desity;
    private float Origin_fogStartDistance;

    [SerializeField] private float waterDrag;
    [SerializeField] private float waterSpeed;
    private float OriginDrag;
    private float OriginSpeed;

    public GameObject inwater;
    public GameObject outwater;
    public GameObject diving_water;

    private bool UI_ON=false;
    void Start()
    {


        OriginDrag = 20;
        OriginSpeed = 10;
        Origin_color = RenderSettings.fogColor;
   Origin_Desity = RenderSettings.fogDensity;
        Origin_fogStartDistance = RenderSettings.fogStartDistance;
        inwater.GetComponent<AudioSource>();
        outwater.GetComponent<AudioSource>();
        diving_water.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            UI_ON = true;


        }
        if (Input.GetKey(KeyCode.N))
        {


            UI_ON = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {





        if (other.gameObject.CompareTag("Player"))
        {
            diving_water.GetComponent<AudioSource>().Play();
            inwater.GetComponent<AudioSource>().Play();

            other.GetComponent<fire>().enabled = false;
            other.GetComponent<gun_sound>().enabled = false;

        }


    }
    private void OnTriggerStay(Collider other)
    {





        if (other.gameObject.CompareTag("Player"))
        {
         iswater = true;


            if (UI_ON == true)
            {
                RenderSettings.fogStartDistance = Origin_fogStartDistance;
                RenderSettings.fogColor = Origin_color;

                RenderSettings.fogDensity = Origin_Desity;

            }
            else
            {
                RenderSettings.fogStartDistance = -300;
                RenderSettings.fogColor = waterColor;
                RenderSettings.fogDensity = waterfogDensity;
            }
            other.GetComponent<player_control>().gravity = waterDrag;
            other.GetComponent<player_control>().speed = waterSpeed;

        }


    }

    private void OnTriggerExit(Collider other)
    {





        if (other.gameObject.CompareTag("Player"))
        {

            iswater = false;

            outwater.GetComponent<AudioSource>().Play();
            inwater.GetComponent<AudioSource>().Stop();
            RenderSettings.fogStartDistance = Origin_fogStartDistance;
            RenderSettings.fogColor = Origin_color;
                
            RenderSettings.fogDensity = Origin_Desity;
            other.GetComponent<player_control>().gravity = OriginDrag;
            other.GetComponent<player_control>().speed = OriginSpeed;

            other.GetComponent<fire>().enabled = true;
            other.GetComponent<gun_sound>().enabled = true;
        }
    }


    IEnumerator water_damage()
    {
        yield return new WaitForSeconds(20f);
      
        

    }

}
