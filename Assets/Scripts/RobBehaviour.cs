using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    public GameObject backdoor;
    public GameObject PdaFrente;
    NavMeshAgent agent;
    

    public enum ActionState { IDLE, WORKING };
    ActionState state = ActionState.IDLE;
    Nodes.Status treeStatus = Nodes.Status.RUNNING;

    [Range(0, 1000)]
    public int money = 800;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf HasGotMoney = new Leaf("Has Got Money", HasMoney);
        Leaf goTobac0kdoor = new Leaf("Go To Backdoor", GoToBackDoor);
        Leaf goToVan = new Leaf("Go to Van", GoToVan);
        Leaf goToFronteckdoor = new Leaf("Go to FrontDoor", GoToFrontedoor);
        Selector OPENDOOR = new Selector("OPEN DOOR");


        OPENDOOR.AddChild(goTobac0kdoor);
        OPENDOOR.AddChild(goToFronteckdoor);

        steal.AddChild(goToFronteckdoor);
        steal.AddChild(OPENDOOR);
        steal.AddChild(HasGotMoney);
        //steal.AddChild(goTobac0kdoor);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        steal.AddChild(steal);
        

        tree.printTree();

        
    }
    public Nodes.Status HasMoney()
    {
        if (money >= 500)
            return Nodes.Status.FAILURE;
        return Nodes.Status.SUCESS;
        
    }

    public Nodes.Status GoToDiamond()
    {
        Nodes.Status s = GoToLocation(diamond.transform.position);
        if (s == Nodes.Status.SUCESS) 
        {
            diamond.transform.parent = this.gameObject.transform;
        }
        return s;
    }

    public Nodes.Status GoToVan()
    {
        Nodes.Status s = GoToLocation(van.transform.position);
        if (s == Nodes.Status.SUCESS)
        {
            money += 300;
            diamond.SetActive(false);
        }
        return s;
    }

    public Nodes.Status GoToBackDoor()
    {
        return GoToDoor(backdoor);
    }

    public Nodes.Status GoToFrontedoor()
    {
        return GoToDoor(PdaFrente);
    }

    public Nodes.Status GoToDoor(GameObject door)
    {
        Nodes.Status s = GoToLocation(door.transform.position);
        if (s == Nodes.Status.SUCESS)
        {
            if (!door.GetComponent<Lock>().isLocked)
            {
                door.SetActive(false);
                return Nodes.Status.SUCESS;
            }
            return Nodes.Status.FAILURE;
        }
        else return s;
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
        if(treeStatus != Nodes.Status.SUCESS)
            treeStatus = tree.Process();
    }
}
