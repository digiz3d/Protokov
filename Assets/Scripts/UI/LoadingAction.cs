using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAction : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider.gameObject.SetActive(false);
    }
    private void Update()
    {
        slider.value += Time.deltaTime;
    }

    public void Show(float duration)
    {
        slider.minValue = 0;
        slider.maxValue = duration;
        slider.value = 0;
        slider.gameObject.SetActive(true);
    }

    public void Hide()
    {
        slider.gameObject.SetActive(false);
        slider.value = 0;
    }
}
