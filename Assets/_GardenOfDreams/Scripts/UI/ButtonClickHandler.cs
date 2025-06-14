using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private Button _trackingButton;

    public event Action OnClicked;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }
}
