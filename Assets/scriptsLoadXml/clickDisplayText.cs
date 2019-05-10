using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Canvas/loadXmlText
public class clickDisplayText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject fromXml;
    public string textFromXml;
    public string checkText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickDisplay()
    {

        obj = GameObject.Find("Canvas/loadXmlText");
        fromXml = GameObject.Find("carrier");
        textFromXml =
            "品牌：" + fromXml.GetComponent<car>().brand + "\n" +
            "车型：" + fromXml.GetComponent<car>().type + "\n" +
            "颜色：" + fromXml.GetComponent<car>().carColor + "\n" + 
            "价格：" + fromXml.GetComponent<car>().price + "\n" 
            ;

        obj.GetComponent<Text>().text = textFromXml;

    }
    public void checkDisplay()
    {

        obj = GameObject.Find("Canvas/checkLoading");
        fromXml = GameObject.Find("carrier");
        checkText = fromXml.GetComponent<car>().checkIndex + "\n";

        obj.GetComponent<Text>().text = checkText;

    }
}
