using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class clickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject btnObj = GameObject.Find("Canvas/loadXml");
        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate {
            tryClick();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void tryClick()
    {
        string brand = GameObject.Find("carrier").GetComponent<car>().brand;
    }
}
