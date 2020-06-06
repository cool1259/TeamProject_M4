using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class fire : NetworkBehaviour
{
    public GameObject Bulletprefab;
    public Transform multi_FirePos;
    public Transform FirePos;
    public GameObject aim;




    // Start is called before the first frame update
    void Start()
    {

        
        

        aim = GameObject.Find("aim_fire");
      //  FirePos = AIM_CAMVAS.transform.GetChild(0).GetChild(1);
    }

    // Update is called once per frame
    void Update()

    {

        if (!isLocalPlayer)
        {
            return;
        }
        var a = GameObject.Find("자기장");
        //멀티
        if (!a)
        {
            if (Input.GetMouseButton(0))
            {
                CmdMultiFire();

            }
            if (Input.GetMouseButtonDown(0))
            {
                CmdMultiFire();
            }
        }
        //솔플
        else
        {
            if (Input.GetMouseButton(0))
            {
                CmdFire();

            }
            if (Input.GetMouseButtonDown(0))
            {
                CmdFire();
            }
        }


    }




    [Command]
  
    void CmdMultiFire()
    {
        GameObject bullet=Instantiate(Bulletprefab, multi_FirePos.position, multi_FirePos.rotation);


        if(!Input.GetKey(KeyCode.R))
         bullet.transform.Rotate(Vector3.right*3f);



        // bullet.transform.LookAt(fire_target);
        // bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 1000f * Time.deltaTime;
        //  bullet.transform.Translate(Vector3.forward * 200f * Time.deltaTime);
        NetworkServer.Spawn(bullet);
        

      Destroy(bullet, 3f);
    }

    void CmdFire()
    {
        GameObject bullet = Instantiate(Bulletprefab, FirePos.position, FirePos.rotation);

        // bullet.transform.LookAt(fire_target);
        // bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 1000f * Time.deltaTime;
        //  bullet.transform.Translate(Vector3.forward * 200f * Time.deltaTime);
        NetworkServer.Spawn(bullet);


        Destroy(bullet, 3f);
    }

}
