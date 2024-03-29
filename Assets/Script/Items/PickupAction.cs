using Units;
using UnityEngine;

namespace Items
{
    public class PickupAction : MonoBehaviour
    {

        public DeliveryItemData cargo = null;

        public GameObject childItemModel = null;

        public GameObject previousDeliveryPickupSpot = null;

        public GameObject deliveryDestination = null;

        [SerializeField] private Transform _backPack;

        public void deliverItem()
        {
            cargo = null;
            Destroy(childItemModel);
            childItemModel = null;
            previousDeliveryPickupSpot.SetActive(true);
            previousDeliveryPickupSpot = null;
            deliveryDestination.SetActive(false);
            deliveryDestination = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (cargo != null || !other.gameObject.CompareTag("Item") || deliveryDestination != null) return;

            PickupItem deliveryItem = other.GetComponent<PickupItem>();

            if (deliveryItem == null) return;

            cargo = Instantiate(deliveryItem.deliveryItemData);

            previousDeliveryPickupSpot = other.gameObject;

            other.gameObject.SetActive(false);

            childItemModel = Instantiate(cargo.model, _backPack.position, _backPack.rotation, _backPack);

            deliveryDestination = DeliveryManager.instance.GetDeliveryPoint();

            HudManager.instance.SetArrowDirection(deliveryDestination);

            HudManager.instance.SetItemDetails(deliveryItem.deliveryItemData);

            AudioManager.instance.PlayPickup();
        }
    }

}
