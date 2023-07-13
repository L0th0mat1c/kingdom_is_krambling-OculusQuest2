using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HubManager : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
