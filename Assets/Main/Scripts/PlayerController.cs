using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private GameObject toGoRock;
	[SerializeField]
	private GameObject rotationPointsFolder;

	private Vector3 toGoV3;

	private float lifeTimeOnRock = 3f;
	[SerializeField]
	private float deathTime = 4f;
	private float deathTimer = 0f;

	private Rigidbody2D myRB;

	[SerializeField]
	private List<GameObject> rotationPoints = new List<GameObject>();
	

	private bool сanChange = false;
	private bool isLeft = true;
	private bool isDead = false;

	void Start () {
		myRB = gameObject.GetComponent<Rigidbody2D>();

	}
	
	void Update () {
		//управление персонажем
		if (Input.GetMouseButtonDown(0))
		{
			ChangeRock();
		}

		//таймер смерти, обновляется каждый раз при смене камня
		deathTimer += Time.deltaTime;
		if (deathTimer > deathTime)
		{
			Death();
		}


		if (isDead)
		{
			//закручивание после смерти
			if (isLeft)
			{
				gameObject.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 200f);
			}
			else
			{
				gameObject.transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * 200f);
			}
		}
	}

	//при входе в зону камня - появляется возможность за него ухватиться
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag == "Rock")
		{
			сanChange = true;
			toGoRock = collision.gameObject;  //каменять который можно схватить
			//позиция, на которую перемещается рука персонажа при удачной попытке схватить камень
			Vector3 newPos = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
			toGoV3 = newPos;

		}
	}
	//при выходе из зоны триггера камня, возможность за него схватиться - пропадает
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.tag == "Rock")
		{
			сanChange = false;
		}
	}

	private void ChangeRock()
	{
		if (сanChange)
		{
			deathTimer = 0f;
			if (isLeft) // проверка руки (правая/левая)
			{ //для левой руки
				rotationPoints[1].transform.parent = null; //изменение родителя точки вращения правой руки на null
				gameObject.transform.parent = rotationPoints[1].transform; //привязка игрока к вышеобозначенной точке вращения
				rotationPoints[0].transform.parent = rotationPointsFolder.transform; //привязка старой точки вращения к персонажу
				rotationPoints[1].transform.position = toGoV3; // привязка у центру нового камня
				rotationPoints[1].GetComponent<CircleCollider2D>().enabled = false; //выключение коллайдера правой руки
				rotationPoints[0].GetComponent<CircleCollider2D>().enabled = true; //включение коллайдера левой руки
				isLeft = false; //изменение проверки (с левой на правую руку)
			}
			else
			{
				//все вышеописанные действия, но для правой руки
				rotationPoints[0].transform.parent = null;
				gameObject.transform.parent = rotationPoints[0].transform;
				rotationPoints[1].transform.parent = rotationPointsFolder.transform;
				rotationPoints[0].transform.position = toGoV3;
				rotationPoints[0].GetComponent<CircleCollider2D>().enabled = false;
				rotationPoints[1].GetComponent<CircleCollider2D>().enabled = true;
				isLeft = true;
			}

			if (toGoRock.GetComponent<Rock>() != null)//проверка, что бы избежать ошибки при попытке перейти на стартовый камень
			{
				toGoRock.GetComponent<Rock>().RockGrab();
			}


		}
		else Death();
	}

	private void Death()
	{
		gameObject.transform.parent = null; //во избежании вращения вокруг руки после смерти
		myRB.gravityScale = 1f;//"включение" гравитации для падения
		isDead = true; 
		Destroy(gameObject, 2f);	
	}

}
