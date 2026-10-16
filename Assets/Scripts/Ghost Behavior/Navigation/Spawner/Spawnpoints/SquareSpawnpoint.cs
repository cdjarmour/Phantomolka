using UnityEditor;
using UnityEngine;


public class SquareSpawnpoint : Spawnpoint {
    [SerializeField] private float sizeX;
    [SerializeField] private float sizeY;
    public override Vector3 GetSpawnPosition() {
        float boundX = sizeX / 2;
        float boundY = sizeY / 2;
        float xPos = transform.position.x + Random.Range(-boundX, boundX);
        float yPos = transform.position.z + Random.Range(-boundY, boundY);
        return new Vector3(xPos, transform.position.y, yPos);
    }


    protected override void DrawGizmo() {
        float boundX = sizeX / 2;
        float boundZ = sizeY / 2;
        Vector3[] corners = {
           transform.position + new Vector3(-boundX, 0,  boundZ),
           transform.position + new Vector3( boundX, 0,  boundZ),
           transform.position + new Vector3( boundX, 0, -boundZ),
           transform.position + new Vector3(-boundX, 0, -boundZ),
       };


        Handles.color = Color.green;
        Handles.DrawAAConvexPolygon(corners);
    }
}
