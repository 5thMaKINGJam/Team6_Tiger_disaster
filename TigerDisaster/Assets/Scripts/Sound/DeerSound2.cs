using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerSound2 : MonoBehaviour
{
    void OnEnable()
    {
        // AudioSource�� Ȱ��ȭ�� ������ �Ҹ� ���
        Invoke("ActiveSD", 0.45f);





    }

    void ActiveSD()
    {
        AudioManager.Instance.PlaySFX("Strangedeer");
    }
}
