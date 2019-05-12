using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyTurret : MonoBehaviour {
    
 //   PlayerAttack_v2 playerAttack_v2;

	//void Start () {
 //       playerAttack_v2 = transform.parent.GetComponent<PlayerAttack_v2>();

 //   }
	
	//// Update is called once per frame
	//void Update ()
 //   {

 //       if (!playerAttack_v2.isCoolDown && Input.GetKey(KeyCode.Mouse0))
 //       {
 //           playerAttack_v2.isCoolDown = true;
 //           playerAttack_v2.curCoolDown = playerAttack_v2.maxCoolDOwn;
 //           if (playerAttack_v2.playerMovement.facingRight)
 //           {
 //               GameObject bullet = Instantiate(playerAttack_v2.playerProjectile, transform.position, playerAttack_v2.spawnPosition.rotation);
 //               bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y, bullet.transform.localScale.z);
 //               bullet.GetComponent<pProjectile>().SetDirection(playerAttack_v2.spawnPosition.forward);
 //           }
 //           else if (!playerAttack_v2.playerMovement.facingRight)
 //           {
 //               GameObject bullet = Instantiate(playerAttack_v2.playerProjectile, transform.position, playerAttack_v2.spawnPosition.rotation);
 //               bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * -1, bullet.transform.localScale.y, bullet.transform.localScale.z);
 //               bullet.GetComponent<pProjectile>().SetDirection(playerAttack_v2.spawnPosition.forward);
 //           }
 //       }
 //   }
}
