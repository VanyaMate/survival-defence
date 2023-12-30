using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "items/inventory_item", fileName = "SO_InventoryItem", order = 0)]
    public class SO_InventoryItem : SO_Item
    {
        public float Weight;
        public MonoBehaviour Prefab;
    }
}