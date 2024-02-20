using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] private float thrustSpeed = 1250f;
    [SerializeField] private float rotateSpeed = 60f;
    [SerializeField] private AudioClip mainEngine;
    
    [SerializeField] private ParticleSystem leftBooster;
    [SerializeField] private ParticleSystem rightBooster;
    [SerializeField] private ParticleSystem mainBooster;
    
    // CACHE - e.g. references for readability or speed
    private Rigidbody rb; 
    private AudioSource audioSource;
    
    // STATE - private instance (member) variables
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        { 
            rb.AddRelativeForce(Vector3.up * (thrustSpeed * Time.deltaTime));
            if (!mainBooster.isPlaying)
            { 
                mainBooster.Play();
            }
            else
            {
                mainBooster.Stop();
            }
            
            if (!audioSource.isPlaying)
            { 
                audioSource.PlayOneShot(mainEngine);
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }
    // Rotation simplified into a METHOD called ApplyRotation, can use this method in ProcessRotation to simplify code
    // float variable given to ApplyRotation as a placeholder
    // Can feed in additional variables to ApplyRotation so it can serve multiple purposes, in this case to multiply it by rotateSpeed (pos or neg)
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
        rb.freezeRotation = false; // unfreeze rotation so physics system can take over after movement input
    }
}
