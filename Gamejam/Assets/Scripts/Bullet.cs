using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask Mask;
    public float Speed;
    public float Force;

    // Use this for initialization
    void Start ()
    {
        transform.parent = null;
        StartCoroutine(_Timeout());
	}
	
	// Update is called once per frame
	void Update()
    {
        //Raycast
        Vector3 traveldirection = transform.TransformDirection(Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + traveldirection / 25, traveldirection, Speed * Time.deltaTime, Mask);

        //Check for collision
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy" || hit.collider.tag == "Player")
            {
                //Impact force
                Vector2 direction = transform.position - hit.collider.transform.position;
                hit.collider.GetComponent<Rigidbody2D>().AddForce(-direction * Force);

                //Deal damage
                Health h = hit.collider.GetComponent<Health>();
                h.TakeDamage();
            }

            Destroy(gameObject);
        }  

        //move bullet forward
        transform.Translate(Vector2.down * Speed * Time.deltaTime);                
    }

    //Destroys the bullet if it's out for too long
    IEnumerator _Timeout()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
