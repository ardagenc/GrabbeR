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

    [SerializeField] private HingeJoint hinge;
    
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
        float xAxis = controllerStick.localEulerAngles.x;
        float zAxis = controllerStick.eulerAngles.z;
        float craneMovementX;
        float craneMovementY;

        craneMovementX = ClampGrabberMovement(zAxis, rotationClampValue);
        craneMovementY = ClampGrabberMovement(xAxis, rotationClampValue);

        float locationX = crane.position.x - craneMovementX * speed * Time.deltaTime;
        float locationY = hinge.anchor.y - craneMovementY * Time.deltaTime;

        crane.position = new Vector3(locationX, crane.position.y, crane.position.z);
        hinge.anchor = new Vector3(0f, locationY, 0f);
    }

    private float ClampGrabberMovement(float axis, float clampValue)
    {
        if(axis < 180)
        {
            return  axis / clampValue ;   
        }
        else
        {
            return (axis - 360) / clampValue;
        }
    }

    private void ClampAxisRotation(ref float axis, float clampValue, float cameraOffset)
    {
    if(axis < clampValue + cameraOffset || axis > 360 - clampValue + cameraOffset)
    {
        return;
    }
    else if(axis > clampValue + cameraOffset && axis < 180 + cameraOffset)
    {
        axis = clampValue + cameraOffset;
    }
    else if(axis < 360 - clampValue + cameraOffset && axis > 180 + cameraOffset)
    {
        axis = 360 - clampValue + cameraOffset;
    }
    }

    private void UpdateControllerStickRotation()
    {
        controllerStick.Rotate(verticalAxis * speed, 0f, -horizontalAxis * speed, Space.Self);

        Vector3 eulerAngles = controllerStick.eulerAngles;
        eulerAngles.y = Camera.main.transform.rotation.eulerAngles.y;

        ClampAxisRotation(ref eulerAngles.z, rotationClampValue, Camera.main.transform.eulerAngles.z);
        ClampAxisRotation(ref eulerAngles.x, rotationClampValue, Camera.main.transform.eulerAngles.x);

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
