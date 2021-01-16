using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FieldOfView : NetworkBehaviour
{
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float viewDistance = 50f;

    [SerializeField] private int rayCastCount;
    [SerializeField] List<float> rayCastAngles = new List<float>();
    [SerializeField] private LayerMask targetMask, obstacleMask;

    [Client]
    private void Start() {
        if (!isLocalPlayer){ return;}
        CalculateRayAngles();
    }

    [Client]
    void Update()
    {
        if (!isLocalPlayer){ return;}
        FaceMouse();
    }

    [Client]
    private void FaceMouse(){
        //Shortcut
        Vector3 pos = transform.position;

        //Convert mouse coords on screen to on world
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //By subtracting the mosue coords with player's coords, each coord is the distance from the player.
        var relativeX = cursorPos.x - pos.x;
        var relativeY = cursorPos.y - pos.y;

        //Change player's relative Z axis according to this Vector
        transform.up = new Vector2(relativeX, relativeY);

        var rightViewLine = DirInAngle(viewAngle/2);
        var leftViewLine = DirInAngle(-viewAngle/2);

        //Draw the raycast for debugging
        Debug.DrawRay(pos, transform.up*viewDistance, Color.red);
        Debug.DrawRay(pos, rightViewLine*viewDistance, Color.blue);
        Debug.DrawRay(pos, leftViewLine*viewDistance, Color.blue);

        foreach (int angle in rayCastAngles){
            Debug.DrawRay(pos, DirInAngle(angle)*viewDistance, Color.blue);
        }

        //for each designated number of rays, shoot them out of the player, evenly distributed within the view angle.

    }

    [Client]
    private void CalculateRayAngles(){
        rayCastAngles.Clear();

        int rayCount = rayCastCount;

        //if rayCount is odd, one of them is transform.up ray, which is already casted.
        if (rayCount % 2 != 0){ rayCount--;}

        for (int i = 1; i <= rayCastCount; i++){
            float rayCastAngle = (2*i - 1)*viewAngle / (4 * rayCount);

            rayCastAngles.Add(rayCastAngle);
            rayCastAngles.Add(-rayCastAngle);
        }
    }

    [Client]
    private Vector3 DirInAngle(float angle) {

        //converting to relative angle
        angle -= transform.eulerAngles.z;

        //coords of a point that is x angle away from (0,0) is (sin(x), cos(x))
        //angle is converted to radian as that's what's used in trignometry. tbh idk
        return new Vector3(Mathf.Sin(angle *Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

}
