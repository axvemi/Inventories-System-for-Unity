using System;

namespace Axvemi.ClassicInventory
{
    /// <summary>
    /// Exception thrown when its not possible to add an item to the inventory
    /// </summary>
    public class FailedToAddItemToInventoryException : Exception { }

    /// <summary>
    /// Exception thrown when its not possible to move an item
    /// </summary>
    public class FailedToMoveItemToSlotException : Exception {}

    /// <summary>
    /// Slot of the inventory. Contains a item, or nothing
    /// </summary>
    public class InventorySlot
    {
        private InventoryItemSO item = null;
        public InventoryItemSO Item { get => item; set => item = value; }

        private int ammount = 0;
        public int Ammount { get => ammount; set => ammount = value; }

        public Action<InventoryItemSO> onSlotItemUpdated;

        #region ITEM STORAGE
        /// <summary>
        /// Adds the item to this slot
        /// If the item is null set reset slot
        /// </summary>
        /// <exception cref="FailedToMoveItemToSlotException">If its not the correct Item to add this exception gets raised</exception>
        public void StoreItem(InventoryItemSO item, int ammount = 1) {
            //If the item is not the same, or item is not null (reset) throw exception
            if((this.item != item && item != null) && this.item != null) {
                throw new FailedToMoveItemToSlotException();
            }
            
            this.item = item;
            this.ammount = item == null ? 0 : this.ammount + ammount;
            onSlotItemUpdated?.Invoke(this.item);
        }

        /// <summary>
        /// Moves the item in this slot to another slot
        /// If the other slot is this type, or null move
        /// If its another type, and the ammount is all in this slot, swap
        /// If it fails raise an exception
        /// </summary>
        /// <param name="slot">Target slot to where move the item</param>
        /// <param name="ammount">Ammount to move to the slot</param>
        public void MoveItemToSlot(InventorySlot slot, int ammount) {
            if(slot == null) {
                throw new FailedToMoveItemToSlotException();
            }

            if(slot.item == null || slot.item == this.item) {
                
                //While there is ammount to move and the slot has remaining space or its infinite
                while(ammount > 0 && (slot.Ammount < item.MaxAmmount || item.MaxAmmount == 0)){
                    slot.StoreItem(this.item, 1);
                    this.ammount--;
                    ammount--;
                }

                //This slot is empty
                if(this.ammount == 0) {
                    this.item = null;
                }

                onSlotItemUpdated?.Invoke(this.item);
            }
            //The ammount of the slot is the ammount that we want to move
            //Swap slots
            else if(this.ammount == ammount) {
                (this.ammount, slot.ammount) = (slot.ammount, this.ammount);
                (this.item, slot.item) = (slot.item, this.item);
                onSlotItemUpdated?.Invoke(this.item);
            }
            else {
                throw new FailedToMoveItemToSlotException();
            }
        }

        #endregion

        #region INVENTORY STORAGE
        /// <summary>
        /// Try to add an item to the inventory
        /// First add it to one where already has one of its type
        /// If it cannot or doesn't exists search for an empty one
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <exception cref="FailedToAddItemToInventoryException">If the item cannot be added this exception gets thrown</exception>
        public static void AddItem(Inventory<InventorySlot> inventory, InventoryItemSO item) {
            try {
                GetInventorySlotToAddItem(inventory, item).StoreItem(item, 1);
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
        private static InventorySlot GetInventorySlotToAddItem(Inventory<InventorySlot> inventory, InventoryItemSO item) {
            InventorySlot slot = null;
            //Can stack unlimited ammount
            if(item.MaxAmmount == 0){
                slot = inventory.Slots.Find(s => (s.Item == item));
            }
            else{
                slot = inventory.Slots.Find(s => (s.Item == item) && (s.Ammount < item.MaxAmmount));
            }
            //If the item is new or no stack found return a new slot
            if(slot == null){
                slot = inventory.Slots.Find(s => (s.Item == null));
            }

            return slot;
        }
        #endregion

        public override string ToString()
        {
            return "Item: " + (item == null ? "No item" : item.Id);
        }
    }
}
