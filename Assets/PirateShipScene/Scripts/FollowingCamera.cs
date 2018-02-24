using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {
    public Transform target;
    public float walkDistance;
    public float runDistance;
    public float height;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float heightDampening = 2.0f;
    public float rotationDampening = 3.0f;
    public string playerTagName = "Player";

    private Transform _myTransform;
    private float _x;
    private float _y;
    private bool _camButtonDown;
    private bool _rotateCameraKeyPressed;


    private void Awake()
    {
        _myTransform = transform;
    }

    // Use this for initialization
    void Start() {
        if (target == null)
        {
            Debug.LogWarning("We do not have a target");
        }
        else
        {
            CameraSetUp();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("RotateCamera"))
        {
            _camButtonDown = true;
        }
        if (Input.GetButtonUp("RotateCamera"))
        {
            _x = 0;
            _y = 0;

            _camButtonDown = false;
        }

        if (Input.GetButtonDown("RotateCameraHorizontalButtons") || Input.GetButtonDown("RotateCameraVerticalButtons"))
        {
            _rotateCameraKeyPressed = true;
        }
        if (Input.GetButtonUp("RotateCameraHorizontalButtons") || Input.GetButtonUp("RotateCameraVerticalButtons"))
        {
            _x = 0;
            _y = 0;

            _rotateCameraKeyPressed = false;
        }
	}

    void LateUpdate()
    {

        if (target != null)
        {
            if (_rotateCameraKeyPressed)
            {
                _x += Input.GetAxis("RotateCameraHorizontalButtons") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
            if (_camButtonDown) {
                _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                RotateCamera();
            }
            else
            {
                _x = 0;
                _y = 0;

                float wantedRotationAngle = target.eulerAngles.y;
                float wantedHeight = target.position.y + height;

                float currentRotationAngle = _myTransform.eulerAngles.y;
                float currentHeight = _myTransform.position.y;

                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDampening * Time.deltaTime);
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDampening * Time.deltaTime);

                Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
                _myTransform.position = target.position;
                _myTransform.position -= currentRotation * Vector3.forward * walkDistance;

                _myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);

                _myTransform.LookAt(target);
            }
        }
        else
        {
            GameObject go = GameObject.FindGameObjectWithTag(playerTagName);
            if(go == null)
            {
                return;
            }
            target = go.transform;
        }
        
    }

    public void CameraSetUp()
    {
        _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
        _myTransform.LookAt(target);

    }

    private void RotateCamera()
    {
        Quaternion rotation = Quaternion.Euler(_y, _x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -walkDistance) + target.position;
        _myTransform.rotation = rotation;
        _myTransform.position = position;
    }
}
