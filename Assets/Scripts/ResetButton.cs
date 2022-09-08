using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [Header("Content Controllers")]
    [SerializeField] ContentController HorizontalContentController;
    [SerializeField] ContentController VericaleContentController;

    [Header("Cards")]
    [SerializeField] List<Sprite> CardSprites;

    [Header("Prefabs")]
    [SerializeField] GameObject HorizontalPrefab;
    [SerializeField] GameObject VerticalPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        HorizontalContentController.RemoveAllCards();
        VericaleContentController.RemoveAllCards();

        CardsCollection cardsCollection = new CardsCollection();
        cardsCollection.BaseInitialize();
        cardsCollection.Serialize();

        Debug.Log("1");

        ResetWindow(cardsCollection.GorizontalSprites, ref HorizontalContentController, ref HorizontalPrefab, "Horizontal");
        ResetWindow(cardsCollection.VerticalSprites, ref VericaleContentController, ref VerticalPrefab, "Vertical");
    }

    void ResetWindow(List<string> cards, ref ContentController controller, ref GameObject prefab, string tag)
    {
        Debug.Log(tag + " reset started!");
        foreach (string str in cards)
        {
            Sprite sprite = null;
            foreach (Sprite spr in CardSprites)
            {
                if (str.Contains(spr.name))
                {
                    sprite = spr;
                    break;
                }
            }

            Image image = Instantiate(prefab.GetComponent<Image>());
            image.GetComponent<CardController>().SetParticleSystemMaterial(str);
            image.GetComponent<CardController>().SetContentController(controller);

            controller.AddContent(image, tag);

            image.GetComponent<Image>().sprite = sprite;;
        }

        Debug.Log(tag + " reset complated!");
    }
}
