using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorkMovement : MonoBehaviour {

    public float moveSpeed = 1;
    //private Animator animator;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        float moveHorizontal = moveSpeed * Time.deltaTime;
        float moveVertical = 0;

        transform.Translate(new Vector3(moveHorizontal, moveVertical));
    }
}
