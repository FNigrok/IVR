using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour
{

    public InventoryBase invbase;

    public List<ItemsInventory> items = new List<ItemsInventory>();

    public GameObject GameObjectShow;

    public GameObject InventoryMainObject;

    public int maxCount;

    public Camera cam;

    public EventSystem es;

    public int currentID;
    public ItemsInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject background;

    //public HoldingPosition hold;

    private void Start()
    {
        if (items.Count == 0)
        {
            AddGraf();
        }
        
        //for(int i = 0; i <maxCount; i++)
        //{
        //    AddItem(i, invbase.items[Random.Range(0, invbase.items.Count)], Random.Range(1, 32));
        //}
        UpdateInventory();
    }

    private void Update()
    {
        if(currentID != -1)
        {
            MoveObject();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            background.SetActive(!background.activeSelf);
            if (background.activeSelf)
            {
                UpdateInventory();
            }
        }

    }

    public void SearchForItem(Item item, int count)
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(items[i].id == item.id)
            {
                if(items[i].count < 64)
                {
                    items[i].count += count;
                    if(items[i].count > 64)
                    {
                        count = items[i].count - 64;
                        items[i].count = 32;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        if(count > 0)
        {
            for(int i = 0; i < maxCount; i++)
            {
                if(items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.sprite;

        if(count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void UpdateInventory()
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObject.GetComponent<Image>().sprite = invbase.items[items[i].id].sprite;
        }
    }
    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyItemsInventory(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = invbase.items[currentItem.id].sprite;

            AddItem(currentID, invbase.items[0], 0);
        }
        else
        {
            ItemsInventory itemsInventory = items[int.Parse(es.currentSelectedGameObject.name)];
            if (currentItem.id != itemsInventory.id)
            {
                AddInventoryItem(currentID, itemsInventory);
                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if(itemsInventory.count + currentItem.count <= 64)
                {
                    itemsInventory.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, invbase.items[itemsInventory.id], itemsInventory.count + currentItem.count - 64);
                    itemsInventory.count = 64;
                }
                itemsInventory.itemGameObject.GetComponentInChildren<Text>().text = itemsInventory.count.ToString();
            }
            currentID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }

    public bool FindInInventory(Item item, int count1)
    {
        
        bool f = false;
        for (int i = 0; i < maxCount; i++)
        {
            int count2 = items[i].count;
            if (items[i].id == item.id)
            {
                if (count2 >= count1)
                {
                    if((count2 -= count1) <= 0){
                        items[i].count = 0;
                        items[i].id = 0;
                        i = maxCount;
                    }
                    else
                    {
                        SearchForItem(item, -count1);
                    }
                    f = true;
                }
                else
                {
                    f = false;
                }
            }
        }
        return f;
    }

    //public void CheckIfNotEmpty(int id)
    //{
    //    if(items[id].id != 0)
    //    {
    //        GameObject NewItem = Instantiate(GameObjectShow, InventoryMainObject.transform) as GameObject;
    //        NewItem.name = items[id].ToString();
    //        ItemsInventory itemsInventory = new ItemsInventory
    //        {
    //            itemGameObject = NewItem
    //        };
    //        //Item newItem = new Item();
    //        //invbase.ReturnItem(newItem, id);
    //        Instantiate(NewItem, new Vector3(hold.itemHold.position.x, hold.itemHold.position.y) , Quaternion.identity);
    //    }
    //}
    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemsInventory CopyItemsInventory(ItemsInventory old)
    {
        ItemsInventory New = new ItemsInventory
        {
            id = old.id,
            itemGameObject = old.itemGameObject,
            count = old.count
        };
        return New;
    }

    public void AddInventoryItem(int id, ItemsInventory itemsInventory)
    {
        items[id].id = itemsInventory.id;
        items[id].count = itemsInventory.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = invbase.items[itemsInventory.id].sprite;

        if (itemsInventory.count > 1 && itemsInventory.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = itemsInventory.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraf()
    {
        for(int i = 0; i < maxCount; i++)
        {
            GameObject NewItem = Instantiate(GameObjectShow, InventoryMainObject.transform) as GameObject;
            NewItem.name = i.ToString();
            ItemsInventory itemsInventory = new ItemsInventory
            {
                itemGameObject = NewItem
            };
            RectTransform rt = NewItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            NewItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);
            Button button = NewItem.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectObject(); });
            items.Add(itemsInventory);
        }
    }
   


    
}

[System.Serializable]
 public class ItemsInventory
{
    public int id;
    public GameObject itemGameObject;
    public int count;


}
