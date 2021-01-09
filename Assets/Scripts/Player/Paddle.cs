using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Player {
    public enum Powerup {
        MAGNET_BALL,
        ONE_HIT_KILL
    }

    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        Dictionary<Powerup, bool> activePowerups = null;

        private void Start() {
            activePowerups = new Dictionary<Powerup, bool>();

            ResetPowerups();
        }

        private void ResetPowerups() {
            activePowerups[Powerup.MAGNET_BALL] = false;
            activePowerups[Powerup.ONE_HIT_KILL] = false;
        }

        public Dictionary<Powerup, bool> GetActivePowerups () { return activePowerups; }

        public void ActivatePowerup(Powerup powerup, float duration) {
            // Handle Stack of Powerups in future
            activePowerups[powerup] = true;

            StartCoroutine(DeactivatePowerup(powerup, duration));
        }

        private IEnumerator DeactivatePowerup(Powerup powerup, float duration) {

            yield return new WaitForSeconds(duration);
            activePowerups[powerup] = false;

            Debug.Log("Deactivating Powerup");
        }
    }

}
