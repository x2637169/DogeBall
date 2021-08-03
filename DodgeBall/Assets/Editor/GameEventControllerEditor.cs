using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventController))]
public class GameEventControllerEditor : Editor
{
    //// 序列化物件
    //private GameEventController gameEventController;
    //private SerializedProperty TimeEvent;

    //private void OnEnable()
    //{
    //    gameEventController = (GameEventController)target;
    //    TimeEvent = serializedObject.FindProperty("timeEvents");
    //}


    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();
    //    EditorGUILayout.PropertyField(TimeEvent, false);
    //    EditorGUI.indentLevel += 1;
    //    if (TimeEvent.isExpanded)
    //    {
    //        EditorGUILayout.PropertyField(TimeEvent.FindPropertyRelative("Array.size"));
    //        for (int i = 0; i < TimeEvent.arraySize; i++)
    //        {
    //            var b = TimeEvent.GetArrayElementAtIndex(i);
    //            EditorGUILayout.PropertyField(b);
    //            if (b.isExpanded)
    //            {
    //                SerializedProperty a = TimeEvent.GetArrayElementAtIndex(i).FindPropertyRelative("GameEvents");
    //                for (int y = 0; y < a.arraySize; y++)
    //                {
    //                    if (a.isExpanded)
    //                    {
    //                        if (TimeEvent.GetArrayElementAtIndex(0).FindPropertyRelative("GameEvents").GetArrayElementAtIndex(0).FindPropertyRelative("gameEvent").enumValueIndex == 0)
    //                        {
    //                            EditorGUILayout.PropertyField(TimeEvent.GetArrayElementAtIndex(0).FindPropertyRelative("GameEvents").GetArrayElementAtIndex(0).FindPropertyRelative("addBall"));
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    serializedObject.ApplyModifiedProperties();
    //}

}
