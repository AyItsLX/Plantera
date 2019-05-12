using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour {

    private Vector3 randomDirection;
    private float randomSpeed = 0f;
    private float randomRotateSpeed = 0f;
    private float randomSize = 0f;

	void Start ()
    {
        randomDirection = new Vector3(transform.position.x - Random.Range(-90, 90), transform.position.y - Random.Range(-90,90), 0);
        randomSpeed = Random.Range(0.1f, 0.5f);
        randomRotateSpeed = Random.Range(100f, 200f);
        randomSize = Random.Range(0.25f, 2f);
        transform.localScale = new Vector3(randomSize, randomSize, 0);
        Destroy(gameObject.transform.parent.gameObject, 2f);
    }
	
	void Update ()
    {
        if (transform.localScale.x >= 0 && transform.localScale.y >= 0)
        {
            transform.localScale += new Vector3(-Time.deltaTime, -Time.deltaTime, 0);
        }
        transform.position += randomDirection * Time.deltaTime * randomSpeed;
        transform.Rotate(Vector3.forward * Time.deltaTime * randomRotateSpeed);
    }
}
