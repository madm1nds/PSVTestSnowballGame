using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AlignmentTextPicture
{
    Left,
    Right,
    Center
}

public class TextPictureConverter : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteNumbers;
    private float positionCurrentElement = 0;
    private float spaceBetweenElements = 10;
    public void SetImageNumber(GameObject setObject, string convertibleString, float indent, float spaceBetweenElements, AlignmentTextPicture alignmentTextPicture)
    {
        int checkLength = 0;
        int positionCount = 0;
        string TransformedChar;
        positionCurrentElement = indent;
        this.spaceBetweenElements = spaceBetweenElements;

        GameObject[] setObjectArray = new GameObject[setObject.transform.childCount];
        Image[] setObjectImages = new Image[setObject.transform.childCount];
        RectTransform[] setObjectRectTransform = new RectTransform[setObject.transform.childCount];
        for (int i = 0; i < setObjectArray.Length; i++)
        {
            setObjectArray[i] = setObject.transform.GetChild(i).gameObject;
            setObjectImages[i] = setObject.transform.GetChild(i).GetComponent<Image>();
            setObjectRectTransform[i] = setObject.transform.GetChild(i).GetComponent<RectTransform>();
        }

        if (setObjectArray.Length - 1 >= convertibleString.Length)
        {
            checkLength = convertibleString.Length;
        }
        else
        {
            checkLength = setObjectArray.Length - 1;
            Debug.Log("Осторожно! Число элементов в строке больше, чем элементов в наборе! Это может привести к потери данных!");
        }
        for (int i = 0; i < setObjectArray.Length; i++)
        {
            setObjectArray[i].SetActive(false);
        }
        for (int i = 0; i < checkLength; i++)
        {
            switch (convertibleString[i])
            {
                case '/': TransformedChar = "slash"; break;
                case '\\': TransformedChar = "backslash"; break;
                case '|': TransformedChar = "verticalslash"; break;
                case ':': TransformedChar = "colon"; break;
                case '<': TransformedChar = "leftbrackets"; break;
                case '>': TransformedChar = "rightbrackets"; break;
                case '?': TransformedChar = "question"; break;
                case '*': TransformedChar = "asterisk"; break;
                case '"': TransformedChar = "quotation"; break;
                default: TransformedChar = System.Convert.ToString(convertibleString[i]); break;
            }

            for (int j = 0; j < spriteNumbers.Length; j++)//мы находим нужный спрайт
            {
                positionCount = spriteNumbers[j].name.LastIndexOf("_") + 1;//обрезаем всё что после
                if (TransformedChar == spriteNumbers[j].name.Substring(positionCount))//если мы нашли по имени
                {
                    setObjectImages[i + 1].sprite = spriteNumbers[j];
                    setObjectRectTransform[i + 1].sizeDelta = new Vector2(spriteNumbers[j].rect.width, spriteNumbers[j].rect.height);
                    setObjectArray[i + 1].SetActive(true);
                    if (alignmentTextPicture == AlignmentTextPicture.Right || alignmentTextPicture == AlignmentTextPicture.Center)
                    {
                        positionCurrentElement -= spriteNumbers[j].rect.width;//при якоре по центру, алгоритм выравнивает по правой стороне                   
                        positionCurrentElement -= spaceBetweenElements;
                    }
                    break;
                }
            }
        }

        setObject.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(positionCurrentElement),
                                                                        setObject.GetComponent<RectTransform>().sizeDelta.y);
        if (alignmentTextPicture == AlignmentTextPicture.Center)
        {
            positionCurrentElement /= 2;
        }

        for (int i = 0; i < checkLength; i++)
        {
            SetAnchoredPosition(setObjectRectTransform[i], setObjectRectTransform[i + 1]);
        }
    }
    public void SetAnchoredPosition(RectTransform previousElement, RectTransform currentElement)
    {
        positionCurrentElement += ((previousElement.sizeDelta.x) -
                                    (currentElement.sizeDelta.x) + spaceBetweenElements) / 2 +
                                     currentElement.sizeDelta.x;
        currentElement.anchoredPosition = new Vector2(positionCurrentElement, 0);
    }
}
