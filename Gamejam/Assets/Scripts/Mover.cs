using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float Speed;

    public void Move(Vector2 direction)
    {
        transform.GetChild(0).Translate(direction * Speed * Time.deltaTime);
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
            transform.Translate(direction * (Speed * 0.1f * temp) * Time.deltaTime);
            temp++;
            yield return null;
        }

        //full speed
        while (Input.GetKey(key))
        {
            transform.Translate(direction * Speed * Time.deltaTime);
            yield return null;
        }

        //slowdown
        while (temp > 0)
        {
            transform.Translate(direction * (Speed * 0.1f * temp) * Time.deltaTime);
            temp--;
            yield return null;
        }
    }
}
