using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Player {
    public enum Powerup {
        SLOW_BALL
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
            activePowerups[Powerup.SLOW_BALL] = false;
        }

        public Dictionary<Powerup, bool> GetActivePowerups () { return activePowerups; }
    }
}
