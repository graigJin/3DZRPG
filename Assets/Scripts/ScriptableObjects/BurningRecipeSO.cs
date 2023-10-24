using Assets.Scripts;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class BurningRecipeSo : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float maxBurningTimer;
    }
}
