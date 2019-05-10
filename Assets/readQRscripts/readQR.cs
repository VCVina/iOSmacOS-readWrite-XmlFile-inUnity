using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class readQR : MonoBehaviour
{
    public string transport = "default";
    public string innerText = "default";
    void Awake()
    {
        DontDestroyOnLoad(this);
        //DontDestroyOnLoad(cu);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
