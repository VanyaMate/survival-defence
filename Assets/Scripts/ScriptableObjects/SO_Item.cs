using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create SO_Item", fileName = "SO_Item", order = 0)]
    public class SO_Item : ScriptableObject
    {
        public Sprite Icon;
        public string Title;
    }
}