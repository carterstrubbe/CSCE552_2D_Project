using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyData : ScriptableObject {

    public string title { get; set; }

    public int hp { get; set; }
    public float movementSpeed { get; set; }
    public float damage { get; set; }
    public float knockbackStrength { get; set; }

    public Rigidbody2D rb { get; set; }
    public CircleCollider2D agroCollider { get; set; }
    public bool isEngaged { get; set; }
    public LinkedList<GameObject> targets { get; set; }

}