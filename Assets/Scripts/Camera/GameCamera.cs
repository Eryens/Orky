using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    [SerializeField]
    private Transform refDork;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float xOffset;

    private Vector3 calculatedPos;
    public float horzExtent;

    // Update is called once per frame
    void Update () {
        calculatedPos.x = refDork.position.x + xOffset;
        calculatedPos.y = player.position.y;
        calculatedPos.z = -10f;
        transform.position = calculatedPos;

        CheckForOutOfBoundaryPlayer();
    }

    private void CheckForOutOfBoundaryPlayer()
    {
        horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        // If player goes out of boundary
        if (player.position.x > transform.position.x + horzExtent)
        {
            GameManager.instance.SpecialGameOver();
        }
    }
}
