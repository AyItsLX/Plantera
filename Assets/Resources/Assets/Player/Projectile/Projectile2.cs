using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour {

    public float speed;
    private Vector3 direction;
    public GameObject hitMarker;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;

        if (GameManager.HasWon)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 _direction)
    {
        direction = _direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BossObj")
        {
            Instantiate(hitMarker, transform.position, Quaternion.identity);
            other.GetComponent<BossBehaviour>().HitSound();
            other.GetComponent<BossBehaviour>().bossHealth -= 1f;
            Destroy(gameObject);
        }
    }
}
