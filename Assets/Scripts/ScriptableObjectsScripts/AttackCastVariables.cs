using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "AttackCast", menuName = "ScriptableObjects/AttackCast")]
public class AttackCastVariables : ScriptableObject {
    [SerializeField] private LayerMask _layerToCheck = default;
    [SerializeField] private int _bufferSizeMax = default;
    //NOTE: プレイヤーから見て1の何倍の距離からキャストを開始するか
    [SerializeField, Tooltip("プレイヤーから見て1の何倍の距離からキャストを開始するか")] private float _distanceFactor = default;
    [SerializeField] private float _leftAdjustment = default;
    [SerializeField] private float _rightAdjustment = default;

    public LayerMask GetLayerMask => _layerToCheck;
    public int GetBufferSizeMax => _bufferSizeMax;
    public float GetDistanceFactor => _distanceFactor;
    public float GetLeftAdjustment => _leftAdjustment;
    public float
        GetRightAdjustment => _rightAdjustment;
}