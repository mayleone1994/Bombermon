using UnityEngine;
using System.Collections;

public class BreakRock : MonoBehaviour {
	
	private Collider2D colliderRock;
	private Animator anim;

	void Start(){
		colliderRock = GetComponent<Collider2D> ();
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Explosion")) {
			anim.SetTrigger("Break");
			colliderRock.isTrigger = true;
			Destroy (gameObject, 2);
		}
	}
}
