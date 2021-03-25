using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

public class AIMovement : NetworkBehaviour
{
    [Header("MovementControls")]
    [SerializeField] private float aiMovementSpeed = 6f;
    [SerializeField] private float aiWalkSpeed = 3f;
    [SerializeField] private float pauseDuration = 3f;
    [SerializeField] private float maxMoveDuration = 2f;
    [SerializeField] private bool doRandomMovement = false;
    private bool moving;
    private Vector3[] moveDirections = new Vector3[] {Vector3.up, Vector3.right, -Vector3.up, -Vector3.right};
    private Vector3 randomDirection;
    private Rigidbody2D rb;

    [Header("A* Pathfinding")]
    [SerializeField] private Tilemap unwalkableTilemap;
    [SerializeField] private float possibleTargetPathRadius;
    private List<Vector3Int> unwalkableCoords;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("MoveRandomly");

        var testNode = new Node();

        if (testNode.pos == null){
            Debug.Log("testNode pos is null");
        }
        else{
            Debug.Log(testNode.pos);
        }
    }

    [Server]
    private void FixedUpdate() {
        if (moving){
            Move(randomDirection);
        }
    }

    private IEnumerator MoveRandomly(){
        if (isLocalPlayer){ Debug.Log("Player is running this?");}

        while (true){
            yield return new WaitUntil(() => doRandomMovement);

            Debug.Log("Choosing random duration.");
            float randomMoveDuration = Random.Range(0.1f, maxMoveDuration);
            randomDirection = moveDirections[Random.Range(0,3)];

            moving = true;

            Debug.Log("Walking...");
            yield return new WaitForSeconds(randomMoveDuration);
            Debug.Log("Done walking.");

            moving = false;

            Debug.Log("Waiting...");
            yield return new WaitForSeconds(pauseDuration);
            Debug.Log("Done Waiting.");
        }
    }

    private void Move(Vector3 direction){
        rb.velocity = direction*aiMovementSpeed*Time.deltaTime;
    }

    [SerializeField]
    private struct Node{
        public float total;
        public float distance2Start;
        public float distanceToEnd;
        public Vector3 pos;
        public int prevNode;
    }

    private List<Node> openNodes = new List<Node>();
    private List<Node> closedNodes = new List<Node>();
    private Vector3 targetPath;

    private int[] surroudingNodesOffset = {-1,0,1};
    
    private void PathFinding(){
        var randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        targetPath = new Vector3(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));

        Node startNode = CreateStartingNode(transform.position);
        openNodes.Add(startNode);

        while(true){
            Node node = FindLowestDistance2Start();
            openNodes.Remove(node);
            closedNodes.Add(node);

            Node[] neighbourNodes = CreateNeighbours(node);

            foreach (Node neighbourNode in neighbourNodes){
                if (unwalkableTilemap.HasTile(unwalkableTilemap.WorldToCell(neighbourNode.pos))){
                    continue;
                }

            }
        }

    }

    private Node CreateNewNode(Vector3 pos, Node originNode){
        Node newNode = new Node();

        if (unwalkableTilemap.HasTile(unwalkableTilemap.WorldToCell(pos))){
            return newNode;
        }

        newNode.pos = pos;

        newNode.prevNode  = openNodes.IndexOf(originNode);

        newNode.distance2Start = (pos - transform.position).magnitude;

        newNode.distanceToEnd = pos == transform.position
            ? 0
            : (targetPath - transform.position).magnitude;

        newNode.total = newNode.distance2Start + newNode.distanceToEnd;

        return newNode;
    }

    private Node CreateStartingNode(Vector3 pos){
        Node newNode = new Node();

        newNode.pos = pos;
        newNode.prevNode = -1;
        
        return newNode;
    }

    private Node FindLowestDistance2Start(){
        Node lowestDistance2StartNode = openNodes[0];

        if (openNodes.Count == 1){
            return lowestDistance2StartNode;
        }

        foreach (Node node in openNodes){
            if (node.total < lowestDistance2StartNode.total){
                lowestDistance2StartNode = node;
            }
            else if (node.total == lowestDistance2StartNode.total && node.distance2Start < lowestDistance2StartNode.distance2Start){
                lowestDistance2StartNode = node;
            }
        }

        return lowestDistance2StartNode;

    }

    private Node[] CreateNeighbours(Node originNode){
        Node[] newOpenNodes = new Node[8];
        int nodeNumber = 0;

        foreach (int y in surroudingNodesOffset){
            foreach (int x in surroudingNodesOffset){
                if (nodeNumber > 8){ break;}
                CreateNewNode(new Vector3(x,y,0), originNode);

                nodeNumber++;
            }
        }

        return newOpenNodes;
    }

    private float distanceFromStart(Node node){
        float distanceFromStart = 0;


        return distanceFromStart;

    }

}
