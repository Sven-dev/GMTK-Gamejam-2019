using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float WaitTime;

    private AudioSource Audio;

    // Use this for initialization
    void Start()
    {
        Audio = GetComponent<AudioSource>();

        Health.OnPlayerDeath += StopMusic;
        Field.FirstKill += StartMusic;
    }

    private void Update()
    {
        float pitch = Field.TimeScale;
        if (pitch > 1)
        {
            pitch = 1;
        }

        Audio.pitch = pitch;
    }

    IEnumerator _Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Audio.Play();
    }

    private void StartMusic()
    {
        StartCoroutine(_Wait(WaitTime));
        Field.FirstKill -= StartMusic;
    }

    private void StopMusic()
    {
        Audio.Stop();
    }

    private void OnDestroy()
    {
        Health.OnPlayerDeath -= StopMusic;
        Field.FirstKill -= StartMusic;
    }
}