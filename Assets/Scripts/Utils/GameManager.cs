using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class GameManager : Singleton<GameManager>
    {
        public int getCoinCount = 0;
        public int maxCoinCount = 3;

        public int catapultScore = 0;

        public float comboMagnification = 1.0f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            if (EventManager.Instance.onCoinCollected == null)
            {
                Debug.Log("EventManager.onCoinCollected is null");
            }
            EventManager.Instance.onCoinCollected.AddListener(ResponToCoinCollected);
            EventManager.Instance.onScoreChanged.AddListener(ResponToScoreChanged);
        }

        private void OnDisable()
        {
            EventManager.Instance?.onCoinCollected.RemoveListener(ResponToCoinCollected);
            EventManager.Instance?.onScoreChanged.RemoveListener(ResponToScoreChanged);
        }

        private void ResponToCoinCollected()
        {
            Debug.Log("Coin Collected");
            this.getCoinCount++;
        }

        private void ResponToScoreChanged(int scoreDelta)
        {
            this.catapultScore += scoreDelta;
        }

        public void ComboMagnificate()
        {
            comboMagnification += 0.1f;
            comboMagnification = Mathf.Round(comboMagnification * 10) / 10f;
            EventManager.Instance.onTextPoped.Invoke("x" + comboMagnification.ToString(), 1.5f, Color.yellow);
        }

        public float CalculateTotalScore()
        {
            float coinMagnification = 1.0f;
            if (getCoinCount == 1)
            {
                coinMagnification = 1.25f;
            }
            else if (getCoinCount == 2)
            {
                coinMagnification = 1.5f;
            }
            else
            {
                coinMagnification = 2.0f;
            }
            return catapultScore * comboMagnification * coinMagnification;
        }
    }
}