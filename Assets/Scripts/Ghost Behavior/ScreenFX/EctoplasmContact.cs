using UnityEngine;

public class EctoplasmContact : MonoBehaviour
{
    [SerializeField] ScreenOverlayManager _overlayManager;
    [SerializeField] GhostManager _ghostManager;
    [SerializeField] AudioClip _deathSound;
    [Range(0,1)][SerializeField] float deathVolume;


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            AudioSource.PlayClipAtPoint(_deathSound, transform.position, deathVolume);
            _ghostManager.RemoveGhost(this.gameObject);
            transform.parent.gameObject.SetActive(false);
            _overlayManager.EctoplasmEnable();
        }
    }
}
