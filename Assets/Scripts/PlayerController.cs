using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 0.1f;

    private  Rigidbody rb;

    public AudioClip collectSound;
    private AudioSource audioSource;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start() 
    {
        rb=GetComponent<Rigidbody>();
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

        other.gameObject.SetActive(false);

        audioSource.PlayOneShot(collectSound); 
    }
}
