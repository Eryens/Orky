using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float moveSpeed = 1;
    private Animator animator;
    public bool hasHit = false;

    public bool isBoss = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        MoveForward();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            if (((collider.tag == "Player" || collider.tag == "Dork") && animator != null) && !hasHit)
            {

                //Debug.Log("has hit is set to true");
                FindObjectOfType<AudioManager>().Play("EnemyDeath");
                //Debug.Log("champi dies");
                animator.SetTrigger("Die");
                StartCoroutine(DepopEnemy());
            }
        }
    }

    private IEnumerator DepopEnemy()
    {
        yield return null;
        hasHit = true;
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    private void MoveForward()
    {
        float moveHorizontal = moveSpeed * Time.deltaTime;
        moveHorizontal = -moveHorizontal;
        float moveVertical = 0;

        transform.Translate(new Vector3(moveHorizontal, moveVertical));
    }
}
