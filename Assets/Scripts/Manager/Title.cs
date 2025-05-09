using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
   public void OnClickStartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("StageSelect");
    }
    public void OnClickExitGame()
    {
        // Exit the game
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}