using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* date 2019.08.02
 * author HR
 * desc
 *  자동문 기능
 *  하지만 문 방향에 따라 열리는 방향이 달라져야한다.
 */
public class DoorRow : MonoBehaviour {

    private Collider[] _hits;
    private bool _isOpen = false;

    private Vector3 _pos;
    private Vector3 _size;
    private Vector3 _door;

    private float speed = 0.02f;
    // Use this for initialization
	void Start () {
        _pos = this.transform.position;
        _size = this.GetComponent<BoxCollider>().bounds.extents;

        /* 문의 좌표를 잡아준다*/
        _door = _size * 2 - _pos;
        _door.x = _pos.x;
        _door.z = _pos.z + _size.z;
    }
	
	// Update is called once per frame
	void Update () {
        _hits = Physics.OverlapBox(_door, new Vector3(_size.x + _size.z * 2, 1, _size.z));

        if(!_isOpen && _hits.Length >= 1) //현재 문이 열려있지 않은 경우
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * Quaternion.Euler(0, 0, 90), speed);
            if(this.transform.eulerAngles.y <= 180f)
            {
                _isOpen = true;
            }
        }

        if(_isOpen && _hits.Length <= 0) //현재 문이 열려있는 경우
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * Quaternion.Euler(0, 0, -90), speed);
            if(this.transform.eulerAngles.y <= 270f)
            {
                _isOpen = false;
            }
        }
	}
}
