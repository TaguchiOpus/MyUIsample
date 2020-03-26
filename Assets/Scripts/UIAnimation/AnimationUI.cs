using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class AnimationUI : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    protected RectTransform aniTarget;      // アニメーションするターゲット
    [SerializeField]
    protected float aniLength = 0.0f;              // アニメーションの長さ(秒)
    [SerializeField]
    protected AnimationCurve aniCurve = new AnimationCurve();      // アニメーションカーブ
    [SerializeField]
    protected bool isPlay = false;                  // 再生中フラグ
    [SerializeField]
    protected bool isForward = true;        // 再生/逆再生フラグ
    [SerializeField]
    protected AnimationBase.PlayType aniPlayType = AnimationBase.PlayType.Once;         // 再生タイプ
    #endregion

    #region Constructor
    public bool IsForward { get { return isForward; } }
    public AnimationBase UiAnimation { get { return uiAnimation; } }

    #endregion

    #region Field
    protected bool isInitialized = false;
    protected bool isReady = true;
    protected bool isStandby = false;
    protected AnimationBase uiAnimation;
    protected Action onAnimationFinish = null;
    #endregion

    #region Property
    public bool IsReady { get { return isReady; } }
    public bool IsPlay { get { return isPlay; }set { isPlay = value; } }

    public bool IsStandby { get { return isStandby; } }
    #endregion

    private void Start()
    {
        Initialize();
    }

    public void SetAniTarget(RectTransform target)
    {
        aniTarget = target;
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    /// <param name="forward">再生/逆再生</param>
    /// <param name="onFinish">アニメーション終了時の処理</param>
    public void Play()
    {
        isPlay = true;
        isStandby = false;
        uiAnimation.Play();
    }

    public void StandbyAnimation(bool forward = true, Action onFinish = null)
    {
        if (onFinish != null)
            onAnimationFinish = onFinish;
        isForward = forward;
        isPlay = true;
        isReady = false;
        isStandby = true;
        uiAnimation.StandbyAnimation(isForward, aniPlayType, () => { isPlay = false; if (onAnimationFinish != null) onAnimationFinish(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay&& !isReady)
        {
            if (!uiAnimation.IsPlay && !isStandby)
            {
                uiAnimation.StandbyAnimation(isForward, aniPlayType, () => { isPlay = false; if (onAnimationFinish != null) onAnimationFinish(); });
                uiAnimation.Play();
            }
            uiAnimation.UpdateAnimation();
        }
    }

    protected float MyLerp(float from,float to,float by)
    {
        return (from * (1 - by)) + (to * by);
    }

    /// <summary>
    /// 初期化関数(Start時に呼び出す)
    /// </summary>
    public IEnumerator Initialize(bool is_play,bool is_wait = true) 
    {
        if (!isInitialized)
        {
            isInitialized = true;
            if (aniTarget == null)
                aniTarget = GetComponent<RectTransform>();

            Load();

            if (is_play)
                uiAnimation.StandbyAnimation(isForward,aniPlayType, () => { isPlay = false; if (onAnimationFinish != null) onAnimationFinish(); });

            if (is_wait)
                yield return new WaitForEndOfFrame();
            isReady = false;
            uiAnimation.Play();
        }
    }

    public void Initialize()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            if (aniTarget == null)
                aniTarget = GetComponent<RectTransform>();

            Load();
            isReady = false;
        }
    }


    protected virtual void Load()
    {
        uiAnimation = new AnimationBase(aniTarget, aniLength, aniCurve);
    }
}

[CustomEditor(typeof(AnimationUI))]
public class AnimationUIEditor : Editor
{
    SerializedProperty _isPlay;
    SerializedProperty _isForward;
    SerializedProperty _aniTarget;
    SerializedProperty _aniCurve;
    SerializedProperty _aniLength;
    SerializedProperty _aniPlayType;

    protected virtual void Setup()
    {
        _isPlay = serializedObject.FindProperty("isPlay");
        _isForward = serializedObject.FindProperty("isForward");
        _aniTarget = serializedObject.FindProperty("aniTarget");
        _aniCurve = serializedObject.FindProperty("aniCurve");
        _aniLength = serializedObject.FindProperty("aniLength");
        _aniPlayType = serializedObject.FindProperty("aniPlayType");

    }

    private void OnEnable()
    {
        Setup();
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();    // [SerializedObject]が表示される
        AnimationUI _target = target as AnimationUI;

        /* これだとInspectorで変更した値が更新されない
         * _target.isPlay = EditorGUILayout.Toggle("再生", _target.isPlay);
        _target.isForward = EditorGUILayout.Toggle("再生/逆再生", _target.isForward);
        _target.aniTarget = (RectTransform)EditorGUILayout.ObjectField("ターゲット",_target.aniTarget, typeof(RectTransform));
        _target.aniCurve = EditorGUILayout.CurveField("カーブ", _target.aniCurve);
        _target.aniLength = EditorGUILayout.FloatField("再生時間", _target.aniLength);
        _target.aniPlayType = (AnimationBase.PlayType)EditorGUILayout.EnumPopup("再生タイプ", _target.aniPlayType);*/

        serializedObject.Update();

        EditorGUILayout.PropertyField(_isPlay);
        EditorGUILayout.PropertyField(_isForward);
        EditorGUILayout.PropertyField(_aniTarget);
        EditorGUILayout.PropertyField(_aniLength);
        EditorGUILayout.PropertyField(_aniCurve);


        serializedObject.ApplyModifiedProperties();
    }
}