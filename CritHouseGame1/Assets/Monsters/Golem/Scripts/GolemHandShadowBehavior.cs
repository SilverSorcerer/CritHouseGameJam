using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHandShadowBehavior : MonoBehaviour
{
    #region //Variable Declarations
    //Variable to enable debugging this script
    bool debugMode = false;

    //The GameManager Object
    public GameObject GameManagerObj = null;
    GameManagerScript myManager = null;

    //A reference to the player's gameObject, used to track its position and chase the player
    public GameObject playerObject = null;

    Rigidbody2D body;

    //if false, hide the hand and don't make it chase the player
    public bool isActive = false;

    //should the hand be visible on the screen?
    bool isVisible = false;

    


    //general modifier to how fast the hand can move
    public float speedModifier = 0.3f;

    float horizontalVelocity;
    float verticalVelocity;
    public float cornerMovementSpeed = 7.0f;

    Vector2 travelDirection = new Vector2();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Get GameManager Component from its GameObject to track game state with
        myManager = GameManagerObj.GetComponent(typeof(GameManagerScript)) as GameManagerScript;

        body = GetComponent<Rigidbody2D>();
        isActive = false;
        isVisible = false;
    }

    void Update()
    {
        if (myManager.currentFloor == 1) isActive = true;
        else isActive = false;

        if (isActive){
            horizontalVelocity =  playerObject.transform.position.x - this.transform.position.x;
            verticalVelocity = playerObject.transform.position.y - this.transform.position.y;
            Debug.Log("Hand Velocity x: " + horizontalVelocity + "     Hand Velocity y: " + verticalVelocity);

            travelDirection = new Vector2(horizontalVelocity, verticalVelocity);
            travelDirection.Normalize();

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
