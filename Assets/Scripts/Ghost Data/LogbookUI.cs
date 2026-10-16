using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform container;
    [SerializeField] private LogbookEntry entryPrefab;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            bool open = !panel.activeSelf;
            panel.SetActive(open);
            if (open)
                Rebuild();
        }
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