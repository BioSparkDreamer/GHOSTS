using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    // name of scene to link door to
    public string targetScene;

    // triggers when collider is entered/collided with
    void OnTriggerEnter2D(Collider2D door) 
    {
        // when player collides with door
        if (door.CompareTag("Player")) 
        {
            // loads scene named targetScene
            SceneManager.LoadScene(targetScene);
        }

    }
}
