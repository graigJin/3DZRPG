using System;
using Assets.Scripts;
using Counters;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounterVisual : MonoBehaviour
{

    private const string CUT = "Cut";

    [FormerlySerializedAs("containerCounter")] [SerializeField] private CuttingCounter cuttingCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounterOnOnCut;
    }

    private void CuttingCounterOnOnCut(object sender, EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }

}