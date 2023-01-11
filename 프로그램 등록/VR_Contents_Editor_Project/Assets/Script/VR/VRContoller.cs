using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRContoller : MonoBehaviour {
    private void Update()
    {
        /* 컨트롤러 앞 부분 트리거 당겼을 때 */
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){

        }

       

        /* 위 부분 터치 패드 눌렀을 때*/
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            GameObject.Find("Controllers/SchedulerController").GetComponent<ButtonController>().PlayButtonClick();
        }
        if (false)
        {
            GameObject.Find("Controllers/SchedulerController").GetComponent<ButtonController>().ResetButtonOnClick();
        }
    }
}
