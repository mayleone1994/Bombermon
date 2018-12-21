using UnityEngine;
using System.Collections;

public class PokeballScenario : MonoBehaviour {

	public GameObject[] pokemons;
	public AudioClip clip;
	private bool canGet = true;
	private SpriteRenderer spr;

	void Start(){

		spr = GetComponent<SpriteRenderer> ();
	}

	void Update(){

		if (!canGet)
			spr.material.color = Color.gray;
		else
			spr.material.color = Color.white;
	}

	void OnCollisionEnter2D(Collision2D col){
		var i = Random.Range (0, pokemons.Length);
		if (canGet && col.gameObject.CompareTag("Player")) {
			if (col.gameObject.GetComponent<Player> ().isPlayer1  &&
			    (GameManager.pokemonPlayer1.gameObject.name == "Voltorb" && col.gameObject.GetComponent<Player>().havePokeball)) {
				AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
				col.gameObject.GetComponent<Player> ().WaitAgain();
				GameManager.pokemonPlayer1 = pokemons[i];
				StartCoroutine("Wait");
			}
			if (!col.gameObject.GetComponent<Player> ().isPlayer1 &&
			    (GameManager.pokemonPlayer2.gameObject.name == "Voltorb" && col.gameObject.GetComponent<Player>().havePokeball)) {
				AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
				col.gameObject.GetComponent<Player> ().WaitAgain();
				GameManager.pokemonPlayer2 = pokemons[i];
				StartCoroutine("Wait");
			}
		}
	}

	IEnumerator Wait(){
		canGet = false;
		yield return new WaitForSeconds (5);
		canGet = true;
	}
}
