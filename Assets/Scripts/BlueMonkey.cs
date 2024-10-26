using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class BlueMonkey : Enemy {

    private GameObject player;
    public GameObject projectile;
    private float timer;
    private float projectileTimer;
    private float attackDelay;
    private float attackRange;
    private float chaseRange;
    private float projectileInterval = 3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        hp = 5;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agroCollider = GetComponent<CircleCollider2D>();
        attackingActive = false;
        engaged = false;
        damage = 5;
        movementSpeed = 1f;
        timer = 0f;
        projectileTimer = 0f;
        attackDelay = 1.5f;
        attackRange = agroCollider.radius - agroCollider.radius/5;
        chaseRange = agroCollider.radius - agroCollider.radius/7;
    }

    public override void FixedUpdate() {

        direction = GetDirection();
        lastPosition = transform.position;

        if (!engaged) {
            // TRY TO MOVE TO PLAYER
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x, dir.y) * movementSpeed;
        } else {
            Attack();
        }
    }

    public override void Attack() {
        Vector3 targetPosition = target.transform.position;
        Vector2 newPosition;
        double distanceToTarget = 
        Math.Sqrt(Math.Pow(transform.position.x - targetPosition.x, 2)
         + Math.Pow(transform.position.y - targetPosition.y, 2));

         if (timer > attackDelay) {
            attackingActive = false;
            timer = 0f;
         }

        if (distanceToTarget >= attackRange && distanceToTarget < chaseRange || attackingActive == true) {
            attackingActive = true;
            timer += Time.deltaTime;
            // Throw poop
            if (projectileTimer == 0f) {
                projectileTimer += Time.deltaTime;
                Vector3 relativePosition = targetPosition - transform.position;
                Debug.Log("spawningProjectile");
                GameObject projectileInstance = Instantiate(projectile, (Vector2)transform.position + GetTargetDirection(target).normalized/6, Quaternion.identity);
                projectileInstance.transform.rotation = Quaternion.LookRotation(Vector3.forward,relativePosition);
            } else if (projectileTimer <= projectileInterval) {
                projectileTimer += Time.deltaTime;
            } else {
                projectileTimer = 0f;
            }
            
        } else if (distanceToTarget < attackRange && attackingActive == false) {
            // Run from target and create distance to throw poop
            newPosition = rb.position + GetTargetDirection(target).normalized * -1 * movementSpeed * Time.deltaTime;
            SetRbPosition(newPosition);
        } else if (distanceToTarget > chaseRange) {
            // Chase target
            newPosition = rb.position + GetTargetDirection(target).normalized * movementSpeed * Time.deltaTime;
            SetRbPosition(newPosition);
        }

        // Expand collider and add another range that signals to chase player if he is running away
    }

    private Vector2 GetTargetDirection(GameObject target) {
        Vector2 targetPosition = target.transform.position;

        float changeX = targetPosition.x - transform.position.x;
        float changeY = targetPosition.y - transform.position.y;

        return new Vector2(changeX, changeY);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "arrow") {
            hp -= 1;
        }
    }

    // Update is called once per frame
   

    
}  // Ending bracket of class BlueMonkey
