using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DearSound : MonoBehaviour
{
    void OnEnable()
    {
        // AudioSource가 활성화될 때마다 소리 출력
        AudioManager.Instance.PlaySFX("DeerShock");
    }
}