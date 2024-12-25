using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using Gamelogic;
using UnityEngine.SceneManagement;

namespace Gamelogic
{
    public class LevelManager : Singleton<LevelManager>
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void NextLevel()
        {
        }

        public void ReturnToTitle()
        {
            Debug.Log("ReturnToTitle");
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "Scenes/Title_Tilemap")
                Restart();
            else
                SceneManager.LoadScene("Scenes/Title_Tilemap");
        }

        public void Restart()
        {
            Debug.Log("Restart");
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}