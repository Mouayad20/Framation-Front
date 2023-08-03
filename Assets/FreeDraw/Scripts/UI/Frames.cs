
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frames : View
{
  
    [SerializeField] private Button _GoToHomeButton;
    [SerializeField] private Button _SaveVideoButton;

    public override void Initialize()
    {
        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
            //  ViewManager.Show<Board5>();
        });

        _SaveVideoButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("Options");
            //  ViewManager.Show<Board5>();
        });
    }
}



