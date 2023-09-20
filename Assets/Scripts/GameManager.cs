using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool _isPaused;
    
    public GameObject[] pickUpPrefabs;
    [SerializeField]
    public Item[] items; // used in deserializtion

    // Serialization
    [Serializable]
    private class SaveData
    {
        public Dictionary<string, int> SerializedInventory;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0.0f : 1.0f;
        //TODO: some other pause stuff?
    }

    private void SaveGame()
    {
        var data = new SaveData
        {
            SerializedInventory = InventoryManager.Instance.Items
                .ToDictionary(p=> p.Key.name, p=> p.Value)
        };
        var output = JsonConvert.SerializeObject(data);
        
        File.WriteAllText($"{Application.persistentDataPath}/save.json", output);
    }

    private void LoadGame()
    {
        var path = $"{Application.persistentDataPath}/save.json";
        if (File.Exists(path))
        {
            var input = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<SaveData>(input);
            
            // i use the following code because deserealizing a SO throws an error
            if (data == null || data.SerializedInventory == null) return;

            InventoryManager.Instance.Items = new Dictionary<Item, int>();
            foreach (var nameCounterPair in data.SerializedInventory)
            {
                Debug.Log($"{nameCounterPair.Key} : {items[0].name}");
                var item = items.First(i => nameCounterPair.Key.Contains(i.name));
                InventoryManager.Instance.Items[item] = nameCounterPair.Value;
            }
            
            InventoryManager.Instance.OnItemChangedCallback?.Invoke();
        }
        else
        {
            InventoryManager.Instance.Items = new Dictionary<Item, int>();
        }
    }
}
