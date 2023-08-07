using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scroll : MonoBehaviour

{
    [SerializeField] Transform scroll;

    void Start()
    {
        GameObject ButtonEditPhoto  = scroll.GetChild(0).gameObject;
        GameObject g;

        for(int i=0 ; i<30 ; i++)
        {
            g = Instantiate(ButtonEditPhoto, scroll);
            Text buttonText   = g.GetComponentInChildren<Text>();
            Image buttonImage = g.GetComponentInChildren<Image>();
            if (buttonText != null && buttonImage != null  )
            {
                buttonText.text = "edit " + i;
                Texture2D newImage = LoadImageFromDisk("Assets\\Resources\\sprites\\frame"+i+".jpg");
                buttonImage.sprite = Sprite.Create(
                    newImage,
                    new Rect(0, 0,
                        newImage.width,
                        newImage.height
                    ),
                    Vector2.zero
                );  
            }
            g.GetComponent<Button>().onClick.AddListener(()=> {
                print(buttonText.text);
            });
        }
        Destroy(ButtonEditPhoto);
        
    }

    Texture2D LoadImageFromDisk(string imagePath)
    {
        byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        return texture;
    }

    
}
