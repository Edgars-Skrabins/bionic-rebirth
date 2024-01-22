using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Transform player;

    public float sens;

    private float xRotation = 0f;

    private void Start()
    {
        GameObject getPlayer = GameObject.FindWithTag("Player");
        player = getPlayer.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        sens = PlayerPrefs.GetFloat("Sens");
    }

    private void Update()
    {
        sens = PlayerPrefs.GetFloat("Sens");
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
