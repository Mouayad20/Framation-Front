using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Save : MonoBehaviour
{

    [SerializeField] TMP_InputField input;
    [SerializeField] Text textview;

    public void SaveData()
    {
        string data = input.text;
        PlayerPrefs.SetString("InputData" , data);
    }

    // Update is called once per frame
    void ShowData()
    {
        textview.text = PlayerPrefs.GetString("InputData");
        
    }
}
