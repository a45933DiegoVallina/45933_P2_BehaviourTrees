using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobBehaviour : MonoBehaviour
{
    BehaviourTree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = new BehaviourTree();
        Nodes steal = new Nodes("Steal Something");
        Nodes goToDiamond = new Nodes("Go To Diamond");
        Nodes goToVan = new Nodes("Go to Van");

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        steal.AddChild(steal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
