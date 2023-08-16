using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] private static TMP_Text numberText;
    private int selectedValue;

    private void Start()
    {
        // Load the previously saved value
        selectedValue = PlayerPrefs.GetInt("SelectedValue", 0);
        SetNumberText(selectedValue);
    }

    public void Dropdown(int index)
    {
        selectedValue = index;
        SetNumberText(selectedValue);
        PlayerPrefs.SetInt("SelectedValue", selectedValue);
    }
    public void SetNumberText(int value)
    {
        // print("value : " + value);
        numberText.text = GetNumberFromIndex(value).ToString();
        print("Selected value: " + numberText.text);
    }

    private int GetNumberFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                return 240;
            case 1:
                return 270;
            case 2:
                return 720;
            default:
                return 0;
        }
    }
}