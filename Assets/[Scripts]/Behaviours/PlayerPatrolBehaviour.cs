////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerPatrolBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 10:21 AM
//Last Modified On : 10/02/2022 5:25 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Moved player gameLevel from MenuManager.cs to here for clean code
//                   => Add Comments
//Description : Class for make player can move in/out, patroll in the scene (Menu,End Scene)
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerPatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    private float patrolSpeed;
    [SerializeField]
    private Boundary horizontalBoundary;
    [SerializeField]
    private float playerPatrolY = -4.2f;

    // use player states to determine when should we do something about it
    // used this for pushing the play scene after player avator move outside the screen
    // the movein and moveaway can be consider a transition
    public enum playerStates { MOVEIN, PATROL, MOVEAWAY, FINISHMOVING }

    private bool isPatrolRight = true;
    private playerStates playerState = playerStates.MOVEIN;

    // Update is called once per frame
    void Update()
    {
        // use switch statement to modify the states
        switch (playerState)
        {
            // when player move in, it will slowly move into the screen from the bottom
            case playerStates.MOVEIN:
                transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);

                // if player position Y is achieved the prefered position then make it start patrol
                if (transform.position.y >= playerPatrolY)
                {
                    playerState = playerStates.PATROL;
                }
                break;

            // patrol is simply move left and right with assigned speed
            case playerStates.PATROL:
                if (isPatrolRight)
                {
                    if (transform.position.x >= horizontalBoundary.max)
                    {
                        isPatrolRight = false;
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x + patrolSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (transform.position.x <= horizontalBoundary.min)
                    {
                        isPatrolRight = true;
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x - patrolSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    }

                }

                break;

            // when MoveAway enter, player will move out the screen from bottom to top with a fast speed
            case playerStates.MOVEAWAY:
                transform.position += new Vector3(0.0f, 6.0f * Time.deltaTime, 0.0f);

                if (transform.position.y > 6.0f)
                {
                    playerState = playerStates.FINISHMOVING;
                }

                break;

            // this is just a gameLevel that tells MenuManager can push to next scene
            case playerStates.FINISHMOVING:

                break;
        }


    }

    // the gameLevel getter and setter
    public playerStates GetPlayerState()
    {
        return playerState;
    }

    public void SetPlayerState(playerStates state)
    {
        playerState = state;
    }
}
