using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PassLevelObj : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GlobalMananger.isPassLevel1 = true;
            SceneManager.LoadScene("0");
        }
    }
}
