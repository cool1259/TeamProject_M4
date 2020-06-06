using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monter_bullet : MonoBehaviour
{
    public Transform target_position;
    public GameObject target;
  
    void Start()
    {
       
        target = GameObject.FindGameObjectWithTag("Player");
        target_position = target.transform;

       // Destroy(gameObject,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
            transform.LookAt(target_position);

            transform.Translate(Vector3.forward * 200f * Time.deltaTime);
       
    }
    private void OnTriggerEnter(Collider other)
    {





        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("aaqq");
          
           //   StartCoroutine(delete_bullet());
            //  other.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        var hit = other.gameObject;
        var health = hit.GetComponent<health>();

        //이것도서버에서만발동됨 왜냐면
        //테이크데미지함수에서 !isserver에걸리거든
        if (health != null)
        {
            health.TakeDamage(3);

        
           
        }

    }

    IEnumerator delete_bullet()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

   
}

