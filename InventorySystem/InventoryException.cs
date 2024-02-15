using System;

namespace FEngine.InventorySystem
{
    public enum InventoryOperation
    {
        Add,
        Remove,
    }
    public class InventoryException : Exception
    {
        public InventoryException(InventoryOperation operation,string message) : base($"{operation} Error:{message}")
        {
            
        }
    }
}