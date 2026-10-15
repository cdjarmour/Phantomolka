using UnityEngine;


public class DistanceFade : MonoBehaviour {


    [SerializeField] Transform _player;


    [Tooltip("Units per opacity adjustment")]
    [SerializeField] private float Falloff;


    [Range(0f, 1f)]
    [SerializeField] private float MinOpacity;
    [Range(0f, 1f)]
    [SerializeField] private float MaxOpacity;


    private Material mat;


    private void Start() {
        mat = GetComponent<Renderer>().material;
    }


    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(transform.position, _player.position);
        Color c = mat.color;
        float opacity = MaxOpacity - dist * Falloff;
        c.a = Mathf.Clamp(opacity, MinOpacity, MaxOpacity);
        mat.color = c;
    }
}
