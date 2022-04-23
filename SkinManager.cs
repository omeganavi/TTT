using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    static int selectedSkin = 0;
    public GameObject playerSkin;
 

    public void Start()
    {
        playerSkin.GetComponent<SpriteRenderer>().sprite = skins[selectedSkin];
    }



    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];



    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count -1;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void PlayGame()
    {

        playerSkin.GetComponent<SpriteRenderer>().sprite = skins[selectedSkin];
        SceneManager.LoadScene("tutorial");

    }

   

}
