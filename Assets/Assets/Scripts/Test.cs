using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform player;
    public Transform popup;
    public TextMeshProUGUI textPro;
    [TextArea]
    public string emojiCode;

    [ContextMenu("Chat")]
    public void ApperChat()
    {
        popup.transform.localPosition = new Vector2(player.transform.position.x, player.transform.position.y + 5);
        textPro.text = emojiCode + " \U0001F600";
    }
}
