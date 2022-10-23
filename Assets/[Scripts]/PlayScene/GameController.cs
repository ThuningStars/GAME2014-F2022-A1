////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: GameController.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 03:55 PM
//Last Modified On : 10/23/2022 06:20 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add more lists for different enemies
//Description : script for controlling the spawn for enemy
// the initial code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<GameObject> firstEnemyList;
    private List<GameObject> secondEnemyList;

    private GameObject firstEnemyPrefab;
    private GameObject secondEnemyPrefab;

    private float totalPlayTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        firstEnemyPrefab = Resources.Load<GameObject>("Prefabs/FirstEnemy");
        secondEnemyPrefab = Resources.Load<GameObject>("Prefabs/SecondEnemy");
        BuildEnemyList();
    }

    private void FixedUpdate()
    {

        if (totalPlayTime == 0.0f)
        {
            firstEnemyList[0].SetActive(true);
            List<int> indexOrder = new List<int> { 1, 3, 5, 4 };
            firstEnemyList[0].GetComponent<EnemyBehaviour>().SpawnEnemy(4, 10.0f, indexOrder);
        }

        totalPlayTime += Time.deltaTime;
    }

    public void BuildEnemyList()
    {
        firstEnemyList = new List<GameObject>();
        secondEnemyList = new List<GameObject>();

        for (var i = 0; i < 2; i++)
        {
            var enemy = Instantiate(firstEnemyPrefab);
            enemy.SetActive(false);
            firstEnemyList.Add(enemy);
            enemy.name = "FirstEnemy";
        }

        for (var i = 0; i < 2; i++)
        {
            var enemy = Instantiate(secondEnemyPrefab);
            enemy.SetActive(false);
            secondEnemyList.Add(enemy);
        }
    }
}
