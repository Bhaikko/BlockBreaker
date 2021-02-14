using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;

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

        // Cached Component References
        AudioSource audioSource;
        Rigidbody2D rigidBody2D;
        PowerupHandler powerupHandler;

        Paddle paddle;
        GameMode gameMode;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            powerupHandler = FindObjectOfType<PowerupHandler>();

            gameMode = FindObjectOfType<GameMode>();
            paddle = gameMode.GetPaddle();

            transform.position = paddle.transform.position + new Vector3(0.0f, 0.5f, 0.0f);

            paddleToBallVector = transform.position - paddle.transform.position;

            launchVelocity = new Vector2(xVelocity, yVelocity);
            launchSpeed = launchVelocity.magnitude;
            launchVelocity.Normalize();

            if (gameMode.hasStarted) {
                rigidBody2D.velocity = launchVelocity * launchSpeed;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!gameMode.hasStarted) {
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
                gameMode.hasStarted = true;
                
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

            if (gameMode.hasStarted) {
                AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
                audioSource.PlayOneShot(clip);

                rigidBody2D.velocity += velocityTweak;
            }
        }

        public void ChangeBallSize(float size = 1.0f)
        {
            transform.localScale = new Vector3(size, size, size);
        }

        public void IncreaseBallSpeed(float multiplier) {
            rigidBody2D.velocity *= multiplier;
        }

        private void CheckForPowerUps(Collision2D collision) {
            Dictionary<Powerup, bool> activePowerups = powerupHandler.GetActivePowerups();

            if (activePowerups[Powerup.MAGNET_BALL]) {
                launchSpeed = rigidBody2D.velocity.magnitude;

                paddleToBallVector = this.transform.position - paddle.transform.position;
                if (collision.contacts[0].normal == Vector2.up) {
                    launchVelocity = Vector2.Reflect(launchVelocity, collision.contacts[0].normal);
                    launchVelocity.y = Mathf.Abs(launchVelocity.y);
                } else {
                    launchVelocity = collision.contacts[0].normal;
                }

                gameMode.hasStarted = false;
            }
        }

    }
}
