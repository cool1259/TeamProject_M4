using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class TIME : NetworkBehaviour
{
    Text time_;
   

    private int time;
    private int m_time;
    private int s_time;

    [SyncVar]
    public float tt;
    // Start is called before the first frame update
    void Start()
    {
        time_= GetComponent<Text>();

 
    }

    // Update is called once per frame
    void Update()
    {


        time = (int)tt;
        m_time = time / 60;
        s_time = time % 60;
        time_.text = m_time.ToString("00") + " : " + s_time.ToString("00");

        if (!isServer)
            return;
            tt += Time.deltaTime;
           
        
    }
}
