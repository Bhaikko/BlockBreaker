using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Player;

namespace BlockBreaker.Environment {
    public class Pickup : MonoBehaviour
    {
        [SerializeField] Powerup powerupType = Powerup.MAGNET_BALL;

        [SerializeField] float dropSpeed = 1.0f;
        [SerializeField] float duration = 2.0f;

        void Update()
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();

            if (paddle) {
                paddle.ActivatePowerup(powerupType, duration);
                Destroy(gameObject);
            }
        }
    }
}
