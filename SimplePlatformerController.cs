using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // Controls how fast the character moves left and right
    public float jumpForce; // Initial force that gets added to the character's jump speed when you jump
    public float gravity; // Force of gravity that will act on your character's jump speed
    public float height; // Number of units representing the height of your player character

    private float jumpSpeed; // Keeps track of the character's current vertical velocity
    private bool jumping; // Keeps track of whether the character is currently jumping or not

    void Update()
    {
        /**
            Moving the player left and right

            We use Input.GetAxisRaw("Horizontal") in order to read from the left and right keys. The function returns
            1 if the right key is pressed, -1 if the left key is pressed, and 0 if both or neither are pressed.

            We also multiply by Time.deltaTime to make sure we are moving the appropriate amount each frame.
        **/
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        /**
            Jumping

            The player starts jumping when the up key is pressed and we set the jump speed equal to the force of the jump.
            Each frame that we are jumping, we subtract gravity * Time.deltaTime from our jump speed so that the character
            eventually ends up going back down. We then check if we've hit the floor by using a Raycast to check if we've
            hit something within height/2.0f from the Player's center.
        **/
        if (!jumping && Input.GetKeyDown("up")) {
            jumping = true;
            jumpSpeed = jumpForce;
        }

        if (jumping) {
            transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            jumpSpeed -= gravity * Time.deltaTime;

            RaycastHit2D checkFloor = Physics2D.Raycast(transform.position, Vector3.down, height/2.0f);
            if (checkFloor.collider != null) {
                jumpSpeed = 0;
                jumping = false;

                transform.position = (Vector3) checkFloor.point + Vector3.up * height/2.0f;
            }
        }
    }
}
