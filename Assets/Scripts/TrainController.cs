using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float speed = 30.0f;

    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        transform.Translate( Vector3.back * Time.deltaTime * speed);
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }
}
