using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeldButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Graphic _targetGraphic;
    public Color normalColor;
    public Color hoverColor;
    public Color heldColor;

    private bool _hovered = false;
    private bool _held = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateColors();
    }

    public bool Hovered
    {
        get { return _hovered; }
    }

    public bool Held
    {
        get { return _held; }
    }

    private void UpdateColors()
    {
        if (_held)
        {
            _targetGraphic.color = heldColor;
        } else if (_hovered)
        {
            _targetGraphic.color = hoverColor;
        }  else
        {
            _targetGraphic.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
        UpdateColors();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _held = true;
        UpdateColors();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _held = false;
        UpdateColors();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovered = false;
        _held = false;
        UpdateColors();
    }
}
