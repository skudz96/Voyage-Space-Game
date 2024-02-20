using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS
    private float invokeDelay = 1f;
    [SerializeField] private AudioClip onFinish;
    [SerializeField] private AudioClip rocketBlowup;
    
    [SerializeField] private ParticleSystem onFinishParticles;
    [SerializeField] private ParticleSystem rocketBlowupParticles;
    
    // CACHE
    private AudioSource audioSource;
    
    // STATE
    private bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) {return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You bumped into a friendly!");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(onFinish);

        onFinishParticles.Play();
        
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", invokeDelay);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(rocketBlowup);
        
        rocketBlowupParticles.Play();
        
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", invokeDelay);
    }
    
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex >= 1)
        {
            SceneManager.LoadScene(currentSceneIndex = 0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    private void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
