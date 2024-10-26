// Copyright 2023 Carter Strubbe

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// NOTES FOR ADDITION: Make the player do more damage and be more resistant to damage when not moving and attacking vs moving and attacking

public class PlayerController : MonoBehaviour
{

  
    public UiManager uiManager;
    public Camera mainCam;
    public AudioSource src;
    public AudioClip arrowSound;
    public AudioClip bananaSound;
    public AudioClip pooSound;

    private Vector2 mousePos;
    public static PlayerController instance;
    // 0 = static 1 = left, 2 = right, 3 = up, 4 = down
    public int direction { get; set; }
    public int lastDirection { get; set; }
    public bool attackTriggered { get; set; }

    public GameObject arrow;
    public float moveSpeed = 0.05f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;

    Rigidbody2D rb;

    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    public float damage;

    public int hp;
    private const int maxHp = 100;
    public Image healthBar;

    private bool isDead;

    //private NonPlayableCharacter[] followers;

    // Start is called before the first frame update
    void Start()
    {
        attackTriggered = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hp = 100;
        instance = this;
    } // Ending bracket of function Start

    void Update() {
        if (hp <= 0 && !isDead) {
            isDead = true;
            uiManager.GameOver();
        }
    }

    private void FixedUpdate() {

       // Debug.Log(GetHP());
       
        if(canMove) {
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero) {

            bool success = TryMove(movementInput);

            if(!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
                
                    if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                    } // Ending bracket of INNER INNER if

            } // Ending bracket of INNER if

                if(movementInput.x > 0) {
                    animator.SetBool("isMovingRight", success);
                    direction = 2;
                    lastDirection = 2;
                } else {
                    animator.SetBool("isMovingRight", false);
                }
                if(movementInput.x < 0) {
                    animator.SetBool("isMovingLeft", success);
                    direction = 1;
                    lastDirection = 1;
                } else {
                    animator.SetBool("isMovingLeft", false);
                }

                if(movementInput.y > 0) {
                    animator.SetBool("isMovingUp", success);
                    direction = 3;
                    lastDirection = 3;
                } else {
                    animator.SetBool("isMovingUp", false);
                }
                if(movementInput.y < 0) {
                    animator.SetBool("isMovingDown", success);
                    direction = 4;
                    lastDirection = 4;
                } else {
                    animator.SetBool("isMovingDown", false);
                }
            } else {
                animator.SetBool("isMovingLeft", false);
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
                direction = 0;
            } // Ending bracket of OUTER if

        }
        
    } // Ending bracket of fuction FixedUpdate

    private bool TryMove(Vector2 direction) {
        // Check for potential collisions
        // foreach(RaycastHit2D hit in castCollisions) {
        //     if(hit.collider.isTrigger) {
        //         castCollisions.Remove(hit);
        //         Debug.Log("Soupy");
        //     }
        // }
        // if(castCollisions.contains(RaycastHit2D.collider.isTrigger)) {
        //     castCollisions.Remove(RaycastHit2D.collider.isTrigger);
        //}
        int count = rb.Cast(direction, movementFilter, 
            castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if(count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }

    }  // Ending bracket of method TryMove

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("MonkeyPoo")) {
            TakeDamage(5);
            src.clip = pooSound;
            src.Play();
        } 
        
    }  // Ending bracket of method OnCollisionEnter2D

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Banana")) {
            src.clip = bananaSound;
            src.Play();
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {

        

        Debug.Log("Fired");
        SpawnArrow();
        
    }

    private void SpawnArrow() {
        Debug.Log("Spawning arrow");

        src.clip = arrowSound;
        src.Play();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - rb.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        
        // Vector3 offset;
        // if (lookDirection.x > 0) {
        //     if (lookDirection.y > 0) {
        //         offset = new Vector3(0.1f, 0.1f, 0f);
        //     } else {
        //         offset = new Vector3(0.1f, -0.2f, 0f);
        //     }
        // } else {
        //     if (lookDirection.y > 0) {
        //         offset = new Vector3(-0.1f, 0.1f, 0f);
        //     } else {
        //         offset = new Vector3(-0.1f, -0.2f, 0f);
        //     }
        // }
        
        Debug.Log(lookDirection);
        GameObject spawnedArrow = Instantiate(arrow, transform.position, transform.rotation);
        spawnedArrow.GetComponent<Rigidbody2D>().rotation = lookAngle;
        //spawnedArrow.GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    public void TakeDamage(int amt) {
        if (hp-amt > 100) {
            hp = 100;
        } else {
            hp -= amt;
        }
        
        float tempHp = hp;
        float tempMax = maxHp;
        healthBar.fillAmount = tempHp / tempMax;

    }
    public void DecreaseHP(object damageTaken) {
        if (damageTaken is float) {
            int damageT = (int) damageTaken;
            hp -= damageT;
        }
            
    }

    // public void addFollower(NonPlayableCharacter followerToAdd) {
    //     for (int i = 0; i < followers.length; ++i) {
    //         if (followers[i] == null) {
    //             followers[i] = followerToAdd;
    //         } 
    //     }
    // }

}  // Ending bracket of class PlayerController
