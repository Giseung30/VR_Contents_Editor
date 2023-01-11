using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTypeButton : MonoBehaviour
{
    /**
* date 2019.03.26
* author GS
* desc
* 음성의 타입을 결정하는 버튼에 삽입되는 스크립트.
*/
    [Header("Dynamic")]
    /* 인자 값 */
    public int _type;

    [Header("Script")]
    public VoiceController _voiceController;

    /* 음성 타입 버튼을 클릭 했을 경우 */
    public void OnClickVoiceTypeButton()
    {
        /* 인자 값 음성 총괄 스크립트에 넘겨줌 */
        _voiceController._voiceType = _type;
    }
}