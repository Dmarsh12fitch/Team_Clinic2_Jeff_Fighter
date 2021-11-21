using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Image HealthDisplay;
    public Image SuperDisplay;

    public void PlayerHealthChange(float value)
    {
        HealthDisplay.fillAmount = value;
    }

    public void PlayerSuperChange(float value)
    {
        SuperDisplay.fillAmount = value;
    }
}
