using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEXT_COLOR_CHANGE : MonoBehaviour
{
    // Start is called before the first frame update

    bool a = true;
    public Color C1;
    public Color C2;
    public Text subject;
    // Start is called before the first frame update
    void Start()
    {

        subject = gameObject.GetComponent<Text>();
        
        subject.color = C1;
    }

    // Update is called once per frame
    void Update()
    {



        if (a)
        {
            StartCoroutine(color_change());
            a = false;
        }
    }

    IEnumerator color_change()
    {
        yield return new WaitForSeconds(0.35f);
        subject.color = C1;

        StartCoroutine(color_change2());
    }
    IEnumerator color_change2()
    {
        yield return new WaitForSeconds(0.35f);
        subject.color = C2;
        StartCoroutine(color_change());
    }
}

