using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

//MainCamera

public class loadXml : MonoBehaviour
{
    public car carTest;
    protected string xmlPath;
    protected string tempXmlPath;
    public string carIDbyQR;
    // Start is called before the first frame update
    void Start()
    {
        carIDbyQR = "testabcfirst";
        xmlPath = Application.streamingAssetsPath + "/carDatabaseXml.xml";
        tempXmlPath = Application.persistentDataPath + "/carDatabaseXml.xml";
        ReadXml();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReadXml()
    {
        Debug.Log(tempXmlPath);
        //___________________________________________________
        if (!File.Exists(tempXmlPath))
            saveNewXml("first#奔驰#512B#红色#153", true);
        //___________________________________________________

        XmlDocument xm1 = new XmlDocument();
        xm1.Load(tempXmlPath);
        string dataFromInternet = carIDbyQR + "#奥拓#512B#红色#153";//来自互联网的返回车型的数据
        Debug.Log(dataFromInternet);//////
        XmlNode searchPoint = xm1.SelectSingleNode("/cars/car[@carID='" + carIDbyQR + "']");
        carTest.checkIndex = carTest.checkIndex + searchPoint;
        if (searchPoint == null)
        {
            saveNewXml(dataFromInternet,false);
            displayXml();
        }
        else
        {
            Debug.Log("未执行存储");
            displayXml();
        }




        void saveNewXml(string carData,bool choose)
        {
            Debug.Log("执行存储函数");
            string[] readCarElement = carData.Split('#');
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
            //___________________________________________________
            if (choose)
                addXml.Load(xmlPath);
            else
                addXml.Load(tempXmlPath);
            //___________________________________________________
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
            //___________________________________________________

            //FileStream fileStream = new FileStream(xmlPath, FileMode.Create);
            //StreamWriter streamWriter = new StreamWriter(fileStream);
            //streamWriter.Write(addXml.InnerXml);
            //streamWriter.Flush();
            //streamWriter.Close();

            //FileStream fileStream2 = new FileStream(tempXmlPath, FileMode.Create);
            //StreamWriter streamWriter2 = new StreamWriter(fileStream2);
            //streamWriter2.Write(addXml.InnerXml);
            //streamWriter2.Flush();
            //streamWriter2.Close();

            //___________________________________________________

            File.WriteAllText(tempXmlPath,addXml.InnerXml);
            //___________________________________________________
            Debug.Log("保存完毕");
        }


        void displayXml()
        {
            XmlDocument displayXmlOrigin = new XmlDocument();
            XmlDocument displayXmlTemp = new XmlDocument();
            //修改
            //displayXml.Load(tempXmlPath);
            displayXmlOrigin.Load(xmlPath);
            displayXmlTemp.Load(tempXmlPath);

            Debug.Log("新文件读取完毕");

            carTest.checkIndex = displayXmlTemp.InnerText;

            XmlNodeList searchPoints = displayXmlTemp.SelectNodes("/cars/car[@carID='" + carIDbyQR + "']");
            foreach (XmlNode nodecheck in searchPoints)
            {
                //nodecheck指向所有xml子树
                foreach (XmlNode node in nodecheck.ChildNodes)
                {
                    XmlElement elementReader = (XmlElement)node;
                    switch (elementReader.Name)
                    {
                        case "brand":
                            carTest.brand = elementReader.InnerText;
                            Debug.Log(elementReader.InnerText);
                            break;
                        case "type":
                            carTest.type = elementReader.InnerText;
                            Debug.Log(elementReader.InnerText);
                            //了解一下类型转换int.Parse(elementReader.InnerText);
                            break;
                        case "carColor":
                            carTest.carColor = elementReader.InnerText;
                            break;
                        case "price":
                            carTest.price = elementReader.InnerText + "万CNY";
                            break;
                    }
                }
            }
        }
    }
}
