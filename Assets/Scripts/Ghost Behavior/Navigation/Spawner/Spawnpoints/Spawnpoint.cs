using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Spawnpoint : MonoBehaviour {
    [SerializeField] protected List<SpawnConditions> spawnConditions = new List<SpawnConditions>();

    public static readonly List<Spawnpoint> All = new List<Spawnpoint>();
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => All.Clear();
    protected virtual void OnEnable() => All.Add(this);
    protected virtual void OnDisable() => All.Remove(this);

    public abstract Vector3 GetSpawnPosition();
    protected abstract void DrawGizmo();

    public bool MeetsRequirements(List<SpawnConditions> conditions) {
        foreach (SpawnConditions sc in conditions)
            if (!spawnConditions.Contains(sc)) return false;

        foreach (SpawnConditions sc in spawnConditions)
            if (!conditions.Contains(sc)) return false;

        return true;
    }

    private void OnDrawGizmosSelected() {
        DrawGizmo();
    }
}

public enum SpawnConditions {
    Inside,
    Outside,
    Inanimate,
    Roadblock
}