using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject player; // El personaje principal
    public GameObject vehicle; // El veh�culo
    private bool isPlayerInVehicle = false;
    [SerializeField]
    private GameObject playerCamera; // C�mara del personaje
    [SerializeField]
    private GameObject vehicleCamera; // C�mara del veh�culo

    void Start()
    {
        // Inicia con el control del personaje y su c�mara
        EnablePlayerControl();
    }

    void Update()
    {
        // Si el personaje no est� en el veh�culo
        if (!isPlayerInVehicle)
        {
            // Verifica si se presiona la tecla 'F'
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Lanza un rayo desde la posici�n del personaje hacia adelante
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
            // Si el personaje ya est� en el veh�culo, permite controlarlo
            // Si se presiona una tecla para salir del veh�culo (por ejemplo, F), sale del veh�culo
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

        // Activa la c�mara del personaje y desactiva la c�mara del veh�culo
        playerCamera.SetActive(true);
        vehicleCamera.SetActive(false);

        // Desactiva el AudioListener de la c�mara del veh�culo
        playerCamera.GetComponent<AudioListener>().enabled = true;
        vehicleCamera.GetComponent<AudioListener>().enabled = false;

        // Desactiva el control del veh�culo
        vehicle.GetComponent<NewCarUserControl>().enabled = false;
    }

    void EnterVehicle()
    {
        // Desactiva el control del personaje
        player.GetComponent<movimeintoJugador>().enabled = false;

        // Activa el control del veh�culo
        vehicle.GetComponent<NewCarUserControl>().enabled = true;

        // Mueve al personaje a una posici�n fuera del veh�culo (ajusta seg�n tu escena)
        player.transform.position = new Vector3(0f, -10f, 0f);

        // Cambia la c�mara activa al veh�culo y desactiva la c�mara del personaje
        playerCamera.SetActive(false);
        vehicleCamera.SetActive(true);

        // Desactiva el AudioListener de la c�mara del personaje
        playerCamera.GetComponent<AudioListener>().enabled = false;
        vehicleCamera.GetComponent<AudioListener>().enabled = true;

        isPlayerInVehicle = true;
    }

    void ExitVehicle()
    {
        // Activa el control del personaje
        player.GetComponent<movimeintoJugador>().enabled = true;

        // Desactiva el control del veh�culo
        vehicle.GetComponent<NewCarUserControl>().enabled = false;

        // Mueve al personaje a una posici�n cerca del veh�culo (ajusta seg�n tu escena)
        player.transform.position = vehicle.transform.position + new Vector3(2f, 0f, 2f);

        // Cambia la c�mara activa al personaje y desactiva la c�mara del veh�culo
        playerCamera.SetActive(true);
        vehicleCamera.SetActive(false);

        // Desactiva el AudioListener de la c�mara del veh�culo
        playerCamera.GetComponent<AudioListener>().enabled = true;
        vehicleCamera.GetComponent<AudioListener>().enabled = false;

        isPlayerInVehicle = false;
    }
}
