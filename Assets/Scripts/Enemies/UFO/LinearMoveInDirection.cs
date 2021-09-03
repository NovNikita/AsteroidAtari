using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMoveInDirection : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;

    void Update()
    {
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
