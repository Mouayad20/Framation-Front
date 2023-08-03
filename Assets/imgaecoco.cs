// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.UI;

// public class imgaecoco : MonoBehaviour
// {
//     [SerializeField] Image textureImage;
//     // Start is called before the first frame update
//     void Start()
//     {
//         byte[] textureBytes = File.ReadAllBytes("C:\\Users\\USER\\Desktop\\im.jpg");
//         Texture2D loadedTexture = new Texture2D(0, 0);
//         loadedTexture.LoadImage(textureBytes);
//         textureImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
//         textureImage.SetNativeSize();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
// list<string> imges = [
//     ""C:\\Users\\USER\\Desktop\\im.jpg"",
//     "path2",
//     "path3",
//     "path4",
// ]