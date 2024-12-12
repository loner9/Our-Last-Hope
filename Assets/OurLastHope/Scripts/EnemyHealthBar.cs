using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        _slider.gameObject.SetActive(false);
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObject != null)
        {
            _camera = cameraObject.GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = target.position + offset;
    }

    public void updateHealthBar(float currentValue, float maxValue)
    {
        if (currentValue < maxValue)
        {
            _slider.gameObject.SetActive(true);
        }
        if (currentValue <= 0)
        {
            _slider.value = 0;
            _slider.gameObject.SetActive(false);
        }
        else
        {
            _slider.value = currentValue / maxValue;
        }
    }
}
