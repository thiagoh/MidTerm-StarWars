using UnityEngine;
using System.Collections;

// reference to the UI namespace
using UnityEngine.UI;

// reference to manage my scenes
using UnityEngine.SceneManagement;
using System;

/**
 * Author: Thiago de Andrade Souza
 * SudentID: 300886181
 * GameController class: controls the game events
 * Last Modified: 10/22/2016
 */
public class GameController : MonoBehaviour {
    // PUBLIC INSTANCE VARIABLES
    public int enemyCount;
    // enemy prefab
    public GameObject enemy;

    // end game sound
    private AudioSource _endGameSound;
    // reference to player controller
    private PlayerController playerController;

    [Header("UI Objects")]
    // score
    public Text ScoreLabel;
    // hull points
    public Text HullPointsLabel;
    // game over label
    public Text GameOverLabel;
    // final score
    public Text FinalScoreLabel;
    // restart button
    public Button RestartButton;

    // Use this for initialization
    void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        
        _GenerateEnemies();

        GameOverLabel.gameObject.SetActive(false);
        FinalScoreLabel.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);

        ScoreLabel.gameObject.SetActive(true);
        HullPointsLabel.gameObject.SetActive(true);

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

        _GenerateEnemies();

        GameOverLabel.gameObject.SetActive(false);
        FinalScoreLabel.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);

        ScoreLabel.gameObject.SetActive(true);
        HullPointsLabel.gameObject.SetActive(true);

        playerController.gameObject.SetActive(true);
        playerController.restartGame();
    }
    public void gameOver() {
        _endGameSound.Play();
        GameOverLabel.gameObject.SetActive(true);
        FinalScoreLabel.text = "Final Score: " + playerController.getScore();
        FinalScoreLabel.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        ScoreLabel.gameObject.SetActive(false);
        HullPointsLabel.gameObject.SetActive(false);
        playerController.gameObject.SetActive(false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var item in enemies) {
            Destroy(item);
        }
    }
}
