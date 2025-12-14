using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Nodes steal = new Nodes("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go to Van", GoToVan);

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        steal.AddChild(steal);

        tree.printTree();
        tree.Process();

        
    }

    public Nodes.Status GoToDiamond()
    {
        agent.SetDestination(diamond.transform.position);
        return Nodes.Status.SUCESS;
    }

    public Nodes.Status GoToVan()
    {
        agent.SetDestination(van.transform.position);
        return Nodes.Status.SUCESS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
