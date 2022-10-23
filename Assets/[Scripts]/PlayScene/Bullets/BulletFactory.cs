////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BulletFactory.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/23/2022 02:23 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file
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
    private Sprite enemyBulletSprite;

    // Bullet Parent
    private Transform bulletParent;

    // Adapt portrait and landscape mode
    private bool isPortrait;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            isPortrait = false;

        }
        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown ||
                 Screen.orientation == ScreenOrientation.Portrait)
        {
            isPortrait = true;
        }
    }

    private void Update()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            isPortrait = false;

        }
        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown ||
                 Screen.orientation == ScreenOrientation.Portrait)
        {
            isPortrait = true;
        }
    }

    private void Initialize()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprites/Bullet");
        enemyBulletSprite = Resources.Load<Sprite>("Sprites/EnemySmallBullet");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletBehaviour>().bulletType = type;

        switch (type)
        {
            case BulletType.PLAYER:
                if(isPortrait)
                {
                    bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                    bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.UP);
                    bullet.name = "PlayerBullet";
                    break;
                }
                else
                {
                    bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                    bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.RIGHT);
                    bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                    bullet.name = "PlayerBullet";
                    break;
                }
            case BulletType.ENEMY:
                if (isPortrait)
                {
                    bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                    bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.DOWN);
                    bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                    bullet.name = "EnemyBullet";
                    break;
                }
                else
                {
                    bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                    bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.LEFT);
                    bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                    bullet.name = "EnemyBullet";
                    break;
                }

        }

        bullet.SetActive(false);
        return bullet;
    }

}
