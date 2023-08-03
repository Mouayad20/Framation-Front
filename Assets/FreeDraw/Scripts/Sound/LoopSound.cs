// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class LoopSound : MonoBehaviour
// {
//     [SerializeField] Slider volumeSlider;
//     // [SerializeField] AudioSource backgroundmusic;
    
//     void Start()
//     {
//         if (!PlayerPrefs.HasKey("musicVolume"))
//         {
//             PlayerPrefs.SetFloat("musicVolume", 1);
//             Load();
//         }
//         else
//         {
//             Load();
//         }
//     }

//     public void ChangeVolume()
//     {   
//         Audio_Manager.ChangeVolumeTo("start", volumeSlider.value);
//         AudioListener.volume = volumeSlider.value;   
//         Save();
//         // backgroundmusic.volume = volumeSlider.value;
//         Save();
//     }

//     public void Load()
//     {
//         volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
//     }
//     private void Save()
//     {
//         PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopSound : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Audio_Manager audioManager;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        audioManager.ChangeVolumeTo("start", volumeSlider.value);
        // AudioListener.volume = volumeSlider.value ;
        Save();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
