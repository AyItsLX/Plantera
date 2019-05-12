using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Methods : MonoBehaviour {

    public PlayerMovement_v2 playerMovement; // Player control script
    public GameManager gameManager;

    public BossBehaviour bossBehaviour;

    public AudioClip[] Beep;
    public AudioSource BeepAudio;

    public void OnPlayerControl()
    {
        gameManager.runTimer = true;
        bossBehaviour.isInMenu = false;
        playerMovement.enabled = true;
    }

    public void _1()
    {
        BeepAudio.PlayOneShot(Beep[2]);
    }

    public void _2()
    {
        BeepAudio.PlayOneShot(Beep[1]);
    }

    public void _3()
    {
        BeepAudio.PlayOneShot(Beep[0]);
    }

    public void _Beep()
    {
        BeepAudio.PlayOneShot(Beep[3]);
    }
}
