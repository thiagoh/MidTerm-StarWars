using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    // PUBLIC INSTANCE VARIABLES +++++++++++++++++++++++++++++++++++++++
    //public float speed;
    public Boundary boundary;
    public float speed;
    public Camera camera;

    // PRIVATE INSTANCE VARIABLES
    private Vector2 _newPosition = new Vector2(0.0f, 0.0f);
    private int _scoreValue = 0;
    private int _hullPoints = 5;
    private GameController gameController;

    // Use this for initialization
    void Start() {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update() {
        this._CheckInput();
    }

    private void _CheckInput() {
        this._newPosition = gameObject.GetComponent<Transform>().position; // current position

        if (Input.GetAxis("Horizontal") > 0) {
            this._newPosition.x += this.speed; // add move value to current position
        }


        if (Input.GetAxis("Horizontal") < 0) {
            this._newPosition.x -= this.speed; // subtract move value to current position
        }

        /* movement by keyboard
		// movement by mouse
		Vector2 mousePosition = Input.mousePosition;
		this._newPosition.x = this.camera.ScreenToWorldPoint (mousePosition).x;
		*/

        this._BoundaryCheck();

        gameObject.GetComponent<Transform>().position = this._newPosition;
    }

    public int getScore() {
        return _scoreValue;
    }

    public int getHullPoints() {
        return _hullPoints;
    }

    public void earnScorePoints(int value) {
        _scoreValue += value;
    }

    public void loseHullPoints() {
        _hullPoints -= 1;

        if (_hullPoints <= 0) {
            gameController.gameOver();
        }
    }

    private void _BoundaryCheck() {
        if (this._newPosition.x < this.boundary.xMin) {
            this._newPosition.x = this.boundary.xMin;
        }

        if (this._newPosition.x > this.boundary.xMax) {
            this._newPosition.x = this.boundary.xMax;
        }
    }

}
