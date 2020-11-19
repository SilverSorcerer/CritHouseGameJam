using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHandBehavior : MonoBehaviour
{
    //Variable to enable debugging this script
    bool debugMode = false;

    Rigidbody2D body;

    //if false, hide the hand and don't make it chase the player
    public bool isActive = false;

    //should the hand be visible on the screen?
    bool isVisible = false;

    //A reference to the player's gameObject, used to track its position and chase the player
    public GameObject playerObject = null;

    //The amount of health points the hand posesses
    public int handHealth = 100;

    //general modifier to how fast the hand can move
    public float speedModifier = 0.3f;

    float horizontalVelocity;
    float verticalVelocity;
    public float cornerMovementSpeed = 7.0f;

    Vector2 travelDirection = new Vector2();


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        isActive = false;
        isVisible = false;
    }

    void Update()
    {
        horizontalVelocity =  playerObject.transform.position.x - this.transform.position.x;
        verticalVelocity = playerObject.transform.position.y - this.transform.position.y;
        Debug.Log("Hand Velocity x: " + horizontalVelocity + "     Hand Velocity y: " + verticalVelocity);

        travelDirection = new Vector2(horizontalVelocity, verticalVelocity);
        travelDirection.Normalize();
    }

    void FixedUpdate()
    {
        if (isActive){

            

            #region //Hand Movement
            if (horizontalVelocity != 0 && verticalVelocity != 0) // Check for diagonal movement
                {
                    // limit movement speed diagonally, so you move at 70% speed
                    horizontalVelocity *= cornerMovementSpeed;
                    verticalVelocity *= cornerMovementSpeed;
                }

            body.velocity = new Vector2(horizontalVelocity * speedModifier, verticalVelocity * speedModifier);
            #endregion
        } 
    }
}
