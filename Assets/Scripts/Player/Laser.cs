using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Environment;

namespace BlockBreaker.Player {
    public class Laser : MonoBehaviour
    {
        [SerializeField] float MoveSpeed = 10.0f;

        private void MoveUpwards() {
            transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime, Space.World);
        }

        void Update()
        {
            MoveUpwards();       
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Block>()) {
                Destroy(gameObject);
            }
        }
    }
}
