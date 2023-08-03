
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board5 : View
{
    [SerializeField] private Button _motionButton;
    [SerializeField] private Button _numberFrameButton;
    [SerializeField] private Button _drawAnotherViewButton;
    [SerializeField] private Button _showButton;
    [SerializeField] private Button _GoToHomeButton;
    [SerializeField] private GameObject _color_panel;
    [SerializeField] private GameObject _video_panel;
    [SerializeField] private GameObject _video_panel_2;
    [SerializeField] private GameObject _Drawskeleton;
    [SerializeField] private GameObject _DrawskeletonTwo;
    [SerializeField] private GameObject _Finish;
    public override void Initialize()
    {
    _motionButton.onClick.AddListener(()=>{
                // كود قصة التحريك بدال حرف ال m
                // لتنعرض الحركة
                Audio_Manager.Instance.PlaySound("About");
                // ViewManager.Show<Board0>();
            });
    _numberFrameButton.onClick.AddListener(()=>{
                Audio_Manager.Instance.PlaySound("About");
                // ViewManager.Show<Board0>();
            });
    _drawAnotherViewButton.onClick.AddListener(()=>{
                // لازم يتم حفظ المشهد والتقسيم عفريمات وبعدين ينتقل عالواجهة الأولى تبع الرسم ليرسم مشهد تاني
                Audio_Manager.Instance.PlaySound("GoToBack");
                ViewManager.Show<Board1>();
                _color_panel.SetActive(true);
                _video_panel.SetActive(false);
                _video_panel_2.SetActive(false);
                _Drawskeleton.SetActive(true);
                _DrawskeletonTwo.SetActive(false);
                _Finish.SetActive(false);


            });
    _showButton.onClick.AddListener(()=>{
                // لازم يتم حفظ المشهد والتقسيم عفريمات وبعدين ينتقل عالواجهة يلي رح تنعرض فيها الفريمات كلها
                Audio_Manager.Instance.PlaySound("GoToBack");
                ViewManager.Show<Board6>();
            //     GameObject ButtonEditPhoto  = transform.GetChild (0).gameObject;
            //     GameObject g;
            //     for(int i=0 ; i<10 ; i++)
            //         {
            //             g = Instantiate (ButtonEditPhoto , transform);
            //         }
            //         Destroy(ButtonEditPhoto);
            });
    _GoToHomeButton.onClick.AddListener(()=>{
                Audio_Manager.Instance.PlaySound("GoToBack");
                ViewManager.Show<Board0>();
            });

    }     
}



