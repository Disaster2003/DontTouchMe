using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = PlayerComponent.instance.GetHPBarRatio();
    }
}
