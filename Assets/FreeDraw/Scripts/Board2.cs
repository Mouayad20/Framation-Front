using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Board2 : View
{
    [SerializeField] private Button _GoToHomeButton;
    public override void Initialize()
    {
        _GoToHomeButton.onClick.AddListener(()=>{
             Audio_Manager.Instance.PlaySound("GoToBack");
             ViewManager.ShowLast();
        });
    }
}



