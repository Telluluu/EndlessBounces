using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ball"))
            {
                GameManager.Instance.OnGetCoin();
                Destroy(this.gameObject);
            }
        }
    }
}