////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PickUpManager.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/24/2022 03:15 AM
//Last Modified On : 10/24/2022 05:36 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => changed spawn timer range
//Description : script for manage the pick up spawn
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public PickUpBehaviour HPBehaviour;
    public PickUpBehaviour MPBehaviour;
    public PickUpBehaviour scoreBehaviour;
    public PickUpBehaviour damageBehaviour;

    // Update is called once per frame
    void Update()
    {
        if(!HPBehaviour.isStartTimer && HPBehaviour.isWaiting)
        {
            float timer = Random.Range(15, 50);
            HPBehaviour.isStartTimer = true;
            HPBehaviour.isWaiting = false;
            HPBehaviour.timer = timer;
        }

        if (!MPBehaviour.isStartTimer && MPBehaviour.isWaiting)
        {
            float timer = Random.Range(10, 30);
            MPBehaviour.isStartTimer = true;
            MPBehaviour.isWaiting = false;
            MPBehaviour.timer = timer;
        }

        if (!scoreBehaviour.isStartTimer && scoreBehaviour.isWaiting)
        {
            float timer = Random.Range(5, 10);
            scoreBehaviour.isStartTimer = true;
            scoreBehaviour.isWaiting = false;
            scoreBehaviour.timer = timer;
        }

        if (!damageBehaviour.isStartTimer && damageBehaviour.isWaiting)
        {
            float timer = Random.Range(30,80);
            damageBehaviour.isStartTimer = true;
            damageBehaviour.isWaiting = false;
            damageBehaviour.timer = timer;
        }
    }
}
