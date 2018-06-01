using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour {

	const float dist = 2.21f; //дистанция на которой будут генерироваться новые камни
	private bool hasGenerated = false;//предотвращение повторной генерации
	[SerializeField]
	private LayerMask rockLayer;
	[SerializeField]
	private List<GameObject> rockList = new List<GameObject>();


	//использовалось для проверки работы алгоритма генерации

	//private void Update()
	//{
	//	if (Input.GetMouseButtonDown(1))
	//	{
	//		if (!hasGenerated)
	//		{
	//			hasGenerated = true;
	//			GenerateNewRocks();
	//		}
	//	}
	//}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		//генерация, когда игрок рядом (Destroyer следует за игроком и обладает радиусом, большим, чем видит камера)
		if (collision.transform.tag == "Destroyer" && !hasGenerated)
		{
			hasGenerated = true;
			GenerateNewRocks();
			
		}
	}
	private void GenerateNewRocks()
	{
		int randShape = Random.Range(0, rockList.Count); //выбор рандомного префаба камня
		Vector2 newTrans = GenerateLocation();//выбор локации

		//генерация нового камня
		GameObject newObject = Instantiate(rockList[randShape], new Vector3(newTrans.x, newTrans.y, 0),Quaternion.identity,null);

		//изменение размера нового камня
		float scale = Random.Range(0.5f, 1.5f);
		Vector3 newObjectSize = new Vector3(newObject.transform.localScale.x * scale, newObject.transform.localScale.y * scale, newObject.transform.localScale.z * scale);
		newObject.transform.localScale = newObjectSize;
	}
	private Vector2 GenerateLocation() //генератор будущей локации нового камня
	{
		bool isViable = false; //для проверки, подходит ли локация для генерации нового камня
		Vector2 location = new Vector2(0,0);
		while (!isViable) // цикл продолжается до тех пор, пока локация не будет подходить требованиям
		{
			location = Random.insideUnitCircle.normalized * dist + new Vector2(gameObject.transform.position.x, gameObject.transform.position.y); //генерация локации
			if (location.y >= gameObject.transform.position.y)// первое требование, что бы новый камень был на уровне или выше (по оси y), чем предыдущий
			{
				Collider2D collider = Physics2D.OverlapCircle(location, 1.5f, rockLayer);//во избежании генерации камней рядом
				if (collider == null)
				{
					isViable = true;
				}
			}
		}
		return location;
	}

}
