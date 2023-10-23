using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}
