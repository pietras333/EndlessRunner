using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    ScoreManager scoreManager;

    [SerializeField]
    private int sideIndex = 0;

    [SerializeField]
    private float initialSpeed = 2f;

    [SerializeField]
    private float acceleration = 0.1f;

    [SerializeField]
    private float maxSpeed = 20f;

    private float currentSpeed;

    bool gameOver = false;

    private void Start()
    {
        currentSpeed = initialSpeed;
    }

    public void Update()
    {
        if (gameOver)
        {
            return;
        }

        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        float xZero = -1.25f; // Center lane
        float xMinusOne = -3.75f; // Left lane
        float xOne = 1.5f; // Right lane

        // Get target X position based on sideIndex
        float targetX = sideIndex switch
        {
            -1 => xMinusOne,
            0 => xZero,
            1 => xOne,
            _ => xZero
        };

        // Smoothly interpolate to target position
        float x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * 10f);

        transform.position = new Vector3(
            x,
            transform.position.y,
            transform.position.z + currentSpeed * Time.deltaTime
        );
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Game Over");
        gameOver = true;
        scoreManager.OnGameOver();
    }

    public void OnLeftInput(bool isPressed)
    {
        sideIndex = isPressed ? sideIndex - 1 : sideIndex;
        sideIndex = Mathf.Clamp(sideIndex, -1, 1);
    }

    public void OnRightInput(bool isPressed)
    {
        sideIndex = isPressed ? sideIndex + 1 : sideIndex;
        sideIndex = Mathf.Clamp(sideIndex, -1, 1);
    }
}
