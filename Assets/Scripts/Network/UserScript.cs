using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class UserScript : NetworkBehaviour {
    public GameObject currentStage;
    public GUISkin skin;
    public string userName = "default"; 
    [SyncVar(hook = "FindLobby")]public GameObject lobby;
    public bool clicked = false;
    bool advice = true;

    public GameObject charObject;
    public Hero myHero = null;
    public int id = -2;

    bool foundChars = false;
    string heroPath = "";


            void Start()
            {
                var tmp = GameObject.Find("Handler");
                if (isServer)
                {
                    FindLobby(tmp);
                    Debug.Log("I found lobby");
                }

                if (isLocalPlayer)
                {
                    this.name = "Me";
                }
                Invoke("CancelAdvice", 5f);
            }

            void LateUpdate()
            {
                if (!isLocalPlayer)
                    return;

                if (Input.GetButton("Submit") && myHero != null)
                {
                    CmdIncrement();
                }
            }

    [Command]
    public void CmdUpdateMyHero(GameObject playerUpdate)
    {
        RpcUpdateMyHero(playerUpdate,id);
    }

    [ClientRpc]
    public void RpcUpdateMyHero(GameObject playerUpdate,int updID)
    {
        //lobby.GetComponent<LobbyHandler>().players.RemoveAt(updID-1); 
        //lobby.GetComponent<LobbyHandler>().players.Add(playerUpdate);
    }

    [Command]
    public void CmdIncrement()
    {
        RpcIncrement();
    }

    [ClientRpc]
    void RpcIncrement()
    {
        if (!clicked)
        {
            lobby.GetComponent<LobbyHandler>().count += 1;
            clicked = true;
        }
    }

            void OnGUI()
            {
                GUI.skin = skin;
                if (!isLocalPlayer)
                    return;

                if(advice) GUI.Label(new Rect(Screen.width*0.4f, Screen.height*0.05f,Screen.width*0.2f,40f), "Press `submit` key (ENTER by default) to continue...");

                if (GUI.Button(new Rect(Screen.width - MainmenuGUI.xOffset, Screen.height * 0.8f + MainmenuGUI.yOffset, 200f, 40f), "Quit"))
                {
                    Application.Quit();
                }

                if (!foundChars)
                {
                    GUI.Label(new Rect(Screen.width *0.4f , Screen.height * 0.9f, 200f, 40f), "Heroes don't found");
                }

                //hero loading
                if (myHero == null)
                {
                    //string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    //md += "\\My Games\\mistoforos";

                    string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);  //путь к Документам
                                                                                                //there is problems when it is located not on a disk C
                    if (md.Length > 5)
                    {
                        md += "\\My Games\\mistoforos\\Characters";
                    }
                    else
                    {
                        md = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName).ToString();
                        md += "\\Characters";
                    }
            
                    if (Directory.Exists(md))
                        {
                            foundChars = true;
                            DirectoryInfo[] dirs = new DirectoryInfo(md).GetDirectories();
                            for (int i=0; i < dirs.Length;i++)
                            {
                                //Debug.Log(dirs[i].FullName);

                                if(GUI.Button(new Rect(Screen.width *0.4f,Screen.height*0.2f + i * 40f, 200f, 40f),dirs[i].Name))
                                {
                        
                                heroPath = dirs[i].FullName;
                                heroPath += "\\data.mstfrschar";

                                Debug.Log(heroPath);


                                charObject = Hero.Load(File.OpenRead(heroPath));
                                //charObject.AddComponent<NetworkIdentity>();
                                //charObject.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
                                charObject.transform.parent = this.transform;
                                charObject.name = this.name + " charObject";

                                myHero = charObject.GetComponent<Hero>();

                                CmdUpdateMyHero(this.gameObject);
                                }
                            }
                        }
                        else
                        {
                            foundChars = false;
                            // it is not exists :'<<
                        }
                }
            }

    void CancelAdvice()
    {
        advice = false;
    }

    void FindLobby(GameObject tmp)
    {
        lobby = tmp;
    }

}
