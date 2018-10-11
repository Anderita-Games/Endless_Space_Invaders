using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour {
	public Sprite[] Aliens;
	float Speed;
	float Origin;
	float Offset = .5f;
	public GameObject Laser;
	bool Exploding = false;

	void Start () {
		Speed = this.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
		Origin = this.transform.position.x;
		StartCoroutine(Move());
	}

	IEnumerator Move () { //Left --> Down --> Right --> Repeat
		while (GameObject.Find("Canvas").GetComponent<GameMaster>().Game_Active == true) {
			while(this.transform.position.x >= Origin - Offset) {
				this.transform.position = new Vector2(this.transform.position.x + Speed * -1, this.transform.position.y);
				yield return new WaitForSeconds(.5f);
			}
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x - Speed, this.gameObject.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.x * 1.5f);
			while(this.transform.position.x <= Origin + Offset) {
				this.transform.position = new Vector2(this.transform.position.x + Speed * 1, this.transform.position.y);
				yield return new WaitForSeconds(.5f);
			}
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + Speed, this.gameObject.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.x * 1.5f);
		}
	}
		
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - Speed), Vector2.down);
		if (!hit || hit.transform.tag != "Invader") { //So they dont shoot the other invaders
			Fire();
		}

		if (this.transform.position.y <= -6) {
			Destroy(this.gameObject);
		}
	}

	//TODO: CHANGING SPRITES
	/*public void Change_Appearance (int Type) {
		this.GetComponent<SpriteRenderer>().sprite = Aliens[Type];
	}*/

	void OnCollisionEnter2D (Collision2D Coll) {
		if (Coll.gameObject.name == "Laser" && Exploding == false) {
			StartCoroutine(Death_Star());
		}
	}

	IEnumerator Death_Star () {
		Exploding = true;
		this.GetComponent<SpriteRenderer>().sprite = Aliens[6];
		GameObject.Find("Canvas").GetComponent<GameMaster>().Score++;
		yield return new WaitForSeconds(.25f);
		Destroy(this.gameObject);
	}

	void Fire () {
		if (this.transform.position.x > Origin - Offset && this.transform.position.x  < Origin + Offset) { //Prevents invaders from running into laser
			if (Random.Range(0, 251) == 0) {
				GameObject Clone = Instantiate(Laser);
				Clone.name = "Laser";
				Clone.transform.rotation = new Quaternion(0, 0, 180, 0);
				Clone.transform.position = new Vector2(this.transform.position.x, this.transform.position.y  - .230f);
			}
		}
	}
}
