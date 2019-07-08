using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[Header("Mouse Movement Controls")]
	public float mouseSensitivity = 5; // how fast the mouse moves
	public float distFromTarget = 3; // distance from camera target
	public float rotationSmoothTime = 0.12f; // time to move/slow camera down by for smoothness
	public Transform target; // where to assign camera position
	public Vector2 pitchMinMax = new Vector2(-40, 85); // stop camera from orbiting over/under character

	private float yaw; // yaw - x-axis
	private float pitch; // pitch - y-axis
	private Vector3 currentRotation, rotationSmoothVelocity;

	void LateUpdate()
	{
		// x axis kept because it is naturally left to right
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;

		// invert vertical rotation
		// TODO: Maybe make this an option and add this to options menu later
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

		// assign y axis value (and keep within bounds)
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		// assign cameras rotation
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity,
			rotationSmoothTime);

		// cameras yaw, pitch & roll
		transform.eulerAngles = currentRotation;

		// set camera's position
		transform.position = target.position - transform.forward * distFromTarget;
	}
}
