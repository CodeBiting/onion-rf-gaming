using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float minViewDistance = 25f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private bool isInInventory = false; // Variable para controlar si estás en el inventario

    public float sensitivity = 100f;
    float xRotation = 0f;

    void Start()
    {
        // Si no estás en el inventario, bloquea el cursor
        if (!isInInventory)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (!isInInventory)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    // Método para cambiar el estado del inventario
    public void SetInventory(bool isInInventory)
    {
        this.isInInventory = isInInventory;
        // Si estás en el inventario, desbloquea el cursor y hazlo visible
        Cursor.lockState = isInInventory ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isInInventory;
    }
}
