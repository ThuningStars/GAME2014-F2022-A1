////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: GameController.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 03:55 PM
//Last Modified On : 10/24/2022 05:23 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add more lists for different enemies => set up time line for spawn enemies
//                   => delete some enemies, save score to playerprefs, background transition, time line finsihed
//                   => add more third enemies for third level
//Description : script for controlling the spawn for enemy
// the initial code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private List<GameObject> firstEnemyList;
    private List<GameObject> secondEnemyList;
    private List<GameObject> thirdEnemyList;

    private GameObject firstEnemyPrefab;
    private GameObject secondEnemyPrefab;
    private GameObject thirdEnemyPrefab;

    private float totalPlayTime = 0.0f;
    private float timeLine = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        firstEnemyPrefab = Resources.Load<GameObject>("Prefabs/FirstEnemy");
        secondEnemyPrefab = Resources.Load<GameObject>("Prefabs/SecondEnemy");
        thirdEnemyPrefab = Resources.Load<GameObject>("Prefabs/ThirdEnemy");

        BuildEnemyList();

    }

    private void FixedUpdate()
    {
        //first level
        // 0 - 30s
        if (totalPlayTime == 0.0f)
        {
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 1, 3, 5, 4 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(4, 10.0f, indexOrder);
        }
        // 30s - 60s
        else if (totalPlayTime >= 35.0f && timeLine < 35.0f)
        {
            timeLine = 35.0f;

            GameObject enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 4, 5, 3 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(3, 10.0f, indexOrder);

        }
        else if (totalPlayTime >= 36.0f && timeLine < 36.0f)
        {
            timeLine = 36.0f;
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 3, 3, 1 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(3, 12.0f, indexOrder);

        }
        else if (totalPlayTime >= 40.0f && timeLine < 40.0f)
        {
            timeLine = 40.0f;
            GameObject enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 4, 5, 2 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(3, 10.0f, indexOrder);

        }
        else if (totalPlayTime >= 42.0f && timeLine < 42.0f)
        {
            timeLine = 42.0f;
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> {2, 6};
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(2, 12.0f, indexOrder);
        }
        //60s - 120s
        else if (totalPlayTime >= 60.0f && timeLine < 60.0f)
        {
            timeLine = 60.0f;
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 2, 6, 4, 3, 1, 5 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(6, 10.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> { 3, 2, 6, 5, 3, 2, 4, 2};
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(8, 8.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(3);
            enemy.SetActive(true);
            indexOrder = new List<int> { 2 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(1, 10.0f, indexOrder);

        }
        else if (totalPlayTime >= 64.0f && timeLine < 64.0f)
        {
            timeLine = 64.0f;
            GameObject enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> {4, 3, 1, 3, 5, 6, 2};
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(7, 8.0f, indexOrder);
        }
        else if (totalPlayTime >= 75.0f && timeLine < 75.0f)
        {
            // enemy pack of 2

            timeLine = 75.0f;
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 4, 3 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(2, 30.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> { 3, 4};
            enemy.GetComponent<EnemyBehaviour>().isBehindOthers = true;
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(2, 30.0f, indexOrder);
        }
        else if (totalPlayTime >= 90.0f && timeLine < 90.0f)
        {
            // enemy pack of 3

            timeLine = 90.0f;
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> {4};
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(1, 8.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> {3};
            enemy.GetComponent<EnemyBehaviour>().isBehindOthers = true;
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(1, 8.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> {5};
            enemy.GetComponent<EnemyBehaviour>().isBehindOthers = true;
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(1, 8.0f, indexOrder);
        }
        //second level
        // 120s - 300s
        else if (totalPlayTime >= 120.0f && timeLine < 120.0f)
        {
            totalPlayTime = 120;
            GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundManager>().SetGameLevel(LevelState.SECOND);

            // enemy pack of 3

            timeLine = 120.0f;
            GameObject enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,4,4,4 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(18, 10.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3 };
            enemy.GetComponent<EnemyBehaviour>().isBehindOthers = true;
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(18, 10.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> { 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 };
            enemy.GetComponent<EnemyBehaviour>().isBehindOthers = true;
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(18, 10.0f, indexOrder);


            enemy = GetUnActiveEnemyInList(3);

            enemy.SetActive(true);
            indexOrder = new List<int> { 2,6,1,0,6, 2, 6, 1, 0, 6, 2, 6, 1, 0, 6, 2, 6, 1 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(18, 10.0f, indexOrder);


            enemy = GetUnActiveEnemyInList(3);

            enemy.SetActive(true);
            indexOrder = new List<int> {2, 4, 3, 1, 4};
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(5, 12.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(1);

            enemy.SetActive(true);
            indexOrder = new List<int> { 1, 2, 6, 2, 0, 1, 2, 6, 2, 0, 1, 2, 6, 2, 0, 1, 2, 6 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(18, 10.0f, indexOrder);

            enemy = GetUnActiveEnemyInList(2);

            enemy.SetActive(true);
            indexOrder = new List<int> { 6, 2, 1, 6, 2, 1, 6, 2, 1 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(9, 20.0f, indexOrder);
        }
        else if (totalPlayTime >= 240 && timeLine < 240)
        {
            timeLine = 240;
            GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundManager>().SetGameLevel(LevelState.BOSS);

            GameObject enemy = GetUnActiveEnemyInList(3);

            enemy.SetActive(true);
            List<int>  indexOrder = new List<int> { 2, 3, 4 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(3, 15.0f, indexOrder);
        }
        else if (totalPlayTime >= 245 && timeLine < 245)
        {
            timeLine = 245;
            GameObject enemy = GetUnActiveEnemyInList(3);

            enemy.SetActive(true);
            List<int> indexOrder = new List<int> { 2, 3, 4, 0 };
            enemy.GetComponent<EnemyBehaviour>().SpawnEnemy(4, 15.0f, indexOrder);
        }
        else if (totalPlayTime >= 300 && timeLine < 300)
        {
            timeLine = 300;

            PlayerPrefs.SetInt("score", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().score);

            DestroyImmediate(gameObject);

            SceneManager.LoadScene(sceneBuildIndex: 3);
        }


        totalPlayTime += Time.deltaTime;
    }

    private GameObject GetUnActiveEnemyInList(int enemyCount)
    {

        if (enemyCount == 1)
        {

            for (int i = 0; i < firstEnemyList.Count; i++)
            {
                if (!firstEnemyList[i].activeSelf)
                {
                    return firstEnemyList[i];
                }
            }

            GameObject newEnemy = Instantiate(firstEnemyPrefab);
            newEnemy.SetActive(false);
            firstEnemyList.Add(newEnemy);
            newEnemy.name = "FirstEnemy";

            return newEnemy;

        }
        else if(enemyCount == 2)
        {
            for (int i = 0; i < secondEnemyList.Count; i++)
            {
                if (!secondEnemyList[i].activeSelf)
                {
                    return secondEnemyList[i];
                }
            }

            GameObject newEnemy = Instantiate(secondEnemyPrefab);
            newEnemy.SetActive(false);
            secondEnemyList.Add(newEnemy);
            newEnemy.name = "SecondEnemy";

            return newEnemy;

        }
        else if (enemyCount == 3)
        {
            for (int i = 0; i < thirdEnemyList.Count; i++)
            {
                if (!thirdEnemyList[i].activeSelf)
                {
                    return thirdEnemyList[i];
                }
            }

            GameObject newEnemy = Instantiate(thirdEnemyPrefab);
            newEnemy.SetActive(false);
            thirdEnemyList.Add(newEnemy);
            newEnemy.name = "ThirdEnemy";

            return newEnemy;

        }

        return null;
    }

    public void BuildEnemyList()
    {
        firstEnemyList = new List<GameObject>();
        secondEnemyList = new List<GameObject>();
        thirdEnemyList = new List<GameObject>();

        for (var i = 0; i < 5; i++)
        {
            var enemy = Instantiate(firstEnemyPrefab);
            enemy.SetActive(false);
            firstEnemyList.Add(enemy);
            enemy.name = "FirstEnemy";
        }

        for (var i = 0; i < 5; i++)
        {
            var enemy = Instantiate(secondEnemyPrefab);
            enemy.SetActive(false);
            secondEnemyList.Add(enemy);
            enemy.name = "SecondEnemy";
        }

        for (var i = 0; i < 5; i++)
        {
            var enemy = Instantiate(thirdEnemyPrefab);
            enemy.SetActive(false);
            thirdEnemyList.Add(enemy);
            enemy.name = "ThirdEnemy";

        }
    }
}
