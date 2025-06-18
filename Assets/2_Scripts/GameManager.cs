using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameFinished = false;

    private int itemCount = 0;
    public TextMeshProUGUI itemCountText;

    private void Awake()
    {
        isGameFinished = false;
        if (instance == null) instance = this;
        Debug.Log("GameManager √ ±‚»≠µ ");
    }

    public void AddItem()
    {
        itemCount++;
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        if (itemCountText != null)
            itemCountText.text = $"Coin: {itemCount}";
    }

    public void ResetItemCount()
    {
        itemCount = 0;
        UpdateItemUI();
    }
}
