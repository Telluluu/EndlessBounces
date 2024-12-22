using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gamelogic
{
    public class EventManager : Singleton<EventManager>
    {
        private void OnEnable()
        {
            FindObjectOfType<BallController>().onCoinCollected.AddListener(ResponToCoinCollected);
        }

        private void OnDisable()
        {
            FindObjectOfType<BallController>()?.onCoinCollected?.RemoveListener(ResponToCoinCollected);
        }

        private void ResponToCoinCollected()
        {
            Debug.Log("Coin Collected");
            GameManager.Instance.getCoinCount++;
        }
    }
}