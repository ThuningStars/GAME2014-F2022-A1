////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MenuManager.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/02/2022 11:09 AM
//Last Modified On : 10/02/2022 5:45 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => Moved PlayerState to PlayerPatrolBehaviour.cs
//                   => Clean Code and Add comments
//Description : Class for manage all the scenes that has menu or buttons and work with the button click.
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // if we need the player or menu do something
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject menu;

    // only used for easier to get Player state
    private PlayerPatrolBehaviour player;

    private void Start()
    {
        // if player gameobjecy has been assigned
        // because instruction, play scene don't need player to patrol
        if (playerObject != null)
            player = playerObject.GetComponent<PlayerPatrolBehaviour>();
    }

    public void Update()
    {
        // if player need to do sth and if player avator already outside the screen then we can push to play scene
        if (player != null)
            if (player.GetPlayerState() == PlayerPatrolBehaviour.playerStates.FINISHMOVING)
            {
                SceneManager.LoadScene(sceneBuildIndex: 2);
            }
    }

    // work with quit button
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    // work with start button and restart button
    public void GoToGamePlayScreen()
    {
        // just in case we want player to move out or hide the menu UI
        if(menu != null)
            menu.SetActive(false);

        if (player != null)
            player.SetPlayerState(PlayerPatrolBehaviour.playerStates.MOVEAWAY);
    }

    // work with instruction button
    public void GoToInstructionScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    // work with menu button and back button on instruction scene
    public void GoToMenuScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    // work with the debug temp button in play scene
    public void GoToGameOverScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }
}
