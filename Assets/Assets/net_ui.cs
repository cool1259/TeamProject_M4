using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
namespace UnityEngine.Networking
{
    /// <summary>
    /// An extension for the NetworkManager that displays a default HUD for controlling the network state of the game.
    /// <para>This component also shows useful internal state for the networking system in the inspector window of the editor. It allows users to view connections, networked objects, message handlers, and packet statistics. This information can be helpful when debugging networked games.</para>
    /// </summary>
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("The high level API classes are deprecated and will be removed in the future.")]
    public class net_ui : MonoBehaviour
    {
        public GameObject menual;
        /// <summary>
        /// The NetworkManager associated with this HUD.
        /// </summary>
        public NetworkManager manager;
        /// <summary>
        /// Whether to show the default control HUD at runtime.
        /// </summary>
        [SerializeField] public bool showGUI = true;
        /// <summary>
        /// The horizontal offset in pixels to draw the HUD runtime GUI at.
        /// </summary>
        [SerializeField] public int offsetX;
        /// <summary>
        /// The vertical offset in pixels to draw the HUD runtime GUI at.
        /// </summary>
        [SerializeField] public int offsetY;

        // Runtime variable
        bool m_ShowServer;



        void Update()
        {
            if (!showGUI)
                return;

            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (UnityEngine.Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        manager.StartServer();
                    }
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        manager.StartHost();
                    }
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    manager.StartClient();
                }
            }
            if (NetworkServer.active)
            {
                if (manager.IsClientConnected())
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        manager.StopHost();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        manager.StopServer();
                    }
                }
            }
        }


        void OnGUI()
        {

            if (!showGUI)
                return;

            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 20;

            int xpos = 750 + offsetX;
            int ypos = 600 + offsetY;
            const int spacing = 128;

            bool noConnection = (manager.client == null || manager.client.connection == null ||
                manager.client.connection.connectionId == -1);


            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (noConnection)
                {
                    if (UnityEngine.Application.platform != RuntimePlatform.WebGLPlayer)
                    {

                        if (GUI.Button(new Rect(xpos + 100, ypos, 200, 100), "Single Play", myButtonStyle))

                        {

                            manager.StartHost();
                           // GameObject sing = GameObject.Find("Single_Play");
                           // sing.transform.GetChild(0).gameObject.SetActive(true);

                        }
                        ypos += spacing;

                        if (GUI.Button(new Rect(xpos + 100, ypos, 200, 100), "Multi Play", myButtonStyle))

                        {

                            GameObject sing = GameObject.Find("Single_Play");
                            sing.transform.GetChild(0).gameObject.SetActive(false);
                           // sing.gameObject.SetActive(false);
                            //    this.transform.GetChild(0).gameObject.SetActive(true);
                            GameObject a = GameObject.Find("Empty");

                            a.transform.GetChild(0).gameObject.SetActive(true);

                            this.gameObject.SetActive(false);



                        }
                        ypos += spacing;
                    }

                    //   if (GUI.Button(new Rect(xpos, ypos, 400, 200), "게임 참여", myButtonStyle))
                    //  {
                    //      manager.StartClient();
                    //  }

                    //  manager.networkAddress = GUI.TextField(new Rect(xpos + 100, ypos, 95, 20), manager.networkAddress);
                    ypos += spacing;

                    if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        // cant be a server in webgl build
                        GUI.Box(new Rect(xpos, ypos, 200, 25), "(  WebGL cannot be server  )");
                        ypos += spacing;
                    }

                }
                else
                {
                    GUI.Label(new Rect(xpos, ypos, 200, 20), "Connecting to " + manager.networkAddress + ":" + manager.networkPort + "..");
                    ypos += spacing;


                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Cancel Connection Attempt"))
                    {
                        manager.StopClient();
                    }
                }
            }






        }
        public void on_click()
        {
            manager.StartHost();
        }
        IEnumerator MULTI_GAME_START()
        {
            yield return new WaitForSeconds(3f);
            
            var pp = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < pp.Length; i++)
            {
                pp[i].transform.localPosition = new Vector3(200f, 200f, 200f);

            }

        }
    }
}
