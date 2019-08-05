using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    private Text Label;

	// Use this for initialization
	void Start ()
    {
        Label = GetComponent<Text>();
        StartCoroutine(_Blink());
	}

    IEnumerator _Blink()
    {
        yield return new WaitForSeconds(2.5f);
        while(true)
        {
            Label.enabled = true;
            yield return new WaitForSeconds(1);

            Label.enabled = false;
            yield return new WaitForSeconds(1);
        }
    }
}