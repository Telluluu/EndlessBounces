using System;
using UnityEngine;

namespace Gamelogic
{
    public class GameManager : Singleton<GameManager>
    {
        public int getCoinCount = 0;
        public int maxCoinCount = 3;

        public int catapultScore = 0;

        public float comboMagnification = 1.0f;

        public float goal = 999.0f;

        private BallController _ball;

        private bool isSettled = false;

        private void Awake()
        {
            // DontDestroyOnLoad(this);
            EventManager.Instance.onCoinCollected.AddListener(ResponToCoinCollected);
            EventManager.Instance.onScoreChanged.AddListener(ResponToScoreChanged);
            EventManager.Instance.onGameWin.AddListener(TestWin);
            EventManager.Instance.onGameLose.AddListener(TestLose);
            _ball = FindAnyObjectByType<BallController>();
            if (_ball == null)
            {
                throw new Exception("GameManager: Ball not found");
            }
        }

        private void Update()
        {
            if (isSettled == false)
            {
                if (CalculateTotalScore() >= goal)
                {
                    EventManager.Instance.onGameWin.Invoke();
                }
                else if (_ball.isLaunched && _ball.isStopped)
                {
                    EventManager.Instance.onGameLose.Invoke();
                }
            }
        }

        private void TestWin()
        {
            Debug.Log("Win");
            EventManager.Instance.onScoreChanged.Invoke(0);
            var rootPanel = GameObject.Find("RootPanel").GetComponent<GameUI.UI>();
            var ballRb = _ball.GetComponent<Rigidbody2D>();
            ballRb.isKinematic = true;
            ballRb.velocity = Vector2.zero;
            rootPanel.settlePanel.gameObject.SetActive(true);
            rootPanel.settlePanel.Settle();
        }

        private void TestLose()
        {
            Debug.Log("Lose");
            if (CalculateRank() == 0)
            {
                EventManager.Instance.onScoreChanged.Invoke(0);
                var rootPanel = GameObject.Find("RootPanel").GetComponent<GameUI.UI>();
                var ballRb = _ball.GetComponent<Rigidbody2D>();
                ballRb.isKinematic = true;
                ballRb.velocity = Vector2.zero;
                rootPanel.settlePanel.gameObject.SetActive(true);
                rootPanel.settlePanel.Settle();
            }
            else
            {
                TestWin();
            }
        }

        private void OnDisable()
        {
            EventManager.Instance?.onCoinCollected.RemoveListener(ResponToCoinCollected);
            EventManager.Instance?.onScoreChanged.RemoveListener(ResponToScoreChanged);
        }

        private void ResponToCoinCollected()
        {
            this.getCoinCount += 1;
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

        public float CalculateCoinMagnification()
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
            else if (getCoinCount == 3)
            {
                coinMagnification = 2.0f;
            }
            else
            {
                coinMagnification = 1.0f;
            }
            return coinMagnification;
        }

        public float CalculateTotalScore()
        {
            return catapultScore * comboMagnification * CalculateCoinMagnification();
        }

        public int CalculateRank()
        {
            float percent = CalculateTotalScore() / goal;
            if (percent < 0.6)
            {
                return 0;
            }
            else if (percent < 0.8)
            {
                return 1;
            }
            else if (percent < 0.99)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}