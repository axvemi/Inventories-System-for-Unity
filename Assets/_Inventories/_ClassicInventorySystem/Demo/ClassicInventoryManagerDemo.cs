using UnityEngine;
using System.Linq;

namespace Axvemi.Inventories.ClassicInventory.Demo
{
    /// <summary>
    /// Contains the inventory for the example, and the methods to loot
    /// </summary>
    public class ClassicInventoryManagerDemo : MonoBehaviour
    {
        public Inventory<ClassicInventorySlot> Inventory = null;

        [Header("INVENTORY")]
        [SerializeField] private int inventorySlotAmmount = 0;
        private InventoryItemSO[] gameItems = null;

        [Header("UI")]
        [SerializeField] private ClassicInventoryUIController inventoryUIController = null;

        private void Awake() {
            Inventory = new Inventory<ClassicInventorySlot>();
            for (int i = 0; i < inventorySlotAmmount; i++) {
                Inventory.Slots.Add(new ClassicInventorySlot());
            }

            inventoryUIController.Inventory = this.Inventory;
        }

        private void Start() {
            gameItems = Resources.LoadAll("Demo", typeof(InventoryItemSO)).Cast<InventoryItemSO>().ToArray();

            //ADD DEMO ITEMS
            AddItemToInventory("0");
            AddItemToInventory("0");
            AddItemToInventory("1", 5);
            AddItemToInventory("2", 2);
            AddItemToInventory("3", 30);
            AddItemToInventory("4", 60);
        }

        public void AddItemToInventory(InventoryItemSO item){
            ClassicInventorySlot.AddItem(this.Inventory, item);
        }

        public void AddItemToInventory(string itemID, int ammount = 1){
            InventoryItemSO item = gameItems.Single(x => x.Id == itemID);
            for (int i = 0; i < ammount; i++) {
                AddItemToInventory(item);    
            }
            
        }
    }
}
