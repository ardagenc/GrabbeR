using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float horizontalAxis;
    float verticalAxis;
    [SerializeField] private float speed;

    [SerializeField] private Transform crane;

    [SerializeField] private Transform controllerStick;
    [SerializeField] private float rotationClampValue;

    [SerializeField] private Transform[] grabber;
    private bool isOpen = true;
    
    void Update()
    {
        HandleAxis();
        GrabberMovement();
        UpdateControllerStickRotation();
        HandleGrabber();
    }

    private void HandleAxis()
    {
        if(Input.GetMouseButton(0))
        {
            horizontalAxis = Input.GetAxisRaw("Mouse X");
            verticalAxis = Input.GetAxisRaw("Mouse Y");
        }
        else
        {
            horizontalAxis = 0f;
            verticalAxis = 0f;
        }
    }

    private void GrabberMovement()
    {
        float zAxis = controllerStick.eulerAngles.z;
        float craneMovement;

        if(zAxis < 180)
        {
            craneMovement = zAxis / rotationClampValue ;   
        }
        else
        {
            craneMovement = (zAxis - 360) / rotationClampValue;
        }


        float locationX = crane.position.x - craneMovement * speed * Time.deltaTime;

        crane.position = new Vector3(locationX, crane.position.y, crane.position.z);
    }

    private void ClampAxisRotation(ref float axis, float clampValue)
    {
        if(axis < clampValue || axis > 360 - clampValue)
        {
            return;
        }
        else if(axis > clampValue && axis < 180)
        {
            axis = clampValue;
        }
        else if(axis < 360 - clampValue && axis > 180)
        {
            axis = 360 - clampValue;
        }
    }

    private void UpdateControllerStickRotation()
    {
        controllerStick.Rotate(verticalAxis, 0f, -horizontalAxis);

        Vector3 eulerAngles = controllerStick.eulerAngles;

        ClampAxisRotation(ref eulerAngles.z, rotationClampValue);
        ClampAxisRotation(ref eulerAngles.x, rotationClampValue);

        controllerStick.eulerAngles = eulerAngles;

        if(!Input.GetMouseButton(0))
        {
            controllerStick.rotation = Quaternion.Lerp(controllerStick.rotation, Camera.main.transform.rotation, 0.1f);
        }
    }

    /*
    // Bu kod benim ilk aklıma gelen haliyle yazdığım hali. Kod tekrarı var ve okuması zor.
    //
    private void ControllerStickMovement()
    {
        controllerStick.Rotate(verticalAxis, 0f, -horizontalAxis);

        float xAxis = controllerStick.eulerAngles.x;
        float zAxis = controllerStick.eulerAngles.z;        
        
        if(zAxis < rotationClampValue || zAxis > 360 - rotationClampValue)
        {
            controllerStick.eulerAngles = new Vector3(controllerStick.eulerAngles.x, controllerStick.eulerAngles.y, controllerStick.eulerAngles.z); 
        }
        else if( zAxis > rotationClampValue &&  zAxis < 180)
        {
            controllerStick.eulerAngles = new Vector3(controllerStick.eulerAngles.x, controllerStick.eulerAngles.y, rotationClampValue); 
        }
        else if( zAxis < 360 - rotationClampValue &&  zAxis > 180)
        {
            controllerStick.eulerAngles = new Vector3(controllerStick.eulerAngles.x, controllerStick.eulerAngles.y, 360 - rotationClampValue); 
        }

        if(xAxis < rotationClampValue || xAxis > 360 - rotationClampValue)
        {
            controllerStick.eulerAngles = new Vector3(controllerStick.eulerAngles.x, controllerStick.eulerAngles.y, controllerStick.eulerAngles.z); 
        }
        else if( xAxis > rotationClampValue &&  xAxis < 180)
        {
            controllerStick.eulerAngles = new Vector3(rotationClampValue, controllerStick.eulerAngles.y, controllerStick.eulerAngles.z); 
        }
        else if( xAxis < 360 - rotationClampValue &&  xAxis > 180)
        {
            controllerStick.eulerAngles = new Vector3(360 - rotationClampValue, controllerStick.eulerAngles.y, controllerStick.eulerAngles.z); 
        }

        if(!Input.GetMouseButton(0))
        {
            //controllerStick.eulerAngles = Vector3.Lerp(controllerStick.eulerAngles, Vector3.zero , 0.2f);
            controllerStick.rotation = Quaternion.Lerp(controllerStick.rotation, Camera.main.transform.rotation , 0.1f);
        }
    }
    */

    private void HandleGrabber()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (isOpen)
            {
                for (int i = 0; i < grabber.Length; i++)
                {
                    grabber[i].DOLocalRotate(new Vector3(-50f, grabber[i].eulerAngles.y, grabber[i].eulerAngles.z), 1f);
                }

                isOpen = false;
            }
            else
            {
                for (int i = 0; i < grabber.Length; i++)
                {
                    grabber[i].DOLocalRotate(new Vector3(0f, grabber[i].eulerAngles.y, grabber[i].eulerAngles.z), 1f);
                }

                isOpen = true;
            }

        }
            
    }

}
