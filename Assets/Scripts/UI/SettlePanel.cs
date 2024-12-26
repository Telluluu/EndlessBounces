using UnityEngine;
using TMPro;
using Gamelogic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameUI
{
    public class SettlePanel : MonoBehaviour
    {
        public TMP_Text level;
        public TMP_Text levelStatus;
        public TMP_Text goal;
        public TMP_Text score;
        public TMP_Text comboMagnification;
        public TMP_Text coinsMagnification;
        public TMP_Text finalScore;
        public TMP_Text rank;

        public Button nextLevelButton;

        public Button returnTitleButton;

        private void Start()
        {
            if (nextLevelButton == null)
                nextLevelButton = GameObject.Find("NextButton").GetComponent<Button>();

            if (returnTitleButton == null)
                returnTitleButton = GameObject.Find("ReturnButton").GetComponent<Button>();

            nextLevelButton.onClick.AddListener(LevelManager.Instance.NextLevel);
            returnTitleButton.onClick.AddListener(() => { transform.parent.GetComponent<GameUI.UI>().SelectLevel(); });
        }

        public void Settle()
        {
            var ui = transform.parent.GetComponent<UI>();
            level.text = ui.infoText.text;

            levelStatus.text = GameManager.Instance.CalculateRank() > 0 ? "Clear" : "Lose";
            goal.text = ui.goalValue.text;
            score.text = ui.scoreValue.text;
            comboMagnification.text = ui.comboMagnification.text;
            coinsMagnification.text = ui.coinsMagnification.text;
            finalScore.text = ui.finalValue.text;
            rank.text = ui.rank.text;
        }
    }
}