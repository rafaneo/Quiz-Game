using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Manager : MonoBehaviour
{
    public Button start;
    public TextMeshProUGUI count;
  
    public void on_click_start()
    { 
        StartCoroutine(Countdown(1.5f));
        start.interactable = false;
    }
    public IEnumerator Countdown(float secconds)
	{
        for (int i =3; i > 0; i--) { 
            count.text = i.ToString(); 
            yield return new WaitForSeconds(secconds);

		}
        count.text = "START!";
        yield return new WaitForSeconds(1f);
        count.gameObject.SetActive(false);

	}
 
}
