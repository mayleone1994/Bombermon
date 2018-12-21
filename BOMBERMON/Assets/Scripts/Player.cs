using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public bool isPlayer1;
	public Pokeball pokeball;
	public KeyCode rigth, left, up, down, fire, escape;
	public Explosion explosion;
	public AudioClip clip, clip2;
	public int life;
	[HideInInspector]
	public bool havePokeball;
	
	private float dir;
	private float speed;
	private bool canMove, isSlow;
	private Animator animator;
	private Rigidbody2D rb;
	private Vector2 direction;
	private SpriteRenderer spr;
	private Transform other;
	private Pokeball pb;
	
	void Start () {
		isSlow = false;
		speed = 4.1f;
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		spr = GetComponent<SpriteRenderer> ();
		havePokeball = true;
		other = isPlayer1 ? GameObject.Find ("Player 2").transform : GameObject.Find("Player 1").transform;
		
	}
	
	void Update(){
		if (GameManager.gameOver) {
			animator.SetBool ("isMoving", false);
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			spr.material.color = Color.white;
		}
		
		if (!GameManager.gameOver) {
			Key ();
			OrderLayer ();
			
			canMove = direction.x != 0 && direction.y == 0 || direction.x == 0 && direction.y != 0;
			
			if (canMove) {
				animator.SetFloat ("direction", dir);
			}
			animator.SetBool ("isMoving", (direction.x != 0 || direction.y != 0));
			
			if (direction == Vector2.zero) {
				rb.constraints = RigidbodyConstraints2D.FreezeAll;
			} else {
				rb.constraints = RigidbodyConstraints2D.None;
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			}
		}
	}
	
	void FixedUpdate(){
		if (canMove) {
			rb.velocity = new Vector2(direction.x*speed, direction.y*speed);
		}
		if (GameManager.gameOver)
			rb.velocity = Vector2.zero;
	}
	
	void Slow(){
		isSlow = true;
		speed = 1.2f;
		spr.material.color = Color.green;
		StartCoroutine ("TimeSlow");
	}
	
	IEnumerator TimeSlow(){
		yield return new WaitForSeconds (10);
		speed = 4.1f;
		spr.material.color = Color.white;
		isSlow = false;
	}
	
	void Key(){
		direction = Vector2.zero;
		dir = 0;
		if (Input.GetKey (rigth)) {
			dir = 3;
			direction = Vector2.right;
		}
		if (Input.GetKey (left)) {
			dir = 2;
			direction = -Vector2.right;
		}
		if (Input.GetKey (up)) {
			dir = 4;
			direction = Vector2.up;
		}
		if (Input.GetKey (down)) {
			direction = -Vector2.up;
			dir = 1;
		}
		if (isPlayer1) {
			if (Input.GetKeyDown (fire)) {
				if (GameManager.pokemonPlayer1.gameObject.name == "Voltorb") {
					if (havePokeball) {
						pb = (Pokeball)Instantiate (pokeball, transform.position, Quaternion.identity);
						pb.player1Instance = true;
						AudioSource.PlayClipAtPoint (clip2, Camera.main.transform.position);
						StartCoroutine ("Wait");
					}
				} else {
					pb = (Pokeball)Instantiate (pokeball, transform.position, Quaternion.identity);
					AudioSource.PlayClipAtPoint (clip2, Camera.main.transform.position);
					pb.player1Instance = true;
				}
			}
		} else {
			if (Input.GetKeyDown (fire) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				if (GameManager.pokemonPlayer2.gameObject.name == "Voltorb") {
					if (havePokeball) {
						pb = (Pokeball)Instantiate (pokeball, transform.position, Quaternion.identity);
						AudioSource.PlayClipAtPoint (clip2, Camera.main.transform.position);
						pb.player1Instance = false;
						StartCoroutine ("Wait");
					}
				} else {
					pb = (Pokeball)Instantiate (pokeball, transform.position, Quaternion.identity);
					AudioSource.PlayClipAtPoint (clip2, Camera.main.transform.position);
					pb.player1Instance = false;
				}
			}
		}
		
		if (isPlayer1) {
			if (Input.GetKeyDown (escape) && GameManager.pokemonPlayer1.gameObject.name != "Voltorb")
				GameManager.pokemonPlayer1 = null;
		} else {
			if (Input.GetKeyDown (escape) && GameManager.pokemonPlayer2.gameObject.name != "Voltorb")
				GameManager.pokemonPlayer2 = null;
		}
	}
	
	public void WaitAgain(){
		StartCoroutine ("Wait");
	}
	
	IEnumerator Wait(){
		havePokeball = false;
		yield return new WaitForSeconds (2);
		havePokeball = true;
	}
	
	void OrderLayer(){
		if (other.position.y > transform.position.y) {
			spr.sortingOrder = 3;
		} else if (other.position.y <= transform.position.y) {
			spr.sortingOrder = 2;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (!GameManager.gameOver) {
			if (col.gameObject.CompareTag ("Dugtrio") || col.gameObject.CompareTag ("Explosion")) {
				Instantiate(explosion, transform.position, Quaternion.identity);
				spr.enabled = false;
				AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
				transform.FindChild("Identity").gameObject.GetComponent<SpriteRenderer>().enabled = false;
				life = 0;
			}
			if (col.gameObject.CompareTag("Coffey") && !isSlow)
				Slow();
		}
	}
}
