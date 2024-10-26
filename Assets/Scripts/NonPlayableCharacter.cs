// Copyright 2023 Carter Strubbe
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters {

public class NonPlayableCharacter : MonoBehaviour {

    public GameObject banana;

    // Stat Variables
    public int hp { get; set; }
    public float movementSpeed { get; set; }
    protected float damage { get; set; }

    // Characteristic Variables
    protected bool isHostile { get; set; }
    protected bool isRecruitable { get; set; }

    // Status Variables
    public bool engaged { get; set; }
    public bool attackingActive { get; set; }

    // Animation Variables
    protected bool isMovingDown { get; set; }
    protected bool isMovingLeft { get; set; }
    protected bool isMovingRight { get; set; }
    protected bool isMovingUp { get; set; }

    // Movement Variables

    // 0 = static 1 = left, 2 = right, 3 = up, 4 = down
    public int direction { get; set; }
    protected Vector2 lastPosition;

    // Editor Variables
    public GameObject target { get; set; }
    public Rigidbody2D rb { get; set; }
    public CircleCollider2D agroCollider { get; set; }
    public Animator anim { get; set; }
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    public NonPlayableCharacter() {
        movementFilter = new ContactFilter2D();
        rb = new Rigidbody2D();
        anim = new Animator();
        engaged = false;
    }  // Ending bracket of constructor

    private void Start() {
       // rb = this.GetComponent();
       lastPosition = transform.position;
    }

    // -- Update and Movement ----------------------------------------------------------------- // 

    private void Update() {
        if (hp <= 0) {
            var i = Random.Range(0.0f, 10.0f);
            Debug.Log(i);
            if (i < 1) {
                Instantiate(banana, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public virtual void FixedUpdate() {
        direction = GetDirection();
        lastPosition = transform.position;
    }  // Ending bracket of function update

    public bool TryMove(Vector2 direction) {
        // Check for potential collisions
        
        int count = rb.Cast(direction, movementFilter, 
            GetCastCollisions(), movementSpeed * Time.fixedDeltaTime + collisionOffset);
            
            if(count == 0) {
                rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
    }  // Ending bracket of function TryMove

    public virtual void Attack() {

    }

    protected int GetDirection() {
        int rv = 0;
        float changeX = Mathf.Abs(lastPosition.x - transform.position.x);
        float changeY = Mathf.Abs(lastPosition.y - transform.position.y);

        if (transform.position.x > lastPosition.x && changeX > changeY) {
            rv = 2;
        } else if (transform.position.x < lastPosition.x && changeX > changeY) {
            rv = 1;
        } else if (transform.position.y > lastPosition.y && changeY > changeX) {
            rv = 3;
        } else if (transform.position.y < lastPosition.y && changeY > changeX) {
            rv = 4;
        } else if (changeX == 0 && changeY == 0) {
            rv = 0;
        }

        return rv;
    }  // Ending bracket of function GetDirection

    // -- Getters and Setters --------------------------------- // 

    public void SetAnimLeft(bool isLeft) {
        anim.SetBool("IsMovingLeft", isLeft);
    }  // Ending bracket of function SetAnimLeft

    public void SetAnimRight(bool isRight) {
        anim.SetBool("IsMovingRight", isRight);
    }  // Ending bracket of function SetAnimRight

    public void SetAnimUp(bool isUp) {
        anim.SetBool("IsMovingUp", isUp);
    }  // Ending bracket of function SetAnimUp

    public void SetAnimDown(bool isDown) {
        anim.SetBool("IsMovingDown", isDown);
    }  // Ending bracket of function SetAnimDown

    public void SetAnimAttack(bool isAttacking) {
        anim.SetBool("IsAttacking", isAttacking);
    }  // Ending bracket of function SetAnimAttack

    // -- Editor --------------------------------------- //

    public List<RaycastHit2D> GetCastCollisions() {
        return this.castCollisions;
    }  // Ending bracket of function GetCastCollisions

    public void SetCastCollisions(List<RaycastHit2D> newCollisions) {
        castCollisions = newCollisions;
    }

    public void SetRbPosition(Vector2 newPosition) {
        rb.MovePosition(newPosition);
    }  // Ending bracket of function SetRb

    // -- Events ---------------------------------------- // 

}  // Ending bracket of class NonPlayableCharacter

}  // Ending bracket of namespace Characters

