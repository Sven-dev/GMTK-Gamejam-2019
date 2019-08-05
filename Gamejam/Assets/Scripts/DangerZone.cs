using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour {

    private int BulletsInTrigger;
    private bool InDanger;

	// Use this for initialization
	void Start ()
    {
        BulletsInTrigger = 0;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletsInTrigger++;
            StartCoroutine(_SlowTime());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletsInTrigger--;
            if (BulletsInTrigger < 1)
            {
                InDanger = false;
                BulletsInTrigger = 0;
            }
        }
    }

    //Slow time
    IEnumerator _SlowTime()
    {
        InDanger = true;
        while (InDanger)
        {
            if (Time.timeScale > 0.25f)
            {
                Time.timeScale -= 0.05f;
            }

            yield return null;
        }

        //Speed time back up
        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.25f;
            yield return null;
        }
    }
}
