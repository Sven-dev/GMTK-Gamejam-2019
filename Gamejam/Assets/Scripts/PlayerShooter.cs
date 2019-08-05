using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{
    public float Shake;

    [Space]
    public float Cooldown;
    public bool Coolingdown;
    public Bar CooldownBar;

    private Rigidbody2D Rigidbody;
    private CameraShake Cam;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        Coolingdown = false;
   
        Rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        Cam = Camera.main.GetComponent<CameraShake>();
	}

    public override void Shoot()
    {
        base.Shoot();

        Rigidbody.AddForce(transform.up * Force);
        Cam.Shake(0.1f, Shake);
    }

    public void Aim(KeyCode key)
    {
        if (!Coolingdown)
        {
            StartCoroutine(_Aim(key));
        }
        else
        {
            CooldownBar.Blink();
        }
    }

    IEnumerator _Aim(KeyCode key)
    {
        //Slow time
        while(Input.GetKey(key))
        {
            if (Time.timeScale > 0.25f)
            {
                Time.timeScale -= 0.05f;
                Field.TimeScale = Time.timeScale;
            }

            yield return null;
        }

        Shoot();
        StartCoroutine(_Cooldown());
        
        //Speed time back up
        while(Time.timeScale < 1)
        {
            Time.timeScale += 0.25f;
            Field.TimeScale = Time.timeScale;
            yield return null;
        }
    }

    IEnumerator _Cooldown()
    {
        Coolingdown = true;
        float progress = 0;
        while(progress < Cooldown)
        {
            progress += Time.deltaTime;
            if (CooldownBar != null)
            {
                CooldownBar.updateUI(progress / Cooldown);
            }

            yield return null;
        }

        Coolingdown = false;
    }
}
