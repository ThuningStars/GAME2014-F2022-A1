////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BackgroundBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 04:23 AM
//Last Modified On : 10/23/2022 02:23 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Add comments => change name => scale background sprite => switch background
//                   if level change => debug
//Description : Class for manage the scrolling background, needs 2 background gameobject to make it smoother
//               and also scale for different devices
//              The moving code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BackgroundType
{
    GROUND,
    CLOUD
}

public class BackgroundBehaviour : MonoBehaviour
{
    public float verticalSpeed;
    public Boundary boundary;
    public BackgroundType backgroundType;

    public GameObject firstLevelMap;
    public GameObject secondLevelMap;
    public GameObject transitionMap;
    public GameObject bossLevelMap;

    public bool isSwitchOnReset = false;

    private void Start()
    {
        Vector3 screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        // initial screenWidth = 2.37, width : x = scale, initial scale = 1
        // so x = initial screenWidth = 2.37
        // so scaleForDifferentDiveces = screenWidth / x
        float scale = screenWorldSize.x / 2.37f;
        gameObject.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
    }

    // it should be in the fixed update otherwise it may cause some glitch
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            BackgroundManager.Instance.SetGameLevel(LevelState.SECOND);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BackgroundManager.Instance.SetGameLevel(LevelState.BOSS);
        }
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
        if (backgroundType == BackgroundType.GROUND)
        {
            if(transitionMap != null)
                if (transitionMap.activeSelf == true)
                {
                    transitionMap.SetActive(false);
                }
        }

        if (isSwitchOnReset)
        {
            LevelState level = BackgroundManager.Instance.GetGameLevel();

            switch (level)
            {
                case LevelState.SECOND:
                    if(backgroundType == BackgroundType.GROUND)
                    {
                        firstLevelMap.SetActive(false);
                        secondLevelMap.SetActive(true);
                    }
                    break;

                case LevelState.BOSS:
                    if (backgroundType == BackgroundType.GROUND)
                    {
                        firstLevelMap.SetActive(false);
                        secondLevelMap.SetActive(false);
                        transitionMap.SetActive(true);
                        bossLevelMap.SetActive(true);
                    }
                    break;
            }

            isSwitchOnReset = false;
        }


        transform.position = new Vector2(0.0f, boundary.max);
    }

}
