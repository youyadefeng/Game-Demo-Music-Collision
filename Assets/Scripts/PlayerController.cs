using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rig;
    [Header("Move")]
    public float moveForce = 20f;
    public float bounceForce = 20f;
    [Header("Collison")]
    public float enemyBoundForce = 20f;
    public float playerBoundForce = 20f;
    public ParticleSystem boundParticle;
    [Header("Sound")]
    AudioSource audioSource;
    public AudioClip collisionWallSound;



    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        MoveControl();
    }
    
    void MoveControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rig.AddForce(Vector3.forward * moveForce * verticalInput);
        rig.AddForce(Vector3.right * moveForce * horizontalInput);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = (other.gameObject.transform.position - transform.position).normalized;
            var enemyRig = other.gameObject.GetComponent<Rigidbody>();
            enemyRig.AddForce(direction * enemyBoundForce, ForceMode.Impulse);
            rig.AddForce(-direction * playerBoundForce, ForceMode.Impulse);
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            BoundFromWall(other.gameObject.name);
            Instantiate(boundParticle, transform.position, transform.rotation);
            audioSource.PlayOneShot(collisionWallSound, 1);
        }
    }

    void BoundFromWall(string wallName)
    {
        Vector3 direction = Vector3.zero;
        switch (wallName)
        {
            case "Left":
            direction = Vector3.right;
            break;

            case "Right":
            direction = Vector3.left;
            break;

            case "Up":
            direction = Vector3.back;
            break;

            case "Low":
            direction = Vector3.forward;
            break;
        }
        rig.velocity = Vector3.zero;
        rig.AddForce(direction * bounceForce, ForceMode.Impulse);
    }
}
