using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddContent(Image image, string tag)
    {
        image.transform.SetParent(this.transform);
        image.transform.localScale = new Vector3(1, 1, 1);
        image.tag = tag;
    }

    public void DeleteContent(GameObject @object)
    {
        if(@object.tag == "Vertical") 
        {
            CardsCollection cardsCollection = new CardsCollection();
            cardsCollection.Deserialize();

            foreach(string sprite in cardsCollection.VerticalSprites)
            {
                if (sprite.Contains(@object.GetComponent<Image>().sprite.name))
                {
                    cardsCollection.VerticalSprites.Remove(sprite);
                    break;
                }
            }

            cardsCollection.Serialize();
        }
        else if (@object.tag == "Horizontal")
        {
            CardsCollection cardsCollection = new CardsCollection();
            cardsCollection.Deserialize();

            foreach (string sprite in cardsCollection.GorizontalSprites)
            {
                if (sprite.Contains(@object.GetComponent<Image>().sprite.name))
                {
                    cardsCollection.GorizontalSprites.Remove(sprite);
                    break;
                }
            }

            cardsCollection.Serialize();
        }

        Destroy(@object);
    }

    public void RemoveAllCards()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<CardController>().DeleteSelf();
        }
    }
}
