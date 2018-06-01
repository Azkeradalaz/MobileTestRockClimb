using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField]
	private float damp = 10f;
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float offsetY = 0;

	void LateUpdate()
	{
		float targetX = target.position.x;
		float targetY = target.position.y + offsetY;
		targetX = Mathf.Lerp(transform.position.x, targetX, damp * Time.deltaTime);
		targetY = Mathf.Lerp(transform.position.y, targetY, damp * Time.deltaTime);
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}

	public void GetNewTarget(Transform newTarget)
	{
		target = newTarget;
	}
}