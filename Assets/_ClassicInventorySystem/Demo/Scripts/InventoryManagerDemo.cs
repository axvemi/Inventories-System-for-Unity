using UnityEngine;
using QFSW.QC;
using System.Linq;

namespace Axvemi.ClassicInventory.Demo
{
    /// <summary>
    /// Contains the inventory for the example, and the methods to loot
    /// </summary>
    public class InventoryManagerDemo : MonoBehaviour
    {
        public Inventory Inventory = null;

        [Header("INVENTORY")]
        [SerializeField] private int inventorySlotAmmount = 0;
        private InventoryItem[] gameItems = null;

        [Header("UI")]
        [SerializeField] private InventoryUIController inventoryUIController = null;

        private void Awake() {
            Inventory = new Inventory(inventorySlotAmmount);
            inventoryUIController.Inventory = this.Inventory;
        }

        private void Start() {
            gameItems = Resources.LoadAll("Demo", typeof(InventoryItem)).Cast<InventoryItem>().ToArray();

            //ADD DEMO ITEMS
            AddItemToInventory("0");
            AddItemToInventory("0");
            AddItemToInventory("1", 5);
            AddItemToInventory("2", 2);
            AddItemToInventory("3", 30);
            AddItemToInventory("4", 60);
        }

        public void AddItemToInventory(InventoryItem item){
            this.Inventory.AddItem(item);
        }

        public void AddItemToInventory(string itemID, int ammount = 1){
            InventoryItem item = gameItems.Single(x => x.Id == itemID);
            for (int i = 0; i < ammount; i++) {
                AddItemToInventory(item);    
            }
            
        }
    }
}
