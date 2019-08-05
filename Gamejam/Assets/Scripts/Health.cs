using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool player = false;
    public int Number;

    [Space]
    public AudioClip HitSFX;
    public AudioClip DeathSFX;

    [Space]
    public List<SpriteRenderer> NumberSprites;

    private SpriteRenderer NumberSprite;
    private SpriteRenderer CharacterSprite;
    private AudioSource Audio;

    private AI AI;

    public delegate void Death();
    public static Death OnDeath;

    public delegate void PlayerDeath();
    public static Death OnPlayerDeath;

    // Use this for initialization
    private void OnEnable()
    {
        if (transform.parent != null)
        {
            AI = transform.parent.GetComponent<AI>();
            if (AI != null)
            {
                AI.ActiveAI = Number;
            }
        }
    }

    private void Start()
    {
        NumberSprite = GetComponentInChildren<SpriteRenderer>();
        CharacterSprite = GetComponent<SpriteRenderer>();
        Audio = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        //Take the damage
        Number--;
        if (AI != null)
        {
            AI.ActiveAI--;
        }

        //Change the object
        Destroy(transform.GetChild(0).gameObject);
        NumberSprite = Instantiate(NumberSprites[Number], transform);

        if (Number == 0)
        {
            Audio.PlayOneShot(DeathSFX);
            StartCoroutine(_Death());
            if (OnDeath != null)
            {
                OnDeath();
            }
           
            if (player)
            {
                OnPlayerDeath();
                Destroy(GetComponent<Aimer>());
            }

            Transform Parent = transform.parent;
            transform.parent = null;
            Destroy(Parent.gameObject);
        }
        else
        {
            Audio.PlayOneShot(HitSFX);
            StartCoroutine(_Blink());
        }
    }

    IEnumerator _Blink()
    {
        NumberSprite.color = Palet.Red;
        CharacterSprite.color = Palet.Red;
        yield return new WaitForSeconds(0.05f);
        NumberSprite.color = Palet.Black;
        CharacterSprite.color = Palet.DarkGray;
        yield return new WaitForSeconds(0.05f);
        NumberSprite.color = Palet.Red;
        CharacterSprite.color = Palet.Red;
        yield return new WaitForSeconds(0.05f);
        NumberSprite.color = Palet.Black;
        CharacterSprite.color = Palet.DarkGray;
    }

    IEnumerator _Death()
    {
        //delete hitbox
        Destroy(GetComponent<Collider2D>());
        CharacterSprite.color = Palet.Red;

        //Fade character to softer red
        StartCoroutine(_Fade());

        yield return new WaitForSeconds(0.75f);

        //Fade out number
        while (NumberSprite.color.a > 0)
        {
            Color temp = NumberSprite.color;
            temp.a -= 0.01f;
            NumberSprite.color = temp;
            yield return null;
        }
    }

    IEnumerator _Fade()
    {
        float progress = 0;
        while (progress < 1)
        {
            CharacterSprite.color = Color.Lerp(Palet.Red, Palet.FadedRed, progress);
            progress += Time.deltaTime / 45f;
            yield return null;
        }
    }
}
