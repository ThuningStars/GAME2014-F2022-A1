using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class InstructionSceneManager : MonoBehaviour
{
    public void BackButtonPress()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
