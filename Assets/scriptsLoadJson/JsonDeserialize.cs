using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

public class JsonDeserialize : MonoBehaviour
{
    public Text mytext;
    string inputString;

    // Start is called before the first frame update
    void Start()
    {
        /*
        TextAsset jsonData = Resources.Load<TextAsset>("JsonData"); //读取正确 
        print(jsonData);

        JsonArrayModel jsonObject = JsonMapper.ToObject<JsonArrayModel>(jsonData.text);

        mytext.text += jsonObject.name + "\n";
        mytext.text += jsonObject.color + "\n";

        Debug.Log(jsonObject.name);
        Debug.Log(jsonObject.color);
        */
    }

    void Update() {

    }
    public void loadJson()
    {
        TextAsset jsonData1 = Resources.Load<TextAsset>("JsonData1"); //读取正确 
        print(jsonData1);

        JsonObjectModel jsonObject1 = JsonMapper.ToObject<JsonObjectModel>(jsonData1.text);
        print(jsonObject1);
        mytext.text = "";
        foreach (var info in jsonObject1.animals)
        {
            Debug.Log(info.name);
            Debug.Log(info.color);
            mytext.text += info.name + "\n";
            mytext.text += info.color + "\n";
        }
    }
}