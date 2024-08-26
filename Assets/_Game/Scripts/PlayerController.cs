using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float horizontalAxis;
    float verticalAxis;
    [SerializeField] private float controllerStickSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [SerializeField] private Transform crane;

    [SerializeField] private Transform controllerStick;
    [SerializeField] private float rotationClampValue;

    [SerializeField] private Transform grabberAnchor;
    [SerializeField] private Transform[] grabber;
    [SerializeField] private float grabberCloseAngle;
    [SerializeField] private float grabberCloseSpeed;
    private bool isOpen = true;

    [SerializeField] private HingeJoint hinge;

    Tween originalPos;


    public static event Action onGrab;

    public bool IsOpen { get => isOpen; set => isOpen = value; }

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

        float locationX = crane.position.x - craneMovementX * horizontalSpeed * Time.deltaTime;
        locationX = Mathf.Clamp(locationX, -10, 5);

        //hinge anchor method
        //float locationY = hinge.anchor.y - craneMovementY * verticalSpeed *  Time.deltaTime;

        float locationY = crane.position.y + craneMovementY * verticalSpeed *  Time.deltaTime;
        locationY = Mathf.Clamp(locationY, 10, 20);
        crane.position = new Vector3(locationX, locationY, crane.position.z);
        //hinge.anchor = new Vector3(0f, locationY, 0f);
        
        if(Input.GetMouseButtonDown(0))
        {
            originalPos.Kill();
        }

        if(Input.GetMouseButtonUp(0))
        {
            GrabberOriginalPos();
        }

    }

    public void GrabberOriginalPos()
    {
        Debug.Log("grabber original pos");

        grabberAnchor.GetComponent<Rigidbody>().velocity = Vector3.zero;
        originalPos = grabberAnchor.DOMove(crane.position - hinge.anchor, 3f).SetEase(Ease.OutBack);
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
        controllerStick.Rotate(verticalAxis * controllerStickSpeed, 0f, -horizontalAxis * controllerStickSpeed, Space.Self);

        Vector3 eulerAngles = controllerStick.eulerAngles;
        eulerAngles.y = Camera.main.transform.rotation.eulerAngles.y;

        ClampAxisRotation(ref eulerAngles.z, rotationClampValue, Camera.main.transform.eulerAngles.z);
        ClampAxisRotation(ref eulerAngles.x, rotationClampValue, Camera.main.transform.eulerAngles.x);

        controllerStick.eulerAngles = eulerAngles;
            
        if(!Input.GetMouseButton(0))
        {
            controllerStick.rotation =  Camera.main.transform.rotation;
            //Quaternion.Lerp(controllerStick.rotation, Camera.main.transform.rotation, 0.1f);
        }

    }

    private void HandleGrabber()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            onGrab?.Invoke();

            if (isOpen)
            {
                for (int i = 0; i < grabber.Length; i++)
                {
                    grabber[i].DOLocalRotate(new Vector3(grabber[i].eulerAngles.x, grabber[i].eulerAngles.y, grabberCloseAngle), grabberCloseSpeed);
                }

                isOpen = false;
            }
            else
            {
                for (int i = 0; i < grabber.Length; i++)
                {
                    grabber[i].DOLocalRotate(new Vector3(grabber[i].eulerAngles.x, grabber[i].eulerAngles.y, 0f), grabberCloseSpeed);
                }

                isOpen = true;
            }

        }
            
    }

}
