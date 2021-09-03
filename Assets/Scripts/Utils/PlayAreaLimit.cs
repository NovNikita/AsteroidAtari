using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaLimit : MonoBehaviour
{
    //this script keeps object it attached to in play area, teleporting it left/right and top/bottom
    //if it exits camera view

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;  //getting reference to main camera at start
    }

    void Update()
    {
        //if object is off screen on left side - teleport it to right, and vice versa
        if (transform.position.x < mainCamera.ViewportToWorldPoint(Vector3.zero).x) 
            transform.position = new Vector3(mainCamera.ViewportToWorldPoint(Vector3.right).x, transform.position.y, transform.position.z);
        if (transform.position.x > mainCamera.ViewportToWorldPoint(Vector3.right).x)
            transform.position = new Vector3(mainCamera.ViewportToWorldPoint(Vector3.zero).x, transform.position.y, transform.position.z);

        //if object is off screen on top - teleport it to bottom, and vice versa
        if (transform.position.y > mainCamera.ViewportToWorldPoint(Vector3.up).y)
            transform.position = new Vector3(transform.position.x, mainCamera.ViewportToWorldPoint(Vector3.zero).y, transform.position.z);
        if (transform.position.y < mainCamera.ViewportToWorldPoint(Vector3.zero).y)
            transform.position = new Vector3(transform.position.x, mainCamera.ViewportToWorldPoint(Vector3.up).y, transform.position.z);
    }
}
