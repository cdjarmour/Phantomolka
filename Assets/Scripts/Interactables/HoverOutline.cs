using UnityEngine;
using UnityEngine.InputSystem;

public class HoverOutline : MonoBehaviour
{

    [SerializeField] private float InteractionRange = 5f;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color normalColor;

    private Outline hitObject; 

    // Update is called once per frame
    void Update()
    {
        Outline curr = null;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit obj, InteractionRange) && obj.collider.CompareTag("Interactable")) {
            curr = obj.collider.GetComponent<Outline>();
        }

        if (curr != null)
            curr.OutlineColor = Mouse.current.leftButton.isPressed ? pressedColor : normalColor;


        if (curr == hitObject) return;
        if (hitObject != null) hitObject.enabled = false;
        hitObject = curr;
        if (hitObject != null) hitObject.enabled = true;

    }
}
