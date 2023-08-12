
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frames : View
{
  
    [SerializeField] private Button     _GoToHomeButton;
    [SerializeField] private Button     _SaveVideoButton;
    [SerializeField] private GameObject _BackToFramesUIButton;
    [SerializeField] private GameObject _ColorPen;
    [SerializeField] private GameObject _DrawAnotherViewButton;
    [SerializeField] private GameObject _ShowButton;
    [SerializeField] private GameObject _ClearBoard ;


    public override void Initialize()
    {
        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
             ViewManager.Show<Drawing>();
             _DrawAnotherViewButton.SetActive(true);
             _ShowButton.SetActive(true);
             _BackToFramesUIButton.SetActive(false);
             _ColorPen.SetActive(false);
             _ClearBoard.SetActive(false);
        });

        _SaveVideoButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("Options");
        });
    }
}



