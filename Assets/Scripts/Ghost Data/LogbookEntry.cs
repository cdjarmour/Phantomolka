using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogbookEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image portrait;

    public void Setup(GhostData data, bool discovered)
    {
        portrait.sprite = data.portrait;
        portrait.color = discovered ? Color.white : Color.black;
        nameText.text = discovered ? data.ghostName : "???";
        descriptionText.text = discovered ? data.description : "Undiscovered";
    }
}