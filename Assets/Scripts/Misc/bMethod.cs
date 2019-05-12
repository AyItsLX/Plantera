using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bMethod : MonoBehaviour {

    public AudioClip[] roarClip;
    AudioSource roarAudio;

	void Start ()
    {
        if (gameObject.name == "Boss_Body")
        {
            roarAudio = GetComponent<AudioSource>();
        }
	}

    public void SwingShake()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(1, 2, 0.1f, 1);
    }

    public void ShakeScreen()
    {
        roarAudio.clip = roarClip[0];
        roarAudio.Play();
        EZCameraShake.CameraShaker.Instance.ShakeOnce(2, 3, 0.1f, 2);
    }

    public void ShakeScreen1()
    {
        roarAudio.clip = roarClip[1];
        roarAudio.Play();
        EZCameraShake.CameraShaker.Instance.ShakeOnce(2, 3, 0.1f, 2);
    }

    public void ShakeScreen2()
    {
        roarAudio.clip = roarClip[2];
        roarAudio.Play();
        EZCameraShake.CameraShaker.Instance.ShakeOnce(3, 3, 0.1f, 3);
    }
}
