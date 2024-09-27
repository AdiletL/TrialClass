using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 700;

    private void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }

        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);

        if (direction.magnitude > 0)
        {
            RotateToDirection(direction);
            MoveToDirection(direction);
            playerAnimation.UpdateRun(movementSpeed / 2);
        }
        else
        {
            playerAnimation.UpdateIdle();
        }

        playerJump.UpdateJump();
    }

    private void RotateToDirection(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveToDirection(Vector3 direction)
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }
}
