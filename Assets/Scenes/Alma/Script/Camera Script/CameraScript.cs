using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public GameObject zoom;

    public Transform playerTransform;
    public Vector3 _cameraOffset;


    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationSpeed = 5.0f;

    void Start()
    {

        _cameraOffset = transform.position - playerTransform.position;
        zoom.SetActive(false);
        
    }

    void LateUpdate()
    {
        if (RotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;
        }

        Vector3 newPos = playerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(playerTransform);
    }
    
    void Update()
    {



        if (Input.GetMouseButtonDown(1))
        {
            zoom.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            zoom.SetActive(false);
        }
    }
}
