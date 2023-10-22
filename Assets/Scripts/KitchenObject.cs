using UnityEngine;

namespace Assets.Scripts
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        private IKitchenObjectParent _kitchenObjectParent;

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
        {
            if (_kitchenObjectParent != null)
            {
                _kitchenObjectParent.ClearKitchenObject();
            }

            _kitchenObjectParent = kitchenObjectParent;

            if (kitchenObjectParent.HasKitchenObject())
            {
                Debug.LogError("KitchenObjectParent already has a KitchenObject");
            }
            kitchenObjectParent.SetKitchenObject(this);

            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetKitchenObjectParent()
        {
            return _kitchenObjectParent;
        }

        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,
            IKitchenObjectParent kitchenObjectParent)
        {
            Transform cutKitchenObjectPrefab = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kitchenObject = cutKitchenObjectPrefab.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

            return kitchenObject;
        }
    }
}
