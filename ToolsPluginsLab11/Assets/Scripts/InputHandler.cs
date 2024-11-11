using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] InventoryManager inventoryManager;
    public void ValidateNameInput()
    {
        string input = inputField.text;
        inventoryManager.LinearSearchByName(input);
    }

    public void ValidateIDInput()
    {
        string input = inputField.text;
        int id = Int32.Parse(input);
        inventoryManager.BinarySearchByID(id);
    }

    public void ValidateArraySort()
    {
        inventoryManager.QuickSortByValue(0, inventoryManager.inventory.Length - 1);
        inventoryManager.PrintInventory();
    }
}
