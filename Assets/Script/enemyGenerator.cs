/*
2D Space Shooter
 
Code by Alberto Betella (alberto@betella.net)
 
Relased Under a Creative Commons License:
Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA 4.0)
http://creativecommons.org/licenses/by-nc-sa/4.0/
*/

using UnityEngine;
using System.Collections;


public class enemyGenerator : MonoBehaviour {

	public GameObject alien;

	private float timeBetweenEnemies = 2f;  // 0.2 = 5 shots per second
	private int secondsBeforeFirstEnemyAppears = 1; // Wait X seconds before the first enemy appears after the game starts

	private float timeLastAlien;


	void FixedUpdate () {

		if (Time.realtimeSinceStartup > secondsBeforeFirstEnemyAppears && Time.time >= timeLastAlien) {

		GameObject.Instantiate(alien);
		timeLastAlien = Time.time + timeBetweenEnemies;

		}
	}
}