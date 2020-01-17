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
    [SyncVar(hook = "FindLobby")] public GameObject lobby;
    public bool clicked = false;
    bool advice = true;

    public GameObject charObject;
    public Hero myHero = null;
    public int id = -2;

    bool foundChars = false;
    string heroPath = "";

    private bool delay = false;


    void Start()
    {

        //lobby finding
        var tmp = GameObject.Find("Handler");
        if (isServer)
        {
            FindLobby(tmp);
           
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

            if (Input.GetButton("Submit") && myHero != null && !delay)
            {
                CmdIncrement();
                delay = true;
                StartCoroutine("Delay");
            }

        //Sync. players' heroes if lobby says that they're not loaded
        //bool value should optimize this code
        if (!lobby.GetComponent<LobbyHandler>().heroesLoaded)
        {
            string md = "\\Characters";
            DirectoryInfo[] dirs = new DirectoryInfo(Environment.CurrentDirectory + md).GetDirectories();
            foreach (var dir in dirs)
            {
                byte[] arr;
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + md + "\\" + dir.Name + "\\data.mstfrschar", FileMode.Open))
                {
                    BinaryReader br = new BinaryReader(fs);
                    arr = br.ReadBytes((int)fs.Length);
                    br.Close();
                }

                CmdSyncHero(md + "\\" + dir.Name, arr);
            }
        }
    }

    [Command]
    public void CmdSyncHero(string path, byte[] data)
    {
        RpcSyncHero(path, data);
    }

    [ClientRpc]
    public void RpcSyncHero(string path, byte[] data)
    {
        //Debug.Log(data == null);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        try
        {
            if(!Directory.Exists(Environment.CurrentDirectory + path))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + path);
            }

            using (FileStream fs = new FileStream(Environment.CurrentDirectory + path + "\\data.mstfrschar", FileMode.CreateNew, FileAccess.Write))
            {
                BinaryWriter br = new BinaryWriter(fs);
                br.Write(data);
                br.Flush();
                br.Close();
                fs.Close();
            }
        }
        catch(IOException e)
        {
            //it is normal situation, when IOException calls with `Could not create file 'filename' file is already exists`
            //because FileMode is CreateNew calls it.
            //we don't want to override existing files
            string ignore = "System.IO.IOException: Could not create file";
            if (e.ToString().Substring(0, ignore.Length).Equals(ignore))
            {
                //do nothing
            }
            else
            {
                Debug.LogError(e);
            }
        }
    }

    [Command]
    public void CmdUpdateMyHero(string path, int userID)
    {
        RpcUpdateMyHero(path, userID);
    }

    [ClientRpc]
    public void RpcUpdateMyHero(string path,int updID)
    {
        Debug.Log("Trying at: " + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + path);
        GameObject newHero = Hero.Load(new FileStream(Environment.CurrentDirectory + path, FileMode.Open));
        lobby.GetComponent<LobbyHandler>().Attach(newHero, updID - 1);
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

                    string md = "\\Characters";
                    
                    if (Directory.Exists(md))
                        {
                            foundChars = true;
                            DirectoryInfo[] dirs = new DirectoryInfo(Environment.CurrentDirectory + md).GetDirectories();
                            for (int i=0; i < dirs.Length;i++)
                            {
                                //Debug.Log(dirs[i].FullName);

                                if(GUI.Button(new Rect(Screen.width *0.4f,Screen.height*0.2f + i * 40f, 200f, 40f),dirs[i].Name))
                                {
                        
                                heroPath += dirs[i].Name;
                                heroPath += "\\data.mstfrschar";

                                CmdUpdateMyHero(md + "\\" + heroPath, id);
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

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        delay = false;
    }

}
