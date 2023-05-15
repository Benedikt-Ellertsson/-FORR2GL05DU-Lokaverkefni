using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController controller;
	public Animator animator;

	public float runSpeed = 40f; // Hraði leikmanns

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump")) // Þegar ýtt er á "W" eða "Space" þá...
		{
			jump = true;
			animator.SetBool("IsJumping", true); // Set jumping animation í gang.
		}

		if (Input.GetButtonDown("Crouch")) // Þegar ýtt er á "S" eða "control" þá...
		{
			crouch = true; // Crouch bool true
		}
		else if (Input.GetButtonUp("Crouch")) // Þegar sleppt er "S" eða "control" takka þá...
		{
			crouch = false; // Crouch bool false
		}

	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false); // Þegar leikmaður lendir þá er slökkt á stökk animation
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching); // Þegar spilari ýtir niður til að skríða þá er breytt í skríða animation
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
