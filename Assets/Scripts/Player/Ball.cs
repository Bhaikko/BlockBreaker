using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Player {

    public class Ball : MonoBehaviour
    {
        // Configuration Parameters
        [SerializeField] float xVelocity = 2.0f;
        [SerializeField] float yVelocity = 15.0f;
        [SerializeField] float randomFactor = 0.5f;

        [SerializeField] AudioClip[] ballSounds = null;

        // State Parameters
        Vector2 paddleToBallVector = new Vector2();
        Vector2 launchVelocity = new Vector2();
        float launchSpeed = 0.0f;
        bool hasStarted = false;

        // Cached Component References
        AudioSource audioSource;
        Rigidbody2D rigidBody2D;
        Paddle paddle;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            paddle = FindObjectOfType<Paddle>();

            paddleToBallVector = this.transform.position - paddle.transform.position;

            launchVelocity = new Vector2(xVelocity, yVelocity);
            launchSpeed = launchVelocity.magnitude;
            launchVelocity.Normalize();
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
                
                rigidBody2D.velocity = launchVelocity * launchSpeed; 
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Paddle>()) {
                CheckForPowerUps(collision);

                return;
            }

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

        public void ChangeBallSize(float size = 1.0f)
        {
            transform.localScale = new Vector3(size, size, size);
        }

        private void CheckForPowerUps(Collision2D collision) {
            Dictionary<Powerup, bool> activePowerups = paddle.GetActivePowerups();

            if (activePowerups[Powerup.MAGNET_BALL]) {
                launchSpeed = rigidBody2D.velocity.magnitude;

                paddleToBallVector = this.transform.position - paddle.transform.position;
                if (collision.contacts[0].normal == Vector2.up) {
                    launchVelocity = Vector2.Reflect(launchVelocity, collision.contacts[0].normal);
                    launchVelocity.y = Mathf.Abs(launchVelocity.y);
                } else {
                    launchVelocity = collision.contacts[0].normal;
                }

                hasStarted = false;
            }
        }
    }
}
