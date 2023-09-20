using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    // [FormerlySerializedAs("name")] 
    // public string itemName;
    public Sprite sprite;
}
