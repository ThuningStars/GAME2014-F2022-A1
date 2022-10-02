using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Boundary verticalBoundary;

    private bool isStartButtonClick = false;
    private bool isPlayerFinishMoving = false;

    public void Update()
    {
        if (isStartButtonClick == true)
        {
            player.transform.position += new Vector3(0.0f, 6.0f * Time.deltaTime, 0.0f);

            if (player.transform.position.y > verticalBoundary.max)
            {
                isPlayerFinishMoving = true;
            }
        }

        if(isPlayerFinishMoving && isStartButtonClick)
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

    public void StartGame()
    {
        menu.SetActive(false);

        isStartButtonClick = true;
        player.GetComponent<PlayerPatrolBehaviour>().patrolSpeed = 0;
    }

    public void GoToInstructionScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

}
