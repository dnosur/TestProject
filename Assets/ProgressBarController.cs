using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [Header("Progress Bar")]
    [SerializeField] Slider slider;

    [Header("Load Info Content")]
    [SerializeField] private Text loadText;
    [SerializeField] private Image loadImg;

    [Header("Animator")]
    [SerializeField] Animator animator;

    private List<string> text = new List<string>();
    private List<Sprite> sprites = new List<Sprite>();

    private int LoadObjId = 1;
    private int AssetsCount = 0;

    private float FillSpeed = .25f;
    private float targetProgress = 0;

    private bool isLoad;

    // Update is called once per frame
    void Update()
    {
        if (isLoad)
        {
            if (slider.value == slider.maxValue)
            {
                text = null;
                sprites = null;
                isLoad = false;

                LoadObjId = 0;
                AssetsCount = 0;

                LoadEnd();
            }
            else if (slider.value < targetProgress)
            {
                slider.value += FillSpeed * Time.deltaTime;

                if (slider.value < slider.maxValue / (AssetsCount - LoadObjId))
                {
                    loadText.text = text[LoadObjId - 1];
                    loadImg.sprite = sprites[LoadObjId - 1];
                }
                else LoadObjId++;
            }
        }
    }

    private void LoadEnd()
    {
        animator.Play("New Animation");
    }

    public void IncrementProgress(int newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    public void AllowLoad() { isLoad = true; }

    public void AddText(string text) { this.text.Add(text); }
    public void AddSprites(Sprite sprite) { sprites.Add(sprite); }

    public void SetAssetsCount(int assetsCount) { AssetsCount = assetsCount; }
}
