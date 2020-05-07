using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public Bullet BulletPrefab;
    public float Force;

    private Transform BulletSpawn;
    private AudioSource Audio;
    private Rigidbody2D Rigidbody;

    // Use this for initialization
    public virtual void Start ()
    {
        if (transform.childCount != 0)
        {
            BulletSpawn = transform.GetChild(0);
        }

        Audio = GetComponent<AudioSource>();
        Rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot()
    {
        foreach (Transform spawn in BulletSpawn)
        {
            Instantiate(BulletPrefab, spawn);
        }

        Rigidbody.AddForce(transform.up * Force);

        Audio.pitch = 1.25f * Field.TimeScale;
        Audio.Play();
    }
}