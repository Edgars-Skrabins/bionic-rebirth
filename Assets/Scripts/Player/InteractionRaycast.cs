using UnityEngine;

public class InteractionRaycast : MonoBehaviour
{

    private Camera cam;
    [SerializeField] private int interactRange;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        
        
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactRange))
        {
            InteractionObject target = hit.transform.GetComponent<InteractionObject>();
            if (target != null)
            {
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    //Get method from InteractionObject to command to do something like pickup or open door
                    target.Interact();
                    EventManager.I.InvokeOnPlayeInteract();
                }
            }

        }

    }

}
