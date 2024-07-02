using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Innocent,
    Criminal,
    Holder,
    Hostage,
    Snatcher
}

public class NPCAgent : AIAgent
{
    public Status status;
    public GameObject itemToDrop;

    public GameObject alertInfo;
    public float _dRadius = 0.4f;

    public bool isDetected = false;

    private void Update()
    {
        if (status == Status.Innocent || status == Status.Criminal || status == Status.Snatcher)
        {
            WanderBehaviour();
        }

        // If Detected
        if (isDetected)
        {
            alertInfo.SetActive(true);
        }
        else if(!isDetected)
        {
            alertInfo.SetActive(false);
        }
    }

    public override void GotCaught()
    {
        if (status == Status.Snatcher)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }

        base.GotCaught();
    }
}