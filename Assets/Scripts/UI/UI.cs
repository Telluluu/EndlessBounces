using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Gamelogic;

namespace GameUI
{
    public class UI : MonoBehaviour
    {
        public TMP_Text infoText;
        public TMP_Text goalValue;
        public TMP_Text finalValue;

        public Transform finalValuePopTrans;
        public Transform scoreValuePopTrans;
        public Transform coinsMagnificationPopTrans;

        // ScoreIcon;
        public TMP_Text comboMagnification;

        public TMP_Text scoreValue;

        public TMP_Text coinsMagnification;

        public TMP_Text rank;

        public UnityEngine.UI.Image coin_1;
        public UnityEngine.UI.Image coin_2;
        public UnityEngine.UI.Image coin_3;

        private Color normalColor;
        private Color gotColor;

        // Rank
        private Vector3 originalRotation;

        public float shakeAmplitude = 5.0f;  // 摇摆的幅度
        public float shakeSpeed = 2f;  // 摇摆的速度

        public SettlePanel settlePanel;

        public Button retryButton;
        public Button selectButton;
        public GameObject levelSelectPanel;

        private void Start()
        {
            normalColor = coin_1.color;
            gotColor = new Color(coin_1.color.r, coin_1.color.g, coin_1.color.b, 255.0f);

            Init();
        }

        private void Init()
        {
            infoText.text = "1-1";
            goalValue.text = Gamelogic.GameManager.Instance.goal.ToString();
            finalValue.text = "0";
            comboMagnification.text = "x1.0";
            scoreValue.text = "0";
            coinsMagnification.text = "1.0";
            rank.text = "B";

            Gamelogic.EventManager.Instance.onCoinCollected.AddListener(PopUpCoinDelta);
            Gamelogic.EventManager.Instance.onCoinCollected.AddListener(UpdateCoinInfo);
            Gamelogic.EventManager.Instance.onCoinCollected.AddListener(UpdateRank);

            Gamelogic.EventManager.Instance.onScoreChanged.AddListener(PopUpScoreDelta);
            Gamelogic.EventManager.Instance.onScoreChanged.AddListener(UpdateScoreInfo);
            Gamelogic.EventManager.Instance.onScoreChanged.AddListener((int deltaScore) => UpdateRank());

            retryButton.onClick.AddListener(() => Gamelogic.LevelManager.Instance.Restart());
            selectButton.onClick.AddListener(() =>
            {
                levelSelectPanel.SetActive(true);
                this.transform.localScale = Vector3.zero;
            });
        }

        private void Update()
        {
            // 计算 Z 轴旋转摇摆的角度
            float shakeAngle = Mathf.Sin(Time.time * shakeSpeed) * shakeAmplitude;

            finalValue.transform.rotation = Quaternion.Euler(originalRotation.x, originalRotation.y, shakeAngle);
            scoreValue.transform.rotation = Quaternion.Euler(originalRotation.x, originalRotation.y, shakeAngle);
            coinsMagnification.transform.rotation = Quaternion.Euler(originalRotation.x, originalRotation.y, shakeAngle);
        }

        private void UpdateRank()
        {
            float percent = Gamelogic.GameManager.Instance.CalculateTotalScore() / Gamelogic.GameManager.Instance.goal;
            if (percent < 0.6)
            {
                rank.text = "B";
            }
            else if (percent < 0.8)
            {
                rank.text = "A";
            }
            else if (percent < 0.99)
            {
                rank.text = "S";
            }
            else
            {
                rank.text = "SS";
            }
        }

        private void UpdateScoreInfo(int deltaScore)
        {
            comboMagnification.text = "x" + Gamelogic.GameManager.Instance.comboMagnification.ToString("F1");

            scoreValue.text = (Gamelogic.GameManager.Instance.catapultScore * Gamelogic.GameManager.Instance.comboMagnification).ToString("F0");
            finalValue.text = Gamelogic.GameManager.Instance.CalculateTotalScore().ToString("F0");
        }

        private void PopUpScoreDelta(int deltaScore)
        {
            float delta = deltaScore * Gamelogic.GameManager.Instance.comboMagnification;

            //int deltaScore = (int)(Gamelogic.GameManager.Instance.catapultScore * Gamelogic.GameManager.Instance.comboMagnification
            //    - int.Parse(scoreValue.text));
            TextPoper.Instance.FloatingText(scoreValuePopTrans, "+" + delta.ToString(), 1.0f, Color.white);

            float deltaFinalScore = delta * GameManager.Instance.CalculateCoinMagnification();
            TextPoper.Instance.FloatingText(finalValuePopTrans, "+" + deltaFinalScore.ToString("F0"), 1.0f, Color.white);
        }

        private void PopUpCoinDelta()
        {
            float deltaCoinMagnification = Gamelogic.GameManager.Instance.CalculateCoinMagnification() - float.Parse(coinsMagnification.text);
            TextPoper.Instance.FloatingText(coinsMagnificationPopTrans, "+" + deltaCoinMagnification.ToString("F2"), 1.0f, Color.white);

            float deltaFinalScore = Gamelogic.GameManager.Instance.CalculateTotalScore() - float.Parse(finalValue.text);
            TextPoper.Instance.FloatingText(finalValuePopTrans, "+" + deltaFinalScore.ToString("F0"), 1.0f, Color.white);
        }

        private void UpdateCoinInfo()
        {
            int coinCount = Gamelogic.GameManager.Instance.getCoinCount;
            switch (coinCount)
            {
                case 0:

                    coin_1.color = normalColor;
                    coin_2.color = normalColor;
                    coin_3.color = normalColor;
                    break;

                case 1:
                    coin_1.color = gotColor;
                    coin_2.color = normalColor;
                    coin_3.color = normalColor;
                    break;

                case 2:
                    coin_1.color = gotColor;
                    coin_2.color = gotColor;
                    coin_3.color = normalColor;
                    break;

                case 3:
                    coin_1.color = gotColor;
                    coin_2.color = gotColor;
                    coin_3.color = gotColor;
                    break;

                default:
                    break;
            }
            this.coinsMagnification.text = Gamelogic.GameManager.Instance.CalculateCoinMagnification().ToString("F2");
            finalValue.text = Gamelogic.GameManager.Instance.CalculateTotalScore().ToString("F0");
        }
    }
}