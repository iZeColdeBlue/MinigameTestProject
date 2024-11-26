using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;

    private  Rigidbody rb;

    public AudioClip collectSound;
    public AudioClip deathSound;

    public AudioSource backgroundMusic;
    private AudioSource audioSource;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start() 
    {
        rb=GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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

            audioSource.PlayOneShot(collectSound);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You got caught!");

            audioSource.PlayOneShot(deathSound);

            backgroundMusic.Stop();

            rb.isKinematic = true;
        }
    }
}
