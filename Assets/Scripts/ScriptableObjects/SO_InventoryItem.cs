using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create SO_InventoryItem", fileName = "SO_InventoryItem", order = 0)]
    public class SO_InventoryItem : SO_Item
    {
        public float Weight;
        public MonoBehaviour Prefab;
    }
}