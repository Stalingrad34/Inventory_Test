using UnityEngine;

public class InventorySection : MonoBehaviour
{
    [SerializeField] private Connector connector = default;
    [SerializeField] private ItemModel reservedItemModel = default;
    [SerializeField] private HingeJoint hingePoint = default;
    private Item attachedItem = default;
    
    
    public ItemModel ReservedItemModel => reservedItemModel;
    public Item AttachedItem => attachedItem;
    public bool IsFilled => attachedItem != default;
    

    public void AttachItem(Item item, bool withHinge = true)
    {
        if (withHinge)
            ActivatedHingePoint(item.AttachPoint.position);

        SetItem(item);
        
        connector.SendPost(item.Model.ID, "AttachItem");
    }

    public void DetachItem(bool withHinge = true)
    {
        if (withHinge)
            DeactivateHingePoint();

        attachedItem.DetachToInventory();
        SetItem(default);
        
        connector.SendPost(attachedItem.Model.ID, "DetachItem");
    }

    private void ActivatedHingePoint(Vector3 connectedAnchor)
    {
        hingePoint.gameObject.SetActive(true);
        hingePoint.connectedAnchor = connectedAnchor;
    }

    private void DeactivateHingePoint() =>
        hingePoint.gameObject.SetActive(false);

    private void SetItem(Item item) =>
        attachedItem = item;
    
}