using UnityEngine;
using System;

public class GhostManager : MonoBehaviour { 
    public event Action<GameObject> OnGhostVanish;

    public void RemoveGhost(GameObject go) {
        OnGhostVanish?.Invoke(go);
    }
}