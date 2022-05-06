
using UnityEngine;

public class WeatherStates : MonoBehaviour
{
    public GameObject clearDay, rainDay, snowDay, mistDay, cloudsDayBroken;
    public void ClearDay()
    {
        clearDay.SetActive(true);
        rainDay.SetActive(false);
        snowDay.SetActive(false);
        mistDay.SetActive(false);
        cloudsDayBroken.SetActive(false);
    }
    public void RainDay()
    {
        clearDay.SetActive(false);
        rainDay.SetActive(true);
        snowDay.SetActive(false);
        mistDay.SetActive(false);
        cloudsDayBroken.SetActive(false);
    }

    public void SnowDay()
    {
        clearDay.SetActive(false);
        rainDay.SetActive(false);
        snowDay.SetActive(true);
        mistDay.SetActive(false);
        cloudsDayBroken.SetActive(false);
    }
    public void MistDay()
    {
        clearDay.SetActive(false);
        rainDay.SetActive(false);
        snowDay.SetActive(false);
        mistDay.SetActive(true);
        cloudsDayBroken.SetActive(false);
    }

    public void CloudsDayBroken()
    {
        clearDay.SetActive(false);
        rainDay.SetActive(false);
        snowDay.SetActive(false);
        mistDay.SetActive(false);
        cloudsDayBroken.SetActive(true);
    }

}
