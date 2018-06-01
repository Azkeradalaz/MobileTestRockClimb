using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnRock : MonoBehaviour {
	[SerializeField]
	float rotationSpeed = -200f;
	void Update()
	{
		gameObject.transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed); //вращение вокруг оси Z
	}

}
