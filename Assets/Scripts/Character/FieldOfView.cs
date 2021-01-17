using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FieldOfView : NetworkBehaviour
{
    [Range(0,360)] [SerializeField] private float viewAngle = 90f;
    [Range(0, 100000)] [SerializeField] private float viewDistance = 50f;

    [Range(3, 100000)] [SerializeField] private int rayCastCount;
    [SerializeField] List<float> rayCastAngles = new List<float>();
    [SerializeField] private LayerMask targetMask, obstacleMask;

    private void Start() {
        CalculateRayAngles();
    }

    void Update()
    {
        CastRays();
    }

    private void CastRays(){
        var rightViewLine = DirInAngle(viewAngle/2);
        var leftViewLine = DirInAngle(-viewAngle/2);

        foreach (int angle in rayCastAngles){

        }

        //Visualizing the rays
        Debug.DrawRay(transform.position, transform.up*viewDistance, Color.red);
        Debug.DrawRay(transform.position, rightViewLine*viewDistance, Color.black);
        Debug.DrawRay(transform.position, leftViewLine*viewDistance, Color.black);

        foreach (int angle in rayCastAngles){
            Debug.DrawRay(transform.position, DirInAngle(angle)*viewDistance, Color.blue);
        }

    }

    private void CalculateRayAngles(){
        rayCastAngles.Clear();

        int rayCount = rayCastCount - 3;

        //if rayCount is odd, one of them is transform.up ray, which is already casted.
        if (rayCount % 2 != 0){ rayCount--;}

        if (rayCount < 1){ return;}

        for (int i = 1; i <= rayCount; i++){
            float rayCastAngle = (2*i - 1)*viewAngle / (4 * rayCount);

            rayCastAngles.Add(rayCastAngle);
            rayCastAngles.Add(-rayCastAngle);
        }
    }

    private Vector3 DirInAngle(float angle) {

        //converting to relative angle
        angle -= transform.eulerAngles.z;

        //coords of a point that is x angle away from (0,0) is (sin(x), cos(x))
        //angle is converted to radian as that's what's used in trignometry. tbh idk
        return new Vector3(Mathf.Sin(angle *Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

}
