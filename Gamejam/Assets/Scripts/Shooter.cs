using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public Bullet BulletPrefab;
    public float Force;

    protected Transform BulletSpawn;
    protected AudioSource Audio;

    // Use this for initialization
    protected virtual void Start ()
    {
		BulletSpawn = transform.GetChild(0);
        Audio = GetComponent<AudioSource>();
	}

    public virtual void Shoot()
    {
        foreach (Transform spawn in BulletSpawn)
        {
            Instantiate(BulletPrefab, spawn);
        }

        Audio.pitch = 1.25f * Field.TimeScale;
        Audio.Play();
    }
}
