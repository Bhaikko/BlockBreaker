using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Player {
    public enum Powerup {
        MAGNET_BALL,
        ONE_HIT_KILL,
        INCREASE_BALL_SIZE
    }

    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        Dictionary<Powerup, bool> activePowerups = null;

        Ball ball = null;

        private void Start() {
            activePowerups = new Dictionary<Powerup, bool>();

            ball = FindObjectOfType<Ball>();

            ResetPowerups();
        }

        private void ResetPowerups() {
            activePowerups[Powerup.MAGNET_BALL] = false;
            activePowerups[Powerup.ONE_HIT_KILL] = false;
            activePowerups[Powerup.INCREASE_BALL_SIZE] = false;
        }

        public Dictionary<Powerup, bool> GetActivePowerups () { return activePowerups; }

        private void ActivateSpecificPowerup(Powerup powerup) {
            switch (powerup) {
                case Powerup.INCREASE_BALL_SIZE:
                    ball.ChangeBallSize(2.0f);
                    break;

                default:
                    break;

            }
        }

        private void DeactivateSpecificPowerup(Powerup powerup) {
            switch (powerup) {
                case Powerup.INCREASE_BALL_SIZE:
                    ball.ChangeBallSize();
                    break;

                default:
                    break;

            }
        }

        public void ActivatePowerup(Powerup powerup, float duration) {
            // Handle Stack of Powerups in future
            activePowerups[powerup] = true;

            ActivateSpecificPowerup(powerup);

            StartCoroutine(DeactivatePowerup(powerup, duration));
        }

        private IEnumerator DeactivatePowerup(Powerup powerup, float duration) {

            yield return new WaitForSeconds(duration);
            activePowerups[powerup] = false;

            DeactivateSpecificPowerup(powerup);

            Debug.Log("Deactivating Powerup");
        
        }
    }

}
