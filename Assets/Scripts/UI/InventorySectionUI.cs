using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySectionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryUI inventoryUI = default;
    [SerializeField] private ItemModel reservedItemModel = default;
    [SerializeField] private Image imageUI = default;
    [SerializeField] private InventorySection attachedInventorySection = default;
    [SerializeField] private bool detachWithHinge = default;
    public ItemModel ReservedItemModel => reservedItemModel;
    public InventorySection AttachedInventorySection => attachedInventorySection;
    public bool DetachWithHinge => detachWithHinge;

    public void SetImage(Sprite sprite)
    {
        imageUI.sprite = sprite;
        imageUI.color = Color.white;
    }
        

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageUI.color = Color.green;
        inventoryUI.SetSelectedSectionUI(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageUI.color = Color.white;
        inventoryUI.SetSelectedSectionUI(default);
    }
}
