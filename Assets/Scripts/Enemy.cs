// Copyright 2023 Carter Strubbe
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters {

public class Enemy : NonPlayableCharacter {

   // HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    public Enemy() : base() {
    }  // Ending bracket of constructor

    public void OnTriggerEnter2D(Collider2D collider) {
        
        if (!engaged) {
            if(collider.gameObject.name == "Player" || collider.gameObject.tag == "NPC") {
                engaged = true;
                target = collider.gameObject;
            }  // Ending bracket of if
        }
        
    }  // Ending bracket of method OnTriggerEnter2D

    public void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
            engaged = false;
            target = null;
        }
    }  // Ending bracket of method OnTriggerExit2D

    public override void Attack() {

    }

    public override void FixedUpdate() {
        
    }


};  // Ending bracket of class Enemy
}  // Ending bracket of namespace Characters