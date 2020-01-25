using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public string charName, charSurname;
    public int level = 1;
    public int exp = 0;
    public float maxhp, hp;
    public float attack;
    protected bool destroy = false;

    public virtual void SaveOnDisk() {}

    public void LateUpdate()
    {
        if (hp <= 0 && !destroy)
        {
            StartCoroutine("Destroy");
            destroy = true;
        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}

/*
 * http://www.cyberforum.ru/csharp-beginners/thread212794.html
 * http://mycsharp.ru/post/21/2013_06_12_rabota_s_fajlami_v_si-sharp_klassy_streamreader_i_streamwriter.html
 * https://metanit.com/sharp/tutorial/6.1.php
 */

/* implementation:
 * public void SaveOnDisk(){
    string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);  //путь к Документам
    if (!Directory.Exists(md + "\\My Games\\mistoforos"))
    {
        Directory.CreateDirectory(md + "\\My Games\\mistoforos");
    }

    md += "\\My Games\\mistoforos";

    if (!Directory.Exists(md + charName + "_" + charSurname))
    {
        Directory.CreateDirectory(md + charName + "_" + charSurname);
    }

    md += charName + "_" + charSurname;

    FileStream savedFile = File.Create("data.mstfrschar");
    BinaryFormatter bf = new BinaryFormatter();
    bf.Serialize(savedFile, this);
    }
    */
