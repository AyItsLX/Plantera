using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEffect : MonoBehaviour {

    public SpriteRenderer[] _sprite;
    float fadeEffect = 1;

	void Start ()
    {
        Destroy(gameObject, 1.5f);
	}

	void Update ()
    {
        transform.position += new Vector3(0, Time.deltaTime * 2f, 0);

        fadeEffect -= Time.deltaTime * 0.75f;

        foreach (SpriteRenderer temp in _sprite)
        {
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, fadeEffect);
        }
	}
}
