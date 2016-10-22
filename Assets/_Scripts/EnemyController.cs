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

/**
 * Author: Thiago de Andrade Souza
 * SudentID: 300886181
 * EnemyController class: controls the enemies and its events
 * Last Modified: 10/22/2016
 */
public class EnemyController : MonoBehaviour {
    // PUBLIC INSTANCE VARIABLES
    // enemy's speed
    public Speed speed;
    // boundary where the enemy can move
    public Boundary boundary;

    // PRIVATE INSTANCE VARIABLES
    // current speed
    private float _CurrentSpeed;
    // current drift
    private float _CurrentDrift;

    // reference to player controller
    private PlayerController playerController;
    // reference to game controller
    private GameController gameController;

    // Use this for initialization
    void Start() {

        // get the references
        playerController = GameObject.FindObjectOfType<PlayerController>();
        gameController = GameObject.FindObjectOfType<GameController>();

        resetPosition();
    }

    // Update is called once per frame
    void Update() {
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
        currentPosition.y -= this._CurrentSpeed;
        gameObject.GetComponent<Transform>().position = currentPosition;

        // Check bottom boundary
        if (currentPosition.y <= boundary.yMin) {
            resetPosition();
        }
    }

    // resets the gameObject
    private void resetPosition() {
        this._CurrentSpeed = Random.Range(speed.minSpeed, speed.maxSpeed);
        Vector2 resetPosition = new Vector2(Random.Range(boundary.xMin, boundary.xMax), boundary.yMax);
        gameObject.GetComponent<Transform>().position = resetPosition;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        // if the enemy reaches the "DeathPlane" the player wins points
        if (other.gameObject.CompareTag("DeathPlane")) {
            if (playerController != null) {
                playerController.earnScorePoints(10);
            }
        }
        // if the enemy reaches the Player, the player lose a hull point
        if (other.gameObject.CompareTag("Player")) {
            playerController.loseHullPoint();
        }
    }
}
