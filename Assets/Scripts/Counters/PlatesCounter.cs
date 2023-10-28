using System;
using Assets.Scripts;
using UnityEngine;

namespace Counters
{
    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        
        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
        
        private float _spawnPlateTimer;
        private readonly float _spawnPlateTimerMax = 4f;
        private int platesSpawnedAmount;
        private int platesSpawnedAmountMax = 4;
        
        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;

            if (_spawnPlateTimer > _spawnPlateTimerMax)
            {
                _spawnPlateTimer = 0f;
                if (platesSpawnedAmount < platesSpawnedAmountMax)
                {
                    platesSpawnedAmount++;
                    
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject())
            {
                if (platesSpawnedAmount > 0)
                {
                    platesSpawnedAmount--;
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
