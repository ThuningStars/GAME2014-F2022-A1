////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BulletFactory.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/23/2022 01:54 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add more sprites for enemy bullets
//Description : script for control all the bullet properties : direction, rotation, sprite
//              The code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{
    // Bullet Prefab
    private GameObject bulletPrefab;

    // Sprite Textures
    private Sprite playerBulletSprite;
    private Sprite enemyGreenBulletSprite;
    private Sprite enemyRedBulletSprite;
    private Sprite enemyBlueBulletSprite;

    // Bullet Parent
    private Transform bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprites/Characters/Player/Projectile/PlayerBullet");
        enemyRedBulletSprite = Resources.Load<Sprite>("Sprites/Characters/Enemy/Projectile/EnemyBulletRed");
        enemyGreenBulletSprite = Resources.Load<Sprite>("Sprites/Characters/Enemy/Projectile/EnemyBulletGreen");
        enemyBlueBulletSprite = Resources.Load<Sprite>("Sprites/Characters/Enemy/Projectile/EnemyBulletBlue");

        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("Bullets").transform;
    }

    // create bullets with static direction
    public GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletBehaviour>().bulletType = type;

        switch (type)
        {
            case BulletType.PLAYER:

                bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.UP, 6);
                bullet.name = "PlayerBullet";
                break;

            case BulletType.FIRSTENEMY:

                bullet.GetComponent<SpriteRenderer>().sprite = enemyRedBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.DOWN, 3);
                bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, - 135.0f);
                bullet.name = "FirstEnemyBullet";
                break;

            case BulletType.SECONDENEMY:

                bullet.GetComponent<SpriteRenderer>().sprite = enemyGreenBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.DOWN, 3);
                bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, - 135.0f);
                bullet.name = "SecondEnemyBullet";
                break;

        }

        bullet.SetActive(false);
        return bullet;
    }

    // create bullets with given direction
    public GameObject CreateBullet(Vector2 direction, BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletBehaviour>().bulletType = type;

        switch (type)
        {
            case BulletType.SECONDENEMY:

                bullet.GetComponent<SpriteRenderer>().sprite = enemyGreenBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(direction, 6);
                bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
                bullet.name = "SecondEnemyBullet";
                break;
        }

        bullet.SetActive(false);
        return bullet;
    }


}
