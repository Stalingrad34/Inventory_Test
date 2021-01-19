using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemModel model = default;
    [SerializeField] private Transform attachPoint = default;
    [SerializeField] protected UnityEvent onItemAttached = default;
    [SerializeField] protected UnityEvent onItemDetached = default;
    protected Camera mainCamera = default;
    protected bool isAttached = default;
    
    
    public ItemModel Model => model;
    public Transform AttachPoint => attachPoint;
    public bool IsAttached => isAttached;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public virtual void OnMouseDrag()
    {

        if (isAttached)
            return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        transform.position = mousePosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnMouseUp()
    {
        if (isAttached)
            return;
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 100);
        Inventory inventory = default;
        foreach (var hit in hits)
        {
            if (hit.transform.GetComponent<Inventory>())
                inventory = hit.transform.GetComponent<Inventory>();
        }

        if (inventory)
            AttachToInventory(inventory);

    }

    protected abstract void AttachToInventory(Inventory section);
    public abstract void DetachToInventory();

}