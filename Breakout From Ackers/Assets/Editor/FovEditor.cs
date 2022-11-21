using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FOV))]
public class FovEditor : Editor
{
    private void OnSceneGUI()
    {
        FOV fov = (FOV)target;
        //Draws the outer Circle
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.outerRadius);

        Vector3 angle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.outerAngle / 2);
        Vector3 angle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.outerAngle / 2);

        //Draw FOV triangle
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle1 * fov.outerRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle2 * fov.outerRadius);

        //Draw the inner Circle
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.innerRadius);

        Vector3 angle3 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.innerAngle / 2);
        Vector3 angle4 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.innerAngle / 2);

        //Draw FOV triangle
        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle3 * fov.innerRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle4 * fov.innerRadius);


        //Draw line to player if seen
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eY, float angleInDeg)
    {
        angleInDeg += eY;
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }
}
