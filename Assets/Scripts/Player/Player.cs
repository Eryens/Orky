using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health = 3;
    public int healthLossPerHit = 1;

    private bool isBeingHit = false;

    public float pushBackforce = 600f;
    public float pushBackTime = 0.1f;

    public BoxCollider2D playerCollider;

    private Animator animator;

    public HealthDisplay healthDisplay;

    private void Awake()
    {

        healthDisplay = GetComponent<HealthDisplay>();
        animator = GetComponent<Animator>(); 

        if (animator == null)
        {
            Debug.LogError("Animator not found within player");
        }
        if (healthDisplay == null)
        {
            Debug.LogError("HealthDisplay script not found within player");
        }

        healthDisplay.displayedHealth = health;
        healthDisplay.numbOfTotalHearts = health;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Dork")
            OneShot();
        else if (collider.tag == "Enemy"  && isBeingHit == false)
        {
            bool hasEnemyHit = collider.GetComponent<EnemyMovement>().hasHit;
            if (!hasEnemyHit)
                GetHit(collider);
        }

        else if (collider.tag == "VictoryZone")
            GameManager.instance.Victory();
        else if (collider.tag == "GoToBattle")
            GameManager.instance.GoToBattle();
    }

    private void OneShot()
    {
        PlayerLoseHealth(health);
        animator.SetBool("Dead", true);
        GameManager.instance.GameOver();
    }

    private void GetHit(Collider2D collider)
    {
        FindObjectOfType<AudioManager>().Play("PlayerHit");
        isBeingHit = true;
        PlayerPushback(collider);
        PlayerLoseHealth(healthLossPerHit);
    }

    private void PlayHitSound()
    {
        // TBI
    }

    private void PlayerPushback(Collider2D collider)
    {
        Vector3 rel = gameObject.GetComponent<Transform>().position - collider.GetComponent<Transform>().position;
        rel.Normalize();
        Vector2 force = rel * pushBackforce;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        gameObject.GetComponent<PlayerMovement>().canMove = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(force);
        
        animator.SetBool("Hurt", isBeingHit);
        StartCoroutine(NotHitAnymore());
    }

    private void PlayerLoseHealth(int amount)
    {
        health -= amount;
        healthDisplay.displayedHealth = health;
        if (health <= 0)
        {
            animator.SetBool("Dead", true);
            GameManager.instance.GameOver();
        }
    }

    IEnumerator NotHitAnymore()
    {
        yield return new WaitForSeconds(pushBackTime);
        isBeingHit = false;
        gameObject.GetComponent<PlayerMovement>().canMove = true;
        animator.SetBool("Hurt", isBeingHit);
        // resets force
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}
}
