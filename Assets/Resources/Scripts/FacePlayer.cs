using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    public Transform playerPosition;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (playerPosition == isActiveAndEnabled)
        {
            Vector3 direction = playerPosition.position - transform.position;

            transform.forward = direction;
        }
    }
}
