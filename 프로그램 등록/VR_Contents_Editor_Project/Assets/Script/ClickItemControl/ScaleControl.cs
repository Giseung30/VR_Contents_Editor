using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleControl : MonoBehaviour {
    /**
* date 2018.07.22
* author Lugub
* desc
*  아이템크기조정하는 스크립트
* 
*/
    [Header("ReScale Item")]
    public GameObject _reScaleItem;

    [Header("Slider")]
    public Text _xValueText;
    public Text _yValueText;
    public Text _zValueText;

    public Slider _xSlider;
    public Slider _ySlider;
    public Slider _zSlider;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ScaleChange();
        TextChange();
    }

    void ScaleChange()
    {
        _reScaleItem.transform.localScale = new Vector3(_xSlider.value, _ySlider.value, _zSlider.value);

    }

    void TextChange()
    {
        _xValueText.text = (Mathf.Round(_xSlider.value * 100f) * 0.01).ToString();
        _yValueText.text = (Mathf.Round(_ySlider.value * 100f) * 0.01).ToString();
        _zValueText.text = (Mathf.Round(_zSlider.value * 100f) * 0.01).ToString();

    }

    public void ScaleStart(GameObject _reScaleItem)
    {
        this._reScaleItem = _reScaleItem;
        _xSlider.value = _reScaleItem.transform.localScale.x;
        _ySlider.value = _reScaleItem.transform.localScale.y;
        _zSlider.value = _reScaleItem.transform.localScale.z;

    }

}
