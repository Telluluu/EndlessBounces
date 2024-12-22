using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class Coin : MonoBehaviour
    {
        private void OnEnable()
        {
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ball"))
            {
                collider.GetComponent<BallController>().CollectCoin();
                Destroy(this.gameObject);
            }
        }
    }
}