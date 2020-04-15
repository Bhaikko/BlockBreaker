using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] Paddle paddle = null;
    [SerializeField] float xVelocity = 2.0f;
    [SerializeField] float yVelocity = 15.0f;

    // State Parameters
    Vector2 paddleToBallVector = new Vector2();
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = this.transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);

        this.transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            // Add velocity to rigidbody component of this component
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity); 
        }
    }
}
