using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;

    private  Rigidbody rb;

    public AudioClip collectSound;
    public AudioClip deathSound;

    public TextMeshProUGUI counterText;
    public TextMeshProUGUI gameEndText;

    public AudioSource backgroundMusic;
    private AudioSource audioSource;

    private float movementX;
    private float movementY;

    private int maxCounter;
    private int counter = 0;

    // Start is called before the first frame update
    void Start() 
    {
        rb=GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        maxCounter = GameObject.FindGameObjectsWithTag("Diamond").Length;

        SetCountText();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(movementX, 0, movementY);
        rb.AddForce(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collected!");

        if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);

            //audioSource.PlayOneShot(collectSound);

            counter++;

            SetCountText();

            if (counter >= maxCounter) {

                gameEndText.text = "You Win!";
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You got caught!");
    
            //audioSource.PlayOneShot(deathSound);

            backgroundMusic.Stop();

            rb.isKinematic = true;

            gameEndText.text = "WASTED! \n YOU FAILED!";

            Invoke("backToMenu", 5f);
              
        }
    }

    private void SetCountText() {
        counterText.text = "Diamonds: " + counter + " | " + maxCounter;
    }

    private void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
