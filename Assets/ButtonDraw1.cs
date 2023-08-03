
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBack : MonoBehaviour
{
    public string targetScene;

    public void SwitchScene()
    {
        SceneManager.LoadScene("Board 5");
    }
}
