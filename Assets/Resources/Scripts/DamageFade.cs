using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFade : MonoBehaviour {

    Vector3 originalPos;
    int[] randomX = { -1, 1 };
    int randomizeX;

    void Start ()
    {
        randomizeX = Random.Range(0, 2);
        originalPos = transform.position;
        originalPos.y += 5;
    }

	void Update ()
    {
        if (transform.localScale.x <= 1.5f)
        {
            transform.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, 0);
        }

        if (transform.localScale.x >= 1f)
        {
            transform.position += new Vector3(randomX[randomizeX] * Time.deltaTime, 0, 0);
        }

        if (originalPos.y >= transform.position.y)
        {
            transform.position += new Vector3(0, Time.deltaTime * 2, 0);
        }

        if (originalPos.y - 4 <= transform.position.y)
        {
            GetComponentInChildren<TMPro.TextMeshProUGUI>().color -= new Color(0, 0, 0, Time.deltaTime * 2);
        }

        if (GetComponentInChildren<TMPro.TextMeshProUGUI>().color.a <= 0)
        {
            Destroy(gameObject);
        }
	}
}
