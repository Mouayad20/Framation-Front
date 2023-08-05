using UnityEngine.UI;
using UnityEngine;
public class Home: View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _secondButton;
    [SerializeField] private Button _thirdButton;
    [SerializeField] private GameObject _color_panel;
    [SerializeField] private GameObject _video_panel;
    [SerializeField] private GameObject _video_panel_2;
    [SerializeField] private GameObject _Drawskeleton;
    [SerializeField] private GameObject _DrawskeletonTwo;
    [SerializeField] private GameObject _Finish;
    [SerializeField] private GameObject _motionButton;
    [SerializeField] private GameObject _numberFrameButton;
    [SerializeField] private GameObject _drawAnotherViewButton;
    [SerializeField] private GameObject _showButton;
    [SerializeField] private GameObject _save;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _ControlMaxDot ;
    [SerializeField] private GameObject _DeleteDot ;


    public override void Initialize()
    { 
        _startButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("Draw");
            ViewManager.Show<Drawing>();
            _color_panel.SetActive(true);
            _Drawskeleton.SetActive(true);
            _DeleteDot.SetActive(false);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _DrawskeletonTwo.SetActive(false);
            _Finish.SetActive(false);
            _motionButton.SetActive(false);
            _numberFrameButton.SetActive(false);
            _drawAnotherViewButton.SetActive(false);
            _showButton.SetActive(false);
            _save.SetActive(false);
            _text.SetActive(false);
            _ControlMaxDot.SetActive(false);

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


    


