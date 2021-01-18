using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;
using BlockBreaker.Player;

namespace BlockBreaker.Environment {
    public class Pickup : MonoBehaviour
    {
        [SerializeField] Powerup powerupType = Powerup.MAGNET_BALL;

        PowerupHandler powerupHandler;

        [SerializeField] float dropSpeed = 1.0f;
        [SerializeField] float duration = 2.0f;

        void Start() {
            powerupHandler = FindObjectOfType<PowerupHandler>();
        }

        void Update()
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();

            if (paddle) {
                powerupHandler.ActivatePowerup(powerupType, duration);
                Destroy(gameObject);
            }
        }
        
    }
}
