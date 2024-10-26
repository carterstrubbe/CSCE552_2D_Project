using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Characters;

public class AnimationPlayer : MonoBehaviour {

    public NonPlayableCharacter objectToAnimate;

    private void FixedUpdate() {
        
        if (objectToAnimate.attackingActive) {
            if (objectToAnimate.direction == 0) {
                objectToAnimate.SetAnimAttack(false);
                objectToAnimate.SetAnimLeft(false);
                objectToAnimate.SetAnimUp(false);
                objectToAnimate.SetAnimDown(false);
                objectToAnimate.SetAnimRight(false);
            }
            objectToAnimate.SetAnimAttack(true);
            
        } else {
            if (objectToAnimate.direction == 1) {
                objectToAnimate.SetAnimLeft(true);
                objectToAnimate.SetAnimAttack(false);
                objectToAnimate.SetAnimRight(false);
                objectToAnimate.SetAnimUp(false);
                objectToAnimate.SetAnimDown(false);
            } else if (objectToAnimate.direction == 2) {
                objectToAnimate.SetAnimRight(true);
                objectToAnimate.SetAnimAttack(false);
                objectToAnimate.SetAnimLeft(false);
                objectToAnimate.SetAnimUp(false);
                objectToAnimate.SetAnimDown(false);
            } else if (objectToAnimate.direction == 3) {
                objectToAnimate.SetAnimUp(true);
                objectToAnimate.SetAnimAttack(false);
                objectToAnimate.SetAnimLeft(false);
                objectToAnimate.SetAnimRight(false);
                objectToAnimate.SetAnimDown(false);
            } else if (objectToAnimate.direction == 4) {
                objectToAnimate.SetAnimDown(true);
                objectToAnimate.SetAnimAttack(false);
                objectToAnimate.SetAnimLeft(false);
                objectToAnimate.SetAnimUp(false);
                objectToAnimate.SetAnimRight(false);
            } else if (objectToAnimate.direction == 0) {
                objectToAnimate.SetAnimAttack(false);
                objectToAnimate.SetAnimLeft(false);
                objectToAnimate.SetAnimUp(false);
                objectToAnimate.SetAnimDown(false);
                objectToAnimate.SetAnimRight(false);
            }
        }
    }  // Ending bracket of FixedUpdate

}  // Ending bracket of class AnimationPlayer