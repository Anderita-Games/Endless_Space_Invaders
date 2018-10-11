using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameObject Laser;
	public Sprite Explosion;
	float Speed = .05f;
	bool Exploding = false;

	void Update () {
		if (GameObject.Find("Canvas").GetComponent<GameMaster>().Game_Active == true) {
			if (Input.GetMouseButtonDown(0)) {
				GameObject Clone = Instantiate(Laser);
				Clone.name = "Laser";
				Clone.transform.position = new Vector2(this.transform.position.x, this.transform.position.y  + .230f);
			}
			if (Input.GetKey("a") && this.transform.position.x >= -2.4f) {
				transform.position = new Vector2(this.transform.position.x - Speed, this.transform.position.y);
			}
			if (Input.GetKey("d") && this.transform.position.x <= 2.4f) {
				transform.position = new Vector2(this.transform.position.x + Speed, this.transform.position.y);
			}
			if (this.transform.position.x >= -2.4f && this.transform.position.x <= 2.4f) { //UNTESTED Phone Controls
				transform.position = new Vector2(this.transform.position.x + Input.acceleration.x, this.transform.position.y);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D Coll) {
		if (Exploding == false) {
			StartCoroutine(Death_Star());
		}
	}

	IEnumerator Death_Star () {
		Exploding = true;
		this.GetComponent<SpriteRenderer>().sprite = Explosion;
		GameObject.Find("Canvas").GetComponent<GameMaster>().Game_Active = false;
		yield return new WaitForSeconds(.25f);
		Destroy(this.gameObject);
	}
}
