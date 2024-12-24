using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        public TMP_Text infoText;
        public TMP_Text goalValue;
        public TMP_Text finalValue;

        // ScoreIcon;
        public TMP_Text scoreValue;

        public TMP_Text coinsMagnification;

        public Image coin_1;
        public Image coin_2;
        public Image coin_3;

        private Color normalColor;
        private Color gotColor;

        // Rank

        private void Start()
        {
            normalColor = coin_1.color;
            gotColor = new Color(coin_1.color.r, coin_1.color.g, coin_1.color.b, 255.0f);
            Gamelogic.EventManager.Instance.onCoinCollected.AddListener(UpdateCoinInfo);
            Gamelogic.EventManager.Instance.onScoreChanged.AddListener(UpdateScoreInfo);
        }

        private void UpdateScoreInfo(int deltaScore)
        {
            scoreValue.text = Gamelogic.GameManager.Instance.catapultScore.ToString();
            finalValue.text = Gamelogic.GameManager.Instance.CalculateTotalScore().ToString();
        }

        private void UpdateCoinInfo()
        {
            int coinCount = Gamelogic.GameManager.Instance.getCoinCount;
            Color normalColor = coin_1.color;
            Color gotColor = new Color(coin_1.color.r, coin_1.color.g, coin_1.color.b, 255.0f);
            switch (coinCount)
            {
                case 0:
                    this.coinsMagnification.text = "1.0";
                    coin_1.color = normalColor;
                    coin_2.color = normalColor;
                    coin_3.color = normalColor;
                    break;

                case 1:
                    this.coinsMagnification.text = "1.25";
                    coin_1.color = gotColor;
                    coin_2.color = normalColor;
                    coin_3.color = normalColor;
                    break;

                case 2:
                    this.coinsMagnification.text = "1.5";
                    coin_1.color = gotColor;
                    coin_2.color = gotColor;
                    coin_3.color = normalColor;
                    break;

                case 3:
                    this.coinsMagnification.text = "2.0";
                    coin_1.color = gotColor;
                    coin_2.color = gotColor;
                    coin_3.color = gotColor;
                    break;

                default:
                    break;
            }

            finalValue.text = Gamelogic.GameManager.Instance.CalculateTotalScore().ToString();
        }
    }
}