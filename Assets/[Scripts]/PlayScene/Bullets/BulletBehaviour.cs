////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BulletBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/23/2022 01:48 PM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add overload function for different direction setting(static & none-static)
//Description : script for all the bullet behaviours : move, respawn, behaviour, collision check
//              The main code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}


public class BulletBehaviour: MonoBehaviour
{
    [Header("Bullet Properties")]
    public BulletDirection bulletDirection;
    public ScreenBounds bounds;
    public BulletType bulletType;
    public int damageValue = 1;

    private Vector3 velocity;
    private BulletManager bulletManager;

    private Vector2 screenWorldSize;

    void Start()
    {
        AdaptScreenSize();

        bulletManager = FindObjectOfType<BulletManager>();
    }

    void Update()
    {
        Move();
        CheckBounds();
        
    }

    private void AdaptScreenSize()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        bounds.horizontal.max = screenWorldSize.x + 1.0f;
        bounds.horizontal.min = -screenWorldSize.x - 1.0f;
        bounds.vertical.max = screenWorldSize.y + 1.0f;
        bounds.vertical.min = -screenWorldSize.y - 1.0f;
    }

    void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }
    
    // static direction and speed
    public void SetDirection(BulletDirection direction, float speed)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up * speed;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right * speed;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down * speed;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left * speed;
                break;
        }
    }

    // with given direction and set speed
    public void SetDirection(Vector2 direction, float speed)
    {
        var directionNormal = direction.normalized;
        velocity = directionNormal * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (bulletType == BulletType.PLAYER)
        {


            other.gameObject.GetComponent<EnemyBehaviour>().healthValue -= damageValue;
            bulletManager.ReturnBullet(this.gameObject, bulletType);


        }

    }

}