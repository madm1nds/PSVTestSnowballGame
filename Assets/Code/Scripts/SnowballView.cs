using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowballView : MonoBehaviour
{
    public static SnowballView instance;
    public int currentNumberSnowball;

    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<SnowballView>();
        }

        gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChangeSnowball.Run(gameObject, currentNumberSnowball); });
        
    }


    //public void ChangeSnowball()
    //{
    //    Debug.Log("Мы тут");
    //    Vault.instance.gameObjectHippoReadySnowball.GetComponent<SpriteRenderer>().sprite = Vault.instance.spriteSnowball[currentNumberSnowball];
    //    for (int i = 0; i < Vault.instance.gameObjectHippoSnowballSet.Length; i++)
    //    {
    //        Vault.instance.gameObjectHippoSnowballSet[i].GetComponent<SpriteRenderer>().sprite = Vault.instance.spriteSnowball[currentNumberSnowball];
    //    }
    //    for (int i = 0; i < Vault.instance.buttonUIItemsMenu.Length; i++)
    //    {
    //        if (Vault.instance.buttonUIItemsMenu[i].CompareTag("Snowball"))
    //        {
    //            Vault.instance.buttonUIItemsMenu[i].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
    //        }
    //    }
    //    gameObject.GetComponent<Image>().color = new Vector4(0, 199f / 255f, 1, 1);
    //}
}
