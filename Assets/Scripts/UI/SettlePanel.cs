using UnityEngine;
using TMPro;
using Gamelogic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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

        public float scoreSpeed = 1f;
        public TMP_Text[] scoreFields;

        public Sprite nextLevelSprite;
        public Sprite retrySprite;

        private void Start()
        {
            if (nextLevelButton == null)
                nextLevelButton = GameObject.Find("NextButton").GetComponent<Button>();

            if (returnTitleButton == null)
                returnTitleButton = GameObject.Find("ReturnButton").GetComponent<Button>();

            //nextLevelButton.onClick.AddListener(LevelManager.Instance.NextLevel);
            returnTitleButton.onClick.AddListener(() => { transform.parent.GetComponent<GameUI.UI>().SelectLevel(); });
        }

        private void OnDisable()
        {
            nextLevelButton.onClick.RemoveAllListeners();
        }

        public void Settle()
        {
            var ui = transform.parent.GetComponent<UI>();

            level.text = ui.infoText.text;
            bool isClear = GameManager.Instance.CalculateRank() > 0;
            levelStatus.text = isClear ? "Clear" : "Lose";
            if (isClear)
            {
                nextLevelButton.onClick.AddListener(LevelManager.Instance.NextLevel);
                nextLevelButton.GetComponent<Image>().sprite = nextLevelSprite;
            }
            else
            {
                nextLevelButton.onClick.AddListener(LevelManager.Instance.Restart);
                nextLevelButton.GetComponent<Image>().sprite = retrySprite;
            }

            goal.text = ui.goalValue.text;
            score.text = ui.scoreValue.text;
            comboMagnification.text = ui.comboMagnification.text;
            coinsMagnification.text = ui.coinsMagnification.text;
            finalScore.text = ui.finalValue.text;
            rank.text = ui.rank.text;

            StartCoroutine(DisplayScores());
        }

        private IEnumerator DisplayScores()
        {
            foreach (var scoreField in scoreFields)
            {
                // 获取每个分数目标值
                float targetScore = float.Parse(scoreField.text);
                scoreField.text = "0";  // 重置文本显示为0

                // 逐渐增加分数
                yield return StartCoroutine(CountScore(scoreField, targetScore));

                // 控制每个分数显示完后，稍作延迟再显示下一个
                yield return new WaitForSeconds(0.5f);  // 可以根据需要调整间隔时间
            }

            //foreach (var scoreField in scoreFields)
            //{
            //    // 获取每个分数目标值
            //    float targetScore = float.Parse(scoreField.text);
            //    scoreField.text = "0";  // 重置文本显示为0

            //    // 逐渐增加分数
            //    yield return StartCoroutine(CountScore(scoreField, targetScore));

            //    // 控制每个分数显示完后，稍作延迟再显示下一个
            //    yield return new WaitForSeconds(0.5f);  // 可以根据需要调整间隔时间
            //}
        }

        private IEnumerator CountScore(TMP_Text targetText, float targetScore)
        {
            float currentScore = 0f;
            float timeElapsed = 0f;

            // 逐渐增加分数，直到达到目标分数
            while (currentScore < targetScore)
            {
                timeElapsed += Time.deltaTime * scoreSpeed;  // 时间推进，用于控制增加速率
                currentScore = Mathf.Lerp(0f, targetScore, timeElapsed);  // 计算当前分数
                targetText.text = Mathf.FloorToInt(currentScore).ToString();  // 更新文本显示为整数

                yield return null;  // 等待下一帧
            }

            targetText.text = targetScore.ToString("F0");  // 最终确保文本显示为目标分数
        }
    }
}