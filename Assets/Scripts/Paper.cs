using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    [SerializeField] AudioClip pageSound;
    [SerializeField] int pages = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Calling the public method on the GameSession object
            FindObjectOfType<GameSession>().AddToScore(pages);

            // play at the camera
            AudioSource.PlayClipAtPoint(pageSound, Camera.main.transform.position,.3f); 
            Destroy(gameObject);
        }
    }
}
