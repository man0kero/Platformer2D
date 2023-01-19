using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public void NextLevelHandler() {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.buildIndex);
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
