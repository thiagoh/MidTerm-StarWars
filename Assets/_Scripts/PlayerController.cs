using UnityEngine;
using System.Collections;
using System;

/**
 * Author: Thiago de Andrade Souza
 * SudentID: 300886181
 * PlayerController class: controls the player and its events (and sounds)
 * Last Modified: 10/22/2016
 */
public class PlayerController : MonoBehaviour {
    // PUBLIC INSTANCE VARIABLES +++++++++++++++++++++++++++++++++++++++

    // player's movement boundaries
    public Boundary boundary;
    // player's speed
    public float speed;
    // reference to the camera
    public Camera camera;
    // hurt sound
    public AudioSource hurtSound;
    // win points
    public AudioSource winPointsSound;

    // PRIVATE INSTANCE VARIABLES
    private Vector2 _newPosition = new Vector2(0.0f, 0.0f);
    // player's score
    private int _scoreValue;
    // player's hull points
    private int _hullPoints;

    // game controller reference
    private GameController gameController;

    // Use this for initialization
    void Start() {

        // game controller reference
        gameController = GameObject.FindObjectOfType<GameController>();

        // execute restartGame when the game starts
        restartGame();
    }

    public void restartGame() {
        // restart the game Player's routine
        _scoreValue = 0;
        _hullPoints = 5;
    }

    // Update is called once per frame
    void Update() {
        // check input and move the player
        _CheckInput();
    }

    private void _CheckInput() {

        Transform _transform = gameObject.GetComponent<Transform>();

        _newPosition = _transform.position; // current position

        float move = Input.GetAxis("Horizontal");

        if (move > 0) {
            _newPosition.x += speed; // add move value to current position
        }

        if (move < 0) {
            _newPosition.x -= speed; // subtract move value to current position
        }

        boundaryCheck();

        if (move != 0) {

            // rotate the ship when moves left or right
            if (_transform.rotation.y > -0.4f && _transform.rotation.y < 0.4f) {
                _transform.RotateAround(transform.position, transform.up * (move > 0 ? -1f : 1f), Time.deltaTime * 90f);
            }

        } else {
            _transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        _transform.position = _newPosition;
    }

    public int getScore() {
        // player score
        return _scoreValue;
    }

    public int getHullPoints() {
        // player hull points
        return _hullPoints;
    }

    public void earnScorePoints(int value) {
        _scoreValue += value;
        // when win points play some sound to inform the human game player
        winPointsSound.Play();
    }

    public void loseHullPoint() {
        hurtSound.Play();
        _hullPoints -= 1;

        if (_hullPoints <= 0) {
            // if there are no more hull points, end the game
            gameController.gameOver();
        }
    }

    // boundary check 
    private void boundaryCheck() {
        if (this._newPosition.x < this.boundary.xMin) {
            this._newPosition.x = this.boundary.xMin;
        }

        if (this._newPosition.x > this.boundary.xMax) {
            this._newPosition.x = this.boundary.xMax;
        }
    }
}
