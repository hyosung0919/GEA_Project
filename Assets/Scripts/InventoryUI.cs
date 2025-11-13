using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Sprite Dirt;
    public Sprite Grass;
    public Sprite Water;

    public List<Transform> Slot;
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateInventory(Inventory myInven)
    {
        foreach(var slotItems in items)
        {
            Destroy(slotItems);
        }
        items.Clear();

        int idx = 0;

        foreach(var item in myInven.items)
        {
            var go = Instantiate(SlotItem, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;
            SlotItemPrefab sItem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch (item.Key)
            {
                case BlockType.Dirt:
                    sItem.ItemSetting(Dirt, $"{item.Value}");
                    break;
                case BlockType.Grass:
                    sItem.ItemSetting(Grass, $"{item.Value}");
                    break;
                case BlockType.Water:
                    sItem.ItemSetting(Water, $"{item.Value}");
                    break;
            }
        }
    }
}
