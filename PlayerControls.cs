using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	public float speed = 10f;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	public float jumpVelocity = 2f;

	private Rigidbody2D rb;
	private Animator animator;

	private bool isGrounded;
	private bool isAttacking;
	private bool isWalking;

	float xmin;
	float xmax;

	void Start(){
		rb = GetComponent<Rigidbody2D> ();
		animator = GameObject.FindObjectOfType<Animator>();

		isGrounded = true;
		isAttacking = false;
	}

	void Update ()
	{
		if (isAttacking == false) {
			LeftAndRightMovement ();
		}

		if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false){
			rb.velocity = Vector2.up * jumpVelocity;
			isGrounded = false;
		}

		if (Input.GetButtonDown ("Fire1")) {
			animator.SetTrigger("AttackTrigger");
		}

		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !Input.GetButton ("Jump")) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	void LeftAndRightMovement(){
			Vector3 movement = Vector3.zero;
			movement.x = Input.GetAxis ("Horizontal");
			transform.position += movement * speed * Time.deltaTime;
		if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > .1) {
			animator.SetBool("isWalking", true);
		}
		else {
			animator.SetBool("isWalking", false);
			}
		}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Floor") {
			isGrounded = true;
		}
	}


	
}