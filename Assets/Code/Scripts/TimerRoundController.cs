using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerRoundController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteNumbers;
    private float positionCurrentElement = -150;
    private float spaceBetweenElements = 10;
    int currentTime;
    int minutes;
    int seconds;


    private void SetImageNumber(GameObject numberObject, char numberChar)
    {
        switch (numberChar)
        {
            case '0':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[0];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 82);
                break;
            case '1':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[1];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(52, 80);
                break;
            case '2':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[2];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(54, 81);
                break;
            case '3':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[3];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(55, 82);
                break;
            case '4':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[4];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(61, 80);
                break;
            case '5':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[5];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(55, 81);
                break;
            case '6':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[6];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(57, 82);
                break;
            case '7':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[7];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(56, 80);
                break;
            case '8':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[8];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(58, 82);
                break;
            case '9':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[9];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(57, 82);
                break;
            case ':':
                numberObject.GetComponent<Image>().sprite = spriteNumbers[10];
                numberObject.GetComponent<RectTransform>().sizeDelta = new Vector2(23, 62);
                break;
        }
    }
    private void SetAnchoredPosition(GameObject previousElement, GameObject currentElement)
    {
        positionCurrentElement += ((previousElement.GetComponent<RectTransform>().sizeDelta.x) -
                                                           (currentElement.GetComponent<RectTransform>().sizeDelta.x) + spaceBetweenElements) / 2 +
                                                           currentElement.GetComponent<RectTransform>().sizeDelta.x;
        currentElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionCurrentElement, 0);
    }
    void Start()
    {
        StartCoroutine(RunTimer());
    }
    IEnumerator RunTimer()
    {
        do
        {
            string currentTimeString = "";
            positionCurrentElement = -150;
            yield return new WaitForSeconds(1f);
            currentTime++;
            minutes = currentTime / 60;
            seconds = currentTime - (60 * minutes);
            if (minutes < 10)
            {
                if (minutes == 0)
                {
                    currentTimeString += "00:";
                }
                else
                {
                    currentTimeString += "0" + minutes + ":";
                }
            }
            else
            {
                currentTimeString += minutes;
            }


            if (seconds < 10)
            {
                if (seconds == 0)
                {
                    currentTimeString += "00";
                }
                else
                {
                    currentTimeString += "0" + seconds;
                }
            }
            else
            {
                currentTimeString += seconds;
            }
            for (int i = 0; i < currentTimeString.Length; i++)
            {
                SetImageNumber(transform.GetChild(i + 1).gameObject, currentTimeString[i]);
                SetAnchoredPosition(transform.GetChild(i).gameObject, transform.GetChild(i + 1).gameObject);
            }
        } while (true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
