using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAvatarManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;
    public Users m_scriptableUsers;

    private void Start()
    {
        m_text.text = m_scriptableUsers.nomUsuari;
    }
    public void changeReceive()
    {
        SceneManager.LoadScene("Receive");
    }
    public void changePutaway()
    {
        SceneManager.LoadScene("Putaway");
    }
    public void changePicking()
    {
        SceneManager.LoadScene("Picking");
    }
    public void changeExpedition()
    {
        SceneManager.LoadScene("Expedition");
    }
}
