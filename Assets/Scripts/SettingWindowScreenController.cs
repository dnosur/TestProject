using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindowScreenController : MonoBehaviour, IWindow
{
    [Header("Buttons")]
    [SerializeField] Button AudioEffectsButton;
    [SerializeField] Button AudioButton;
    [SerializeField] Button DISButton;
    [SerializeField] Button CloseButton;

    [Header("Controllers")]
    [SerializeField] AudioController audioController;
    [SerializeField] DeveloperInfoScreenController developerInfoScreenController;

    Animator animator;

    private IWindow prevWindow;

    private bool AudioEffectParameter = true;
    private bool AudioParameter = true;
    private bool isInitialize = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseButton.onClick.AddListener(() => Hide());
        AudioEffectsButton.onClick.AddListener(() =>
        {
            if (AudioEffectParameter)
            {
                audioController.OffAudioEffect();
                AudioEffectParameter = false;

                AudioEffectsButton.transform.GetChild(0).GetComponent<Text>().text = "On Audio Effects";
            }
            else
            {
                audioController.OnAudioEffect();
                AudioEffectParameter = true;

                AudioEffectsButton.transform.GetChild(0).GetComponent<Text>().text = "Off Audio Effects";
            }
        });
        AudioButton.onClick.AddListener(() =>
        {
            if (AudioParameter)
            {
                audioController.SetMainAudioVolume(0);
                AudioParameter = false;

                AudioButton.transform.GetChild(0).GetComponent<Text>().text = "On Main Audio";
            }
            else
            {
                audioController.SetMainAudioVolume(1);
                AudioParameter = true;

                AudioButton.transform.GetChild(0).GetComponent<Text>().text = "Off Main Audio";
            }
        });
        DISButton.onClick.AddListener(() =>
        {
            prevWindow = developerInfoScreenController;
            Hide();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(IWindow prevWindow = null)
    {
        if (!isInitialize)
        {
            if (!audioController.GetMainAudioParameter())
            {
                AudioButton.transform.GetChild(0).GetComponent<Text>().text = "On Main Audio";
                AudioParameter = false;
            }
            if (!audioController.GetAudioEffectsParameter())
            {
                AudioEffectsButton.transform.GetChild(0).GetComponent<Text>().text = "On Audio Effects";
                AudioEffectParameter = false;
            }

            isInitialize = true;
        }

        if (prevWindow != null) this.prevWindow = prevWindow;
        animator.Play("SettingWindowScreen");
    }

    public void Hide()
    {
        animator.Play("HideSettingWindowScreen");
    }

    public void OnAnimFinish()
    {
        if(prevWindow != null)
        {
            prevWindow.Show(this);
            prevWindow = null;
        }
    }
}
