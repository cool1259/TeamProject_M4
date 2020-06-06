using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myrandom_spawn_position : MonoBehaviour
{
    // Start is called before the first frame update
    private int rnd_index;
    private int rnd_index2;
    public static bool onstart = false;
    // Start is called before the first frame update
    int count = 0;
    void Awake()
    {
 

        rnd_index = Random.Range(-100, 100); //0,1,2
        rnd_index2 = Random.Range(-100, 100); //0,1,2
      gameObject.transform.position = new Vector3(rnd_index, 100, rnd_index2);
    }

    // Update is called once per frame
    void Update()
    {
        count += 1;
        if (count % 10 == 0)
        {
            rnd_index = Random.Range(-100, 100); //0,1,2
            rnd_index2 = Random.Range(-100, 100); //0,1,2
       gameObject.transform.position = new Vector3(rnd_index, 100, rnd_index2);
        }

    }
}
