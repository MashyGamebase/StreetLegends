using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;

    void Update()
    {
        //DetectAgents();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCAgent>() && other.gameObject.GetComponent<NPCAgent>().status == Status.Criminal)
        {
            other.gameObject.GetComponent<NPCAgent>().isDetected = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<NPCAgent>() && other.gameObject.GetComponent<NPCAgent>().status == Status.Criminal)
        {
            other.gameObject.GetComponent<NPCAgent>().isDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<NPCAgent>() && other.gameObject.GetComponent<NPCAgent>().status == Status.Criminal)
        {
            other.gameObject.GetComponent<NPCAgent>().isDetected = false;
        }
    }

}
