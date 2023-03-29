using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform myTransform;

    private void FixedUpdate()
    {
        myTransform.position = new Vector3(player.transform.position.x, myTransform.position.y, myTransform.position.z);
    }
}