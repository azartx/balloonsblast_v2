using UnityEngine;

public class BalloonClickHandler : MonoBehaviour {
    
    void OnMouseDown()
    {
        Debug.Log("Object clicked: " + gameObject.name);
    }
}