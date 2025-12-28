using UnityEngine;

public class testingInteractable : MonoBehaviour, IInteractable
{

    public SpriteRenderer spriteRenderer;
    public bool canInteract = true;
    public GameObject interactIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    bool IInteractable.canInteract()
    {
        return canInteract;
    }

    void IInteractable.Interact()
    {
        if (canInteract)
        {
            spriteRenderer.color = Color.red;
            canInteract = false;
        }
    }

    void IInteractable.interactIcon(bool set)
    {
        interactIcon.SetActive (set);
    }
   
}
