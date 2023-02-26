using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class End : MonoBehaviour
{
    public void Return()
    {
        FindObjectOfType<GameSession>().Reset();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
