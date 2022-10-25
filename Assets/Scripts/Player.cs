using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Initialized in the Unity inspector
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float invincibilityFramesTime;

    [SerializeField] public int maxHealth = 5;
    [SerializeField] public int maxPew = 5;

    [SerializeField] public GameObject pewPew;
    [SerializeField] public TextMeshProUGUI pewText;
    [SerializeField] public TextMeshProUGUI deathText;

    [SerializeField] public LayerMask wall;

    [SerializeField] public Image[] hearts;

    //Initialized in the script
    private GameManager gameManager;
    private Rigidbody2D playerRb;
    private Vector3 posBeforeFall;

    private int health;
    private int currentPew;
    private int facing = 1;

    private bool canMove = true;
    private bool hasInvincibiltyFrames = false;

    private string levelName;

    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        gameManager = FindObjectOfType<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();

        health = maxHealth;
        currentPew = maxPew;

        pewText.text = currentPew + " / " + maxPew;

        Scene currentScene = SceneManager.GetActiveScene();
        levelName = currentScene.name;

        pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckIfDead())
        {
            Move();
            Jump();
            Shoot();
        }
        else
            playerRb.velocity = Vector2.zero;
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
            facing = horizontalInput < 0 ? -1 : 1;

        Vector2 start = new Vector2(transform.position.x, transform.position.y - 0.514f);
        Vector2 end = start + new Vector2(horizontalInput, 0f);
        RaycastHit2D hit = Physics2D.Linecast(start, end, wall);

        if (hit.transform == null && canMove)
        {
            playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);
        }

        if (CheckIfGrounded())
        {
            posBeforeFall = transform.position;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerRb.velocity.y == 0)
        {
            playerRb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentPew > 0)
        {
            float dir = facing;
            float side = 0.75f * dir;

            GameObject instance = Instantiate(pewPew, new Vector3(transform.position.x + side, transform.position.y), Quaternion.identity);
            instance.GetComponent<PewPew>().dir = dir;

            currentPew--;
            pewText.text = currentPew + " / " + maxPew;
        }
    }

    private bool CheckIfPauseMenuEnabled()
    {
        //return pauseMenu.;
        return true;
    }

    private bool CheckIfDead()
    {
        if (health <= 0)
        {
            deathText.gameObject.SetActive(true);
        }
        return health <= 0;
    }

    private bool CheckIfGrounded()
    {
        float xDir = facing > 0 ? 0.3f : -0.3f;

        Vector2 start = new Vector2(transform.position.x + xDir, transform.position.y - 0.514f);
        Vector2 end = start + new Vector2(0f, -0.1f);
        RaycastHit2D hit = Physics2D.Linecast(start, end, wall);

        return hit.transform != null;
    }

    private void GotHit()
    {
        StartCoroutine(InvincibilityFrames());
        health--;
        hearts[health].gameObject.SetActive(false);
    }

    IEnumerator InvincibilityFrames()
    {
        hasInvincibiltyFrames = true;
        canMove = false;

        yield return new WaitForSeconds(invincibilityFramesTime);
        
        hasInvincibiltyFrames = false;
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && !hasInvincibiltyFrames)
        {
            GotHit();

            Vector2 left = Vector2.left * 300 + Vector2.up * 300;
            Vector2 right = Vector2.right * 300 + Vector2.up * 300;
            
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce((collision.transform.position.x - transform.position.x) > 0 ? left : right);
        }

        if (collision.gameObject.tag.Equals("Health"))
        {
            if (health < maxHealth)
            {
                hearts[health].gameObject.SetActive(true);
                health++;
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.tag.Equals("Ammo"))
        {
            if (currentPew < maxPew)
            {
                currentPew = maxPew;
                collision.gameObject.SetActive(false);
                pewText.text = currentPew + " / " + maxPew;
            }
        }

        if (collision.gameObject.tag.Equals("Bad Ground"))
        {
            if(!hasInvincibiltyFrames) GotHit();
            transform.position = posBeforeFall;
        }

        if (collision.gameObject.tag.Equals("Goal"))
        {
            if (levelName.Equals("Tutorial"))
            {
                gameManager.SendMessage("StartStartMenu");
            }

            else if (gameManager != null) //For running without menu scenes
            {
                Debug.Log(levelName);
                gameManager.SendMessage("LevelCompleted", levelName);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            canMove = true;
        }
    }
}
