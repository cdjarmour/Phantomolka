using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Spawnpoint : MonoBehaviour {
    public static readonly List<Spawnpoint> All = new List<Spawnpoint>();
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => All.Clear();
    protected virtual void OnEnable() => All.Add(this);
    protected virtual void OnDisable() => All.Remove(this);


    public abstract Vector3 GetSpawnPosition();
    protected abstract void DrawGizmo();
    private void OnDrawGizmos() {
        DrawGizmo();
    }
}
