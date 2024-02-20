using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Oscillator : MonoBehaviour
{
    // Variables
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor; // CAN SET RANGE TO SERIALIZEFIELD [Range(0,1)]
    [SerializeField] float period = 2f;



    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // starts at whatever position it is in world

    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon){return;} // protection against dividing by 0, breaking the game
        float cycles = Time.time / period; // continually growing over time
       
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }
}
