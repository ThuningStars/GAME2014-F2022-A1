////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BackgroundManager.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 04:23 AM
//Last Modified On : 10/02/2022 5:34 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Add comments
//Description : Class for manage the scrolling background, needs 2 background gameobject to make it smoother
//              The code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float verticalSpeed;
    public Boundary boundary;

    // it should be in the fixed update otherwise it may cause some glitch
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    // scroll the bakcground with an assigned speed with fixed deltatime to avoid frame drop problem
    public void Move()
    {
        transform.position -= new Vector3(0.0f, verticalSpeed * Time.fixedDeltaTime, 0.0f);
    }

    // if it meet the bound assigned on editor then reset it
    public void CheckBounds()
    {
        if (transform.position.y < boundary.min)
        {
            ResetBackground();
        }
    }

    // simply put it back to the upper bound
    public void ResetBackground()
    {
        transform.position = new Vector2(0.0f, boundary.max);
    }
}
