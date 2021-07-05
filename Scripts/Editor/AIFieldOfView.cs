using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

[CustomEditor(typeof(AI))]
public class AIFieldOfView : OdinEditor
{
    private void OnSceneGUI()
    {
        AI thisAI = (AI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(thisAI.transform.position, Vector3.up, Vector3.forward, 360, thisAI.range);
        Handles.color = Color.red;
        Handles.DrawWireArc(thisAI.transform.position, Vector3.up, Vector3.forward, 360, thisAI.minDistance);
        Vector3 viewAngleA = thisAI.AngleToVector(-thisAI.viewAngle / 2);
        Vector3 viewAngleB = thisAI.AngleToVector(thisAI.viewAngle / 2);

        Handles.color = Color.blue;

        Handles.DrawLine(thisAI.transform.position, thisAI.transform.position + viewAngleA * thisAI.range);

        Handles.DrawLine(thisAI.transform.position, thisAI.transform.position + viewAngleB * thisAI.range);
    }
}
