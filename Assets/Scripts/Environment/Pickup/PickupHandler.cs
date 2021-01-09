using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Environment {
    public class PickupHandler : MonoBehaviour
    {
        [SerializeField] Pickup[] pickupPrefabs;
        [SerializeField] bool isRandomised = false;

        [SerializeField] Pickup specificPickup;
        [SerializeField] float probabilityOfSpawningPickup = 0.25f;

        public void TrySpawningPickup() {
            if (isRandomised) {
                SpawnWithRNG();
            } else {
                SpawnSpecific();
            }
        }

        private void SpawnSpecific()
        {
            if (!specificPickup) {
                Debug.LogError("No Pickup class specified.");
                return;
            }

            Instantiate<Pickup>(specificPickup, transform.position, Quaternion.identity);
        }

        private void SpawnWithRNG()
        {
            float rng = UnityEngine.Random.Range(0.0f, 1.0f);

            if (rng <= probabilityOfSpawningPickup) {
                int pickupIndex = UnityEngine.Random.Range(0, pickupPrefabs.Length);
                Instantiate<Pickup>(pickupPrefabs[pickupIndex], transform.position, Quaternion.identity);
            }
        }
    }
}
