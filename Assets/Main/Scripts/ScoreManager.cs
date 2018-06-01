using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text emeraldScoreText;

	private int score = 0; // счёт очков
	private int emeraldCounter = 0; //счёт изумрудов

	private string scoreString = "Score: ";
	private string emeraldScoreString = "Emeralds: ";



	public void ScoreUp()
	{
		score++;//пополнение счёта очков
		UpdateScoreText();//вывод
	}
	public void EmeraldUp()
	{
		emeraldCounter++;//пополнение счёта изумрудов
		UpdateEmeraldText();//вывод
	}
	private void UpdateScoreText()//функция вывода счёта очков
	{
		scoreText.text = scoreString + score;
	}
	private void UpdateEmeraldText() //функция вывода счёта изумрудов
	{
		emeraldScoreText.text = emeraldScoreString + emeraldCounter;
	}
}
