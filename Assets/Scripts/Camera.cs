using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRb.transform.position.x >= startPos.x)
            gameObject.transform.position = new Vector3(playerRb.transform.position.x,transform.position.y, -10f);
    }
}
