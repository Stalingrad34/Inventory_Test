using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventorySectionUI> sectionsUI = default;
    private InventorySectionUI selectedSectionUI = default;

    public void ShowItemsUI(IEnumerable<ItemModel> itemModels)
    {
        foreach (var section in sectionsUI)
        {
            section.gameObject.SetActive(false);
            foreach (var model in itemModels)
            {
                if (model == section.ReservedItemModel)
                {
                    section.gameObject.SetActive(true);
                    section.SetImage(model.Image);
                }
            }
        }
    }

    public void SetSelectedSectionUI(InventorySectionUI section) =>
        selectedSectionUI = section;

    public void DettachItem()
    {
        if (selectedSectionUI && gameObject.activeSelf)
            selectedSectionUI.AttachedInventorySection.DetachItem(selectedSectionUI.DetachWithHinge);
        selectedSectionUI = default;
        gameObject.SetActive(false);
    }
}
