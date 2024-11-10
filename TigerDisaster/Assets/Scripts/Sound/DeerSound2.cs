using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerSound2 : MonoBehaviour
{
    void OnEnable()
    {
        // AudioSource가 활성화될 때마다 소리 출력
        Invoke("ActiveSD", 0.5f);





    }

    void ActiveSD()
    {
        AudioManager.Instance.PlaySFX("Strangedeer");
    }
}
