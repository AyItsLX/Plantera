using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyAttack : MonoBehaviour {

    public PlayerMovement_v2 playerMovement;
    public GameObject FairyBullet;
    public Transform spawnPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMovement.facingRight)
        {
            GameObject bullet = Instantiate(FairyBullet, spawnPosition.position, spawnPosition.rotation);
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y, bullet.transform.localScale.z);
            bullet.GetComponent<Fairy>().SetDirection(spawnPosition.forward);
        }
        else if (!playerMovement.facingRight)
        {
            GameObject bullet = Instantiate(FairyBullet, spawnPosition.position, spawnPosition.rotation);
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * -1, bullet.transform.localScale.y, bullet.transform.localScale.z);
            bullet.GetComponent<Fairy>().SetDirection(spawnPosition.forward);
        }
    }
}
