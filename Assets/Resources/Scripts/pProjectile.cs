using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pProjectile : MonoBehaviour {

    public float speed;
    private Vector3 direction;

    public GameObject hitMarker;
    public GameObject DamageNo;
    public PlayerMovement_v2 playerMovement;

    void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);

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

    public void SetDirection(Vector3 _direction, PlayerMovement_v2 incomingScript)
    {
        direction = _direction;
        playerMovement = incomingScript;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BossObj")
        {
            //EZCameraShake.CameraShaker.Instance.ShakeOnce(1, 1, 0.1f, 1);
            Instantiate(hitMarker, transform.position, Quaternion.identity);

            GameObject tempObj = Instantiate(DamageNo, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            tempObj.GetComponentInChildren<TMPro.TMP_Text>().text = "" + Random.Range(5,10);

            other.GetComponent<BossBehaviour>().OnBodyHit();
            other.GetComponent<BossBehaviour>().HitSound();
            other.GetComponent<BossBehaviour>().bossHealth -= Random.Range(5, playerMovement.damage);
            Destroy(gameObject);
        }
    }
}
