using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System.Collections.Generic;
using System.Linq;

public class GameBehavior : MonoBehaviour, IManager
{
    private int _itemsCollected = 0;
    private int _playerHP = 1;
    public int MaxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;
    public Button WinButton;
    public Button LossButton;
    public Stack<Loot> LootStack = new Stack<Loot>();

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
        Initialize();
    }

    public void Initialize()
    {
        _state = "Game Manager initialized..";
        _state.FancyDebug();
        //Debug.Log(_state);
        debug(_state);
        LogWithDelegate(debug);

        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));

        FilterLoot();

        var itemShop = new Shop<Collectable>();
        itemShop.AddItem(new Potion());
        itemShop.AddItem(new Antidote());
        Debug.Log("Items for sale: " + itemShop.GetStockCount<Potion>());
    }

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items: " + Items;

            if (_itemsCollected >= MaxItems)
            {
                WinButton.gameObject.SetActive(true);
                UpdateScene("You've found all the items!");
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more!";
            }
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            HealthText.text = "Health: " + HP;

            if (_playerHP <= 0)
            {
                LossButton.gameObject.SetActive(true);
                UpdateScene("You want another life with that?");
            }
            else
            {
                ProgressText.text = "Ouch... that's got hurt.";
            }
        }
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        try
        {
            Utilities.RestartLevel(-1);
            debug("Level successfully restarted...");
        }
        catch (System.ArgumentException exception)
        {
            Utilities.RestartLevel(0);
            debug("Reverting to scene 0: " + exception.ToString());
        }
        finally
        {
            debug("Level restart has completed...");
        }
    }

    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem.Name, nextItem.Name);
        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count);
    }

    public void FilterLoot()
    {
        var rareLoot = (from item in LootStack
                        where item.Rarity >= 3
                        orderby item.Rarity
                        select new { item.Name })
                        .Skip(1);

        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.Name);
        }
    }

    public bool LootPredicate(Loot loot)
    {
        return loot.Rarity >= 3;
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    void OnEnable()
    {
        PlayerBehavior.playerJump += HandlePlayerJump;
        debug("Jump event subscribed...");
    }

    private void OnDisable()
    {
        PlayerBehavior.playerJump -= HandlePlayerJump;
        debug("Jump event unsubscribed...");
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }
}
