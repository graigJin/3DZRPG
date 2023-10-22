using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ContainerCounterVisual : MonoBehaviour
    {

        private const string OPEN_CLOSE = "OpenClose";

        [SerializeField] private ContainerCounter containerCounter;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnPlayerGrabbedObject += ContainerCounterOnOnPlayerGrabbedObject;
        }

        private void ContainerCounterOnOnPlayerGrabbedObject(object sender, EventArgs e)
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
    }
}
