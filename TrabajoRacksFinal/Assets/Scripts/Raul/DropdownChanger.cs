using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropdownChanger : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown sceneDropdown;

    public void OnDropdownValueChanged()
    {
        // Obt�n el �ndice de la opci�n seleccionada
        int selectedSceneIndex = sceneDropdown.value;

        string[] sceneNames = { "Picking", "SampleScene", "Receive", "Putaway", "Expedition" };//nombres de las escenas

        // Verifica que el �ndice est� dentro del rango de las escenas disponibles
        if (selectedSceneIndex >= 0 && selectedSceneIndex < sceneNames.Length)
        {
            // Carga la escena correspondiente al �ndice seleccionado
            SceneManager.LoadScene(sceneNames[selectedSceneIndex]);
        }
    }
}

