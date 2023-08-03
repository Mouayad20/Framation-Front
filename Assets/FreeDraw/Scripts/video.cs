using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class video : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(playGame());
    }

     IEnumerator playGame()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Board 0");
    }
}
