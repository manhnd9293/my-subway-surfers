using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public GameObject originalTrain;
    public GameObject originalHurdle;
    public GameObject originalHouse;
    private GameObject lastHouse;

    public bool gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
        InvokeRepeating("spawnTrains",  1, 1.5f );
        InvokeRepeating("spawnHurdles",  1, 1.5f );
        InvokeRepeating("spawnHouses",  0, 3.5f );
    }

    void spawnTrains()
    {
        if (gameOver)
        {
            return;
        }
        var xPos = Random.Range(-1, 2) * 5;
       
        
        var newTrain = Instantiate(originalTrain, new Vector3(xPos, 2, 100), originalTrain.transform.rotation);
    }
    
    void spawnHurdles()
    {
        if (gameOver)
        {
            return;
        }
        var xPos = Random.Range(-1, 2) * 5;
        var newHurdle = Instantiate(originalHurdle, new Vector3(xPos, 0.7f, 100), originalHurdle.transform.rotation);
    }

    void spawnHouses()
    {
        for (int i = 0; i < 10; i++)
        {
            var nextPosZ = lastHouse != null ? lastHouse.transform.position.z + lastHouse.transform.localScale.z / 2.0f : 0;
            var newHouse = Instantiate(originalHouse, new Vector3(-8.5f, 2.47f, nextPosZ), originalHouse.transform.rotation);
            // Instantiate(originalHouse, new Vector3(8.5f, 2.47f, nextPosZ), new Quaternion(0 , 180, 0, 1));
            lastHouse = newHouse;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
