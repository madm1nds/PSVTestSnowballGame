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
        positionCurrentElement = indent;
        this.spaceBetweenElements = spaceBetweenElements;
        if (setObject.transform.childCount - 1 >= convertibleString.Length)
        {
            checkLength = convertibleString.Length;
        }
        else
        {
            checkLength = setObject.transform.childCount - 1;
            Debug.Log("Осторожно! Число элементов в строке больше, чем элементов в наборе! Это может привести к потери данных!");
        }
        for (int i = 0; i < setObject.transform.childCount; i++)
        {
            setObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        for (int i = 0; i < checkLength; i++)
        {
            string TransformedChar;
            int positionCount = 0;
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
                    setObject.transform.GetChild(i + 1).GetComponent<Image>().sprite = spriteNumbers[j];
                    setObject.transform.GetChild(i + 1).GetComponent<RectTransform>().sizeDelta = new Vector2(spriteNumbers[j].rect.width, spriteNumbers[j].rect.height);
                    setObject.transform.GetChild(i + 1).gameObject.SetActive(true);
                    if (alignmentTextPicture == AlignmentTextPicture.Right || alignmentTextPicture == AlignmentTextPicture.Center)
                    {
                        positionCurrentElement -= spriteNumbers[j].rect.width;//при якоре по центру, алгоритм выравнивает по правой стороне                   
                        positionCurrentElement -= spaceBetweenElements;
                    }
                    break;
                }
            }
        }

        setObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(positionCurrentElement), 
                                                                        setObject.transform.GetComponent<RectTransform>().sizeDelta.y);
        if (alignmentTextPicture == AlignmentTextPicture.Center)
        {
            positionCurrentElement /= 2;
        }

        for (int i = 0; i < checkLength; i++)
        {
            SetAnchoredPosition(setObject.transform.GetChild(i).gameObject, setObject.transform.GetChild(i + 1).gameObject);
        }
    }
    public void SetAnchoredPosition(GameObject previousElement, GameObject currentElement)
    {
        positionCurrentElement += ((previousElement.GetComponent<RectTransform>().sizeDelta.x) -
                                                           (currentElement.GetComponent<RectTransform>().sizeDelta.x) + spaceBetweenElements) / 2 +
                                                           currentElement.GetComponent<RectTransform>().sizeDelta.x;
        currentElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionCurrentElement, 0);
    }
}
