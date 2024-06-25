using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10000f;

    public Transform playerBody;
    //public Transform target;
    float xRotation = 0f;
    public PlayerMovement playerMovement;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (playerMovement.trapping == true)
        {
          
        }
        else if (playerMovement.trapping == false)
        {
            float mouseX = Input.GetAxis("MouseX") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("MouseY") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

        }
        
    }
   

}
