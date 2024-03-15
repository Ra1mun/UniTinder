using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniTinder.UI.Realisation;
using UnityEngine;
using UnityEngine.UI;

public class MatchUser : MonoBehaviour
{
    public event Action OnLikeButtonClickEvent;
    public event Action OnDislikeButtonClickEvent;
    
    [SerializeField] private Button likeButton;
    [SerializeField] private Button dislikeButton;
    [SerializeField] private Image profileBackground;
    [SerializeField] private Image profileAvatar;
    [SerializeField] private TMP_Text profileNickname;
    [SerializeField] private RectTransform interestContainer;

    public void Initialize(
        Sprite background, 
        Sprite avatar, 
        string nickname)
    {
        profileBackground.sprite = background;
        profileAvatar.sprite = avatar;
        profileNickname.text = nickname;
    }

    public void AddInterest(UIInterest interest)
    {
        interest.transform.SetParent(interestContainer);
    }

    private void OnEnable()
    {
        likeButton.onClick.AddListener(OnLikeButtonClick);
        dislikeButton.onClick.AddListener(OnDislikeButtonClick);
    }

    private void OnLikeButtonClick()
    {
        OnLikeButtonClickEvent?.Invoke();
    }

    private void OnDislikeButtonClick()
    {
        OnDislikeButtonClickEvent?.Invoke();
    }

    private void OnDisable()
    {
        likeButton.onClick.RemoveListener(OnLikeButtonClick);
        dislikeButton.onClick.RemoveListener(OnDislikeButtonClick);
    }
}
