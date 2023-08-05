using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Drawing : View
{
    [SerializeField] private Button _GoToHomeButton;
    [SerializeField] private Button _FinishButton;
    [SerializeField] private Button _DrawSkeletonButton;
    [SerializeField] private Button _DrawSkeletonTwoButton ;
    [SerializeField] private Button _ControlMaxDotButton ;
    [SerializeField] private Button _DeleteDotButton ;
    [SerializeField] private GameObject _color_panel;
    [SerializeField] private GameObject _video_panel;
    [SerializeField] private GameObject _video_panel_2;
    [SerializeField] private GameObject _DrawSkeleton;
    [SerializeField] private GameObject _DrawSkeletonTwo;
    [SerializeField] private GameObject _Finish;
    [SerializeField] private GameObject _motionButton;
    [SerializeField] private GameObject _numberFrameButton;
    [SerializeField] private GameObject _drawAnotherViewButton;
    [SerializeField] private GameObject _showButton;
    [SerializeField] private GameObject _save;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _ControlMaxDot ;
    [SerializeField] private GameObject _DeleteDot ;


    public static bool drawSkeltonMode; 
    public static bool drawSkelton2Mode; 
    public static bool deleteDotMode; 
    public static bool controlMaxDotMode; 
    public static bool finishMode; 

    public override void Initialize()
    {
        drawSkeltonMode  = false;
        drawSkelton2Mode = false;

        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
            //  ViewManager.ShowLast();
             ViewManager.Show<Home>();
        });

        _DrawSkeletonButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            _color_panel.SetActive(false);
            _video_panel.SetActive(true);
            _video_panel_2.SetActive(false);
            _DrawSkeletonTwo.SetActive(true);
            _DrawSkeleton.SetActive(false);
            _ControlMaxDot.SetActive(false);
            _DeleteDot.SetActive(true);
            drawSkeltonMode = true;
        });

        _DrawSkeletonTwoButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("DrawAndFinish");
             _color_panel.SetActive(false);
             _video_panel.SetActive(false);
             _video_panel_2.SetActive(true);
             _Finish.SetActive(true);
             _DrawSkeletonTwo.SetActive(false);
             _ControlMaxDot.SetActive(true);
             _DeleteDot.SetActive(false);
             drawSkelton2Mode = true;
        });

        _FinishButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            // _video_panel_2.SetActive(true);
            _motionButton.SetActive(true);
            _numberFrameButton.SetActive(true);
            _drawAnotherViewButton.SetActive(true);
            _showButton.SetActive(true);
            _save.SetActive(true);
            _text.SetActive(true);
            _color_panel.SetActive(false);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _Finish.SetActive(false);
            _DrawSkeletonTwo.SetActive(false);
            _ControlMaxDot.SetActive(false);
            _DeleteDot.SetActive(false);
            finishMode = true;
        });
        
        _motionButton.GetComponent<Button>().onClick.AddListener(()=>{
                    // كود قصة التحريك بدال حرف ال m
                    // لتنعرض الحركة
            Audio_Manager.Instance.PlaySound("About");
                    // ViewManager.Show<Home>();
        });

        _numberFrameButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("About");
                    // ViewManager.Show<Home>();
        });

        _drawAnotherViewButton.GetComponent<Button>().onClick.AddListener(()=>{
                    // لازم يتم حفظ المشهد والتقسيم عفريمات وبعدين ينتقل عالواجهة الأولى تبع الرسم ليرسم مشهد تاني
            Audio_Manager.Instance.PlaySound("GoToBack");
            // ViewManager.Show<Drawing>();
            _color_panel.SetActive(true);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _DrawSkeleton.SetActive(true);
            _DeleteDot.SetActive(true);
            _DrawSkeletonTwo.SetActive(false);
            _Finish.SetActive(false);
            _motionButton.SetActive(false);
            _numberFrameButton.SetActive(false);
            _drawAnotherViewButton.SetActive(false);
            _showButton.SetActive(false);
            _save.SetActive(false);
            _text.SetActive(false);
            _ControlMaxDot.SetActive(false);
            
        });

        _ControlMaxDotButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            controlMaxDotMode = true;
        });

        _DeleteDotButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            deleteDotMode = true;
        });

        _showButton.GetComponent<Button>().onClick.AddListener(()=>{
                    // لازم يتم حفظ المشهد والتقسيم عفريمات وبعدين ينتقل عالواجهة يلي رح تنعرض فيها الفريمات كلها
            Audio_Manager.Instance.PlaySound("GoToBack");
            ViewManager.Show<Frames>();
            GameObject ButtonEditPhoto  = transform.GetChild (0).gameObject;
            GameObject g;
            for(int i=0 ; i<10 ; i++){
                g = Instantiate (ButtonEditPhoto , transform);
            }
            Destroy(ButtonEditPhoto);
        });
    }
}