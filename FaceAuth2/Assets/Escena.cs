
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cambiarescena(string nombredeescena)
    {

        SceneManager.LoadScene(1);
    }
    public void Cambiarescena2(string nombredeescena)
    {

        SceneManager.LoadScene(1);
    }
}
