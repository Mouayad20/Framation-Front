using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Framation;

public class Scroll : MonoBehaviour {


    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject sad;

    GameObject global;

    public void Start(){
        sad = Instantiate( scrollContent.GetChild(0).gameObject);
        scrollContent.GetChild(0).gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        string folderPath = "images"; // Specify the path to the folder containing the images

        string[] imagePaths = Directory.GetFiles(folderPath, "*.png"); // Change the file extension if needed

        ClearAllChildren(scrollContent);
        foreach (string imagePath in imagePaths)
        {
            Texture2D imageTexture = LoadImageFromDisk(imagePath);

            GameObject button = Instantiate( sad, scrollContent);
            button.GetComponentInChildren<Text>().text = "frame " + (int.Parse(Path.GetFileNameWithoutExtension(imagePath)) + 1 );
            button.GetComponentInChildren<Image>().sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), Vector2.zero);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                // Handle button click event here
                Debug.Log("Button clicked: " + imagePath);
            });
        }
        

    }

    void ClearAllChildren(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 1; i--)
        {
            Transform child = parent.GetChild(i);
            Destroy(child.gameObject); // or DestroyImmediate(child.gameObject) if you want to remove them immediately
        }
    }

    private Texture2D LoadImageFromDisk(string imagePath)
    {
        byte[] imageData = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        return texture;
    }

    // [SerializeField] Transform scroll;
    // [SerializeField] private GameObject _GoToHome;
    // GameObject ButtonEditPhoto;

    // void OnEnable() {
    //     print("Length  : " + Directory.GetFiles("images").Length);
        
    //     int i = 0 ;
        
    //     foreach(FileInfo file in new DirectoryInfo("images").GetFiles()){
    //         ButtonEditPhoto  = scroll.GetChild(0).gameObject;
    //         print(">  " + file.FullName);
    //         GameObject g = Instantiate(ButtonEditPhoto, scroll);
    //         Text  buttonText  = g.GetComponentInChildren<Text>();
    //         Image buttonImage = g.GetComponentInChildren<Image>();
    //         if (buttonText != null && buttonImage != null  )
    //         {
    //             buttonText.text = "frame " + (i+1);
    //             Texture2D newImage = LoadImageFromDisk(file.FullName);
    //             buttonImage.sprite = Sprite.Create(
    //                 newImage,
    //                 new Rect(
    //                     0, 0,
    //                     newImage.width,
    //                     newImage.height
    //                 ),
    //                 Vector2.zero
    //             );  
    //         }
    //         g.GetComponent<Button>().onClick.AddListener(()=> {
    //             print(buttonText.text);
    //             Drawing.moveToDrawingBoard = true;
    //             _GoToHome.SetActive(false);
    //             Drawable.drawable.go.SetActive(false);
    //             Drawable.drawable.isDrawing = true;
    //             ViewManager.Show<Drawing>();
    //         });
    //     }
    //     // foreach (Transform child in scroll)
    //     // {
    //         Destroy(scroll.GetChild(0).gameObject);
    //     // }
    //         // Destroy(ButtonEditPhoto);
    // }

    // Texture2D LoadImageFromDisk(string imagePath){
    //     byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
    //     Texture2D texture = new Texture2D(2, 2);
    //     texture.LoadImage(imageData);
    //     return texture;
    // } 
}