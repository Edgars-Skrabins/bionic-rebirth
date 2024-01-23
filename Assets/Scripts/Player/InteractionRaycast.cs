using UnityEngine;

public class InteractionRaycast : MonoBehaviour
{
    [SerializeField] private int interactRange;

    private Camera m_camera;

    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, interactRange)) return;
        InteractionObject target = hit.transform.GetComponent<InteractionObject>();
        if (target == null) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        target.Interact();
        EventManager.I.InvokeOnPlayeInteract();
    }
}
