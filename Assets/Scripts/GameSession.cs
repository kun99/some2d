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

    public int getScore()
    {
        return score;
    }

    public void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if(index==2){
            score = 3;
        }
        else if(index==3){
            score = 6;
        }
        else{
            score = 0;
        }
        ToText();
    }

    public void AddToScore(int points) // This is public. When the coin is hit, it will call this.
    {
        score += 1;
        scoreText.text = score.ToString();  // if we change the value and we want UI to update
                                        // we must do that manually
    }

    public void Reset()
    {
        playerLives = 3;
        score = 0;
        ToText();
    }

    public void ToText()
    {
        livesText.text = playerLives.ToString(); // Make the UI show the initial value
        scoreText.text = score.ToString();       // Make the UI show the initial value
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
        else
        {
            // Reset the ScenePersist
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(gameObject); // Destroy the game session.


        }
    }
}
