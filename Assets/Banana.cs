using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    AudioSource src;
    public AudioClip pickupBananaSound;
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (pc != null && collider.gameObject.CompareTag("Player")) {
            pc.TakeDamage(-10);
            Destroy(gameObject);
        }

    }
}
