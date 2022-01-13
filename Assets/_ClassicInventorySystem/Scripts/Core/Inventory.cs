using System.Collections.Generic;

namespace Axvemi.ClassicInventory
{
    /// <summary>
    /// Composed of a list of slots.
    /// Contains the methods to manage the slots and items
    /// </summary>
    public class Inventory<T>
    {
        private List<T> slots = new List<T>();
        public List<T> Slots { get => slots; set => slots = value; }
    }
}
