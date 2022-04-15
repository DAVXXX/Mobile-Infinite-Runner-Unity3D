using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float speed;
    public float leftBound = -10f;
   // private float timeDelay = 2;
    private float lastIncreased;

    

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       // destination = transform.position;

    }



    // Update is called once per frame
    void FixedUpdate()
    {


        //////
        // if (Time.time > timeDelay + lastIncreased)
        // {
        //lastIncreased = Time.time;
        //speed += 10f;
        //spawnRate -= 0.1f;
        // }
        //////


        


        if ((gameManager.isGameActive == true || gameObject.CompareTag("Enemy") && gameObject.CompareTag("Ground")))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            //transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        }



        if ((gameManager.isGameActive == true && gameObject.CompareTag("Powerup")))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            //transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        }


        if (transform.position.x < leftBound && gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, .4f);
            gameManager.UpdateScore(10);
            //Need to fix the scoring here and in GameManager

        }


        if (transform.position.x < leftBound && gameObject.CompareTag("Powerup"))
        {
            Destroy(gameObject, .4f);

        }


    }



   


}
