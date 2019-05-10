using UnityEngine;
using System.Collections;
using ZXing;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Xml;

public class ScanQRCode : MonoBehaviour 
{
	private WebCamTexture webCamTexture;
	private string resultText;
	private Material quadMat;
    public GameObject resultReader;
    public static string transport;

	// Use this for initialization
	void Start () 
	{
        //___________________________________________________
        if (!File.Exists(Application.persistentDataPath + "/carDatabaseXml.xml"))
        {
            string firstCarData = "testabcfirst#初始化#first#first#first";
            string[] readCarElement = firstCarData.Split('#');
            string getID = "none",
                getBrand = "none",
                getType = "none",
                getCarColor = "none",
                getPrice = "none";

            //get the element into the getID, getBrand, etc
            getID = readCarElement[0];
            getBrand = readCarElement[1];
            getType = readCarElement[2];
            getCarColor = readCarElement[3];
            getPrice = readCarElement[4];

            XmlDocument addXml = new XmlDocument();
            addXml.Load(Application.streamingAssetsPath+"/carDatabaseXml.xml");
            XmlElement newCar = addXml.CreateElement("car");
            newCar.SetAttribute("carID", getID);
            XmlElement newCarBrand = addXml.CreateElement("brand");
            newCarBrand.InnerText = getBrand;
            XmlElement newCarType = addXml.CreateElement("type");
            newCarType.InnerText = getType;
            XmlElement newCarColor = addXml.CreateElement("carColor");
            newCarColor.InnerText = getCarColor;
            XmlElement newCarPrice = addXml.CreateElement("price");
            newCarPrice.InnerText = getPrice;

            newCar.AppendChild(newCarBrand);
            newCar.AppendChild(newCarType);
            newCar.AppendChild(newCarColor);
            newCar.AppendChild(newCarPrice);

            XmlElement originalCars = addXml.DocumentElement;
            originalCars.AppendChild(newCar);

            File.WriteAllText(Application.persistentDataPath + "/carDatabaseXml.xml", addXml.InnerXml);
        }
        //___________________________________________________
        resultReader = GameObject.Find("readQR");
		webCamTexture = new WebCamTexture (4096, 4096);
		WebCamDevice[] devices = WebCamTexture.devices;
		webCamTexture.deviceName = devices[0].name;
		quadMat = GetComponent<Renderer>().material;
		quadMat.mainTexture = webCamTexture;
		webCamTexture.Play ();

		InvokeRepeating ("Scan", 1f, 1f);
	}

	void Update()
	{
		transform.rotation = Quaternion.AngleAxis(webCamTexture.videoRotationAngle, -Vector3.forward);

		var screenAspect = (float)Screen.width / Screen.height;
		var webCamAspect = (float)webCamTexture.width / webCamTexture.height;

		var rot90 = (webCamTexture.videoRotationAngle / 90) % 2 != 0;
		if (rot90) 
		{
			webCamAspect = 1.0f / webCamAspect;
		}

		float sx, sy;
		if (webCamAspect < screenAspect) //0.5625 > 0.5622189
		{
			sx = webCamAspect;
			sy = 1.0f;
		}
		else
		{
			sx = screenAspect;
			sy = screenAspect / webCamAspect;
		}

		if (rot90) 
		{
			transform.localScale = new Vector3 (sy, sx, 1);
		} 
		else 
		{
			transform.localScale = new Vector3 (sx, sy, 1);
		}
			
		var mirror = webCamTexture.videoVerticallyMirrored;
		quadMat.mainTextureOffset = new Vector2(0, mirror ? 1 : 0);
		quadMat.mainTextureScale = new Vector2(1, mirror ? -1 : 1);
        resultReader.GetComponent<readQR>().innerText = resultText;
    }

	private void Scan()
	{
		if (webCamTexture != null && webCamTexture.width > 100) //scan successfully
		{
			resultText = Decode(webCamTexture.GetPixels32 (), webCamTexture.width, webCamTexture.height);
            resultReader.GetComponent<readQR>().transport = resultText;
			Debug.Log (resultText);
            if (resultText != null)
                SceneManager.LoadScene("xmlLoadScene");
        }
	}

	public string Decode(Color32[] colors, int width, int height)
	{
		BarcodeReader reader = new BarcodeReader ();
		var result = reader.Decode (colors, width, height);
		if (result != null) 
		{
			return result.Text;
		}
		return null;
	}

	void OnGUI()
	{
		var text = "web cam size = " + webCamTexture.width + " x " + webCamTexture.height;
		text += "\nrotation = " + webCamTexture.videoRotationAngle;
		text += "\nscreen size = " + Screen.width + " x " + Screen.height;
		text += "\nresultText = " + resultText;
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
	}
}
