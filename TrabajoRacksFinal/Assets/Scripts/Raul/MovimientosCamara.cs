using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class MovimientosCamara : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;

    [SerializeField] private Camera terceraPersona;
    [SerializeField] private Camera primeraPersona;

    private Camera camaraActual;

    void Start()
    {
        Assert.IsNotNull(m_InputAsset);

        m_Input = Instantiate(m_InputAsset);
        m_Input.FindActionMap("Character").FindAction("Camera").performed += CambiarCamara;
        m_Input.FindActionMap("Character").Enable();
        // Empezamos con la cámara en primera persona activada
        camaraActual = primeraPersona;
        terceraPersona.enabled = false;
        primeraPersona.enabled = true;
        terceraPersona.GetComponent<AudioListener>().enabled = false;
    }

    private void CambiarCamara(InputAction.CallbackContext context)
    {
        // Desactiva la cámara actual
        camaraActual.enabled = false;
        camaraActual.GetComponent<AudioListener>().enabled = false;

        // Cambia a la otra cámara
        if (camaraActual == terceraPersona)
        {
            camaraActual = primeraPersona;
        }
        else
        {
            camaraActual = terceraPersona;
        }

        // Activa la nueva cámara
        camaraActual.enabled = true;

        camaraActual.GetComponent<AudioListener>().enabled = true;
        camaraActual.GetComponent<Camera>().enabled = true;
    }
}
