using UnityEngine;
using System;
using System.Diagnostics;

public class CreateVideoFromImages : MonoBehaviour
{

    void Start(){
        string ffmpegPath = @"C:\ffmpeg\bin\ffmpeg.exe"; // Replace with your FFmpeg executable path
        string imagesDirectory = @"C:\Users\HP\Downloads\Compressed\Framation Front"; // Replace with the directory containing your images
        string outputVideoPath = @"C:\Users\HP\Downloads\Compressed\Framation Front\output.mp4"; // Replace with the desired output video path

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = ffmpegPath;
        startInfo.Arguments = $"-framerate 24 -i \"{imagesDirectory}\\%d.png\" -c:v libx264 -r 24 -pix_fmt yuv420p \"{outputVideoPath}\"";
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;
        print(startInfo.ToString());
        using (Process process = Process.Start(startInfo))
        {
            process.WaitForExit();
        }

        print("Video created successfully!");
    }
}