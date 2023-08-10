using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;

public class ScreenRecorder : MonoBehaviour
{
    public int captureWidth = 1920;
    public int captureHeight = 1080;
    public int captureFPS = 30;
    public float captureTime = 10f;
    public string savePath = "D:/recorded.mp4";
    public RawImage previewImage;

    public bool isRecording = false;
    public float timer = 0;
    public string ffmpegPath;
    public Process ffmpegProcess;

    void Start()
    {
        ffmpegPath = @"C:\ffmpeg\bin\ffmpeg.exe"; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRecording)
        {
            isRecording = true;
            timer = 0;
            StartRecording();
        }

        if (isRecording)
        {
            timer += Time.deltaTime;

            if (timer >= captureTime)
            {
                isRecording = false;
                StopRecording();
                previewImage.texture = GetVideoTexture();
            }
        }
    }

    void OnDestroy()
    {
        if (isRecording)
        {
            StopRecording();
        }
    }

   public void  StartRecording()
    {
        string args = "-y -f rawvideo -vcodec rawvideo -s " + captureWidth + "x" + captureHeight + " -pix_fmt rgb24 -r " + captureFPS + " -i - -an -vcodec libx264 -preset ultrafast -crf 0 " + savePath;
        ffmpegProcess = new Process();
        ffmpegProcess.StartInfo.FileName = ffmpegPath;
        ffmpegProcess.StartInfo.Arguments = args;
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardInput = true;
        ffmpegProcess.Start();
    }

    void StopRecording()
    {
        ffmpegProcess.StandardInput.Flush();
        ffmpegProcess.StandardInput.Close();
        ffmpegProcess.WaitForExit();
        ffmpegProcess.Dispose();
    }

    Texture2D GetVideoTexture()
    {
        byte[] fileData = File.ReadAllBytes(savePath);
        Texture2D texture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        texture.LoadImage(fileData);
        return texture;
    }
}