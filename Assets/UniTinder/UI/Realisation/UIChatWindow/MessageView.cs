using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UniTinder.UI.Realisation
{


    public class MessageView : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;

        public void SetText(string text)
        {
            messageText.text = text;
        }
        
    }
}
