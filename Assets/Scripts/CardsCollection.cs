using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[XmlRoot("CardsCollection")]
public class CardsCollection
{
    [XmlArray("GorizontalSprites")]
    [XmlArrayItem("GSprites")]
    public List<string> GorizontalSprites { get; set; }

    [XmlArray("VerticalSprites")]
    [XmlArrayItem("VSprites")]
    public List<string> VerticalSprites { get; set; }

    public CardsCollection() { }

    public void BaseInitialize()
    {
        GorizontalSprites = new List<string>() { "assets/cards/1.png", "assets/cards/2.png", "assets/cards/3.png" };
        VerticalSprites = new List<string>() { "assets/cards/1.png", "assets/cards/1.png", "assets/cards/1.png", "assets/cards/1.png", "assets/cards/1.png", "assets/cards/1.png",
                                               "assets/cards/2.png", "assets/cards/2.png", "assets/cards/2.png", "assets/cards/2.png", "assets/cards/2.png", "assets/cards/2.png",
                                               "assets/cards/3.png", "assets/cards/3.png", "assets/cards/3.png", "assets/cards/3.png", "assets/cards/3.png", "assets/cards/3.png",
                                               "assets/cards/4.png", "assets/cards/4.png", "assets/cards/4.png", "assets/cards/4.png", "assets/cards/4.png", "assets/cards/4.png",
                                               "assets/cards/5.png", "assets/cards/5.png", "assets/cards/5.png", "assets/cards/5.png", "assets/cards/5.png", "assets/cards/5.png",};
    }

    public void Deserialize()
    {
        bool isFileFound = false;

        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + "/Saves/");
        foreach (var file in directoryInfo.GetFiles())
        {
            if (file.Name == "save.xml")
            {
                isFileFound = true;
                break;
            }
        }

        if (isFileFound)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CardsCollection));
            CardsCollection cardsCollection;

            using (FileStream fs = new FileStream(Application.dataPath + "/Saves/save.xml", FileMode.Open))
            {
                cardsCollection = (xmlSerializer.Deserialize(fs) as CardsCollection);

                Debug.Log("Deserialize done!");
            }

            GorizontalSprites = cardsCollection.GorizontalSprites;
            VerticalSprites = cardsCollection.VerticalSprites;
        }
        else
        {
            BaseInitialize();
            Serialize();
        }
    }

    public void Serialize()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CardsCollection));

        using (FileStream fs = new FileStream(Application.dataPath + "/Saves/save.xml", FileMode.Create))
        {
            xmlSerializer.Serialize(fs, this);
        }

        Debug.Log("Serialize Done!");
    }
}
