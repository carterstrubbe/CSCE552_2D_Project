using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    private void Start() {
        if (gameObject.tag == "arrow") {
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    private void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject); 
    }
}  // Ending bracket of class Projectile
