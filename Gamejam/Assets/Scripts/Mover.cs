using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rigidbody;

    public void Move(Vector2 direction)
    {
        //Rigidbody.position += direction * Speed * Time.deltaTime;
        transform.GetChild(0).Translate(direction * Speed * Field.TimeScale * Time.fixedDeltaTime);
    }

    public void Move(KeyCode key, Vector2 direction)
    {
        StartCoroutine(_Move(key, direction));
    }

    IEnumerator _Move(KeyCode key, Vector2 direction)
    {
        int temp = 0;

        //startup
        while (Input.GetKey(key) && temp < 10)
        {
            transform.Translate(direction * (Speed * 0.1f * temp) * Field.TimeScale * Time.fixedDeltaTime);
            temp++;
            yield return new WaitForFixedUpdate();
        }

        //full speed
        while (Input.GetKey(key))
        {
            transform.Translate(direction * Speed * Field.TimeScale * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        //slowdown
        while (temp > 0)
        {
            transform.Translate(direction * (Speed * 0.1f * temp) * Field.TimeScale * Time.fixedDeltaTime);
            temp--;
            yield return new WaitForFixedUpdate();
        }
    }
}
