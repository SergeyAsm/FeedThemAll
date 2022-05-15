using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour
{
    private GameObject followTo;

    public GameObject FollowTo { get => followTo; set { followTo = value; UpdateTransform(); } }

    private void Update()
    {
        if (followTo)
        {
            UpdateTransform();
        }
    }
    private void UpdateTransform()
    {
        transform.position = followTo.transform.position;
    }

}
