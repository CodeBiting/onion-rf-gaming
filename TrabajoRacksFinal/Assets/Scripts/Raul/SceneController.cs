using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject player; // El personaje principal
    public GameObject vehicle; // El vehículo
    private bool isPlayerInVehicle = false;
    [SerializeField]
    private GameObject playerCamera; // Cámara del personaje
    [SerializeField]
    private GameObject vehicleCamera; // Cámara del vehículo

    void Start()
    {
        // Inicia con el control del personaje y su cámara
        EnablePlayerControl();
    }

    void Update()
    {
        // Si el personaje no está en el vehículo
        if (!isPlayerInVehicle)
        {
            // Verifica si se presiona la tecla 'F'
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Lanza un rayo desde la posición del personaje hacia adelante
                Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
                float raycastDistance = 2f;

                // Realiza el raycast
                if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
                {
                    // Verifica si el objeto golpeado tiene el componente NewCarUserControl
                    if (hit.collider.GetComponentInParent<NewCarUserControl>() != null)
                    {
                        EnterVehicle();
                    }
                }
            }
        }
        else
        {
            // Si el personaje ya está en el vehículo, permite controlarlo
            // Si se presiona una tecla para salir del vehículo (por ejemplo, F), sale del vehículo
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExitVehicle();
            }
        }
    }

    void EnablePlayerControl()
    {
        // Activa el control del personaje
        player.GetComponent<movimeintoJugador>().enabled = true;

        // Activa la cámara del personaje y desactiva la cámara del vehículo
        playerCamera.SetActive(true);
        vehicleCamera.SetActive(false);

        // Desactiva el AudioListener de la cámara del vehículo
        playerCamera.GetComponent<AudioListener>().enabled = true;
        vehicleCamera.GetComponent<AudioListener>().enabled = false;

        // Desactiva el control del vehículo
        vehicle.GetComponent<NewCarUserControl>().enabled = false;
    }

    void EnterVehicle()
    {
        // Desactiva el control del personaje
        player.GetComponent<movimeintoJugador>().enabled = false;

        // Activa el control del vehículo
        vehicle.GetComponent<NewCarUserControl>().enabled = true;

        // Mueve al personaje a una posición fuera del vehículo (ajusta según tu escena)
        player.transform.position = new Vector3(0f, -10f, 0f);

        // Cambia la cámara activa al vehículo y desactiva la cámara del personaje
        playerCamera.SetActive(false);
        vehicleCamera.SetActive(true);

        // Desactiva el AudioListener de la cámara del personaje
        playerCamera.GetComponent<AudioListener>().enabled = false;
        vehicleCamera.GetComponent<AudioListener>().enabled = true;

        isPlayerInVehicle = true;
    }

    void ExitVehicle()
    {
        // Activa el control del personaje
        player.GetComponent<movimeintoJugador>().enabled = true;

        // Desactiva el control del vehículo
        vehicle.GetComponent<NewCarUserControl>().enabled = false;

        // Mueve al personaje a una posición cerca del vehículo (ajusta según tu escena)
        player.transform.position = vehicle.transform.position + new Vector3(2f, 0f, 2f);

        // Cambia la cámara activa al personaje y desactiva la cámara del vehículo
        playerCamera.SetActive(true);
        vehicleCamera.SetActive(false);

        // Desactiva el AudioListener de la cámara del vehículo
        playerCamera.GetComponent<AudioListener>().enabled = true;
        vehicleCamera.GetComponent<AudioListener>().enabled = false;

        isPlayerInVehicle = false;
    }
}
