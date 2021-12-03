using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class MovementRadius : MonoBehaviour
{
    [Range(0,50)]
    public int segments = 50;
    [Range(0,100)]
    public float radius = 15;
    LineRenderer MovementRadiusLine;

    void Start ()
    {
        MovementRadiusLine = gameObject.GetComponent<LineRenderer>();

        MovementRadiusLine.SetVertexCount (segments + 1);
        MovementRadiusLine.useWorldSpace = false;
        CreatePoints ();
    }

    void CreatePoints ()
    {
        float x;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

            MovementRadiusLine.SetPosition (i,new Vector3(x,0,z) );             

            angle += (360f / segments);
        }
    }
}
