using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    [SerializeField]
    private Transform player;

    private Vector3 offset = new Vector3(0,0,-10f);

    [SerializeField]
    private float xOffset = 5f;

    private void Awake()
    {
        offset = new Vector3(offset.x + xOffset, offset.y, offset.z);
    }

    // Update is called once per frame
    void Update () {
        transform.position = player.position + offset;	
	}
}
