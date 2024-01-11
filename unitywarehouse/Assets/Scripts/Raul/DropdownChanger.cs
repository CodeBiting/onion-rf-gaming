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
        // Obtén el índice de la opción seleccionada
        int selectedSceneIndex = sceneDropdown.value;

        string[] sceneNames = { "Picking", "SampleScene", "Receive", "Putaway", "Expedition" };//nombres de las escenas

        // Verifica que el índice esté dentro del rango de las escenas disponibles
        if (selectedSceneIndex >= 0 && selectedSceneIndex < sceneNames.Length)
        {
            // Carga la escena correspondiente al índice seleccionado
            SceneManager.LoadScene(sceneNames[selectedSceneIndex]);
        }
    }
}

