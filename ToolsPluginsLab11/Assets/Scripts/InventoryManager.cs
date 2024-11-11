using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] inventory;
    public string[] possibleNames;
    // Start is called before the first frame update
    void Start()
    {
        InitializeInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeInventory()
    {
        int amountOfItems = UnityEngine.Random.Range(0, 21);
        Array.Resize(ref inventory, amountOfItems);
        
        for (int i = 0; i < amountOfItems; i++)
        {
            InventoryItem item = new InventoryItem();
            item.itemName = possibleNames[UnityEngine.Random.Range(0, possibleNames.Length)];
            item.id = UnityEngine.Random.Range(0, 21);
            item.value = UnityEngine.Random.Range(0, 101);
            Debug.Log("Item Name: " + item.itemName + " Item ID: " + item.id + " Item Value: " + item.value);
            inventory[i] = item;
        }

        Array.Sort(inventory, (a, b) => a.id.CompareTo(b.id));
    }
    public string LinearSearchByName(string itemNameToSearch)
    {
        for (int i = 0; i <= inventory.Length; i++)
        {

            if (inventory[i].itemName == itemNameToSearch)
            {
                Debug.Log("Item name " + inventory[i].itemName + " at index: " + i);
                return inventory[i].itemName;
            }
        }
        Debug.Log("No name found with that input");
        return null; 
    }

    public int BinarySearchByID(int targetID)
    {
        int left = 0;
        int right = inventory.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (inventory[mid].id == targetID)
            {
                Debug.Log("ID found at index: " + mid);
                return mid; 
            }
            else if (inventory[mid].id < targetID)
            {
                left = mid + 1; 
            }
            else
            {
                right = mid - 1; 
            }
        }
        Debug.Log("No ID found with that input");
        return -1; 
    }

    public int Partition(InventoryItem[] searchInventory, int first, int last)
    {
        int pivot = searchInventory[last].value;
        int smaller = (first - 1);

        for (int element = first; element < last; element++)
        {
            if (searchInventory[element].value < pivot)
            {
                smaller++;

                int temporary = searchInventory[smaller].value;
                searchInventory[smaller].value = searchInventory[element].value;
                searchInventory[element].value = temporary;
            }
        }

        int temporaryNext = searchInventory[smaller + 1].value;
        searchInventory[smaller + 1].value = searchInventory[last].value;
        searchInventory[last].value = temporaryNext;

        return smaller + 1;

    }

    public void QuickSortByValue(int first, int last)
    {
        if (first < last)
        {
            int pivot = Partition(inventory, first, last);

            QuickSortByValue(first, pivot - 1);
            QuickSortByValue(pivot + 1, last);

        }
    }
    
    public void PrintInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            Debug.Log(inventory[i].value);
        }
    }

}
