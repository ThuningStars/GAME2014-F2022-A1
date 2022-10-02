using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyBGBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    public Boundary screenBounds;
    public float verticalSpeed;
    public float verticalSpeedMax;
    public float verticalSpeedMin;

    // Start is called before the first frame update
    void Start()
    {
        var RandomXPosition = Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var RandomYPosition = Random.Range(verticalBoundary.min, verticalBoundary.max);
        verticalSpeed = Random.Range(verticalSpeedMin, verticalSpeedMax);
        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
    }

    public void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {
            ResetEnemy();
        }
    }

    public void ResetEnemy()
    {
        var RandomXPosition = Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var RandomYPosition = Random.Range(verticalBoundary.min, verticalBoundary.max);
        verticalSpeed = Random.Range(verticalSpeedMin, verticalSpeedMax);
        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0.0f);

    }
}
