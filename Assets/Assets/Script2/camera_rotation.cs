using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_rotation : MonoBehaviour
{
  
 

  

    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;






    public Transform cm;

    public GameObject AIM_CAMVAS;

    // Start is called before the first frame update
    void Start()
    {
   


        //AIM_CAMVAS = GameObject.FindGameObjectWithTag("AIM_CANVAS");

          //AIM_CAMVAS.transform.parent = this.transform;
       // cm = GameObject.FindGameObjectWithTag("aa").transform;
       // cm.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
     
        // 마우스 입력
        rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        //반동
        if (Input.GetMouseButton(0))
            rotationY += 10f * Time.deltaTime;

        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
      



    }
}
