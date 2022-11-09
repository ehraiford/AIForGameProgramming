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
        //Draws the Circle
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 angle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 angle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        //Draw FOV triangle
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle1 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle2 * fov.radius);

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
