using System.Collections.Generic;
using UnityEngine;

namespace Axvemi.ClassicInventory
{
    public class InventoryUIController : MonoBehaviour
    {
        private Inventory<InventorySlot> inventory = null;
        public Inventory<InventorySlot> Inventory { get => inventory; set => inventory = value; }

        [SerializeField] private GameObject inventorySlotPrefab = null;
        [Header("UI")]
        [SerializeField] private Transform slotsContainer = null;

        private List<InventorySlotUIController> inventorySlotControllerList = new List<InventorySlotUIController>();

        #region MONOBEHAVIOUR
        private void Start() {
            CreateInventorySlots();
        }
        #endregion

        private void CreateInventorySlots(){
            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                InventorySlotUIController inventorySlotUIInstance = Instantiate(inventorySlotPrefab, slotsContainer).GetComponent<InventorySlotUIController>();
                inventorySlotUIInstance.Slot = inventory.Slots[i];
                inventorySlotControllerList.Add(inventorySlotUIInstance);
            }
        }
    }
}
