using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    public void fillBar()
    {
        GameObject g = GameObject.Find("Manager");
        int location = g.GetComponent<Main>().currentPlace;
        if(SaveData.current.profile != null)
        {
            float lifePoints = SaveData.current.profile.getLifePoints(location);
            UnityEngine.Debug.Log("FILL BAR Lifepoints --> " + lifePoints);
            int totalLifePoints = SaveData.current.profile.getTotalLifePoints(location);
            UnityEngine.Debug.Log("FILL BAR TotalLifepoints--> " + totalLifePoints);
            this.GetComponent<Image>().fillAmount = lifePoints / totalLifePoints;
        }
    }
}
