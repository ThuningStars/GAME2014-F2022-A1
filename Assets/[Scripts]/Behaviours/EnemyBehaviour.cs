////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemyBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:12 PM
//Last Modified On : 10/24/2022 04:55 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => build corotine for spawn enemies, and list to store 7 x axis for spawning
//                   => add blood and damage properties => add ray collision check
//Description : script for control the enemies behaviour : spawn, respawn
//              The initial code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyBehaviour : MonoBehaviour
{
    public Boundary pingPongMoveBoundary;
    public Boundary screenBounds;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float healthValue;

    private float initialHealth;

    //enemy reset property
    [Header("Enemy Reset Properties")]
    public int timeOfResetInTotal;
    public float spawnInSeconds;
    public List<int> spawnXAxisListIndexOrder = new List<int>();
    public bool isBehindOthers = false;
    private int timeOfReset = 0;
    private bool isStartSpawn = false;
    private float spawnTimer = 0.0f;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public Transform waveSpawnPoint;
    public Transform waveSpawnPoint2;

    public float fireRate = 0.2f;
    public BulletType bulletType;

    private BulletManager bulletManager;
    private bool isMoving = true;

    [Header("SFX Properties")]
    public AudioSource audioSource;
    public AudioSource localAudioSource;

    public AudioClip fire;
    public AudioClip explode;

    // spawn points for enemy to avoid enemy that hide behind another enemy situation
    private List<float> enemySpawnXAxisList = new List<float>();

    private Vector2 screenWorldSize;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = healthValue;
        bulletManager = FindObjectOfType<BulletManager>();
        localAudioSource = GetComponent<AudioSource>();

        AdaptOrientations();
        InvokeRepeating("FireBullets", 0.3f, fireRate);

        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
            CheckBounds();

            if (healthValue <= 0)
            {
                localAudioSource.PlayOneShot(explode);
                isMoving = false;
                transform.position = new Vector3(0.0f, screenBounds.max + 5.0f, 0.0f);

            }
        }

        if (isStartSpawn)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInSeconds)
            {
                healthValue = initialHealth;
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
        else
        {
            var horizontalLength = pingPongMoveBoundary.max - pingPongMoveBoundary.min;
            transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLength) - pingPongMoveBoundary.max,
                transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
        }
    }

    public void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {

            isMoving = false;
        }
    }

    void FireBullets()
    {
        if(healthValue > 0 && gameObject.activeSelf && isMoving)
        {
            if (bulletType == BulletType.FIRSTENEMY || bulletType == BulletType.SECONDENEMY)
            {
                audioSource.PlayOneShot(fire);

                var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, bulletType);
            }
            else if (bulletType == BulletType.ENEMYWAVE)
            {
                audioSource.PlayOneShot(fire);

                var bullet = bulletManager.GetBullet(waveSpawnPoint.position, transform.position, bulletType);
                var bullet2 = bulletManager.GetBullet(waveSpawnPoint2.position, transform.position, bulletType);

            }
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

        int index = spawnXAxisListIndexOrder[timeOfReset];

        if(isBehindOthers)
        {
            transform.position = new Vector3(enemySpawnXAxisList[index], screenBounds.max + 1.0f, 0.0f);
        }
        else
        {
            transform.position = new Vector3(enemySpawnXAxisList[index], screenBounds.max, 0.0f);
        }

        timeOfReset++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ray"))
        {
            healthValue -= 100.0f * Time.deltaTime;

            if (healthValue <= 0.0f)
            {
                int score = 0;
                switch (bulletType)
                {
                    case BulletType.FIRSTENEMY:
                        score = 50;
                        break;
                    case BulletType.SECONDENEMY:
                        score = 75;
                        break;
                    case BulletType.ENEMYWAVE:
                        score = 90;
                        break;

                }

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().score += score;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ray"))
        {
            healthValue -= 100.0f * Time.deltaTime;

            if(healthValue <= 0.0f)
            {
                int score = 0;
                switch (bulletType)
                {
                    case BulletType.FIRSTENEMY:
                        score = 50;
                        break;
                    case BulletType.SECONDENEMY:
                        score = 75;
                        break;
                    case BulletType.ENEMYWAVE:
                        score = 90;
                        break;

                }

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().score += score;
            }
        }
    }

}
