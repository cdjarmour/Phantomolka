using UnityEngine;

public class FlightBobbing : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float Intensity;
    private float initialY;
    private float yPos;
    private float inc = 0;


    private void Start() {
        initialY = transform.position.y;
    }



    void Update()
    {
        inc = (inc + (Time.deltaTime * Speed)) % (Mathf.PI * 2); 
        yPos = initialY + Mathf.Sin(inc) * Intensity;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
