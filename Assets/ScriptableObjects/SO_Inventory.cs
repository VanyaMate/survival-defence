using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "items/inventory", fileName = "SO_Inventory", order = 0)]
    public class SO_Inventory : SO_Item
    {
        public float Weight;
    }
}