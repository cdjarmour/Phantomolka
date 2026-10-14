using UnityEditor;
using UnityEngine;


public class CircleSpawnpoint : Spawnpoint {
    [SerializeField] private float size;


    public override Vector3 GetSpawnPosition() {
        Vector2 point = Random.insideUnitCircle * size;
        Vector3 target = transform.position + new Vector3(point.x, 0, point.y);
        return target;
    }


    protected override void DrawGizmo() {
        Handles.color = Color.green;
        Handles.DrawSolidDisc(transform.position, Vector3.up, size);
    }
}
