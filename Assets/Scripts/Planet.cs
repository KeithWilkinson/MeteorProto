using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float _planetLives = 5f;
    private SpriteRenderer _planetRenderer;

    private EndGameManager endManager;

    private Animation screenShakeAnimation;

    public GameObject earthExplosionEffect;

    private CountdownTimer _timer;


    // Start is called before the first frame update
    void Start()
    {
        screenShakeAnimation = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animation>();
        endManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<EndGameManager>();
        _timer = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CountdownTimer>();
        _planetRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Planet changes colour based on remaining lives
        switch(_planetLives)
        {
            case 4:
                _planetRenderer.color = Color.grey;
                break;
            case 3:
                _planetRenderer.color = Color.green;
                break;
            case 2:
                _planetRenderer.color = Color.yellow;
                break;
            case 1:
                _planetRenderer.color = Color.red;
                break;
        }
    }

    // Damages planet by Meteor damage amount
    public void DamagePlanet(float damage)
    {
        screenShakeAnimation.Play("ScreenShake");
        _planetLives -= damage;
        AudioManager.PlayPlanetDamageSound();
        if (_planetLives <= 0)
        {
            _timer.isOutOfTime = true;
            Instantiate(earthExplosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            endManager.EndGame(false);
        }
    }
}
