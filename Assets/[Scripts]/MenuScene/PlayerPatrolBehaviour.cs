using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerPatrolBehaviour : MonoBehaviour
{
    public float patrolSpeed;
    public Boundary horizontalBoundary;
    public bool isPatrolRight = true;

    // Update is called once per frame
    void Update()
    {
        if(isPatrolRight)
        {
            if(transform.position.x >= horizontalBoundary.max)
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
    }
}
