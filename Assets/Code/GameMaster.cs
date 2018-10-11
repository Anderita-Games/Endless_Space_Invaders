using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
	public GameObject Invaderss;
	public bool Game_Active = true;
	public int Score;
	public UnityEngine.UI.Text Score_Text;
	public UnityEngine.UI.Text Highscore_Text;

	void Start () {
		for (int i = 0; i < 4; i++) {
			Generate_Wave(4 - i * Invaderss.GetComponent<SpriteRenderer>().bounds.size.y * 1.5f);
		}
		StartCoroutine(Waves());
	}

	void Update () {
		Score_Text.text = " Score: " + Score;
		Highscore_Text.text = "Highscore: " + PlayerPrefs.GetInt("Highscore") + " ";

		if (Input.GetMouseButtonDown(0) && Game_Active == false) {
			if (Score > PlayerPrefs.GetInt("Highscore")) {
				PlayerPrefs.SetInt("Highscore", Score);
			}
			SceneManager.LoadScene("Game");
		}
	}

	void Generate_Wave (float y) {
		int Appearance = Random.Range(0, 6);
		int Quantity = 10;
		for (int i = 0; i <= Quantity; i++) {
			GameObject Clone = Instantiate(Invaderss);
			float x = (.4f * i) - 2;
			Invaderss.transform.position = new Vector3(x, y, 0);
			Clone.name = "Clone #" + i;
			//Invader.GetComponent<Invader>().Change_Appearance(Appearance);
		}
	}

	IEnumerator Waves () {
		while (Game_Active == true) {
			Generate_Wave(4);
			yield return new WaitForSeconds(4); //TODO: Formula for this
		}
	}
}
