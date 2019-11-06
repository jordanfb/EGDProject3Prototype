using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void LoadOtherScene(int sceneIndex)
    {
        // probably going to be used for loading the multiplayer menu stuff
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}