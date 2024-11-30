using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float totalTurnTimeInMilis = 200;
    private float averageTurnSpeed;
    private float turnPosX;
    private DateTime turnTime;
    private bool turning = false;
    private int turnDir = 0;
    private List<int> turnJobs;
    private Rigidbody playerRb;
    public int jumpForce;
    private bool jumping = false;
    private GameManager gameManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity = new Vector3(0, -30.0F, 0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        turnJobs = new List<int>();
        playerRb = GetComponent<Rigidbody>();
        averageTurnSpeed = 5.0f * 1000 / totalTurnTimeInMilis;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turnJobs.Add(-1);
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            turnJobs.Add(1);
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && !jumping)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumping = true;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && jumping)
        {
            playerRb.AddForce(Vector3.down * 2 * jumpForce , ForceMode.Impulse);
        }
        
        

        if (turnJobs.Count > 0 && !turning)
        {
            
            turnTime = DateTime.Now;
            turnDir = turnJobs[0];
            turnPosX = transform.position.x;
            turnJobs.RemoveAt(0);
            turning = true;
        }

        if (turning)
        {
            var now = DateTime.Now;
            var timeDif = now.Subtract(turnTime).TotalMilliseconds;
            if (timeDif < totalTurnTimeInMilis)
            {
                var turnVector = turnDir == 1 ? Vector3.right : Vector3.left;
                transform.Translate(turnVector * averageTurnSpeed * Time.deltaTime);
            }
            else
            {
                var expectX = turnPosX + 5.0f * turnDir;
                transform.position = new Vector3(expectX, transform.position.y, transform.position.z);
                turnDir = 0;
                turning = false;
            }
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Train") || other.gameObject.CompareTag("Hurdle") || other.gameObject.CompareTag("House"))
        {
            gameManager.gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
