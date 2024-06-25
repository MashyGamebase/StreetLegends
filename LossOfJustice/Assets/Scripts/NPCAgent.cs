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

    private void Update()
    {
        if (status == Status.Innocent || status == Status.Criminal || status == Status.Snatcher)
        {
            WanderBehaviour();
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