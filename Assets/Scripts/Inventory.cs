using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySection> sections = default;
    [SerializeField] private InventoryUI inventoryUI = default; 
    private bool isTimer = default;
    private float timer = default;

    private void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                isTimer = false;
                timer = 0;
                ShowInventory();
            }
        }
    }


    private void OnMouseDown() =>
        isTimer = true;
    
    private void OnMouseUp() =>
        isTimer = false;


    public InventorySection GetItemPosition(ItemModel model)
    {
        var result = sections.Find(section => section.ReservedItemModel == model);
        if (!result)
            throw new ArgumentNullException($"{model} not find in InventorySections");

        return result;
    }

    private void ShowInventory()
    {
        inventoryUI.gameObject.SetActive(true);
        var sectionsAttachedItems = sections.FindAll(section => section.IsFilled);
        var attachedItemModels = sectionsAttachedItems.Select(section => section.ReservedItemModel);
        inventoryUI.ShowItemsUI(attachedItemModels);
    }
        
}
