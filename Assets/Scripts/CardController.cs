using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] List<Material> CardMaterials;

    private ContentController controller;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            GameObject.Find("Audio").transform.GetChild(1).GetComponent<AudioSource>().Play();
            particleSystem.Play();
            GetComponent<Animator>().Play("HideCard");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteSelf()
    {
        controller.DeleteContent(gameObject);
    }

    public void SetParticleSystemMaterial(string CardName)
    {
        if (CardName.Contains("1"))
        {
            particleSystem.GetComponent<ParticleSystemRenderer>().material = CardMaterials[0];
            CardMaterials = null;
        }
        if (CardName.Contains("2"))
        {
            particleSystem.GetComponent<ParticleSystemRenderer>().material = CardMaterials[1];
            CardMaterials = null;
        }
        if (CardName.Contains("3"))
        {
            particleSystem.GetComponent<ParticleSystemRenderer>().material = CardMaterials[2];
            CardMaterials = null;
        }
        if (CardName.Contains("4"))
        {
            particleSystem.GetComponent<ParticleSystemRenderer>().material = CardMaterials[3];
            CardMaterials = null;
        }
        if (CardName.Contains("5"))
        {
            particleSystem.GetComponent<ParticleSystemRenderer>().material = CardMaterials[4];
            CardMaterials = null;
        }
    }

    public void SetContentController(ContentController controller)
    {
        this.controller = controller;
    }
}
