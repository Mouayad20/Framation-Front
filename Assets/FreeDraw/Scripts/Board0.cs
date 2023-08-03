using UnityEngine.UI;
using UnityEngine;
public class Board0: View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _secondButton;
    [SerializeField] private Button _thirdButton;
    // [SerializeField] private GameObject _DrawskeletonTwoButton;
    // [SerializeField] private GameObject _ButtonFinish;

    // [SerializeField] private Button _GoToHomeButton;
    // [SerializeField] private Button _DrawSkeletonButton;
    // [SerializeField] private Button _FinishButton;
    // [SerializeField] private Button _DrawskeletonTwoButton ;
    [SerializeField] private GameObject _color_panel;
    [SerializeField] private GameObject _video_panel;
    [SerializeField] private GameObject _video_panel_2;
    [SerializeField] private GameObject _Drawskeleton;
    [SerializeField] private GameObject _DrawskeletonTwo;
    [SerializeField] private GameObject _Finish;

    public override void Initialize()
    { 
        _startButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("Draw");
            ViewManager.Show<Board1>();
            //  _ButtonFinish.SetActive(false);
             _color_panel.SetActive(true);
             _video_panel.SetActive(false);
             _video_panel_2.SetActive(false);
             _Drawskeleton.SetActive(true);
             _DrawskeletonTwo.SetActive(false);
             _Finish.SetActive(false);



            });
        _secondButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("Options");
            ViewManager.Show<Board3>();
            });
        _thirdButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("About");
            ViewManager.Show<Board4>();
            });
    }
}


    


