using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolllingCredits : MonoBehaviour {

    public float speed = 50f;
    public bool scrolling = true;

	void Update ()
    {
        if (!scrolling)
            return;

        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (gameObject.transform.localPosition.y > 955)
        {
            scrolling = false;
        }
    }
}
