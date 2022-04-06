using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    private float powerupStr = 10f;
    public bool hasPowerup;
    public bool hasPowerup2;
    public bool isGameOver = false;

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
            Ability2();
            powerupIndicator.gameObject.SetActive(true);            
            Destroy(other.gameObject);
            StartCoroutine(Powerup2CD());

        }

        
    }

    // When have powerup increase in strength of player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            if (gameObject.CompareTag("Powerup"))
            {
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

                enemyRb.AddForce(awayFromPlayer * powerupStr, ForceMode.Impulse);
            }

            
            
        }

        // powerup effects

    }

    // Cooldown for powerup
    IEnumerator PowerupCD()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        
    }

    private void Ability2()
    {
        Vector3 scale = transform.localScale;
        scale.x = 5F;
        scale.y = 5F; // your new value.
        scale.z = 5F;
        playerRb.mass = 10;
        transform.localScale = scale;
              
        


    }

    IEnumerator Powerup2CD()

    {
        yield return new WaitForSeconds(7);
        hasPowerup2 = false;
        Vector3 scale = transform.localScale;
        scale.x = 1F;
        scale.y = 1F; // your new value.
        scale.z = 1F;
        playerRb.mass = 1;
        powerupIndicator.gameObject.SetActive(false);
    }



}
