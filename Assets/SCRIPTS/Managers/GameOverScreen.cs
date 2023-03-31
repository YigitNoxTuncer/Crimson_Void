using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace NOX
{
    public class GameOverScreen : MonoBehaviour
    {
        public void RestartButton()
        {
            SceneManager.LoadScene(0);
        }
        
        public void QuitButton()
        {
            Application.Quit();
        }
    }
}

