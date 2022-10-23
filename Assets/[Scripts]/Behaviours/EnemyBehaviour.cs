////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemyBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:12 PM
//Last Modified On : 10/23/2022 06:12 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => build corotine for spawn enemies, and list to store 7 x axis for spawning
//Description : script for control the enemies behaviour : spawn, color, pingpong movement
//              The initial code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    public Boundary pingPongMoveBoundary;
    public Boundary screenBounds;
    public float horizontalSpeed;
    public float verticalSpeed;

    //enemy reset property
    public int timeOfResetInTotal;
    public float spawnInSeconds;
    public List<int> spawnXAxisListIndexOrder = new List<int>();
    private int timeOfReset = 0;
    public bool isStartSpawn = false;
    private float spawnTimer = 0.0f;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;
    public BulletType bulletType;

    private BulletManager bulletManager;
    private bool isMoving = true;

    // spawn points for enemy to avoid enemy that hide behind another enemy situation
    private List<float> enemySpawnXAxisList = new List<float>();

    private Vector2 screenWorldSize;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

        AdaptOrientations();
        InvokeRepeating("FireBullets", 0.3f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
            CheckBounds();
        }

    }

    private void FixedUpdate()
    {
        if (isStartSpawn)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInSeconds)
            {
                spawnTimer = 0.0f;
                isMoving = true;
                StartSpawnEnemy();
            }
        }
    }

    public void Move()
    {
        if (bulletType == BulletType.FIRSTENEMY || bulletType == BulletType.SECONDENEMY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
        }
        //var horizontalLength = pingPongMoveBoundary.max - pingPongMoveBoundary.min;
        //transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLength) - pingPongMoveBoundary.max,
        //    transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
    }

    public void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {
            //ResetEnemy();

            isMoving = false;
        }
    }

    void FireBullets()
    {
        if (bulletType == BulletType.FIRSTENEMY || bulletType == BulletType.SECONDENEMY)
        {
            var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, bulletType);
        }
    }

    private void AdaptOrientations()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

        screenBounds.min = -screenWorldSize.y - 3.5f;
        screenBounds.max = screenWorldSize.y + 3.5f;
        pingPongMoveBoundary.min = -screenWorldSize.x + 0.4f;
        pingPongMoveBoundary.max = screenWorldSize.x - 0.4f;

        CalculateSpawnXAxisList();
    }

    private void CalculateSpawnXAxisList()
    {
        enemySpawnXAxisList = new List<float>();
        // we want to have 7 points in total(not include the edge of screen)
        // so it would be divided in 8 pieces 
        // so the distance each point should be screenwidth / 8
        float spawnXPointDistance = ((float)screenWorldSize.x * 2) / 8.0f;
        float spawnXPoint = -screenWorldSize.x + spawnXPointDistance;
        for (int i = 0; i < 7; i++)
        {
            enemySpawnXAxisList.Add(spawnXPoint);
            spawnXPoint += spawnXPointDistance;
        }
    }

    public void SpawnEnemy(int resetTimeInTotal, float spawnTimeInSeconds, List<int> spawnPosIndexOrder)
    {

        timeOfResetInTotal = resetTimeInTotal;
        spawnInSeconds = spawnTimeInSeconds;

        foreach (int index in spawnPosIndexOrder)
        {
            spawnXAxisListIndexOrder.Add(index);
        }

        isStartSpawn = true;
        StartSpawnEnemy();
    }

    private void StartSpawnEnemy()
    {
        if (timeOfReset >= timeOfResetInTotal)
        {
            gameObject.SetActive(false);
            timeOfReset = 0;
            isStartSpawn = false;
        }
        AdaptOrientations();

        Debug.Log(timeOfReset);
        Debug.Log(enemySpawnXAxisList.Count);
        Debug.Log(spawnXAxisListIndexOrder.Count);

        int index = spawnXAxisListIndexOrder[timeOfReset];

        transform.position = new Vector3(enemySpawnXAxisList[index], screenBounds.max, 0.0f);

        timeOfReset++;
    }
}
