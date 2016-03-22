/*
2D Space Shooter

Code by Alberto Betella (alberto@betella.net)

Relased Under a Creative Commons License:
Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA 4.0)
http://creativecommons.org/licenses/by-nc-sa/4.0/
*/


using UnityEngine;
using System.Collections;

public class alienController : MonoBehaviour {


	public float enemySpeed = 0.1f;
	public GameObject alienBullet;

	//Bullets related stuff
	private float timeLastShot;
	private float alienBulletXOffset = -0.1f;
	private float alienBulletYOffset = -0.02f;
	private float timeBetweenBullets = 0.5f;  // Time between single bullets 0.2 = 5 shots per second
	private int bulletCounter = 0; //init bullet counter (to avoid continuous shooting)
	private int bulletsPerSeries = 3; // how many bullets are shot before a short break
	private float timeBetweenShotsSeries;  // seconds between a bullet discharge and another
	private float timingShotSeries;
	private bool canShoot = false;


	void Start () {

		//Enemy cannot be hit by the player before he enters the camera view
		GetComponent<Collider2D>().enabled = false;

		// take a break between a discharge and another
		timeBetweenShotsSeries = bulletsPerSeries * timeBetweenBullets + 3;

		//Alien Starting Position X & Y ranges depend on the scene scale (they must be chosen according to the scene)
		transform.position = new Vector3(Random.Range(2.45f, 4f), Random.Range(-1.70f, 0.44f), transform.position.z);

	}

	void FixedUpdate () {

		// Move the alien from right to left
		GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.1f*enemySpeed, 0), ForceMode2D.Impulse);

		// Shoot bullet
		if (canShoot && Time.time >= timeLastShot && bulletCounter < bulletsPerSeries) {
			Instantiate(alienBullet, transform.position+new Vector3 (alienBulletXOffset,alienBulletYOffset,0), Quaternion.identity);
			timeLastShot = Time.time + timeBetweenBullets;
			bulletCounter++;
		} 

		// Restart shooting after the break
		if (Time.time >= timingShotSeries) {
			bulletCounter=0;
			timingShotSeries = Time.time + timeBetweenShotsSeries;
		} 

	}


	// Notice than  when the prefab is visible in the inspector even in the scene camera
	// the functions returns true (but this function works perfectly when compiled)
	void OnBecameVisible() {  

		//Some aliens might not have the capability to shoot bullets
		if (alienBullet!=null) {
			canShoot = true;
		}

		//Enemy can now be hit by the player
		GetComponent<Collider2D>().enabled = true;
	}



	// Gets called when the object goes out of the screen
	void OnBecameInvisible() {  
		Destroy(gameObject);
	}


	// NB. Enemy and its bullet go in two dedicated layers where collisions must be disabled (Edit->project settings->physics 2D)
	void OnTriggerEnter2D(Collider2D thisObject)
	{
		//Increase score
		Camera.main.GetComponent<GUI>().currentScore++;
		// Make the alien briefly blink, then destroy it
		StartCoroutine(blinkUponCollisionAndDestroy(gameObject));

	}


	IEnumerator blinkUponCollisionAndDestroy(GameObject thisPlayer)
	{
		// Color overlay
		thisPlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
		yield return new WaitForSeconds(0.1f);
		//Debug.Log("Enemy Collided");
		Destroy(gameObject);
	}

}