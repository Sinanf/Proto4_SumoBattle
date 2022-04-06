using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float powerupStr = 10f;
    public bool hasPowerup;
    public bool hasPowerup2;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    private BoxCollider forceField;
       


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

        // make new powerups
    }

    // When have powerup increase in strength of player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerupStr, ForceMode.Impulse);
        }

        // powerup effects
    }

    // Cooldown for powerup
    IEnumerator PowerupCD()
    {
        yield return new WaitForSeconds(10);
        hasPowerup = false;
        hasPowerup2 = false;
        forceField.gameObject.SetActive(false);
        powerupIndicator.gameObject.SetActive(false);

    }

    

   
}
