using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // HTP - How To Play

    private GameObject _propManager;
    private GameObject _buttonManager;
    private GameObject _hTPScreen;

    public Slider difficulySlider;
    private Image _handleRenderer;

    public TMP_Text difficultyText;

    public static float spawnRate;
    public static float minMeteorSpeed;
    public static float maxMeteorSpeed;

    private void Awake()
    {
        _hTPScreen = GameObject.FindGameObjectWithTag("HTPScreen");
        _hTPScreen.SetActive(false);
        _handleRenderer = GameObject.FindGameObjectWithTag("SliderHandle").GetComponent<Image>();
        DefaultSettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 3.0f;
        _propManager = GameObject.FindGameObjectWithTag("PropManager");
        _buttonManager = GameObject.FindGameObjectWithTag("ButtonManager");
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Game moves to main game scene
    public void StartButton()
    {
        AudioManager.PlayButtonPressSound();
        SceneManager.LoadScene("Game");
    }

    // Transition from main menu to how to play screen
    public void HTPButton()
    {
        AudioManager.PlayButtonPressSound();
        _propManager.SetActive(false);
        _buttonManager.SetActive(false);
        HTPScreen();
    }

    // Quits game
    public void QuitButton()
    {
        AudioManager.PlayButtonPressSound();
        Application.Quit();
    }

    // Displays how to play screen
    public void HTPScreen()
    {
        _hTPScreen.SetActive(true);
    }

    // Returns back to menu from how to play screen
    public void BackButton()
    {
        AudioManager.PlayButtonPressSound();
        _propManager.SetActive(true);
        _buttonManager.SetActive(true);
        _hTPScreen.SetActive(false);
    }

    // Handles changes to game difficulty and slider colour
    public void DifficultySlider()
    {
        switch(difficulySlider.value)
        {
            case 1:
                _handleRenderer.color = Color.green;
                difficultyText.text = "Easy!";
                difficultyText.color = Color.green;
                spawnRate = 3.0f;
                minMeteorSpeed = 1;
                maxMeteorSpeed = 4;
                break;
            case 2:
                _handleRenderer.color = Color.yellow;
                difficultyText.text = "Medium!!";
                difficultyText.color = Color.yellow;
                spawnRate = 2.5f;
                minMeteorSpeed = 2.5f;
                maxMeteorSpeed = 4.5f;
                break;
            case 3:
                _handleRenderer.color = Color.red;
                difficultyText.text = "Hard!!!";
                difficultyText.color = Color.red;
                spawnRate = 2f;
                minMeteorSpeed = 1.5f;
                maxMeteorSpeed = 5;
                break;
        }
    }

    // Sets game to easy settings by default
    private void DefaultSettings()
    {
        difficultyText.text = "Easy!";
        difficultyText.color = Color.green;
        _handleRenderer.color = Color.green;
        minMeteorSpeed = 1;
        maxMeteorSpeed = 4;
    }
}
