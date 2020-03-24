using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class AnimationPositionUI : AnimationUI
{
    [SerializeField]
     bool isFromTarget = false;
    [SerializeField]
    AnimationPosition.PosType posType = AnimationPosition.PosType.Local;
    [SerializeField]
     Vector3 fromPos = Vector3.zero;
    [SerializeField]
     Vector3 toPos=Vector3.zero;
    [SerializeField]
     AnimationPosition.LerpType lerpType = AnimationPosition.LerpType.Straight;

    private void GetFromPos()
    {
        fromPos = posType == AnimationPosition.PosType.Local ? aniTarget.localPosition : aniTarget.position;
    }

    protected override void Load()
    {

        uiAnimation = new AnimationPosition(aniTarget, aniLength, aniCurve, fromPos, toPos, lerpType, posType, isFromTarget);
    }
}

[CanEditMultipleObjects,CustomEditor(typeof(AnimationPositionUI))]
public class AniamtionPositionUIEditor : AnimationUIEditor
{
    SerializedProperty _isFromTarget;
    SerializedProperty _posType;
    SerializedProperty _fromPos;
    SerializedProperty _toPos;

    protected override void Setup()
    {
        base.Setup();
        _isFromTarget = serializedObject.FindProperty("isFromTarget");
        _posType = serializedObject.FindProperty("posType");
        _fromPos = serializedObject.FindProperty("fromPos");
        _toPos = serializedObject.FindProperty("toPos");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AnimationPositionUI _target = target as AnimationPositionUI;

        serializedObject.Update();

        EditorGUILayout.PropertyField(_isFromTarget);
        EditorGUILayout.PropertyField(_posType);
        if (!_isFromTarget.boolValue)
            EditorGUILayout.PropertyField(_fromPos);
        EditorGUILayout.PropertyField(_toPos);

        serializedObject.ApplyModifiedProperties();
    }
}