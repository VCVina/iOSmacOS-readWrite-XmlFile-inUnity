
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System.Collections.Generic;
using System.Linq;
using System.IO;

public class client1 : MonoBehaviour
{
    public Text inputText;
    public Text outputText;
    public Button button;

    private string message;
    private static byte[] result = new byte[1024];

    private static int length;
    private static int receiveLength = 1;
    public int portNum = 8885;
    public static string serverIP = "172.28.198.217";

    // private static byte[] result1 = new byte[1024];
    //设定服务器IP地址
    IPAddress ip = IPAddress.Parse(serverIP);
    static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, portNum)); //配置服务器IP与端口
            Debug.Log("连接服务器成功");
        }
        catch (Exception)
        {
            Debug.Log("连接服务器失败，请按回车键退出！");
            return;
        }
        //通过clientSocket接收数据
        int receiveLength = clientSocket.Receive(result);
        Debug.Log("接收服务器消息:" + Encoding.UTF8.GetString(result, 0, receiveLength));

    }


    public void SendMessageToServer()
    {

        message = inputText.text;
    
        inputText.text = "";
        if (message != null && message != "")
        {

            clientSocket.Send(Encoding.UTF8.GetBytes(message));
            message = "";

            receiveLength = clientSocket.Receive(result);
            Debug.Log("接收服务器消息:" + Encoding.UTF8.GetString(result, 0, receiveLength));

        }

        else
        {
            Debug.Log("Null you input");
        }

    }

}







