using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Player;
using BlockBreaker.Environment;

namespace BlockBreaker.Core {
    public enum Powerup {
        MAGNET_BALL,
        ONE_HIT_KILL,
        INCREASE_BALL_SIZE,
        ADD_THREE_BALLS,
        WIDEN_PADDLE,
        NARROW_PADDLE,
        PENETRATING_BALL,
        FAST_BALL,
        DECREASE_BALL_SIZE,
        SHIELD
    }

    public class PowerupHandler : MonoBehaviour
    {
        [SerializeField]
        Dictionary<Powerup, bool> activePowerups = null;

        Ball ball = null;
        GameMode gameMode = null;

        private void Start() {
            activePowerups = new Dictionary<Powerup, bool>();

            gameMode = FindObjectOfType<GameMode>();
            ball = FindObjectOfType<Ball>();

            ResetPowerups();
        }

        private void ResetPowerups() {
            activePowerups[Powerup.MAGNET_BALL] = false;
            activePowerups[Powerup.ONE_HIT_KILL] = false;
            activePowerups[Powerup.INCREASE_BALL_SIZE] = false;
            activePowerups[Powerup.ADD_THREE_BALLS] = false;
            activePowerups[Powerup.WIDEN_PADDLE] = false;
            activePowerups[Powerup.NARROW_PADDLE] = false;
            activePowerups[Powerup.PENETRATING_BALL] = false;
            activePowerups[Powerup.FAST_BALL] = false;
            activePowerups[Powerup.DECREASE_BALL_SIZE] = false;
            activePowerups[Powerup.SHIELD] = false;
        }

        public Dictionary<Powerup, bool> GetActivePowerups () { return activePowerups; }

        private void ActivateSpecificPowerup(Powerup powerup) {
            switch (powerup) {
                case Powerup.INCREASE_BALL_SIZE:
                    for (int i = 0; i < gameMode.GetBalls().Count; i++) {
                        gameMode.GetBalls()[i].ChangeBallSize(2.0f);
                    }
                    break;

                case Powerup.ADD_THREE_BALLS:
                    for (int i = 1; i <= 2; i++) {
                        gameMode.SpawnBall();
                    }

                    break;

                case Powerup.WIDEN_PADDLE:
                    FindObjectOfType<Paddle>().ChangeSize(2.0f);
                    break;

                case Powerup.NARROW_PADDLE:
                    FindObjectOfType<Paddle>().ChangeSize(0.5f);
                    break;

                case Powerup.PENETRATING_BALL:
                    foreach (Block block in  FindObjectsOfType<Block>()) {
                        block.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    break;

                case Powerup.FAST_BALL:
                    FindObjectOfType<Ball>().IncreaseBallSpeed(1.25f);
                    break;

                case Powerup.DECREASE_BALL_SIZE:
                    for (int i = 0; i < gameMode.GetBalls().Count; i++) {
                        gameMode.GetBalls()[i].ChangeBallSize(0.5f);
                    }
                    break;

                case Powerup.SHIELD:
                    FindObjectOfType<LoseCollider>().ActivateShield();
                    break;

                default:
                    break;

            }
        }

        private void DeactivateSpecificPowerup(Powerup powerup) {
            switch (powerup) {
                case Powerup.INCREASE_BALL_SIZE:
                    for (int i = 0; i < gameMode.GetBalls().Count; i++) {
                        gameMode.GetBalls()[i].ChangeBallSize();
                    }
                    break;

                case Powerup.WIDEN_PADDLE:
                    FindObjectOfType<Paddle>().ChangeSize();
                    break;

                case Powerup.NARROW_PADDLE:
                    FindObjectOfType<Paddle>().ChangeSize();
                    break;

                case Powerup.PENETRATING_BALL:
                    foreach (Block block in  FindObjectsOfType<Block>()) {
                        block.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    break;

                case Powerup.DECREASE_BALL_SIZE:
                    for (int i = 0; i < gameMode.GetBalls().Count; i++) {
                        gameMode.GetBalls()[i].ChangeBallSize();
                    }
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
