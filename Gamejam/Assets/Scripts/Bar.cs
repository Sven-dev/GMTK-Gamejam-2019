using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image Image;

    // Use this for initialization
    void Start ()
    {
        Image = GetComponent<Image>();
	}

    public void updateUI(float value)
    {
        Image.fillAmount = value;
    }

    public void Blink()
    {
        StartCoroutine(_Blink());
    }

    IEnumerator _Blink()
    {
        Image.color = Palet.Red;
        yield return new WaitForSeconds(0.05f);
        Image.color = Palet.White;
        yield return new WaitForSeconds(0.05f);
        Image.color = Palet.Red;
        yield return new WaitForSeconds(0.05f);
        Image.color = Palet.White;
    }
}