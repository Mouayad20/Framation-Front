using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using Framation;
using SFB;

public class Frames : View
{
  
    [SerializeField] private Button     _GoToHomeButton;
    [SerializeField] private Button     _SaveVideoButton;
    [SerializeField] private GameObject _BackToFramesUIButton;
    [SerializeField] private GameObject _ColorPen;
    [SerializeField] private GameObject _DrawAnotherViewButton;
    [SerializeField] private GameObject _ShowButton;
    [SerializeField] private GameObject _ClearBoard ;
    [SerializeField] private GameObject popupObject  ;

    public override void Initialize()
    {
        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
             ViewManager.Show<Drawing>();
             _DrawAnotherViewButton.SetActive(true);
             _ShowButton.SetActive(true);
             _BackToFramesUIButton.SetActive(false);
             _ColorPen.SetActive(false);
             _ClearBoard.SetActive(false);
        });

        _SaveVideoButton.onClick.AddListener(()=>{
            Audio_Manager.Instance.PlaySound("Options");

            string[] selectedPath = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", false);

            if (!string.IsNullOrEmpty(selectedPath[0]))
            {
                string ffmpegPath = @"C:\ffmpeg\bin\ffmpeg.exe"; // Replace with your FFmpeg executable path
                ProcessStartInfo startInfo       = new ProcessStartInfo();
                startInfo.FileName               = ffmpegPath;
                startInfo.Arguments = $"-framerate 24 -i .\\images\\%d.png \"{selectedPath[0]}\\output.mp4\"";
                startInfo.UseShellExecute        = false;
                startInfo.CreateNoWindow         = true;
                startInfo.RedirectStandardOutput = true;
                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }
                print("Video created successfully! in >  " + selectedPath[0]);
            }    
        });
    }

    void ShowPopup()
    {
        popupObject.SetActive(true);
        Invoke("HidePopup", 1f);
    }

    void HidePopup()
    {
        popupObject.SetActive(false);
    }
}


