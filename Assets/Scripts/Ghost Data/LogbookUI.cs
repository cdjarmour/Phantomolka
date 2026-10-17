using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform container;
    [SerializeField] private LogbookEntry entryPrefab;
    [SerializeField] private Behaviour[] disableWhileOpen;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (!Keyboard.current.tabKey.wasPressedThisFrame)
            return;

        if (PlacedCamera.IsViewing)
            return;
        
        bool open = !panel.activeSelf;
        panel.SetActive(open);

        if (open)
        {
                Rebuild();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
        } 
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        foreach (Behaviour b in disableWhileOpen)
            b.enabled = !open;
    }

    void Rebuild()
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
        
        foreach (GhostData g in GhostLogbook.Instance.AllGhosts)
        {
            LogbookEntry entry = Instantiate(entryPrefab, container);
            entry.Setup(g, GhostLogbook.Instance.isDiscovered(g.ghostName));
        }
    }
}