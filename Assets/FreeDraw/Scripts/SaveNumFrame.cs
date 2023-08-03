using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveNumFrame : MonoBehaviour
{

    [SerializeField] TMP_InputField input;
    [SerializeField] Text testview;
    
    public void SaveData()
    {
        string data = input.text;
        PlayerPrefs.SetString("inputData" , data);
    }

    public void ShowData ()
    {
        testview.text = PlayerPrefs.GetString("inputData"); 
    }
}