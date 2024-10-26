using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class GreenMonkey : Enemy {
    private GameObject player;
    private const float attackCooldownConst = 1.0f;
    private float attackCooldown = attackCooldownConst;
    private PlayerController pc;
    private bool coolingDown = false;
    private bool damaging = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        target = player;
        hp = 15;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackingActive = false;
        engaged = false;
        damage = 10;
        movementSpeed = 0.7f;
    }

    public override void FixedUpdate() {

        direction = GetDirection();
        lastPosition = transform.position;

        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(dir.x, dir.y) * movementSpeed;

        if (damaging && pc != null && !coolingDown) {
            coolingDown = true;
            pc.TakeDamage((int)damage); 
        }

        if (coolingDown) {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0.0f) {
                attackCooldown = attackCooldownConst;
                coolingDown = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("arrow")) {
            hp -= 1;
        } else if (collision.gameObject.CompareTag("Player")) {
            damaging = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            damaging = false;
        }
    }
}
