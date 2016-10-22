using UnityEngine;
using System.Collections;

[System.Serializable]
public class Speed {
    public float minSpeed, maxSpeed;
}

[System.Serializable]
public class Boundary {
    public float xMin, xMax, yMin, yMax;
}


public class EnemyController : MonoBehaviour {
    // PUBLIC INSTANCE VARIABLES
    public Speed speed;
    public Boundary boundary;


    // PRIVATE INSTANCE VARIABLES
    private float _CurrentSpeed;
    private float _CurrentDrift;

    private PlayerController playerController;
    private GameController gameController;

    // Use this for initialization
    void Start() {

        playerController = GameObject.FindObjectOfType<PlayerController>();
        gameController = GameObject.FindObjectOfType<GameController>();

        this._Reset();
    }

    // Update is called once per frame
    void Update() {
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
        currentPosition.y -= this._CurrentSpeed;
        gameObject.GetComponent<Transform>().position = currentPosition;

        // Check bottom boundary
        if (currentPosition.y <= boundary.yMin) {
            this._Reset();
        }
    }

    // resets the gameObject
    private void _Reset() {
        this._CurrentSpeed = Random.Range(speed.minSpeed, speed.maxSpeed);
        Vector2 resetPosition = new Vector2(Random.Range(boundary.xMin, boundary.xMax), boundary.yMax);
        gameObject.GetComponent<Transform>().position = resetPosition;
    }

    public void OnCollisionEnter(Collision other) {
        print("OnCollisionEnter");
        if (other.gameObject.CompareTag("DeathPlane")) {
            playerController.earnScorePoints(10);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        print("OnCollisionEnter2D");
        if (other.gameObject.CompareTag("DeathPlane")) {
            playerController.earnScorePoints(10);
        }
    }

    public void OnCollisionStay(Collision collision) {
        print("OnCollisionStay");
    }

    public void OnCollisionStay2D(Collision2D collision) {
        print("OnCollisionStay2D");
    }

    public void OnTriggerEnter(Collider other) {
        print("OnTriggerEnter");
        if (other.gameObject.CompareTag("DeathPlane")) {
            playerController.earnScorePoints(10);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        print("OnTriggerEnter2D");
        if (other.gameObject.CompareTag("DeathPlane")) {
            playerController.earnScorePoints(10);
        }
    }
}
