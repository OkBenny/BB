using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FruitsSceneManager : MonoBehaviour
{

    public Transform Fruits_Button_Container;
    public GameObject AnimalButtonPrefab;
    public GameObject Next_Button;
    public GameObject Fruits_Menu_Button;
    public int GridSize;
    public List<Sprite> FruitsImages = new List<Sprite>();
    public List<AudioClip> FruitsSounds = new List<AudioClip>();    
    [HideInInspector]
    public AudioSource AudioSource;

    private void Awake()
    {        
        AudioSource = GetComponent<AudioSource>();
     
    }

    void Start()
    {

        SpawnRandomFruitsButton();

        Next_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            

            foreach (Transform child in Fruits_Button_Container)
            {
                Destroy(child.gameObject);
            }

            SpawnRandomFruitsButton();

            Debug.Log("Next!");
        });

        Fruits_Menu_Button.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Back to the menu from Fruits!");
            SceneManager.LoadScene("Menu Scene", LoadSceneMode.Single);
        });



    }

    public void SpawnRandomFruitsButton()
    {
        GameObject button;
        Transform childText;

        for (int i = 0; i < 2; i++)
        {
            int randomFruits = Random.Range(0, FruitsImages.Count);
            Sprite FruitsSprite = FruitsImages[randomFruits];
            Debug.Log(i);
            string FruitsName = FruitsSprite.name.Split('_')[0];
            Debug.Log(FruitsName);
            button = Instantiate(AnimalButtonPrefab);
            button.transform.SetParent(Fruits_Button_Container);
            button.GetComponent<Image>().sprite = FruitsSprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("playing " + FruitsName + " sound");
                PlaySound(FruitsName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = FruitsName;
        }




    }

    public void SpawnFruitsButtons()
    {
        GameObject button;
        Transform childText;

        foreach (Sprite FruitsSprite in FruitsImages)
        {

            string FruitsName = FruitsSprite.name.Split('_')[0];

            button = Instantiate(AnimalButtonPrefab);
            button.transform.SetParent(Fruits_Button_Container);
            button.GetComponent<Image>().sprite = FruitsSprite;
            button.GetComponent<Button>().onClick.AddListener(delegate {
                PlaySound(FruitsName);
            });

            childText = button.transform.GetChild(0);
            childText.GetComponent<Text>().text = FruitsName;

        }
    }

    public void PlaySound(string FruitsName)
    {
        foreach (AudioClip clip in FruitsSounds)
        {
            if (clip.name.Split('_')[0] == FruitsName)
            {
                this.AudioSource.PlayOneShot(clip);
            }
        }
    }
}
