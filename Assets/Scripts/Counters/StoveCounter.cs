using System;
using Assets.Scripts;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class StoveCounter : BaseCounter, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }
        
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned,
        }
        
        [SerializeField] private FryingRecipeSO[] fryingRecipesArray;
        [SerializeField] private BurningRecipeSo[] burningRecipesArray;

        private State _state;
        private float _fryingTimer;
        private float _burningTimer;
        private FryingRecipeSO _fryingRecipeSo;
        private BurningRecipeSo _burningRecipeSo;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                switch (_state)
                {
                    case State.Idle:
                        break;
                    case State.Frying:
                        _fryingTimer += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = _fryingTimer / _fryingRecipeSo.maxFryingTimer
                        });
                        
                        if (_fryingTimer > _fryingRecipeSo.maxFryingTimer)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(_fryingRecipeSo.output, this);

                            _state = State.Fried;
                            _burningTimer = 0f;
                            _burningRecipeSo = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = _state
                            });
                        }
                        break;
                    case State.Fried:
                        _burningTimer += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = _burningTimer / _burningRecipeSo.maxBurningTimer
                        });
                        
                        if (_burningTimer > _burningRecipeSo.maxBurningTimer)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(_burningRecipeSo.output, this);
                            _state = State.Burned;
                            
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = _state
                            });
                            
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                ProgressNormalized = 1f
                            });
                        }
                        break;
                    case State.Burned:
                        break;
                }
            }
        }

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        _fryingRecipeSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                        _state = State.Frying;
                        _fryingTimer = 0f;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = _fryingTimer / _fryingRecipeSo.maxFryingTimer
                        });
                    }
                }
            }
            else
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                    _state = State.Idle;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = _state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = 1f
                    });
                }
            }
        }
        
        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            FryingRecipeSO fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
            return fryingRecipeSo != null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
        {
            FryingRecipeSO fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
            if (fryingRecipeSo != null)
            {
                return fryingRecipeSo.output;
            }

            return null;
        }

        private FryingRecipeSO GetFryingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            foreach (FryingRecipeSO fryingRecipeSo in fryingRecipesArray)
            {
                if (fryingRecipeSo.input == inputKitchenObjectSo)
                {
                    return fryingRecipeSo;
                }
            }
            return null;
        }
        
        private BurningRecipeSo GetBurningRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            foreach (BurningRecipeSo burningRecipeSo in burningRecipesArray)
            {
                if (burningRecipeSo.input == inputKitchenObjectSo)
                {
                    return burningRecipeSo;
                }
            }
            return null;
        }
    }
}
