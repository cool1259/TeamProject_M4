using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int remain_bots;
    public GameObject BOTS;
    private Text winner_text;
    private Text remain_oj;
    private GameObject temp_bot;
    private Text endM;

   
    public int player_num=0;
    public int bot_num = 3;
    // Start is called before the first frame update
    void Start()
    {
  
            winner_text = GameObject.Find("WINNER_TEXT").GetComponent<Text>();
        temp_bot = GameObject.Find("TEMP_BOT");
       // temp_bot = GameObject.FindGameObjectWithTag("BOT");
        remain_oj = GameObject.Find("REMAIN_OBJECT").GetComponent<Text>();

        endM = GameObject.Find("endM").GetComponent<Text>();

        bot_minus();
   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var a = GameObject.Find("자기장");
        if (a)
        {
            if (temp_bot)
            {
                if (remain_bots == 14)
                {
                   bot_minus();
                    Destroy(temp_bot);
                }
            }
          
       
            remain_bots = bot_num; //GameObject.FindGameObjectsWithTag("BOT").Length - 1;

            remain_oj.text =   remain_bots + "생존" ;

       

        }
        else
        {
            if (temp_bot)
            {
                if (GameObject.FindGameObjectsWithTag("Player").Length == 3)
                {
                  
                    Destroy(temp_bot);

                //    Debug.Log("삭제됨오월칠일");
                }
            }
            remain_bots = player_num;//+GameObject.FindGameObjectsWithTag("Player").Length - 1;

            remain_oj.text = remain_bots +"생존";

        }

        if (!temp_bot)
        {
            if (remain_bots == 1)
            {
                winner_text.text = "경기가 종료 되었습니다";
                // Mouse Lock
                Cursor.lockState = CursorLockMode.None;
                // Cursor visible
                Cursor.visible = true;
            }
        }


    }

    public void player_minus()
    {
        player_num = player_num - 1;
    }

    public void player_plus()
    {
        player_num = player_num + 1;
    }

    public void bot_plus()
    {
        bot_num = bot_num + 1;
    }

    public void bot_minus()
    {
        bot_num = bot_num - 1;
    }

    //  IEnumerator aa()
    //  {
    //     yield return new WaitForSeconds(3.0f);
    //     remain_bots = GameObject.FindGameObjectsWithTag("BOT").Length;
    //  }
}
