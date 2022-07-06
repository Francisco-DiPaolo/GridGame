using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Objetive> objetives;
    GameObject Win;
    public string nextLevel;
    public string escena;

    private void Start()
    {
        Win = GameObject.FindGameObjectWithTag("NextLevel");
        foreach (var item in FindObjectsOfType<Objetive>())
        {
            objetives.Add(item);
        }
        Win.SetActive(false);
    }

    void Update()
    {

        if (isComplete())
        {
            Win.SetActive(true);
        }
        else Win.SetActive(false);

        Reinicio();
    }

    bool isComplete()
    {
        foreach (var item in objetives)
        {
            if (!item.Complete) return false;
        }
        return true;
    }

    public void EscenaJuego()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void Reinicio()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(escena);
        }
    }
}
