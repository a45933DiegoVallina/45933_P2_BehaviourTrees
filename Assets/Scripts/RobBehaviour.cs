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

    public enum ActionState { IDLE, WORKING };
    ActionState state = ActionState.IDLE;

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
        return GoToLocation(diamond.transform.position);
    }

    public Nodes.Status GoToVan()
    {
        return GoToLocation(van.transform.position);
    }

    Nodes.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if (state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >=2)
        {
            state = ActionState.IDLE;
            return Nodes.Status.FAILURE;
        }
        else if (distanceToTarget < 2)
        {
            state = ActionState.IDLE;
            return Nodes.Status.SUCESS;
        }
        return Nodes.Status.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
