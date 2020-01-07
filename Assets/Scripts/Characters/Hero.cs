using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public class Hero : Character {
    // from Character:
    //    public string charName, charSurname;
    //    public int level = 1;
    //    public int exp = 0;
    
    public bool mage=false;
    public List<Skill> skills = null;

    public Hero()
    {
        this.charName = "";
        this.charSurname = "";
        this.level = 1;
        this.exp = 0;
        this.mage = false;
        this.skills = null;
    }

    public string ToString()
    {
        return charName + " " + charSurname + ", level: " + level + " : " + exp + ", mage? " + mage;
    }

    public override void SaveOnDisk()
    {
        
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

        md += "\\" + charName + "_" + charSurname;

        if (!Directory.Exists(md))
        {
            Directory.CreateDirectory(md);
        }

        Debug.Log("Cur. path: " + md);

        //crypting alghoritm
        string crypto = charName + "##" + charSurname + "##" + level + "##" + exp + "##" + mage;
            if (skills!=null)
            {
                foreach (var skill in skills)
                {
                    crypto += "##";
                    crypto += "SKILL_ID:" + skill.SKILL_ID;
                }
            }

            byte[] bytes = Encoding.UTF8.GetBytes(crypto);
            for(int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += 1;
            }

        using (FileStream savedFile = File.Create(md + "\\data.mstfrschar"))
        {
            BinaryWriter bw = new BinaryWriter(savedFile);
            bw.Write(bytes);
            bw.Close();
        }
    }

    //returns gameobject with component Hero
    public static GameObject Load(FileStream fs)
    {
        GameObject g = new GameObject("tmp");
        Hero ret = g.AddComponent<Hero>(); 
        BinaryReader br = new BinaryReader(fs);


        byte[] bytes = br.ReadBytes((int)fs.Length);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= 1;
            }

        //string crypto = charName + "##" + charSurname + "##" + level + "##" + exp + "##" + mage;
        
        string decrypto = Encoding.UTF8.GetString(bytes);
        string[] datas = decrypto.Split("##".ToCharArray());

        ret = g.GetComponent<Hero>();

        ret.charName = datas[0];
        ret.charSurname = datas[2];
        ret.level = int.Parse(datas[4]);
        ret.exp = int.Parse(datas[6]);
        ret.mage = bool.Parse(datas[8]);

        if (datas.Length > 9)
        {
           //TODO: add skills
           //Remember короче баг дэбильный, что, например, datas[0] = "Имя", datas[1] = "" (ПОЧЕМУ НЕ ЗНАЮ), datas[2] = "Фамилия", datas[3] = "" и т.д.
        }

        Debug.Log(ret.ToString());

        return g;
    }
}