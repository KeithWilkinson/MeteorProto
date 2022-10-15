using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    private bool _isGameOver;

    public TMP_Text endGameText;
    public TMP_Text gameOverText;
    public TMP_Text menuPrompt;
    public TMP_Text restartPrompt;


    private bool _gameStatus;

    public Animation textAnim;

    public GameObject[] ringEffects;

    private void Awake()
    {
        gameOverText.enabled = false;
        endGameText.enabled = false;
        menuPrompt.enabled = false;
        restartPrompt.enabled = false;
        _isGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_isGameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
        if (_isGameOver == true && Input.GetKeyDown(KeyCode.Backspace))
        {
            LoadMenu();
        }
    }

    // Displays end game text
    public void EndGame(bool hasWon)
    {
        endGameText.enabled = true;
        restartPrompt.enabled = true;
        menuPrompt.enabled = true;
        gameOverText.enabled = true;
        textAnim.Play("TextMove");

        foreach(GameObject i in ringEffects)
        {
            i.SetActive(false);
        }

        // Game is won
        if (hasWon == true)
        {
            endGameText.color = new Color32(13, 255, 16,255);
            endGameText.text = "The earth has been saved!";

        }
        // Game is lost
        else if(hasWon == false)
        {
            endGameText.color = new Color32(255, 13, 13,255);
            endGameText.text = "The earth has been destroyed";
        }
        _isGameOver = true;
    }

    // Resets game scene
    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Loads menu scene
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public bool gameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }
}
