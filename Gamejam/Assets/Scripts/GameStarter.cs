using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public string Scene;
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(Scene);
        }
	}
}
