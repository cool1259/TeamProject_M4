using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetic_field2 : MonoBehaviour
{
    private int rnd_index;
    private int rnd_index2;
    public static bool onstart = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(start_magnetic_field());

       // rnd_index = Random.Range(-300, 300); //0,1,2
       // rnd_index2 = Random.Range(-300, 300); //0,1,2
        //gameObject.transform.position = new Vector3(rnd_index, 0, rnd_index2);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x >= 10f)// && onstart == true)
        {
            transform.localScale -= new Vector3(0.13f, 0, 0.13f);
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BOT"))
        {

            var hit = other.gameObject;
            var health = hit.GetComponent<health>();

            if (health != null)

                health.in_magnetic_field = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BOT"))
        {

            var hit = other.gameObject;
            var health = hit.GetComponent<health>();

            if (health != null)

                health.in_magnetic_field = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BOT"))
        {


            var hit = other.gameObject;
            var health = hit.GetComponent<health>();

            if (health != null)

                health.in_magnetic_field = false;

        }

    }
    IEnumerator start_magnetic_field()
    {
        yield return new WaitForSeconds(1f);
        onstart = true;
        //이때부터자기장줄어듬





    }

}
