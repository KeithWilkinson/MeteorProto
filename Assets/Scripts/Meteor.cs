using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector2 _currentPosition;
    public Vector2 targetPosition;
    private float _meteorSpeed;
    private Rigidbody2D rb;
    private bool _hasPickedUp = false;

    private float _launchPower = 5f;
    public Vector2 _minPower;
    public Vector2 _maxPower;

    LaunchLine line;

    Camera cam;
    Vector2 force;
    Vector3 startPos;
    Vector3 endPos;

    bool isDrawingLine = false;
    bool canFling = true;
    private Planet _planet;

    private EndGameManager endManager;

    public GameObject[] explosionEffects;

    public Animation meteorAnim;

    private void Awake()
    {
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Planet>();
        rb = GetComponent<Rigidbody2D>();
        _meteorSpeed = Random.Range(MainMenu.minMeteorSpeed, MainMenu.maxMeteorSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(_sporeSpeed);
        meteorAnim = gameObject.GetComponent<Animation>();
        endManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<EndGameManager>();
        line = GetComponent<LaunchLine>();
        cam = Camera.main;
        _currentPosition = transform.position;
        targetPosition = GameObject.FindGameObjectWithTag("Planet").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = _meteorSpeed * Time.deltaTime;
        //Default meteor movement
        if(_hasPickedUp == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        if(Input.GetMouseButton(0))
        {
            if(isDrawingLine == true && canFling == true)
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 1;
                line.RenderLine(startPos, currentPoint);
            }
          
        }
        if(endManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    bool hasFlung = false;

    private void FixedUpdate()
    {
        if(hasFlung == true)
        {
            //rb.AddForce(force * _launchPower, ForceMode2D.Impulse);
            StartCoroutine(DestroyMeteor());
        }
    }
    // Stop meteor from moving to allow launching
    private void OnMouseDown()
    {
       if(canFling == true)
       {
            meteorAnim.enabled = false;
            isDrawingLine = true;
            _hasPickedUp = true;
            startPos = this.gameObject.GetComponent<Renderer>().bounds.center;
            startPos.z = 1;
       }
    }

    // Launches meteor
    private void OnMouseUp()
    {
        if(canFling == true)
        {
            meteorAnim.enabled = true;
            isDrawingLine = false;
            _hasPickedUp = false;
            endPos = cam.ScreenToWorldPoint(Input.mousePosition);
            endPos.z = 1;
            force = new Vector2(Mathf.Clamp(startPos.x - endPos.x, _minPower.x, _maxPower.x), Mathf.Clamp(startPos.y - endPos.y, _minPower.y, _maxPower.y));
            rb.AddForce(force * _launchPower, ForceMode2D.Impulse);
            hasFlung = true;
            line.EndLine();
            StartCoroutine(Cooldown());
        }
    }

    // Meteor collision with meteor
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Meteor")
        {
            AudioManager.PlayMeteorCollideSound();
            var whichEffect = Random.Range(0, explosionEffects.Length);
            Instantiate(explosionEffects[whichEffect], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Planet")
        {
            _planet.DamagePlanet(0.5f);
            Destroy(this.gameObject);
        }
    }

    // Cooldown for being able to launch meteor again
    IEnumerator Cooldown()
    {
        canFling = false;
        yield return new WaitForSeconds(4);
        canFling = true;
    }

    // Destroys launched meteor
    IEnumerator DestroyMeteor()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }
}
