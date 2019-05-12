using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public float xMin = -8;
    public float xMax = 48;
    public float yMin = 0;
    public float yMax = 100;

    Transform t;

    // Use this for initialization
    void Awake()
    {
        t = transform;

	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        float x = Mathf.Clamp(target.position.x, xMin, xMax);
        float y = Mathf.Clamp(target.position.y, yMin, yMax);
        t.position = new Vector3(x, y, t.position.z);
    }

}
