using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "items/item", fileName = "SO_Item", order = 0)]
    public class SO_Item : ScriptableObject
    {
        public Sprite Icon;
        public string Title;
    }
}