using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public UnityEngine.UI.Text pressEnter, load;
	public Canvas canvas;
	private bool check = true;

	void Awake(){
		load.enabled = false;
	}

	void Update () {
			if (check)
				StartCoroutine("TextScreen");
			if (Input.GetKeyDown (KeyCode.Return)) {
			GetComponent<AudioSource>().enabled = false;
			canvas.enabled = false;
			load.enabled = true;
			Application.LoadLevel (1);
		}
	}

	IEnumerator TextScreen(){
		check = false;
		pressEnter.enabled = !pressEnter.enabled;
		yield return new WaitForSeconds (0.5f);
		check = true;
	}
}
