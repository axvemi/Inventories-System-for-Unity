using System;
using System.Collections.Generic;

namespace Axvemi.ClassicInventory
{
    /// <summary>
    /// Exception thrown when its not possible to add an item to the inventory
    /// </summary>
    public class FailedToAddItemToInventoryException : Exception { }

    /// <summary>
    /// Composed of a list of slots.
    /// Contains the methods to manage the slots and items
    /// </summary>
    public class Inventory
    {
        private List<InventorySlot> slots = new List<InventorySlot>();
        public List<InventorySlot> Slots { get => slots; set => slots = value; }

        public Inventory(int inventorySize){
            for (int i = 0; i < inventorySize; i++)
            {   
                slots.Add(new InventorySlot());    
            }
        }


        /// <summary>
        /// Try to add an item to the inventory
        /// First add it to one where already has one of its type
        /// If it cannot or doesn't exists search for an empty one
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <exception cref="FailedToAddItemToInventoryException">If the item cannot be added this exception gets thrown</exception>
        public void AddItem(InventoryItem item) {
            try {
                GetInventorySlotToAddItem(item).StoreItem(item, 1);
            }
            catch {
                throw new FailedToAddItemToInventoryException();
            }
        }

        /// <summary>
        /// Gets the first slot that meets the parameters.
        /// First check if there is already one with this type and available ammount
        /// If not, search for an empty one
        /// Else, return null
        /// </summary>
        /// <param name="item">Item to store</param>
        /// <returns>Inventory slot that meets the parameters. Null if none</returns>
        private InventorySlot GetInventorySlotToAddItem(InventoryItem item) {
            InventorySlot slot = null;
            //Can stack unlimited ammount
            if(item.MaxAmmount == 0){
                slot = slots.Find(s => (s.Item == item));
            }
            else{
                slot = slots.Find(s => (s.Item == item) && (s.Ammount < item.MaxAmmount));
            }
            //If the item is new or no stack found return a new slot
            if(slot == null){
                slot = slots.Find(s => (s.Item == null));
            }

            return slot;
        }
    }
}
