using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Characters;

public class RandomMovement : MonoBehaviour {

    public float stayStillTime = 0;
    public float moveTime = 0;
    public NonPlayableCharacter objectToMove;
    private CircleCollider2D agroCollider;
    private float timer = 0f;
    private float interval;
    Vector2 randomDirection;

    public void Start() {
        interval = stayStillTime + moveTime;
        agroCollider = objectToMove.agroCollider;
        randomDirection = GetRandomDirection(-5,5,-5,5);
    }

    public void FixedUpdate() {
        if (!objectToMove.engaged) {
            timer += Time.deltaTime;
           
            if (timer <= moveTime) {
                // Try move was making it so that BlueMonkeys would stop
                // movement once their trigger collider hits anothers box
                // collider. Disabling for now
                //if(objectToMove.TryMove(randomDirection)) {
                    Vector2 currentPosition = objectToMove.rb.position;
                    Vector2 newPosition = currentPosition + randomDirection * objectToMove.movementSpeed * Time.deltaTime;
                    objectToMove.SetRbPosition(newPosition);
                // } else { 
                //     Debug.Log("TryMove issue");
                // }

            } else if (moveTime < timer && timer <= interval) {
                objectToMove.rb.velocity = Vector2.zero;
            } else {
                timer = 0f;
                randomDirection = GetRandomDirection(-5,5,-5,5);
            }  // Ending bracket of OUTER if
        } else {
            objectToMove.Attack();
        }  // Ending bracket of OUTER OUTER if
    }  // Ending bracket of function FixedUpdate

    public Vector2 GetRandomDirection(float minX, float maxX, float minY, float maxY) {
    float randomX = Random.Range(minX, maxX);
    float randomY = Random.Range(minY, maxY);
    return new Vector2(randomX, randomY).normalized;
    }  // Ending bracket of function SetRandomDirection

}  // Ending bracket of class RandomMovement