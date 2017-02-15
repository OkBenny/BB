using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager1 : MonoBehaviour
{

    public GameObject Trucks_Button;
    public GameObject Animals_Button;
    public GameObject Planes_Button;
    public GameObject Fruits_Button;
    [HideInInspector]
    //public Scene Animals;    

    private void Awake()
    {
        /*
        AudioSource = GetComponent<AudioSource>();
        */
    }

    void Start()
    {

        Trucks_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Trucks!");
            SceneManager.LoadScene("Trucks", LoadSceneMode.Single);
        });

        Animals_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Animals!");
            SceneManager.LoadScene("Animals", LoadSceneMode.Single);
        });

        Planes_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Planes!");
            SceneManager.LoadScene("Planes", LoadSceneMode.Single);
        });

        Fruits_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Fruits!");
            SceneManager.LoadScene("Fruits", LoadSceneMode.Single);
        });

    }

}
