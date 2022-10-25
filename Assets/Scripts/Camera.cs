using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRb.transform.position.y >= 0f && playerRb.transform.position.x >= -1.28f)
            gameObject.transform.position = new Vector3(playerRb.transform.position.x,transform.position.y, -10f);
        
    }
}
