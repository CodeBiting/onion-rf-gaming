using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class ServerResponse
{
    public int status;
    public UserData user;
}

[System.Serializable]
public class UserData
{
    public int id;
    public string name;
    public string email;
    public int level;
    public string image;
}

public class UserRequestScript : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField userNameInput;
    [SerializeField]
    private TMP_InputField passwordInput;
    [SerializeField]
    private TextMeshProUGUI statusMessage;
    // URL del servidor
    private string url = "http://localhost:3000/login"; // URL

    public Users m_scriptableUser;


    public void CheckCredentials()
    {
        string inputUserName = userNameInput.text;
        string inputPassword = passwordInput.text;

        // Crea un formulario para enviar los datos al servidor
        WWWForm form = new WWWForm();
        form.AddField("user", inputUserName);
        form.AddField("password", inputPassword);

        StartCoroutine(PostRequest(form));
    }

    public IEnumerator PostRequest(WWWForm form)
    {
        // Crea una solicitud POST
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log(response);
            var json = JsonUtility.FromJson<ServerResponse>(response);

            if (json.user.email.Equals(userNameInput.text))
            {
                statusMessage.color = Color.green;
                statusMessage.text = "Inicio de sesión exitoso";

                m_scriptableUser.nomUsuari = json.user.name;
                Debug.Log("ID: " + json.user.id);
                Debug.Log("Nombre: " + json.user.name);
                Debug.Log("Email: " + json.user.email);
                SceneManager.LoadScene("NouNouAvatar");
            }
            else
            {
                statusMessage.color = Color.red;
                statusMessage.text = "Autenticación fallida";
                Debug.Log("Autenticación fallida");
            }
        }
    }

}
