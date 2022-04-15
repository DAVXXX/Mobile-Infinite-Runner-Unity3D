using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float jumpForce;
    public bool isOnGround = true;

    public bool hasPowerup;


    private const int MAX_JUMPS = 2;
    private int currentJump = 0;

    public float fallMultipler = 4f;
    public float lowJumpMultipler = 4f;

    public float rollSpeed = 30f;

 

    public ParticleSystem dirt;
    public ParticleSystem explostion;

    public GameObject Enemy;
    public GameObject PowerUp;
    public GameObject PowerUpOverlay;
    Rigidbody playerRB;

 

    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();



        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector3.up * Physics.gravity.y * (fallMultipler - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y > 0)
        {
            playerRB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;

        }

        if ((gameManager.isGameActive == true))
        {
            transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime);

        }


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            currentJump = 0;
        }


        if ((collision.gameObject.CompareTag("Enemy")) && hasPowerup && collision.gameObject.tag == "Enemy")
        {
            gameManager.UpdateScore(100);
            gameManager.ExplodeSFX();
            Destroy(collision.gameObject);
            explosion();

        }
        else if (collision.gameObject.CompareTag("Enemy") && !hasPowerup && collision.gameObject.tag == "Enemy")
        {

            gameManager.GameOver();
            explosion();
            Destroy(collision.gameObject);
            Debug.Log("Game Over");

            Destroy(gameObject);

        }
     
    }

    public void playerJump()
    {

        if (isOnGround)
        {
            //transform.Translate(Vector3.up * Time.deltaTime * jumpForce);
            playerRB.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            isOnGround = false;
        }


        if ((isOnGround || MAX_JUMPS > currentJump))
         playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           isOnGround = false;
           currentJump++;
        
        
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            gameManager.PowerUpSFX();
           hasPowerup = true;
            Destroy(other.gameObject);
            PowerUpOverlay.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
         
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        PowerUpOverlay.gameObject.SetActive(false);
    }


    public void explosion()
    {
        ParticleSystem explosion = Instantiate(explostion, transform.position, transform.rotation) as ParticleSystem;
    
        explosion.Play();
        
    }

}
