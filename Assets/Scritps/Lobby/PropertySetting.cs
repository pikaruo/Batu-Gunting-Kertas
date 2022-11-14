using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hastable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class PropertySetting : MonoBehaviourPunCallbacks
{

    [SerializeField] Slider slider;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] string propertyKey;
    [SerializeField] float initialValue = 50;
    [SerializeField] float minValue = 0;
    [SerializeField] float maxValue = 100;
    [SerializeField] bool wholeNumbers = true;


    private void Start()
    {
        // up setup
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.wholeNumbers = wholeNumbers;
        inputField.contentType = wholeNumbers ? TMP_InputField.ContentType.IntegerNumber : TMP_InputField.ContentType.DecimalNumber;

        // ambil initial value dari server kalau ada
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(propertyKey, out var value))
        {
            UpdateSliderInputField((float)value);
        }
        // kalau ada masukkan initial dari inspector
        else
        {
            UpdateSliderInputField(initialValue);
            SetCustomProperty(initialValue);
        }

        // ui interaktif untuk master saja
        if (PhotonNetwork.IsMasterClient == false)
        {
            slider.interactable = false;
            inputField.interactable = false;
        }
    }

    public void InputFromSlider(float value)
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }

        UpdateSliderInputField(value);
        SetCustomProperty(value);
    }

    public void InputFromField(string stringValue)
    {

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (float.TryParse(stringValue, out var floatValue))
        {
            floatValue = Mathf.Clamp(floatValue, slider.minValue, slider.maxValue);
            UpdateSliderInputField(floatValue);
            SetCustomProperty(floatValue);
        }
    }

    private void SetCustomProperty(float value)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var property = new Hastable();
            property.Add(propertyKey, value);
            PhotonNetwork.CurrentRoom.SetCustomProperties(property);
        }
    }

    public override void OnRoomPropertiesUpdate(Hastable propertiesThatChanged)
    {
        if (propertiesThatChanged.TryGetValue(propertyKey, out var value) && PhotonNetwork.IsMasterClient == false)
        {
            UpdateSliderInputField((float)value);
        }
    }

    private void UpdateSliderInputField(float value)
    {
        var floatValue = (float)value;
        slider.value = floatValue;
        if (wholeNumbers)
        {
            inputField.text = (Mathf.RoundToInt(floatValue)).ToString("0");
        }
        else
        {
            inputField.text = (floatValue.ToString("F2"));
        }
    }

}
