using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimalsSceneManager : MonoBehaviour
{

    public Transform AnimalButton_Container;
    public GameObject ButtonPrefab;
    public GameObject Next_Button;
    public GameObject Animals_Menu_Button;
    public int GridSize;
    public int lastAnimal;
    public List<Sprite> AnimalImages = new List<Sprite>();
    public List<AudioClip> AnimalSounds = new List<AudioClip>();    
    [HideInInspector]
    public AudioSource AudioSource;
    

    private void Awake()
    {        
        AudioSource = GetComponent<AudioSource>();
     
    }

    void Start()
    {

        SpawnRandomAnimalButton();

        Next_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            

            foreach (Transform child in AnimalButton_Container)
            {
                Destroy(child.gameObject);
            }

            SpawnRandomAnimalButton();

            Debug.Log("Next!");
        });

        Animals_Menu_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Back to the menu from Animals!");
            SceneManager.LoadScene("Menu Scene", LoadSceneMode.Single);
        });



    }

    public void SpawnRandomAnimalButton()
    {
        GameObject button;
        Transform childText;
        
        int buttonCount = 0;

        while (buttonCount < 2) {

            int randomAnimal = Random.Range(0, AnimalImages.Count);
            Sprite animalSprite = AnimalImages[randomAnimal];
            Debug.Log(buttonCount);
            string animalName = animalSprite.name.Split('_')[0];
            if ( randomAnimal == lastAnimal)
            {
                Debug.Log("duplicate " + randomAnimal + " & " + lastAnimal);
            }
            else
            {
                Debug.Log(animalName);
                button = Instantiate(ButtonPrefab);
                button.transform.SetParent(AnimalButton_Container);
                button.GetComponent<Image>().sprite = animalSprite;
                button.GetComponent<Button>().onClick.AddListener(delegate {
                    Debug.Log("playing " + animalName + " sound");
                    PlaySound(animalName);
                });

                childText = button.transform.GetChild(0);
                childText.GetComponent<Text>().text = animalName;
                lastAnimal = randomAnimal;
                buttonCount++;
            }

                      
        }

        /*
        for (int i = 0; i < 3; i++)
        {
            int randomAnimal = Random.Range(0, AnimalImages.Count);
            Sprite animalSprite = AnimalImages[randomAnimal];
            Debug.Log(i);
            string animalName = animalSprite.name.Split('_')[0];
            Debug.Log(animalName);
            button = Instantiate(AnimalButtonPrefab);
            button.transform.SetParent(AnimalButton_Container);
            button.GetComponent<Image>().sprite = animalSprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("playing " + animalName + " sound");
                PlaySound(animalName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = animalName;
        }
        */



    }

    public void SpawnAnimalButtons()
    {
        GameObject button;
        Transform childText;

        foreach (Sprite animalSprite in AnimalImages)
        {

            string animalName = animalSprite.name.Split('_')[0];

            button = Instantiate(ButtonPrefab);
            button.transform.SetParent(AnimalButton_Container);
            button.GetComponent<Image>().sprite = animalSprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                PlaySound(animalName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = animalName;

        }
    }

    public void PlaySound(string animalName)
    {
        foreach (AudioClip clip in AnimalSounds)
        {
            if (clip.name.Split('_')[0] == animalName)
            {
                this.AudioSource.PlayOneShot(clip);
            }
        }
    }
}

