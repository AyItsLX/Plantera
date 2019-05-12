using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPea : MonoBehaviour {

    public Transform playerPosition;
    public GameObject PeaBulletPrefab;
    public Transform spawnPosition;
    private Vector3 direction;

	void Start ()
    {
		
	}

    public void SpawnBulletPea()
    {
        if (playerPosition == isActiveAndEnabled)
        {
            direction = playerPosition.position - transform.position;
        }

        if (gameObject.name == "L_PeaShooter")
        {
            GameObject tempBullet = Instantiate(PeaBulletPrefab, spawnPosition.position, Quaternion.identity);
            tempBullet.transform.right = direction;
            tempBullet.GetComponent<PeaBullet>().SetDirection(direction.normalized);
        }
        else if (gameObject.name == "R_PeaShooter")
        {
            GameObject tempBullet = Instantiate(PeaBulletPrefab, spawnPosition.position, Quaternion.identity);
            tempBullet.transform.right = direction;
            tempBullet.GetComponent<PeaBullet>().SetDirection(direction.normalized);
        }
        GetComponent<AudioSource>().Play();
    }
}
