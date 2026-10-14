using UnityEngine;
using System;

public class GhostManager : MonoBehaviour { 
    public event Action<GhostVanish> OnGhostVanish;

    public void RemoveGhost(GhostVanish gv) {
        OnGhostVanish?.Invoke(gv);
    }
}