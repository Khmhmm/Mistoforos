using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SequenceLTMemoryManager : MonoBehaviour {

    public GameObject sequence;
    public const string DATA_SEPARATOR = ";";
    public const string SEPARATOR = "[##]";

	public void Save(string path, string fileName)
    {
        //file will be saved in `.quest` format
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        try
        {
            using (FileStream fs = new FileStream(path + "\\" + fileName + ".quest", FileMode.Create, FileAccess.Write))
            {
                BinaryWriter bw = new BinaryWriter(fs);

                string data = "";
                Sequence script = sequence.GetComponent<Sequence>();

                for(int i = 0; i < script.stages.Count; i++)
                {
                    data += "ID:" + i + DATA_SEPARATOR;
                    data += script.stages[i].GetComponent<Stage>().GetType() + DATA_SEPARATOR;
                    data += script.stages[i].GetComponent<Stage>().GetContent() + DATA_SEPARATOR;
                    data += script.stages[i].GetComponent<Stage>().nextStageIndex + DATA_SEPARATOR;
                    data += SEPARATOR;
                }

                byte[] bArr = Encoding.UTF8.GetBytes(data);

                bw.Write(bArr);
                bw.Flush();
                bw.Close();
            }
        }
        catch (IOException e)
        {
            Debug.LogError(e);
        }
        finally
        {
            Debug.Log("SaveFile is done");
        }
    }

    public void Load()
    {

    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.4f,Screen.width * 0.2f, 200f),"Save"))
        {
            Save(Environment.CurrentDirectory + "\\Quests", "test");
        }
    }
}
