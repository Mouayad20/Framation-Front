using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

using UnityEngine.SceneManagement;

public class StartStop : MonoBehaviour
{
    private VideoPlayer player;
    public Button button;
    public Sprite startSprite;
    public Sprite stopSprite;

    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }
    void Update()
    {

    }
    public void ChangeStartStop()
    {
        if(player.isPlaying == false)
        {
            print("isPlaying, isPlaying,isPlaying,isPlaying");
            player.Play();
            button.image.sprite = stopSprite;
        }
        else {
            player.Pause();
            print("Pause, Pause,Pause,Pause");
            button.image.sprite = startSprite;

        }
    }
 
}
