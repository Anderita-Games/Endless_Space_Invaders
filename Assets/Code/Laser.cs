using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	void Update () {
		transform.Translate(Vector2.up * Time.deltaTime * 10);
		if (this.transform.position.y >= 6 || this.transform.position.y <= -6) {
			Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		Destroy(this.gameObject);
	}
}
