using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IInteractable interactableInRange = null;
    public InputAction interact;

    void Start()
    {

    }
    private void OnEnable()
    {
        interact.Enable();
        interact.performed += OnInteract;
    }
    private void OnDisable()
    {
        interact.performed -= OnInteract;
        interact.Disable();
    }

    public void OnInteract(InputAction.CallbackContext interact)
    {
        if (interact.performed)
        {
            interactableInRange?.Interact();
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.canInteract())
        {
            interactableInRange?.interactIcon(false);//syntax for if not null, then call the function interactableInRange.interactIcon(false)
            interactableInRange = interactable;
            interactableInRange.interactIcon(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactable.interactIcon(false);
            interactableInRange = null;

        }
    }
}