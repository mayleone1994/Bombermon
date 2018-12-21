using UnityEngine;
using System.Collections;

public class CreateExplosion : MonoBehaviour {

	public Explosion explosion;
	public AudioClip clip;

	void Start () {
	
		Invoke ("Explode", 0.2f);

	}

	void Explode(){
		//var newPosition = new Vector2 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y));
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
