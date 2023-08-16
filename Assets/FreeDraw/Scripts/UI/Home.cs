using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Home: View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _secondButton;
    [SerializeField] private Button _thirdButton;
    [SerializeField] private GameObject _color_panel;
    [SerializeField] private GameObject _video_panel;
    [SerializeField] private GameObject _video_panel_2;
    [SerializeField] private GameObject _DrawSkeleton;
    [SerializeField] private GameObject _DrawSkeletonTwo;
    [SerializeField] private GameObject _Finish;
    [SerializeField] private GameObject _drawAnotherViewButton;
    [SerializeField] private GameObject _showButton;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _ControlMaxDot ;
    [SerializeField] private GameObject _DeleteDot ;
    [SerializeField] private GameObject _ClearBoard ;
    [SerializeField] private GameObject _ChoseImage ;


    public override void Initialize()
    { 
        _startButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("Draw");
            ViewManager.Show<Drawing>();
            _color_panel.SetActive(true);
            _ClearBoard.SetActive(true);
            _DrawSkeleton.SetActive(true);
            _ChoseImage.SetActive(true);
            _DeleteDot.SetActive(false);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _DrawSkeletonTwo.SetActive(false);
            _Finish.SetActive(false);
            _drawAnotherViewButton.SetActive(false);
            _showButton.SetActive(false);
            _text.SetActive(false);
            _ControlMaxDot.SetActive(false);
            Drawing.vanishMode = true;
            Directory.Delete("images", true);
            Directory.CreateDirectory("images");
            PenTool.frameId = 0 ;
        });

        _secondButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("Options");
            ViewManager.Show<Options>();
        });
        
        _thirdButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("About");
            ViewManager.Show<About>();
        }); 
    }
}