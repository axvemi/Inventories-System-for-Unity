using UnityEngine;

namespace Axvemi.ClassicInventory
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Axvemi/Inventory/Inventory Item")]
    public class InventoryItem : ScriptableObject
    {
        public string Id = string.Empty;
        [Header("DATA")]
        public int MaxAmmount = 0;
        [Header("VISUALS")]
        public Sprite Sprite = null;
    }
}
