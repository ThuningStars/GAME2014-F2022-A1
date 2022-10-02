////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: RandomEnemyBGBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 08:00 AM
//Last Modified On : 10/02/2022 5:14 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Add comments
//Description : Class for random respawn enemy for scene's background (Menu,Instruction Scene)
//              Mostly the code is from in class lab.
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyBGBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    public Boundary screenBounds;

    public float verticalSpeed;
    public float verticalSpeedMax;
    public float verticalSpeedMin;

    // Start is called before the first frame update
    void Start()
    {
        ResetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    // enemy will move vertically from up to down by the random speed
    public void Move()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
    }

    // check bounds to see when need to respawn enemy
    public void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {
            ResetEnemy();
        }
    }

    // spawn the enemy in the random position in a range we assigned in editor
    // and also with random speed in the range we assigned in editor
    public void ResetEnemy()
    {
        // get random position to spawn the enemy
        var RandomXPosition = Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var RandomYPosition = Random.Range(verticalBoundary.min, verticalBoundary.max);
        // get random speed
        verticalSpeed = Random.Range(verticalSpeedMin, verticalSpeedMax);
        // place the enemy on the randome position ready to go
        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0.0f);

    }
}
