using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data; 
public class DataDAO : MonoBehaviour 
{

    private ItemManager manager;
    public Text excelText;
    private void Start()
    {        
        manager = Resources.Load<ItemManager>("Item");
        excelText = GameObject.Find("Canvas/loadXlsText").GetComponent<Text>();
        excelText.text = "";
    }

    public void printExcel()
    {
        foreach (Item i in manager.dataArray)
        {
            Debug.Log(i.itemId + "---" + i.itemName + "---" + i.itemPrice);
            excelText.text += (i.itemId + "---" + i.itemName + "---" + i.itemPrice + "\n").ToString();
        }


        for (int i = 0; i < manager.dataArray.Length; i++)
        {
            if (i == 1)
            {
                Debug.Log(manager.dataArray[i].itemId + "***" + manager.dataArray[i].itemName + "***" + manager.dataArray[i].itemPrice);
            }
        }
    }
    
}