using UnityEngine;
using System.Collections;

public class InstantiateExplosions : MonoBehaviour {
	
	public GameObject[] explosionParts;
	public int distance;
	private const float aux = 0.3f;
	private GameObject explosion;
	
	void Start () {
		// Up:.
		for (int i = 0; i <= distance; i++) {
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y+i+aux),
			                                     Vector2.up, explosionParts[1].GetComponent<SpriteRenderer>().bounds.size.y, 
			                                     LayerMask.GetMask("Rock"));
			explosion = Instantiate(explosionParts[1], new Vector2(transform.position.x,transform.position.y+i+aux),Quaternion.identity) as GameObject;
			if (hit.collider != null){
				explosion.GetComponent<Animator>().SetTrigger("border");
				break;
			}
			if (i == distance)
				explosion.GetComponent<Animator>().SetTrigger("border");
		}
		// Down:.
		for (int i = 0; i <= distance; i++) {
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-i-aux),
			                                     -Vector2.up, explosionParts[2].GetComponent<SpriteRenderer>().bounds.size.y, 
			                                     LayerMask.GetMask("Rock"));
			
			explosion = Instantiate(explosionParts[2], new Vector2(transform.position.x,transform.position.y-i-aux),Quaternion.identity) as GameObject;
			if (hit.collider != null){
				explosion.GetComponent<Animator>().SetTrigger("border");
				break;
			}
			if (i == distance)
				explosion.GetComponent<Animator>().SetTrigger("border");
			
		}
		// Right:.
		for (int i = 0; i <= distance; i++) {
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x+i+aux, transform.position.y),
			                                     Vector2.right, explosionParts[0].GetComponent<SpriteRenderer>().bounds.size.x, 
			                                     LayerMask.GetMask("Rock"));
			explosionParts[0].transform.localScale = new Vector3(1, 1, 1);
			
			explosion =  Instantiate(explosionParts[0], new Vector2(transform.position.x+i+aux,transform.position.y),Quaternion.identity) as GameObject;
			if (hit.collider != null){
				explosion.GetComponent<Animator>().SetTrigger("border");
				break;
			}
			if (i == distance)
				explosion.GetComponent<Animator>().SetTrigger("border");
		}
		// Left:.
		for (int i = 0; i <= distance; i++) {
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x-i-aux, transform.position.y),
			                                     -Vector2.right, explosionParts[0].GetComponent<SpriteRenderer>().bounds.size.x, 
			                                     LayerMask.GetMask("Rock"));
			explosionParts[0].transform.localScale = new Vector3(-1, 1, 1);
			explosion =  Instantiate(explosionParts[0], new Vector2(transform.position.x-i-aux,transform.position.y),Quaternion.identity) as GameObject;
			if (hit.collider != null){
				explosion.GetComponent<Animator>().SetTrigger("border");
				break;
			}
			if (i == distance)
				explosion.GetComponent<Animator>().SetTrigger("border");
			
		}
	}
}
