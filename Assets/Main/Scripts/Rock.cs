using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	private GameObject scoreManager;
	private Camera cam;

	private bool beenGrabbed = false;
	private bool hasEmerald = false;


	void Start () {

		int emeraldGamble = Random.Range(0, 15);
		if (emeraldGamble > 13)
		{
			hasEmerald = true;
		}
		if (hasEmerald)//если есть изумруд
		{
			gameObject.transform.GetChild(1).gameObject.SetActive(true);
			gameObject.transform.GetChild(2).gameObject.SetActive(true);
		}

		scoreManager = GameObject.Find("ScoreManager");
		cam = Camera.main;
	}
	
	
	public void RockGrab()
	{
		// если игрок ещё не трогал камент - то на нём появляются трещины, собирается изумруд(если есть), камера фокусируется на этом камне и добавляются очки
		if (!beenGrabbed)
		{
			beenGrabbed = true;
			scoreManager.GetComponent<ScoreManager>().ScoreUp();
			gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f)); //задание рандомного угла поворота по оси Z
			gameObject.transform.GetChild(0).gameObject.SetActive(true);//включение трещин
			//если есть изумруд - сбор и выключение спрайта изумруда
			if (hasEmerald)
			{
				scoreManager.GetComponent<ScoreManager>().EmeraldUp();
				gameObject.transform.GetChild(2).gameObject.SetActive(false);
			}
			cam.GetComponent<CameraFollow>().GetNewTarget(transform);
		}
	}
	//уничтожение камня, если он вне зоны Destroyer и находится под ним
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.tag == "Destroyer")
		{
			if (collision.transform.position.y > gameObject.transform.position.y +4f)
			{
				Destroy(gameObject);
			}
		}
	}

}
