using _Quarantine.Code.InventorySystem.Items;
using UnityEngine;

public class ItemIDSelectorAttribute : PropertyAttribute
{
    public string FolderPath => ItemsConstants.PathToConfigs;
}