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

    public enum playerStates { MOVEIN, PATROL, MOVEAWAY, FINISHMOVING }

    private bool isPatrolRight = true;
    private playerStates playerState = playerStates.MOVEIN;

    // Update is called once per frame
    void Update()
    {
        switch (playerState)
        {
            case playerStates.MOVEIN:
                transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);
                if (transform.position.y >= playerPatrolY)
                {
                    playerState = playerStates.PATROL;
                }
                break;

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

            case playerStates.MOVEAWAY:
                transform.position += new Vector3(0.0f, 6.0f * Time.deltaTime, 0.0f);

                if (transform.position.y > 6.0f)
                {
                    playerState = playerStates.FINISHMOVING;
                }

                break;

            case playerStates.FINISHMOVING:

                break;
        }


    }

    public playerStates GetPlayerState()
    {
        return playerState;
    }

    public void SetPlayerState(playerStates state)
    {
        playerState = state;
    }
}
