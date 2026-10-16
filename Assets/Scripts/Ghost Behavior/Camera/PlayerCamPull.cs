using UnityEngine;
using UnityEngine.UI;

public class PlayerCamPull : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;
    [SerializeField] private Transform _player;
    [SerializeField] private Roam _roam;
    [SerializeField] private float DetectionDistance;
    [SerializeField] private float PullStrength;
    [SerializeField] private FlightBobbing _fb;

    private bool pulling = false;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionDistance);
    }


    private void OnDisable() {
        if (pulling) {
            pulling = false;
            _roam.enableRoam();
            _fb.enabled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (!pulling && (Vector3.Distance(transform.position, _playerCam.transform.position) <= DetectionDistance)) {
            pulling = true;
            _fb.enabled = false;
            _roam.disableRoam();
        } else if (pulling && (Vector3.Distance(transform.position, _playerCam.transform.position) > DetectionDistance)) {
            pulling = false;
            _fb.enabled = true;
            _roam.enableRoam();
        }

        if (!pulling) return;

        Vector3 direction = transform.position - _playerCam.transform.position;
        Quaternion targetAngle = Quaternion.LookRotation(direction);
        float yRotation = targetAngle.eulerAngles.y;
        float xRotation = targetAngle.eulerAngles.x;

        bool onGhost = Vector3.Angle(_playerCam.transform.forward, direction) < 2f;

        if (onGhost) return;

        float xSign = Mathf.Sign(Mathf.DeltaAngle(_playerCam.transform.eulerAngles.x, xRotation));
        float ySign = Mathf.Sign(Mathf.DeltaAngle(_playerCam.transform.eulerAngles.y, yRotation));

        float step = PullStrength * Time.deltaTime; 
        _player.Rotate(0f, ySign * step, 0f, Space.World);

        float camX = _playerCam.transform.localEulerAngles.x;
        if (camX > 180f) camX -= 360f;
        camX = Mathf.Clamp(camX + xSign * step, -90f, 90f);
        _playerCam.transform.localRotation = Quaternion.Euler(camX, 0f, 0f);
    }
}
