using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Framation;


public class Drawing : View
{
    [SerializeField] public Button _GoToHomeButton;
    [SerializeField] public Button _FinishButton;
    [SerializeField] public Button _DrawSkeletonButton;
    [SerializeField] public Button _DrawSkeletonTwoButton ;
    [SerializeField] public Button _ControlMaxDotButton ;
    [SerializeField] public Button _DeleteDotButton ;
    [SerializeField] public Button _BackToFramesUIButton ;
    [SerializeField] public Button _ClearBoardButton ;
    [SerializeField] public GameObject _color_panel;
    [SerializeField] public GameObject _video_panel;
    [SerializeField] public GameObject _video_panel_2;
    [SerializeField] public GameObject _DrawSkeleton;
    [SerializeField] public GameObject _DrawSkeletonTwo;
    [SerializeField] public GameObject _Finish;
    [SerializeField] public GameObject _drawAnotherViewButton;
    [SerializeField] public GameObject _showButton;
    [SerializeField] public GameObject _text;
    [SerializeField] public GameObject _ControlMaxDot ;
    [SerializeField] public GameObject _DeleteDot ;
    [SerializeField] public GameObject _BackToFramesUI ;
    [SerializeField] public GameObject _ClearBoard ;
    [SerializeField] public Transform  scroll;

    public static bool drawSkeltonMode; 
    public static bool drawSkelton2Mode; 
    public static bool deleteDotMode; 
    public static bool controlMaxDotMode; 
    public static bool moveToDrawingBoard; 
    public static bool finishMode; 
    public static bool vanishMode; 
    public static bool loadFrames; 

    private void Update() {
        if(moveToDrawingBoard){
            _BackToFramesUI.SetActive(true);
            _color_panel.SetActive(true);
            _ClearBoard.SetActive(true);
            _DeleteDot.SetActive(false);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _DrawSkeleton.SetActive(false);
            _DrawSkeletonTwo.SetActive(false);
            _Finish.SetActive(false);
            _drawAnotherViewButton.SetActive(false);
            _showButton.SetActive(false);
            _text.SetActive(false);
            _ControlMaxDot.SetActive(false);
            moveToDrawingBoard = false;
        }
    }
    
    public override void Initialize()
    {
        drawSkeltonMode    = false;
        drawSkelton2Mode   = false;
        moveToDrawingBoard = false;
        vanishMode         = false;
        loadFrames         = false;
        
        _ClearBoardButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("GoToBack");
            Drawable.drawable.ResetCanvas();
        });

        _GoToHomeButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("GoToBack");
            ViewManager.Show<Home>();
        });

        _DrawSkeletonButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            _DeleteDot.SetActive(true);
            _video_panel.SetActive(true);
            _DrawSkeletonTwo.SetActive(true);
            _color_panel.SetActive(false);
            _ClearBoard.SetActive(false);
            _video_panel_2.SetActive(false);
            _DrawSkeleton.SetActive(false);
            _ControlMaxDot.SetActive(false);
            _BackToFramesUI.SetActive(false);
            drawSkeltonMode = true;
        });

        _DrawSkeletonTwoButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("DrawAndFinish");
             _video_panel_2.SetActive(true);
             _Finish.SetActive(true);
             _ControlMaxDot.SetActive(true);
             _text.SetActive(true);
             _color_panel.SetActive(false);
             _ClearBoard.SetActive(false);
             _video_panel.SetActive(false);
             _DrawSkeletonTwo.SetActive(false);
             _BackToFramesUI.SetActive(false);
             _DeleteDot.SetActive(false);
             drawSkelton2Mode = true;
        });

        _FinishButton.onClick.AddListener(()=> {
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            _drawAnotherViewButton.SetActive(true);
            _showButton.SetActive(true);
            _color_panel.SetActive(false);
            _ClearBoard.SetActive(false);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _Finish.SetActive(false);
            _DrawSkeletonTwo.SetActive(false);
            _BackToFramesUI.SetActive(false);
            _ControlMaxDot.SetActive(false);
            _DeleteDot.SetActive(false);
            _text.SetActive(false);
            finishMode = true;
        });

        _drawAnotherViewButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("GoToBack");
            _color_panel.SetActive(true);
            _ClearBoard.SetActive(true);
            _DrawSkeleton.SetActive(true);
            _video_panel.SetActive(false);
            _video_panel_2.SetActive(false);
            _DeleteDot.SetActive(false);
            _DrawSkeletonTwo.SetActive(false);
            _Finish.SetActive(false);
            _drawAnotherViewButton.SetActive(false);
            _showButton.SetActive(false);
            _BackToFramesUI.SetActive(false);
            _text.SetActive(false);
            _ControlMaxDot.SetActive(false);       
            vanishMode    = true;       
        });

        _ControlMaxDotButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            controlMaxDotMode = true;
        });

        _BackToFramesUIButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            ViewManager.Show<Frames>();
            Drawable.drawable.go.SetActive(true);
        });

        _DeleteDotButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("DrawAndFinish");
            deleteDotMode = true;
        });

        _showButton.GetComponent<Button>().onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("GoToBack");
            ViewManager.Show<Frames>();
            loadFrames = true;
        });
    }
}