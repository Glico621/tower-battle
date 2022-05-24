using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTopBottun : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Start");
    }
}
