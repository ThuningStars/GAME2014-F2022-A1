using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static PlayerPatrolBehaviour;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject menu;

    private PlayerPatrolBehaviour player;

    private void Start()
    {
        if(playerObject != null)
            player = playerObject.GetComponent<PlayerPatrolBehaviour>();
    }

    public void Update()
    {
        if (player != null)
            if (player.GetPlayerState() == PlayerPatrolBehaviour.playerStates.FINISHMOVING)
            {
                SceneManager.LoadScene(sceneBuildIndex: 2);
            }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GoToGamePlayScreen()
    {

        if(menu != null)
            menu.SetActive(false);

        if (player != null)
            player.SetPlayerState(PlayerPatrolBehaviour.playerStates.MOVEAWAY);
    }

    public void GoToInstructionScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void GoToMenuScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void GoToGameOverScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }
}
