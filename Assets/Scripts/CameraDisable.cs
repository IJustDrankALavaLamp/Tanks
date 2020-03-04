using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisable : MonoBehaviour
{
    public GameObject GameGrill;
    // Start is called before the first frame update
    void Start()
    {
        GameGrill.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return) == true)
        {
            GameGrill.SetActive(false);
        }
    }
    
}
