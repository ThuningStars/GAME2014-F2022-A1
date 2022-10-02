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
