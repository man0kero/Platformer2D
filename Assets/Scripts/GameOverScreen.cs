using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartHandler() {
        Scene scene  = SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name);
    }

    public void ExitHandler() {
        SceneManager.LoadScene(0);
    }
}
