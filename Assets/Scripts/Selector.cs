using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Nodes
{
    public Selector(string n)
    {
        name = n;
    }

    public override Status Process()
    {
        Status childstatus = children[currentChild].Process();
        if (childstatus == Status.RUNNING) return Status.RUNNING;
        if (childstatus == Status.SUCESS)
        {
            currentChild = 0;
            return Status.SUCESS;
        }

        currentChild++;
        if (currentChild >= children.Count)
        {
            currentChild = 0;
            return Status.SUCESS;
        }
        return Status.RUNNING;
    }
}