using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevlChoose : MonoBehaviour
{
    // Start is called before the first frame update

    public int levelnumber;
    void Start()
    {

    }

    // Update is called once per frame
    public void OpenScence()
    {

        SceneManager.LoadScene("Main");

    }

   

}
