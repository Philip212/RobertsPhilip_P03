using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundDetector : MonoBehaviour
{
    public event Action GroundDetected = delegate { };
    public event Action GroundVanished = delegate { };
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GroundDetected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        GroundVanished?.Invoke();
    }
}
