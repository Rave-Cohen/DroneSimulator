using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textComponent; // Using TMP_Text instead of Text
    //public Image imageComponent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = Color.white;
        //imageComponent.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = Color.black;
       // imageComponent.enabled = false;
    }
}
