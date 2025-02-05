

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(LedShape))]
class LedShapeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LedShape shape = (LedShape) target;

        if (GUILayout.Button("Update")) shape.UpdateLeds();


        

    }

}