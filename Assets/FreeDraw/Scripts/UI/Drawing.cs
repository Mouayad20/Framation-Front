using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Drawing : View
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
    [SerializeField] private GameObject _motionButton;
    [SerializeField] private GameObject _numberFrameButton;
    [SerializeField] private GameObject _drawAnotherViewButton;
    [SerializeField] private GameObject _showButton;
    [SerializeField] private GameObject _save;
    [SerializeField] private GameObject _text;

    public override void Initialize()
    {
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
            _DrawskeletonTwo.SetActive(false);
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
            _Drawskeleton.SetActive(true);
            _DrawskeletonTwo.SetActive(false);
            _Finish.SetActive(false);
            _motionButton.SetActive(false);
            _numberFrameButton.SetActive(false);
            _drawAnotherViewButton.SetActive(false);
            _showButton.SetActive(false);
            _save.SetActive(false);
            _text.SetActive(false);
        
                });
        _showButton.GetComponent<Button>().onClick.AddListener(()=>{
                    // لازم يتم حفظ المشهد والتقسيم عفريمات وبعدين ينتقل عالواجهة يلي رح تنعرض فيها الفريمات كلها
            Audio_Manager.Instance.PlaySound("GoToBack");
            ViewManager.Show<Frames>();
                    GameObject ButtonEditPhoto  = transform.GetChild (0).gameObject;
                    GameObject g;
                    for(int i=0 ; i<10 ; i++)
                        {
                            g = Instantiate (ButtonEditPhoto , transform);
                        }
                        Destroy(ButtonEditPhoto);
                });
            
       

        }
    }



