using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] SettingWindowScreenController settingWindowScreenController;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            settingWindowScreenController.Show();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
