using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour {

    public float speed = 2f;

    private Vector3 direction;

	void Start ()
    {
        Destroy(gameObject, 3f);
	}

	void Update ()
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
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement_v2 playerControl = other.GetComponent<PlayerMovement_v2>();

            playerControl.SpawnHitEffect();
            playerControl.hitAudio.Play();
            playerControl.GetComponent<BoxCollider2D>().enabled = false;
            playerControl.isInInvincible = true;
            playerControl.playerHealth -= 1;
            Destroy(gameObject);
        }
    }
}
