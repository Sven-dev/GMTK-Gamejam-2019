using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetter : MonoBehaviour {

    public bool Active = false;
	
	// Update is called once per frame
	void Update ()
    {
		if (Active)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Game");
            }
        }
	}


}
