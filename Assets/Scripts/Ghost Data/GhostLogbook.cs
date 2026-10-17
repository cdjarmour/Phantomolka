using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostLogbook : MonoBehaviour
{
    public static GhostLogbook Instance { get; private set; }
    
    private GhostData[] allGhosts;
    private readonly HashSet<string> discovered = new HashSet<string>();
    private const string SaveKey = "DiscoveredGhosts";

    public IReadOnlyList<GhostData> AllGhosts => allGhosts;
    public bool isDiscovered(string ghostName) => discovered.Contains(ghostName);

    public bool AllDiscovered() {
        return discovered.Count == allGhosts.Length;
    }


    void Awake()
    {
        Instance = this;
        allGhosts = Resources.LoadAll<GhostData>("GhostAssets");
        Array.Sort(allGhosts, (a, b) => a.order.CompareTo(b.order));
        Load();
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void Register(string ghostName)
    {
        if (string.IsNullOrEmpty(ghostName))
            return;
        if (discovered.Add(ghostName))
            Save();
    }

    void Save() => PlayerPrefs.SetString(SaveKey, string.Join(",", discovered));

    void Load()
    {
        string saved = PlayerPrefs.GetString(SaveKey, "");
        if (saved.Length == 0)
            return;
        foreach (string n in saved.Split(','))
            discovered.Add(n);
    }
}