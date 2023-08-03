using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Board1 : View
{
    [SerializeField] private Button _GoToHomeButton;
    [SerializeField] private Button _DrawSkeletonButton;
    [SerializeField] private Button _FinishButton;
    [SerializeField] private Button _DrawskeletonTwoButton ;
    [SerializeField] private GameObject _color_panel;
    [SerializeField] private GameObject _video_panel;
    [SerializeField] private GameObject _video_panel_2;

    [SerializeField] private GameObject _Drawskeleton;

    [SerializeField] private GameObject _DrawskeletonTwo;

    [SerializeField] private GameObject _Finish;
    public override void Initialize()
    {
        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
            //  ViewManager.ShowLast();
             ViewManager.Show<Board0>();
        });
        _DrawSkeletonButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("DrawAndFinish");
             _color_panel.SetActive(false);
             _video_panel.SetActive(true);
             _video_panel_2.SetActive(false);
             _DrawskeletonTwo.SetActive(true);
             _Drawskeleton.SetActive(false);
             
        });

        _DrawskeletonTwoButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("DrawAndFinish");
             _color_panel.SetActive(false);
             _video_panel.SetActive(false);
             _video_panel_2.SetActive(true);
             _Finish.SetActive(true);
             _DrawskeletonTwo.SetActive(false);

        });
        
        _FinishButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            ViewManager.Show<Board5>();
        });


    }
}



