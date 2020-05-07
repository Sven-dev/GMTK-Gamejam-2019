using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLabel : MonoBehaviour
{
    private Image Background;
    private Text Gameover;
    private Text Continue;

    private GameResetter Resetter;

	// Use this for initialization
	void Start ()
    {
        Background = GetComponent<Image>();
        Gameover = transform.GetChild(0).GetComponent<Text>();
        Continue = transform.GetChild(1).GetComponent<Text>();

        Resetter = GetComponent<GameResetter>();

        Health.OnPlayerDeath += UpdateUI;
    }

    private void OnDestroy()
    {
        Health.OnPlayerDeath -= UpdateUI;
    }

    private void UpdateUI()
    {
        Time.timeScale = 1;
        Field.TimeScale = 1;
        StartCoroutine(_UpdateUI());
    }

    IEnumerator _UpdateUI()
    {
        yield return new WaitForSeconds(1.5f);

        Background.enabled = true;
        Gameover.enabled = true;

        yield return new WaitForSeconds(0.5f);

        Resetter.enabled = true;

        yield return new WaitForSeconds(1);

        Continue.enabled = true;
    }
}
