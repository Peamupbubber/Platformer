using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int health = 5;
    [SerializeField] public float velocity = -2.5f;
    
    [SerializeField] public LayerMask wall;

    [SerializeField] public GameObject[] potions;

    private Rigidbody2D enemyRb;

    private float edge = 0.514f; //This number represents the edge of the blocks
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfDead();
    }

    private void Move() {
        float xDir = velocity > 0 ? edge : -edge;

        Vector2 start = new Vector2(transform.position.x + xDir, transform.position.y - edge);
        Vector2 end = start + new Vector2(0f, -0.1f);

        RaycastHit2D hit = Physics2D.Linecast(start, end, wall);

        if (hit.transform == null)
        {
            velocity *= -1;
        }

        enemyRb.velocity = new Vector2(velocity, enemyRb.velocity.y);
    }

    private void CheckIfDead() {
        if (health <= 0) {
            //Spawn a random potion on death
            int rand = (int)Random.Range(0f, potions.Length);
            Instantiate(potions[rand], gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PewPew")) {
            health--;
        }
    }
}
