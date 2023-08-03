using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(playGame());
    }

     IEnumerator playGame()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Home");
    }
}
