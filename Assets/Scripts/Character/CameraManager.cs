using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[Header("Mouse Movement Controls")]
	public float mouseSensitivity = 5;
	public float distFromTarget = 3;
	public float rotationSmoothTime = 0.12f;
	public Transform target;
	public Vector2 pitchMinMax = new Vector2(-40, 85);

	private float yaw, pitch;
	private Vector3 currentRotation, rotationSmoothVelocity;

	void LateUpdate()
	{
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * distFromTarget;
	}

	void CameraMovement()
	{
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * distFromTarget;
	}
}
