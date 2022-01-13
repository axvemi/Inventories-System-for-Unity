using System.Collections.Generic;
using UnityEngine;

namespace Axvemi.Inventories.ClassicInventory
{
    public class ClassicInventoryUIController : MonoBehaviour
    {
        private Inventory<ClassicInventorySlot> inventory = null;
        public Inventory<ClassicInventorySlot> Inventory { get => inventory; set => inventory = value; }

        [SerializeField] private GameObject inventorySlotPrefab = null;
        [Header("UI")]
        [SerializeField] private Transform slotsContainer = null;

        private List<ClassicInventorySlotUIController> inventorySlotControllerList = new List<ClassicInventorySlotUIController>();

        #region MONOBEHAVIOUR
        private void Start() {
            CreateInventorySlots();
        }
        #endregion

        private void CreateInventorySlots(){
            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                ClassicInventorySlotUIController inventorySlotUIInstance = Instantiate(inventorySlotPrefab, slotsContainer).GetComponent<ClassicInventorySlotUIController>();
                inventorySlotUIInstance.Slot = inventory.Slots[i];
                inventorySlotControllerList.Add(inventorySlotUIInstance);
            }
        }
    }
}
