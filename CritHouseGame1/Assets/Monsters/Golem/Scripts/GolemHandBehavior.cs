using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHandBehavior : MonoBehaviour
{
    
    #region //Variable Declarations

    //Debug this script
    bool debugMode = false;

    //The GameManager Object
    public GameObject GameManagerObj = null;
    GameManagerScript myManager = null;

    //The shadow that this hand is attached to
    public GameObject myShadow = null;

    //The amount of health points the hand posesses
    public int handHealth = 100;

    //Is this the shadow's left hand?
    public bool isLeft = false;
    
    //How far above the hand can the shadow possibly be?
    Vector2 maxHandPositionOffset = new Vector2(0.0f, 1.2f);

    //The offset used when postioning the hand above the shadow
    Vector2 currentHandPositionOffset;

    //How far to the side should the hand hover over its shadow?
    float sideOffset = 0.45f;

    //scalar on how far away hand is currently from shadow
    public float distanceFromShadow = 1.0f;

    #endregion

    void Start(){

        //Get GameManager Component from its GameObject to track game state with
        myManager = GameManagerObj.GetComponent(typeof(GameManagerScript)) as GameManagerScript;

        //Move hand to left or right of shadow, depending on handedness
        if (isLeft) maxHandPositionOffset -= new Vector2(sideOffset, 0);
        else maxHandPositionOffset += new Vector2(sideOffset, 0);
    }

    void Update(){
        
        //Only perform logic if player is on the same floor as the Golem
        if (myManager.currentFloor == 1){

            //Calculate how far away from the shadow the hand should hover
            if (distanceFromShadow > 1.0f) distanceFromShadow = 1.0f;
            if (distanceFromShadow < 0.0f) distanceFromShadow = 0.0f;
            currentHandPositionOffset = maxHandPositionOffset * distanceFromShadow;

            //Move the hand to its shadow's position plus the desired offset
            transform.position = new Vector2(myShadow.transform.position.x + currentHandPositionOffset.x, myShadow.transform.position.y + currentHandPositionOffset.y);
        }
    }
}
