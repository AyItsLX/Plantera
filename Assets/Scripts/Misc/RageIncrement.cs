using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageIncrement : MonoBehaviour {

    public BossBehaviour bossBehaviour;

    public float curBossMultipler;
    public bool cornerDetected;

	void Start ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cornerDetected = true;
            curBossMultipler = bossBehaviour.rageMultiplier;
            bossBehaviour.rageMultiplier *= 7.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cornerDetected = false;
            bossBehaviour.rageMultiplier = curBossMultipler;
        }

    }
}
