using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour {

    public float blinkSpeed = 0.5f;
    public bool startBlinking = false;
    public SpriteRenderer[] pSprite;

    [SerializeField]
    float blink = 1;
    [SerializeField]
    float changeFade = 0;

    void Start ()
    {
        startBlinking = false;
    }
	
	void Update ()
    {
        if (startBlinking)
        {
            if (changeFade == 0)
            {
                blink -= Time.deltaTime * blinkSpeed;
            }
            else if (changeFade == 1)
            {
                blink += Time.deltaTime * blinkSpeed;
            }

            if (changeFade == 0 && blink < 0.1f)
            {
                changeFade = 1;
            }
            else if (changeFade == 1 && blink > 0.9f)
            {
                changeFade = 0;
            }
        }
        else
        {
            changeFade = 0;
            blink = 1;
        }

        foreach (SpriteRenderer temp in pSprite)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainScene")
            {
                temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, blink);
            }
        }
	}
}
