using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("FinalProject");
    }

    public void PlayNormalGame()
    {
        SceneManager.LoadScene("Chessexperiments");
    }

    public void BackButton ()
    {
        SceneManager.LoadScene("Menu");
    }
}
