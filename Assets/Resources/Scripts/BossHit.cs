using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "HitCollider" && other.gameObject.CompareTag("Player"))
        {
            PlayerMovement_v2 playerControl = other.GetComponent<PlayerMovement_v2>();

            playerControl.SpawnHitEffect();
            playerControl.isInInvincible = true;
            playerControl.hitAudio.Play();
            playerControl.playerHealth -= 1f;
        }
    }
}
