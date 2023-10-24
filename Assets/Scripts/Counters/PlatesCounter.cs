using System;
using Assets.Scripts;
using UnityEngine;

namespace Counters
{
    public class PlatesCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
        
        private float _spawnPlateTimer;
        private readonly float _spawnPlateTimerMax = 4f;
        private int platesSpawnedAmount;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;

            if (_spawnPlateTimer > _spawnPlateTimerMax)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
            }
        }
    }
}
