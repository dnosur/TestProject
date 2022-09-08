using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [Header("ProgressBarController")]
    [SerializeField] ProgressBarController progressBarController;

    [Header("Load Content")]
    [SerializeField] GameObject VerticalContent;
    [SerializeField] GameObject HorizontalContent;

    [Header("Prefab Variants")]
    [SerializeField] GameObject HorizontalPrefabVariant;
    [SerializeField] GameObject VerticalPrefabVariant;

    [Header("Windows")]
    [SerializeField] DeveloperInfoScreenController developerInfoScreenController;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(LoadScene());
    }

    private void Update()
    {
    }

    IEnumerator LoadScene()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/AssetBundles/cards");
        yield return myLoadedAssetBundle;

        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield return false;
        }

        CardsCollection cardsCollection = new CardsCollection();
        cardsCollection.Deserialize();

        progressBarController.SetAssetsCount(cardsCollection.GorizontalSprites.Count + cardsCollection.VerticalSprites.Count);
        progressBarController.AllowLoad();

        foreach (string str in myLoadedAssetBundle.assetBundle.GetAllAssetNames())
        {
            while (cardsCollection.GorizontalSprites.Contains(str) || cardsCollection.VerticalSprites.Contains(str))
            {
                GameObject RootContent;
                GameObject PrefabVariant;
                ContentController controller;

                string tag;

                if (cardsCollection.GorizontalSprites.Contains(str))
                {
                    cardsCollection.GorizontalSprites.RemoveAt(cardsCollection.GorizontalSprites.IndexOf(str));

                    RootContent = HorizontalContent;
                    PrefabVariant = HorizontalPrefabVariant;
                    controller = HorizontalContent.GetComponent<ContentController>();

                    tag = "Horizontal";
                }
                else if (cardsCollection.VerticalSprites.Contains(str))
                {
                    cardsCollection.VerticalSprites.RemoveAt(cardsCollection.VerticalSprites.IndexOf(str));

                    RootContent = VerticalContent;
                    PrefabVariant = VerticalPrefabVariant;
                    controller = VerticalContent.GetComponent<ContentController>();

                    tag = "Vertical";
                }
                else
                {
                    break;
                }

                progressBarController.AddText(str);

                Image image = Instantiate(PrefabVariant.GetComponent<Image>());
                image.GetComponent<CardController>().SetParticleSystemMaterial(str);
                image.GetComponent<CardController>().SetContentController(controller);

                controller.AddContent(image, tag);

                AssetBundleRequest request = myLoadedAssetBundle.assetBundle.LoadAssetAsync<Sprite>(str);
                yield return request;

                image.GetComponent<Image>().sprite = request.asset as Sprite;
                progressBarController.AddSprites(request.asset as Sprite);
            }
            progressBarController.IncrementProgress(1);
        }
    }

    void LoadFinish()
    {
        developerInfoScreenController.Show();
        GameObject.Find("Audio").GetComponent<AudioController>().PlayMain();
        gameObject.SetActive(false);
    }
}
