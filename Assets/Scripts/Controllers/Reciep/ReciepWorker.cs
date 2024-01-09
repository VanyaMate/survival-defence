using System;
using System.Collections.Generic;
using Controllers.Inventory;
using ScriptableObjects;
using Unity.VisualScripting;

namespace Controllers.Reciep
{
    [Serializable]
    public class ReciepList
    {
        public SO_InventoryItem Item;
        public int Amount;
    }

    [Serializable]
    public class Reciep
    {
        public List<ReciepList> From;
        public List<ReciepList> To;
    }
    
    public class ReciepItemStatus
    {
        public SO_InventoryItem Item;
        public int Amount;
        public int AmountNeed;
        public bool InQuantity;
    }

    public class ReciepCheck
    {
        public List<ReciepItemStatus> Items;
        public bool Valid;
    }

    public static class ReciepWorker
    {
        public static ReciepCheck Check(
            IInventoryController<SO_InventoryItem> inventory,
            Reciep reciep
        )
        {
            List<ReciepItemStatus> itemStatusList = new List<ReciepItemStatus>();
            bool valid = true;

            reciep.From.ForEach(
                (item) =>
                {
                    int inInventoryAmount = inventory.GetItemAmount(item.Item);
                    bool inAmount = inInventoryAmount >= item.Amount;
                    itemStatusList.Add(
                        new ReciepItemStatus()
                        {
                            Item = item.Item,
                            AmountNeed = item.Amount,
                            Amount = inInventoryAmount,
                            InQuantity = inAmount
                        }
                    );
                    if (!inAmount)
                    {
                        valid = false;
                    }
                }
            );

            return new ReciepCheck()
            {
                Items = itemStatusList,
                Valid = valid
            };
        }

        public static void Convert(
            IInventoryController<SO_InventoryItem> inventory,
            Reciep reciep
        )
        {
            reciep.From.ForEach(
                (item) => { inventory.TakeItem(item.Item, item.Amount); }
            );
            reciep.To.ForEach(
                (item) => { inventory.PutItem(item.Item, item.Amount); }
            );
        }
    }
}