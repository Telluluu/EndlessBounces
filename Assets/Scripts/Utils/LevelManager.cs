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
        // Start is called before the first frame update
        private void Start()
        {
        }

        public void Restart()
        {
            Debug.Log("Restart");
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}