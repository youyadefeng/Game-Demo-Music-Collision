using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [Header("Move")]
    Rigidbody rig;
    Transform playerPos;
    public float moveForce = 150f;
    public float collisionEnemyForce = 20f;
    public int collisionTimes = 5;
    
    [Header("Sound")]
    public AudioClip collisionSound;
    AudioSource audioSource;
    [Header("Particle")]
    public ParticleSystem collisionParticle;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ChasePlayer());
        playerPos = FindObjectOfType<PlayerController>().transform;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(collisionSound, 1);
            Instantiate(collisionParticle, transform.position, collisionParticle.transform.rotation);
            --collisionTimes;
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(collisionSound, 1);
            Instantiate(collisionParticle, transform.position, collisionParticle.transform.rotation);
            Vector3 dir = (transform.position - other.gameObject.transform.position).normalized;
            rig.AddForce(dir * collisionEnemyForce, ForceMode.Impulse);
            --collisionTimes;
        }

        if(collisionTimes <= 0)    
            Destroy(gameObject, 0.2f);
    }

    IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 direction = (playerPos.position - transform.position).normalized;
        rig.AddForce(direction * moveForce);
        StartCoroutine(ChasePlayer());
    }
}
