using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Player {

    public class Ball : MonoBehaviour
    {
        // Configuration Parameters
        [SerializeField] Paddle paddle = null;
        [SerializeField] float xVelocity = 2.0f;
        [SerializeField] float yVelocity = 15.0f;
        [SerializeField] float randomFactor = 0.5f;

        [SerializeField] AudioClip[] ballSounds = null;

        // State Parameters
        Vector2 paddleToBallVector = new Vector2();
        bool hasStarted = false;

        // Cached Component References
        AudioSource audioSource;
        Rigidbody2D rigidBody2D;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            paddleToBallVector = this.transform.position - paddle.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasStarted) {
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
            if (Input.GetMouseButtonDown(0)) {
                hasStarted = true;

                // Add velocity to rigidbody component of this component
                rigidBody2D.velocity = new Vector2(xVelocity, yVelocity); 
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 velocityTweak = new Vector2(
                Random.Range(0, randomFactor), 
                Random.Range(0, randomFactor)    
            );

            if (hasStarted) {
                AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
                audioSource.PlayOneShot(clip);

                rigidBody2D.velocity += velocityTweak;
            
            }
        }
    }
}
