using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AIMovement : NetworkBehaviour
{
    [SerializeField] private float aiMovementSpeed = 6f;
    [SerializeField] private float aiWalkSpeed = 3f;
    [SerializeField] private float pauseDuration = 3f;
    [SerializeField] private float maxMoveDuration = 2f;
    [SerializeField] private Vector3[] moveDirections = new Vector3[] {Vector3.up, Vector3.right, -Vector3.up, -Vector3.right};
    private bool moving;
    private Vector3 randomDirection;

    void Start()
    {
        StartCoroutine("MoveRandomly");
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        if (moving){
            Move(randomDirection);
        }
    }

    private IEnumerator MoveRandomly(){
        if (isLocalPlayer){ Debug.Log("Player is running this?");}

        while (true){
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

    private void Move(Vector3 randomDirection){
        transform.position += randomDirection*aiMovementSpeed*Time.deltaTime;
    }

    
}