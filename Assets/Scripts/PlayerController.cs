using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float speed = 7f;
    private float powerupStr = 10f;
    public bool hasPowerup;
    public bool hasPowerup2;
    public bool hasPowerup3;
    

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;

    


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position;
    }

    // Trigger when powerup taken
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCD());
        }

        if (other.CompareTag("Powerup2"))
        {
            hasPowerup2 = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCD());
        }

        if (other.CompareTag("Powerup3"))
        {
            hasPowerup3 = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCD());
        }

        // make new powerups
    }

    // When have powerup increase in strength of player
    // Powerup effects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerupStr, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup2)
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup3)
        {
            //ability
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(Vector3.up * powerupStr, ForceMode.Impulse);

        }

        
    }

    // Cooldown for powerup
    IEnumerator PowerupCD()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        hasPowerup2 = false;
        hasPowerup3 = false;
        powerupIndicator.gameObject.SetActive(false);
        
    }

   
}
