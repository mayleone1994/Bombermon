using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameObject pokemonPlayer1, pokemonPlayer2;
	public static bool gameOver;
	public SpriteRenderer spr1, spr2;
	public GameObject pokemon;
	public Text gameOverText, otherText;

	private Player p1, p2;
	
	void Start(){
		pokemonPlayer1 = pokemon;
		pokemonPlayer2 = pokemon;
		gameOverText.enabled = otherText.enabled = gameOver = false;
		p1 = GameObject.Find ("Player 1").GetComponent<Player>();
		p2 = GameObject.Find ("Player 2").GetComponent<Player>();
	}

	void Update () {

		CheckGameOver ();

		if (pokemonPlayer1 != null) {
			spr1.sprite = Resources.Load<Sprite> ("Images/"+pokemonPlayer1.gameObject.name);

		} else {
			GameManager.pokemonPlayer1 = pokemon;
		}

		if (pokemonPlayer2 != null) {
			spr2.transform.localScale = new Vector3 (-1, 1, 1);
			spr2.sprite = Resources.Load<Sprite> ("Images/"+pokemonPlayer2.gameObject.name);
		} else {
				GameManager.pokemonPlayer2 = pokemon;
		}
	}

	void CheckGameOver(){
		if (p1.life == 0 && p2.life == 0) {
			gameOverText.text = "Tie!";
			gameOver = gameOverText.enabled = true;
			Invoke("Restart", 2);
		} else if (p2.life == 0) {
			gameOverText.text = "Player 1 wins!";
			gameOver = gameOverText.enabled = true;
			Invoke("Restart", 2);
		} else if (p1.life == 0) {
			gameOverText.text = "Player 2 wins!";
			gameOver = gameOverText.enabled = true;
			Invoke("Restart", 2);
		}

	}

	void Restart(){
		otherText.enabled = true;
		if (gameOver && Input.GetKey(KeyCode.Return)) 
			Application.LoadLevel (1);
	}
}
