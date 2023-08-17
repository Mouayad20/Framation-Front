using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;
using System.Diagnostics;

public class VideoPlayerController : MonoBehaviour
{
    public RawImage videoScreen;
    public string videoFilePath;
    public RenderTexture renderTexture;

    private VideoPlayer videoPlayer;

    void OnEnable()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = videoFilePath;

        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        Texture2D texture = new Texture2D(256, 256);
        texture.LoadImage(File.ReadAllBytes("Assets\\Video\\sketchTexture.renderTexture"));
        Graphics.CopyTexture(texture, renderTexture);
        videoPlayer.targetTexture = renderTexture;

        videoPlayer.aspectRatio = VideoAspectRatio.FitInside;

        videoPlayer.targetMaterialRenderer = videoScreen.GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";//

        videoPlayer.Prepare();
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.K)){
            videoPlayer.Play();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            videoPlayer.Pause();
        }
        if(Input.GetKeyDown(KeyCode.V)){
            string ffmpegPath = @"ffmpeg"; // Replace with your FFmpeg executable path
            ProcessStartInfo startInfo       = new ProcessStartInfo();
            startInfo.FileName               = ffmpegPath;
            File.Delete("Assets\\Video\\sketch.mp4");
            startInfo.Arguments = $"-y -framerate 24 -i .\\images\\%d.png -c:v vp9 \"Assets\\Video\\sketch.mp4\"";
            startInfo.UseShellExecute        = false;
            startInfo.CreateNoWindow         = true ;
            startInfo.RedirectStandardOutput = true ;
            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                Texture2D texture = new Texture2D(256, 256);
                texture.LoadImage(File.ReadAllBytes("Assets\\Video\\sketchTexture.renderTexture"));
                Graphics.CopyTexture(texture, renderTexture);
                videoPlayer.targetTexture = renderTexture;
                videoPlayer.url = "Assets\\Video\\sketch.mp4";
                videoPlayer.Prepare();      
                videoPlayer.Play();
                print("VVVVVVVVVVVVVVVVVVV");
            }
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Video playback has ended, perform any necessary actions here
    }
}