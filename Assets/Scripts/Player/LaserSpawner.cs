using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlockBreaker.Player {
    public class LaserSpawner : MonoBehaviour
    {
        [SerializeField] Laser laserPrefab = null;
        [SerializeField] float rateOfFire = 0.5f;

        public void Start() {
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot() {
            while (true) {
                Instantiate<Laser>(laserPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(rateOfFire);
            }
        }
    }
}
