using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    [SerializeField] public float projectileSpeed;
    [HideInInspector] public float dir;

    private int distance = 0;
 
    // Update is called once per frame
    void Update()
    {
        Move(dir);

        if (distance > 200)
            Destroy(gameObject);
    }

    private void Move(float dir) {
        transform.position += new Vector3(dir, 0) * projectileSpeed * Time.deltaTime;
        distance++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Health") || collision.tag.Equals("Ammo"))
        {
            //Trigger breaking animation
            collision.GetComponent<Animator>().SetTrigger("Hit");

            //Make it fall and hit the ground
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

            //Layering for visual effect and collisions
            collision.gameObject.layer = 11;
            collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Broken";
        }
        Destroy(gameObject);
    }
}
