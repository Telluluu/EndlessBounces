using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameUI
{
    public class LevelSelectPanel : MonoBehaviour
    {
        private int nowSeries = 1;

        public Button returnButton;

        public Button seriesButton_1;
        public Button seriesButton_2;
        public Button seriesButton_3;

        public Button levelButton_1_1;
        public Button levelButton_1_2;
        public Button levelButton_1_3;
        public Button levelButton_1_4;
        public Button levelButton_1_5;
        public Button levelButton_1_6;
        public GameObject rootPanel;

        private void Start()
        {
            seriesButton_1.onClick.AddListener(() => { nowSeries = 1; });
            seriesButton_2.onClick.AddListener(() => { nowSeries = 2; });
            seriesButton_3.onClick.AddListener(() => { nowSeries = 3; });

            levelButton_1_1.onClick.AddListener(() => { SceneManager.LoadSceneAsync(6 * (nowSeries - 1) + 1); });
            levelButton_1_2.onClick.AddListener(() => { SceneManager.LoadSceneAsync(6 * (nowSeries - 1) + 2); });
            levelButton_1_3.onClick.AddListener(() => { SceneManager.LoadSceneAsync(6 * (nowSeries - 1) + 3); });
            levelButton_1_4.onClick.AddListener(() => { SceneManager.LoadSceneAsync(6 * (nowSeries - 1) + 4); });
            levelButton_1_5.onClick.AddListener(() => { SceneManager.LoadSceneAsync(6 * (nowSeries - 1) + 5); });
            levelButton_1_6.onClick.AddListener(() => { SceneManager.LoadSceneAsync(6 * (nowSeries - 1) + 6); });

            returnButton.onClick.AddListener(() =>
            {
                rootPanel.transform.localScale = Vector3.one;
                this.gameObject.SetActive(false);
            });
        }
    }
}