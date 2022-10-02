////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: OnlyForDebugMode.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 02:21 AM
//Last Modified On : 10/02/2022 05:31 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Add Comments
//Description : Class just made for debug and can switch to endscene by the button click
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlyForDebugMode : MonoBehaviour
{
    public void EndSceneButtonPressed()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }

}
