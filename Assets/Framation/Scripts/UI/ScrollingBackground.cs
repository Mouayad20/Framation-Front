using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    public float scrollSpeed;
    [SerializeField]
    private Renderer BackgroundRenderer;
    
    void Update () {
        BackgroundRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
