using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pShadow : MonoBehaviour {

    public Transform pTransform;
    public PlayerMovement_v2 playerMovement;
    public float pTransformY;

	void Start ()
    {
    }
	
	void Update ()
    {
        if (playerMovement.facingRight)
        {
            transform.position = new Vector3(pTransform.localPosition.x + 0.2f, transform.position.y, transform.position.z);

            if (pTransform.position.y >= 0)
            {
                transform.localScale = new Vector3(0, 0, 1);
            }
            else if (pTransform.position.y > pTransformY)
            {
                transform.localScale = new Vector3(pTransform.position.y, pTransform.position.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(-2, 2, 1);
            }
        }
        else if (!playerMovement.facingRight)
        {
            transform.position = new Vector3(pTransform.localPosition.x - 0.2f, transform.position.y, transform.position.z);

            if (pTransform.position.y >= 0)
            {
                transform.localScale = new Vector3(0, 0, 1);
            }
            else if (pTransform.position.y > pTransformY)
            {
                transform.localScale = new Vector3(-pTransform.position.y, -pTransform.position.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
        }
    }
}
