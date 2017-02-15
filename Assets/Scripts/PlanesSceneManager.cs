using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanesSceneManager : MonoBehaviour
{

    public Transform Planes_Button_Container;
    public GameObject AnimalButtonPrefab;
    public GameObject Next_Button;
    public GameObject Planes_Menu_Button;
    public int GridSize;
    public List<Sprite> PlanesImages = new List<Sprite>();
    public List<AudioClip> PlanesSounds = new List<AudioClip>();
    [HideInInspector]
    public AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();

    }

    void Start()
    {

        SpawnRandomPlanesButton();

        Next_Button.GetComponent<Button>().onClick.AddListener(delegate
        {


            foreach (Transform child in Planes_Button_Container)
            {
                Destroy(child.gameObject);
            }

            SpawnRandomPlanesButton();

            Debug.Log("Next!");
        });

        Planes_Menu_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Back to the menu from Animals!");
            SceneManager.LoadScene("Menu Scene", LoadSceneMode.Single);
        });



    }

    public void SpawnRandomPlanesButton()
    {
        GameObject button;
        Transform childText;

        for (int i = 0; i < 2; i++)
        {
            int randomAnimal = Random.Range(0, PlanesImages.Count);
            Sprite animalSprite = PlanesImages[randomAnimal];
            Debug.Log(i);
            string planeName = animalSprite.name.Split('_')[0];
            Debug.Log(planeName);
            button = Instantiate(AnimalButtonPrefab);
            button.transform.SetParent(Planes_Button_Container);
            button.GetComponent<Image>().sprite = animalSprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("playing " + planeName + " sound");
                PlaySound(planeName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = planeName;
        }




    }

    public void SpawnAnimalButtons()
    {
        GameObject button;
        Transform childText;

        foreach (Sprite PlanesSprite in PlanesImages)
        {

            string planeName = PlanesSprite.name.Split('_')[0];

            button = Instantiate(AnimalButtonPrefab);
            button.transform.SetParent(Planes_Button_Container);
            button.GetComponent<Image>().sprite = PlanesSprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                PlaySound(planeName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = planeName;

        }
    }

    public void PlaySound(string animalName)
    {
        foreach (AudioClip clip in PlanesSounds)
        {
            if (clip.name.Split('_')[0] == animalName)
            {
                this.AudioSource.PlayOneShot(clip);
            }
        }
    }
}
