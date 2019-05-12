using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTrigger : MonoBehaviour {

    [Header("Amount to Push Y")]
    public float pushDownBy = 20f;
    public float prevSmallCloudNum;
    public float prevBigCloudNum;

    void Start ()
    {
        prevSmallCloudNum = Random.Range(-23f, 23f);
        prevBigCloudNum = Random.Range(-23, 23f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Cloud1_1" || other.gameObject.name == "Cloud1_2" || other.gameObject.name == "Cloud1_3" || other.gameObject.name == "Cloud1_4" 
            ||
            other.gameObject.name == "Cloud2_1" || other.gameObject.name == "Cloud2_2" || other.gameObject.name == "Cloud2_3" || other.gameObject.name == "Cloud2_4")
        {
            Vector3 tempVec = other.gameObject.transform.position;

            if ((other.gameObject.name == "Cloud2_1" || other.gameObject.name == "Cloud2_2" || other.gameObject.name == "Cloud2_3" || other.gameObject.name == "Cloud2_4")
                && prevSmallCloudNum < 0)
            {
                prevSmallCloudNum = Random.Range(5f, 20f);
                other.gameObject.transform.position = new Vector3(prevSmallCloudNum, tempVec.y - pushDownBy, tempVec.z);
            }
            else if ((other.gameObject.name == "Cloud2_1" || other.gameObject.name == "Cloud2_2" || other.gameObject.name == "Cloud2_3" || other.gameObject.name == "Cloud2_4") 
                && prevSmallCloudNum > 0)
            {
                prevSmallCloudNum = Random.Range(-20f, -5f);
                other.gameObject.transform.position = new Vector3(prevSmallCloudNum, tempVec.y - pushDownBy, tempVec.z);
            }

            if ((other.gameObject.name == "Cloud1_1" || other.gameObject.name == "Cloud1_2" || other.gameObject.name == "Cloud1_3" || other.gameObject.name == "Cloud1_4")
                && prevBigCloudNum < 0)
            {
                prevBigCloudNum = Random.Range(7f, 15f);
                other.gameObject.transform.position = new Vector3(prevBigCloudNum, tempVec.y - pushDownBy, tempVec.z);

            }
            else if ((other.gameObject.name == "Cloud1_1" || other.gameObject.name == "Cloud1_2" || other.gameObject.name == "Cloud1_3" || other.gameObject.name == "Cloud1_4")
                && prevBigCloudNum > 0)
            {
                prevBigCloudNum = Random.Range(-15f, -7f);
                other.gameObject.transform.position = new Vector3(prevBigCloudNum, tempVec.y - pushDownBy, tempVec.z);
            }
        }

        if (other.gameObject.name == "Cloud1(Destroy)" || other.gameObject.name == "Cloud2(Destroy)")
        {
            Destroy(other.gameObject);
        }
    }
}
