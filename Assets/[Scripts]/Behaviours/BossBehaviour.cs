////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BossBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 08:38 PM
//Last Modified On : 10/23/2022 08:38 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file
//Description : script for control the boss behaviour : spawn, attack
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public enum BossState
{
    REST,
    MOVEIN, 
    MOVEOUT, 
    PATROL,  
}


public class BossBehaviour : MonoBehaviour
{
    [Header("Boss Properties")]
    public Boundary pingPongMoveBoundary;
    public Boundary screenBounds;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float patrolYAxis;

    public BossState state;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;
    public BulletType bulletType;

    private BulletManager bulletManager;
    private bool isPatrolRight = true;

    // spawn points for enemy to avoid enemy that hide behind another enemy situation
    private List<float> enemySpawnXAxisList = new List<float>();

    private Vector2 screenWorldSize;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

        AdaptOrientations();
        //InvokeRepeating("FireBullets", 0.3f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        switch (state)
        {
            case BossState.REST:
                break;

            case BossState.MOVEIN:
                transform.position += new Vector3(0.0f, - 2.0f * Time.deltaTime, 0.0f);

                if (transform.position.y <= patrolYAxis)
                {
                    state = BossState.PATROL;
                }
                break;

            case BossState.MOVEOUT:
                transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);

                if (transform.position.y >= screenBounds.max)
                {
                    state = BossState.REST;
                }
                break;

            case BossState.PATROL:
                if (isPatrolRight)
                {
                    if (transform.position.x >= pingPongMoveBoundary.max)
                    {
                        isPatrolRight = false;
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x + horizontalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (transform.position.x <= pingPongMoveBoundary.min)
                    {
                        isPatrolRight = true;
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    }

                }

                break;
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

        screenBounds.min = -screenWorldSize.y - 4.0f;
        screenBounds.max = screenWorldSize.y + 4.0f;
        pingPongMoveBoundary.min = -screenWorldSize.x + 1.1f;
        pingPongMoveBoundary.max = screenWorldSize.x - 1.1f;

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


    }

}
