using UnityEngine;
using System.Collections;

public class Pokeball : MonoBehaviour {
	public GameObject effect;
	public bool player1Instance;

	void Start () {
	
		Invoke ("InstatiatePokemon", 0.2f);
	}
	
	void InstatiatePokemon(){
		Instantiate (effect, transform.position, Quaternion.identity);
		StartCoroutine ("Waiting");
	}

	IEnumerator Waiting(){
		yield return new WaitForSeconds(0.7f);
		if (player1Instance) {
			Instantiate (GameManager.pokemonPlayer1, transform.position, Quaternion.identity);
			GameManager.pokemonPlayer1 = null;
		} else {
			Instantiate (GameManager.pokemonPlayer2, transform.position, Quaternion.identity);
			GameManager.pokemonPlayer2 = null;
		}
		Destroy (gameObject);
	}
}
