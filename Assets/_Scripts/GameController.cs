using UnityEngine;
using System.Collections;

// reference to the UI namespace
using UnityEngine.UI;

// reference to manage my scenes
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {
    // PUBLIC INSTANCE VARIABLES
    public int enemyCount;
    public GameObject enemy;

    private AudioSource _endGameSound;
    private PlayerController playerController;

    [Header("UI Objects")]
    public Text ScoreLabel;
    public Text HullPointsLabel;
    public Text GameOverLabel;
    public Text FinalScoreLabel;
    public Button RestartButton;


    // Use this for initialization
    void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>();

        this._GenerateEnemies();

        this.GameOverLabel.gameObject.SetActive(false);
        this.FinalScoreLabel.gameObject.SetActive(false);
        this.RestartButton.gameObject.SetActive(false);

        this._endGameSound = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {

        updateUI();
    }

    private void updateUI() {

        this.HullPointsLabel.text = "Hull Points: " + playerController.getHullPoints();
        this.ScoreLabel.text = "Score: " + playerController.getScore();
    }

    // generate Clouds
    private void _GenerateEnemies() {
        for (int count = 0; count < this.enemyCount; count++) {
            Instantiate(enemy);
        }
    }

    public void restartGame() {

    }
    public void gameOver() {
        this.GameOverLabel.gameObject.SetActive(true);
        this.FinalScoreLabel.text = "Final Score: " + playerController.getScore();
        this.FinalScoreLabel.gameObject.SetActive(true);
        this.RestartButton.gameObject.SetActive(true);
        this.ScoreLabel.gameObject.SetActive(false);
        //this.plane.SetActive(false);
        //this.island.SetActive(false);
        this._endGameSound.Play();
    }
}
