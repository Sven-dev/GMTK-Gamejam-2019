using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    private int Amount;
    private Text Text;

	// Use this for initialization
	void Start ()
    {
        Amount = 0;
        Text = GetComponent<Text>();
        Text.text = "";

        Health.OnDeath += UpdateText;
	}

    private void OnDestroy()
    {
        Health.OnDeath -= UpdateText;
    }

    private void UpdateText(AI enemy)
    {
        Text.text += "1";
        Shake(0.5f, 7.5f);
    }

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void Shake(float duration, float intensity)
    {
        shakeAmount = intensity;
        shakeDuration = duration;
    }
}