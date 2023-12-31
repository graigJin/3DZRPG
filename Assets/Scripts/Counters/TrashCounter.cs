using Assets.Scripts;

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
