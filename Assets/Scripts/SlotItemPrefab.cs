using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SlotItemPrefab : MonoBehaviour
{
    public Image itemImage;
    public Text itemText;

    public void ItemSetting(Sprite itemSprite, string txt)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
    }
}
