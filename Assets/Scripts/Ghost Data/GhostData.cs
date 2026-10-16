using UnityEngine;

[CreateAssetMenu(menuName = "Ghosts/Ghost Data")]
public class GhostData : ScriptableObject
{
    public string ghostName;
    [TextArea] public string description;
    public Sprite portrait;
    public int order;
}