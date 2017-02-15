using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrucksSceneManager : MonoBehaviour
{

    public Transform TruckButton_Container;
    public GameObject TruckButtonPrefab;
    public GameObject Next_Button;
    public GameObject Trucks_Menu_Button;
    public int GridSize;
    public int lastTruck;
    public List<Sprite> TruckImages = new List<Sprite>();
    public List<AudioClip> Trucksounds = new List<AudioClip>();
    [HideInInspector]
    public AudioSource AudioSource;


    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();

    }

    void Start()
    {

        SpawnRandomTruckButton();

        Next_Button.GetComponent<Button>().onClick.AddListener(delegate
        {


            foreach (Transform child in TruckButton_Container)
            {
                Destroy(child.gameObject);
            }

            SpawnRandomTruckButton();

            Debug.Log("Next!");
        });

        Trucks_Menu_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Back to the menu from Trucks!");
            SceneManager.LoadScene("Menu Scene", LoadSceneMode.Single);
        });



    }

    public void SpawnRandomTruckButton()
    {
        GameObject button;
        Transform childText;

        int buttonCount = 0;

        while (buttonCount < 2)
        {

            int randomTruck = Random.Range(0, TruckImages.Count);
            Sprite Trucksprite = TruckImages[randomTruck];
            Debug.Log(buttonCount);
            string TruckName = Trucksprite.name.Split('_')[0];
            if (randomTruck == lastTruck)
            {
                Debug.Log("duplicate " + randomTruck + " & " + lastTruck);
            }
            else
            {
                Debug.Log(TruckName);
                button = Instantiate(TruckButtonPrefab);
                button.transform.SetParent(TruckButton_Container);
                button.GetComponent<Image>().sprite = Trucksprite;
                button.GetComponent<Button>().onClick.AddListener(delegate {
                    Debug.Log("playing " + TruckName + " sound");
                    PlaySound(TruckName);
                });

                childText = button.transform.GetChild(0);
                childText.GetComponent<Text>().text = TruckName;
                lastTruck = randomTruck;
                buttonCount++;
            }
        }
    }

    public void SpawnTruckButtons()
    {
        GameObject button;
        Transform childText;

        foreach (Sprite Trucksprite in TruckImages)
        {

            string TruckName = Trucksprite.name.Split('_')[0];

            button = Instantiate(TruckButtonPrefab);
            button.transform.SetParent(TruckButton_Container);
            button.GetComponent<Image>().sprite = Trucksprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                PlaySound(TruckName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = TruckName;

        }
    }

    public void PlaySound(string TruckName)
    {
        foreach (AudioClip clip in Trucksounds)
        {
            if (clip.name.Split('_')[0] == TruckName)
            {
                this.AudioSource.PlayOneShot(clip);
            }
        }
    }
}

