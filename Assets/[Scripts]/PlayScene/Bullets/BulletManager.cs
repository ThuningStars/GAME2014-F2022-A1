////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BulletManager.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/23/2022 09:02 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add all kinds of enemy bullets, and overload function for different
//                   bullet behaviour(static and non static direction) => delete some enemies
//Description : script for control the bullet instance, spawn(dequeue) and delete(enqueue)
//              The code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("player")]
    public PlayerBehaviour player;

    [Header("Bullet Properties")]
    [Range(10, 50)]
    public int playerBulletNumber = 50;
    public int playerBulletCount = 0;
    public int activePlayerBullets = 0;
    [Range(10, 50)]
    public int firstEnemyBulletNumber = 50;
    public int firstEnemyBulletCount = 0;
    public int activeFirstEnemyBullets = 0;
    [Range(10, 50)]
    public int secondEnemyBulletNumber = 50;
    public int secondEnemyBulletCount = 0;
    public int activeSecondEnemyBullets = 0;
    [Range(10, 50)]
    public int thirdEnemyBulletNumber = 50;
    public int thirdEnemyBulletCount = 0;
    public int activeThirdEnemyBullets = 0;
    [Range(10, 50)]
    public int thirdEnemyWaveBulletNumber = 50;
    public int thirdEnemyWaveBulletCount = 0;
    public int thirdForthEnemyWaveBullets = 0;

    private BulletFactory factory;
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> firstEnemyBulletPool;
    private Queue<GameObject> secondEnemyBulletPool;
    private Queue<GameObject> thirdEnemyBulletPool;
    private Queue<GameObject> thirdEnemyWaveBulletPool; 

    // Start is called before the first frame update
    void Start()
    {
        playerBulletPool = new Queue<GameObject>(); // creates an empty queue container
        firstEnemyBulletPool = new Queue<GameObject>(); // creates an empty queue container
        secondEnemyBulletPool = new Queue<GameObject>(); // creates an empty queue container
        thirdEnemyBulletPool = new Queue<GameObject>(); // creates an empty queue container
        thirdEnemyBulletPool = new Queue<GameObject>(); // creates an empty queue container
        thirdEnemyWaveBulletPool = new Queue<GameObject>(); // creates an empty queue container

        factory = GameObject.FindObjectOfType<BulletFactory>();
        BuildBulletPools();
    }

    void BuildBulletPools()
    {
        for (int i = 0; i < playerBulletNumber; i++)
        {
            playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
        }

        for (int i = 0; i < firstEnemyBulletNumber; i++)
        {
            firstEnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.FIRSTENEMY));
        }

        for (int i = 0; i < secondEnemyBulletNumber; i++)
        {
            secondEnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.SECONDENEMY));
        }

        for (int i = 0; i < thirdEnemyBulletNumber; i++)
        {
            thirdEnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.THIRDENEMY));
        }

        for (int i = 0; i < thirdEnemyWaveBulletNumber; i++)
        {
            thirdEnemyWaveBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMYWAVE));
        }

        // stats
        playerBulletCount = playerBulletPool.Count;
        firstEnemyBulletCount = firstEnemyBulletPool.Count;
        secondEnemyBulletCount = secondEnemyBulletPool.Count;

        thirdEnemyBulletCount = thirdEnemyBulletPool.Count;
        thirdEnemyWaveBulletCount = thirdEnemyWaveBulletPool.Count;

    }

    //Get regular bullet that direction is static
    public GameObject GetBullet(Vector2 position, BulletType type)
    {
        GameObject bullet = null;

        switch (type)
        {
            case BulletType.PLAYER:
                {
                    if (playerBulletPool.Count < 1)
                    {
                        playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
                    }
                    bullet = playerBulletPool.Dequeue();
                    bullet.GetComponent<BulletBehaviour>().damageValue = player.damage;
                    // stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets++;
                }
                break;
            case BulletType.FIRSTENEMY:
                {
                    if (firstEnemyBulletPool.Count < 1)
                    {
                        firstEnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.FIRSTENEMY));
                    }
                    bullet = firstEnemyBulletPool.Dequeue();
                    bullet.GetComponent<BulletBehaviour>().damageValue = 5;
                    // stats
                    firstEnemyBulletCount = firstEnemyBulletPool.Count;
                    activeFirstEnemyBullets++;
                }
                break;

            case BulletType.SECONDENEMY:
                {
                    if (secondEnemyBulletPool.Count < 1)
                    {
                        secondEnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.SECONDENEMY));
                    }
                    bullet = secondEnemyBulletPool.Dequeue();
                    bullet.GetComponent<BulletBehaviour>().damageValue = 10;

                    // stats
                    secondEnemyBulletCount = secondEnemyBulletPool.Count;
                    activeSecondEnemyBullets++;
                }
                break;

            case BulletType.THIRDENEMY:
                {
                    if (secondEnemyBulletPool.Count < 1)
                    {
                        secondEnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.THIRDENEMY));
                    }
                    bullet = secondEnemyBulletPool.Dequeue();
                    bullet.GetComponent<BulletBehaviour>().damageValue = 15;

                    // stats
                    secondEnemyBulletCount = secondEnemyBulletPool.Count;
                    activeSecondEnemyBullets++;
                }
                break;
        }

        if(bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = position;
        }
        return bullet;
    }

    //Get bullet with a given direction
    public GameObject GetBullet(Vector2 bulletPosition, Vector2 characterPos, BulletType type)
    {
        GameObject bullet = null;

        switch (type)
        {
            case BulletType.ENEMYWAVE:
                {
                    if (secondEnemyBulletPool.Count < 1)
                    {
                        secondEnemyBulletPool.Enqueue(factory.CreateBullet(bulletPosition - characterPos, BulletType.ENEMYWAVE));
                    }
                    bullet = secondEnemyBulletPool.Dequeue();
                    bullet.GetComponent<BulletBehaviour>().damageValue = 20;

                    //set direction
                    bullet.GetComponent<BulletBehaviour>().SetDirection(bulletPosition - characterPos, 6);
                    // stats
                    secondEnemyBulletCount = secondEnemyBulletPool.Count;
                    activeSecondEnemyBullets++;
                }
                break;
        }

        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = bulletPosition;
        }
        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);

        switch (type)
        {
            case BulletType.PLAYER:
                playerBulletPool.Enqueue(bullet);
                //stats
                playerBulletCount = playerBulletPool.Count;
                activePlayerBullets--;
                break;
            case BulletType.FIRSTENEMY:
                firstEnemyBulletPool.Enqueue(bullet);
                //stats
                firstEnemyBulletCount = firstEnemyBulletPool.Count;
                activeFirstEnemyBullets--;
                break;
            case BulletType.SECONDENEMY:
                secondEnemyBulletPool.Enqueue(bullet);
                //stats
                secondEnemyBulletCount = secondEnemyBulletPool.Count;
                activeSecondEnemyBullets--;
                break;
            case BulletType.THIRDENEMY:
                thirdEnemyBulletPool.Enqueue(bullet);
                //stats
                thirdEnemyBulletCount = thirdEnemyBulletPool.Count;
                activeThirdEnemyBullets--;
                break;
            case BulletType.ENEMYWAVE:
                thirdEnemyWaveBulletPool.Enqueue(bullet);
                //stats
                thirdEnemyWaveBulletCount = thirdEnemyWaveBulletPool.Count;
                thirdForthEnemyWaveBullets--;
                break;
        }
    }
}
