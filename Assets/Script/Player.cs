using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bounds = 4.5f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 playerPosition = transform.position;
        float normalizedMoveSpeed = moveSpeed * Time.deltaTime * 60f;

        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * normalizedMoveSpeed, -bounds, bounds);
        transform.position = playerPosition;
    }
}
