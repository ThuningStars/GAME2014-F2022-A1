////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BackgroundManager.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/22/2022 11:52 PM
//Last Modified On : 10/23/2022 02:23 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add function for tell background to switch level in update
//Description : Class for managing the level switch for different levels
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BackgroundManager : MonoBehaviour
{
    private static BackgroundManager _instance;

    public static BackgroundManager Instance { get { return _instance; } }

    private LevelState gameLevel = LevelState.FIRST;
    private LevelState currentLevel = LevelState.FIRST;

    private BackgroundBehaviour ground1;
    private BackgroundBehaviour ground2;
    private BackgroundBehaviour cloud1;
    private BackgroundBehaviour cloud2;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        ground1 = GameObject.Find("Ground").GetComponent<BackgroundBehaviour>();
        ground2 = GameObject.Find("Ground2").GetComponent<BackgroundBehaviour>();
        cloud1 = GameObject.Find("Clouds").GetComponent<BackgroundBehaviour>();
        cloud2 = GameObject.Find("Clouds2").GetComponent<BackgroundBehaviour>();
    }

    private void Update()
    {
        if (isChangeLevel())
        {
            ground1.isSwitchOnReset = true;
            ground2.isSwitchOnReset = true;
            currentLevel = gameLevel;
        }
    }

    public bool isChangeLevel()
    {
        if(currentLevel == gameLevel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public LevelState GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(LevelState newLevel)
    {
        currentLevel = newLevel;
    }

    public LevelState GetGameLevel()
    {
        return gameLevel;
    }

    public void SetGameLevel(LevelState newLevel)
    {
        gameLevel = newLevel;
    }
}
