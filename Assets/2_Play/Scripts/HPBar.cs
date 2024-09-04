using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] float hp;
    private float hpMax;

    // Start is called before the first frame update
    void Start()
    {
        hpMax = hp;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = hp / hpMax;
    }
}
