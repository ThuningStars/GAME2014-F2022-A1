using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float verticalSpeed;
    public Boundary boundary;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        transform.position -= new Vector3(0.0f, verticalSpeed * Time.fixedDeltaTime, 0.0f);
    }

    public void CheckBounds()
    {
        if (transform.position.y < boundary.min)
        {
            ResetBackground();
        }
    }

    public void ResetBackground()
    {
        transform.position = new Vector2(0.0f, boundary.max);
    }
}
