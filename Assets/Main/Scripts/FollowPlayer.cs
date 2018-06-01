using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	[SerializeField]
	private GameObject player;

	void Update () {
		if (player != null)//во избежание ошибки в инспекторе после смерти игрока
		{
			gameObject.transform.position = player.transform.position;
		}
	}

}
