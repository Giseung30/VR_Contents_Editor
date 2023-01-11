using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* date 2019.07.30
 * author HR
 * desc
 *  자동문 기능
 *  하지만 문 방향이 다르기 때문에 별도로 사용할 것.
 */
public class DoorCol : MonoBehaviour
{
    private Collider[] _hits;
    private Vector3 _pos;
    private Vector3 _size;
    private Vector3 _door;
    private bool _isOpen;
    private float speed = 0.02f;
    private void Start()
    {

        _pos = this.transform.position;
        _size = this.GetComponent<BoxCollider>().bounds.extents;

        _door = _size * 2 - _pos;
        _door.x = _pos.x + _size.x;
        _door.z = _pos.z - _size.z;
        //Debug.Log("_pos: " + _pos);
        //Debug.Log("_size:" + _size);
        //Debug.Log(_door);

        _isOpen = false;

    }

    private void Update()
    {
        _hits = Physics.OverlapBox(_door, new Vector3(_size.x / 2, 1, _size.z + _size.x));

        if (!_isOpen && _hits.Length >= 1)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * Quaternion.Euler(0, 0, -90), speed);
        
            if (this.transform.eulerAngles.y <= 250f)
            {
                _isOpen = true;
            }
       }

        if (_isOpen && _hits.Length <= 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * Quaternion.Euler(0, 0, 90), speed);
     
            if (this.transform.eulerAngles.y <= 180f)
            {
                _isOpen = false;
            }
        }

    }

}
