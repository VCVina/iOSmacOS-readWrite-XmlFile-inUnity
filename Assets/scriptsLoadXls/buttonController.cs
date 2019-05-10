using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    public void Click()
    {
        GameObject.Find("carrier").GetComponent<DataDAO>().printExcel();
    }
}
