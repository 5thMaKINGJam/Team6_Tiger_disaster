using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Camera mainCamera;  // 메인 카메라
    private Vector3 originalCameraPos;
    public SpriteRenderer[] monsters;  // 귀신들
    public Sprite[] monsterSprite;  // 바뀔 귀신의 이미지
    public bool isShaking = false;

    void Start()
    {
        originalCameraPos = new Vector3(0, 0, -10);
    }

    void Update() {
        if(Input.GetMouseButton(0) & !isShaking){
            StartCoroutine(Event_2());
        }
    }
    public IEnumerator CameraShake()
    {
        float shakeDuration = 0.5f;
        float shakeIntensity = 0.5f;

        for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            mainCamera.transform.position = originalCameraPos + new Vector3(offsetX, 0, 0);
            yield return null;
        }

        mainCamera.transform.position = originalCameraPos;
    }
    public IEnumerator Event_1()
    {
        monsters[0].sprite = monsterSprite[0];
        // 카메라 흔들기
        float shakeDuration = 0.5f;
        float shakeIntensity = 0.2f;

        for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
            mainCamera.transform.position = originalCameraPos + new Vector3(offsetX, offsetY, 0);

            yield return null;
        }

        // 카메라 위치 복원
        mainCamera.transform.position = originalCameraPos;
        isShaking = false; // 흔들림 종료
    }

    public IEnumerator Event_2()
    {
        // 원래 위치 저장
        Vector3 originalPosition = monsters[1].transform.position;
        Quaternion originalRotation = monsters[1].transform.rotation;

        // 흔들기 설정
        float shakeDuration = 2f;
        float shakeIntensity = 2f;  // X축 회전 강도
        float shakeSpeed = 3f; //x축 움직임 속도
        float rotationIntensity = 5f;  // Z축 회전 강도

        // 1초 동안 X 좌표와 Z 회전을 부드럽게 흔들기
        for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
        {
            float offsetX = Mathf.Sin(elapsed * shakeSpeed) * shakeIntensity;  // X축 흔들기
            float rotationZ = Mathf.Sin(elapsed * 3f) * rotationIntensity;  // Z축 회전

            // X축 흔들림과 Z축 회전 적용
            monsters[1].transform.position = originalPosition + new Vector3(offsetX, 0, 0);
            monsters[1].transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationZ);

            yield return null;
        }

        // 다리 이미지 교체 후 위치와 회전 초기화
        monsters[1].sprite = monsterSprite[1];
        monsters[1].transform.position = originalPosition;
        monsters[1].transform.rotation = originalRotation;
        
        // 카메라 약한 흔들림 시작
        StartCoroutine(CameraShake());

        // 부드럽게 아래로 떨어지는 효과
        float fallSpeed = 100f;  // 속도 낮춤
        while (monsters[1].transform.position.y > -Screen.height)
        {
            monsters[1].transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
