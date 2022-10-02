using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Boundary verticalBoundary;

    private enum playerStates { MOVEIN, PATROL, MOVEAWAY, FINISHMOVING}
    private playerStates playerState = playerStates.MOVEIN;
    private float playerPatrolSpeed;

    public void Start()
    {
        playerPatrolSpeed = player.GetComponent<PlayerPatrolBehaviour>().patrolSpeed;
        player.GetComponent<PlayerPatrolBehaviour>().patrolSpeed = 0;
    }

    public void Update()
    {
        switch(playerState)
        {
            case playerStates.MOVEIN:
                player.transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);
                if (player.transform.position.y >= -4.2f)
                {
                    playerState = playerStates.PATROL;
                }
                break;

            case playerStates.PATROL:
                player.GetComponent<PlayerPatrolBehaviour>().patrolSpeed = playerPatrolSpeed;

                break;

            case playerStates.MOVEAWAY:
                player.transform.position += new Vector3(0.0f, 6.0f * Time.deltaTime, 0.0f);

                if (player.transform.position.y > verticalBoundary.max)
                {
                    playerState = playerStates.FINISHMOVING;
                }

                break;

            case playerStates.FINISHMOVING:
                SceneManager.LoadScene(sceneBuildIndex: 2);

                break;
        }

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void StartGame()
    {
        menu.SetActive(false);

        playerState = playerStates.MOVEAWAY;
        player.GetComponent<PlayerPatrolBehaviour>().patrolSpeed = 0;
    }

    public void GoToInstructionScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

}
