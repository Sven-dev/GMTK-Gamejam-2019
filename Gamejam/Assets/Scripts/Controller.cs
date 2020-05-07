using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Mover Mover;
    private PlayerShooter Shooter;

	// Use this for initialization
	void Start()
    {
        Mover = GetComponent<Mover>();
        Shooter = GetComponentInChildren<PlayerShooter>();
	}
	
	// Update is called once per frame
	void Update()
    {   
        CheckForMove();
        CheckForShot(); 
    }

    private void CheckForShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooter.Aim(KeyCode.Mouse0);
        }
    }

    private void CheckForMove()
    {
        //Move down
        if (Input.GetKeyDown(KeyCode.W))
        {
            Mover.Move(KeyCode.W, Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Mover.Move(KeyCode.UpArrow, Vector2.up);
        }

        //Move right
        if (Input.GetKeyDown(KeyCode.D))
        {
            Mover.Move(KeyCode.D, Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Mover.Move(KeyCode.RightArrow, Vector2.right);
        }

        //Move left
        if (Input.GetKeyDown(KeyCode.A))
        {
            Mover.Move(KeyCode.A, Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Mover.Move(KeyCode.LeftArrow, Vector2.left);
        }

        //Move down
        if (Input.GetKeyDown(KeyCode.S))
        {
            Mover.Move(KeyCode.S, Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Mover.Move(KeyCode.DownArrow, Vector2.down);
        }
    }
}