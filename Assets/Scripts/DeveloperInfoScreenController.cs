using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperInfoScreenController : MonoBehaviour, IWindow
{
    private Animator animator;

    [Header("Buttons")]
    [SerializeField] Button personalLinkButton;
    [SerializeField] Button CloseButton;

    IWindow prevWindow;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        personalLinkButton.onClick.AddListener(() => { Application.OpenURL("https://t.me/danosur"); });
        CloseButton.onClick.AddListener(Hide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test() { }

    public void Show(IWindow prevWindow = null)
    {
        if (prevWindow != null) this.prevWindow = prevWindow;
        animator.Play("ShowDeveloperInfoScreen");
    }

    public void Hide() 
    {
        animator.Play("HideDeveloperInfoScreen");
    }
    public void OnAnimFinish()
    {
        if (prevWindow != null) 
        { 
            prevWindow.Show();
            prevWindow = null;
        }
    }
}
