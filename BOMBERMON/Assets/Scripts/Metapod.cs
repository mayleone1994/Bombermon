using UnityEngine;
using System.Collections;

public class Metapod : MonoBehaviour {

	public AudioClip clip;

	void OnTriggerEnter2D(Collider2D col){

		if (col.CompareTag ("Explosion")) {
			GetComponent<Animator>().SetTrigger("isDead");
			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
			Destroy (gameObject, 2.2f);
		}
	}
}
