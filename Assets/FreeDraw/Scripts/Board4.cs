
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board4 : View
{
    // [SerializeField] private Button _backButton;
    // public override void Initialize()
    // {
    //     _backButton.onClick.AddListener(()=> ViewManager.ShowLast());
    // }

    [SerializeField] private Button _GoToHomeButton;
    public override void Initialize()
    {
        // _backButton.onClick.AddListener(()=> ViewManager.ShowLast());
        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
             ViewManager.ShowLast();
        });
    }
}



