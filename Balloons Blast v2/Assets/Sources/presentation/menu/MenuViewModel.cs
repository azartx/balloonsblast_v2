using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuViewModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void printSomeLog()
    {
        Debug.Log("Clicked!");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame()");
        Application.Quit();
    }
}
