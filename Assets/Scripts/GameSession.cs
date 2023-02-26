using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

     [SerializeField] TextMeshProUGUI livesText;
     [SerializeField] TextMeshProUGUI scoreText;

    void Awake() 
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions >= 2)  
        {
            Destroy(gameObject); 
        } 
        else // This is the first one
        {
            DontDestroyOnLoad(gameObject); // Make this object survive when we load a new scene
        }
    }

    int getScore()
    {
        return score;
    }

    void Start()
    {
        livesText.text = playerLives.ToString(); // Make the UI show the initial value
        scoreText.text = score.ToString();       // Make the UI show the initial value
    }

    public void AddToScore(int points) // This is public. When the coin is hit, it will call this.
    {
        score += 1;
        scoreText.text = score.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }

    public void ProcessPlayerDeath() // This is public. When John dies, he will call this.
    {
        if(playerLives > 1) 
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            livesText.text = playerLives.ToString(); // if we change the value and we want UI to update
                                                     // we must do that manually
        }
        else // Game over.. need to start from the beginning
        {
            // Reset the ScenePersist
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            
            SceneManager.LoadScene(0); // assume that scene 0 is the first one or the menu
            Destroy(gameObject); // Destroy the game session.


        }
    }
}
