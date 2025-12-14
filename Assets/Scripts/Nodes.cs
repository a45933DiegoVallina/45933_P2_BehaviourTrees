using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes
{
    public enum Status { SUCESS, RUNNING, FAILURE}
    public Status status;
    public List<Nodes> children = new List<Nodes>();
    public int currentChild = 0;
    public string name;

    public Nodes() { }

    public Nodes(string n)
    {
        name = n;
    }

    public virtual Status Process()
    {
        return children[currentChild].Process();
    }

    public void AddChild(Nodes n)
    {
        children.Add(n);    
    }


}
