using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scroll : MonoBehaviour

{
    [SerializeField] Transform scroll;
    void Start()
    {
        GameObject ButtonEditPhoto  = scroll.GetChild (0).gameObject;
        GameObject g;
        for(int i=0 ; i<30 ; i++)
        {
            g = Instantiate (ButtonEditPhoto , scroll);
            // g.transform.GetChild (1).GetComponent<Text>().text = "edit "+i ;
        }
        Destroy(ButtonEditPhoto);
        
    }

    
}
