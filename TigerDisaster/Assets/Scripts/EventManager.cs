using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Camera mainCamera;  // 메인 카메라
    private Vector3 originalCameraPos;
    public GameObject wallMonseter1;
    public GameObject wallMonseter2;
    public GameObject neckMonster;
    public GameObject legMonster;
    public Sprite[] monsterSprite;  // 바뀔 귀신의 이미지

    public bool isInEvent = false;

    void Start()
    {
        originalCameraPos = new Vector3(0, 0, -10);

    }

    void Update() {
        // if(Input.GetMouseButton(0) && !isInEvent){
        //     StartCoroutine(Event_3());
        // }
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

    //벽귀신 등장 이벤트
    public void Event_1(){
        wallMonseter1.SetActive(true);
    }

    //벽귀신 뒤로 사라지는 이벤트
    public IEnumerator Event_2(){

        if (!isInEvent){
            isInEvent = true;
            wallMonseter1.SetActive(false);
            wallMonseter2.SetActive(true);
            float slideDuration = 2.0f;  // 이동 시간
            float targetXPosition = -6.7f;  // 목표 위치

            Vector3 startPosition = wallMonseter2.transform.position;
            Vector3 endPosition = new Vector3(targetXPosition, startPosition.y, startPosition.z);  // 목표 X 위치만 설정

            float elapsedTime = 0f;

            while (elapsedTime < slideDuration)
            {
                // 정확한 비율로 이동하도록 보정
                float t = Mathf.Clamp01(elapsedTime / slideDuration);
                wallMonseter2.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 마지막 위치 고정
            wallMonseter2.transform.position = endPosition;
        }
        isInEvent = false;
    }

    //목귀신 흔들리는 이벤트
    public IEnumerator Event_3()
    {   
        if (!isInEvent){
            isInEvent = true;
            neckMonster.SetActive(true);
            SpriteRenderer mstSpriter = neckMonster.GetComponent<SpriteRenderer>();
            mstSpriter.sprite = monsterSprite[1];
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
        }
        isInEvent = false;
    }

    //다리 귀신 흔들리는 이벤트
    public IEnumerator Event_4()
    {   
        if (!isInEvent){
            isInEvent = true;
            legMonster.SetActive(true);
            // 원래 위치 저장
            Vector3 originalPosition = legMonster.transform.position;
            Quaternion originalRotation = legMonster.transform.rotation;

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
                legMonster.transform.position = originalPosition + new Vector3(offsetX, 0, 0);
                legMonster.transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationZ);

                yield return null;
            }

            // 다리 이미지 교체 후 위치와 회전 초기화
            SpriteRenderer mstSpriter = legMonster.GetComponent<SpriteRenderer>();
            mstSpriter.sprite = monsterSprite[0];
            mstSpriter.transform.position = originalPosition;
            mstSpriter.transform.rotation = originalRotation;
            
            // 카메라 약한 흔들림 시작
            StartCoroutine(CameraShake());

            // 부드럽게 아래로 떨어지는 효과
            float fallSpeed = 100f;  // 속도 낮춤
            while (legMonster.transform.position.y > -Screen.height)
            {
                legMonster.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
                yield return null;
            }
        }
        isInEvent = false;
    }

    // public IEnumerator Event_13(){
    //     Vector3 startPosition = new Vector3(0, 7.35f, 0);   // 처음 위치 (씬 밖)
    //     Vector3 endPosition = new Vector3(0, 2.65f, 0);     // 최종 위치 (씬 안)
    //     float duration = 5.0f;                              // 이동 시간 (조정 가능)
    //     float elapsedTime = 0f;

    //     // 초기 위치 설정
    //     monsters[12].transform.position = startPosition;

    //     // 지정된 시간 동안 천천히 이동
    //     while (elapsedTime < duration)
    //     {
    //         float t = elapsedTime / duration;  // 시간 비율
    //         monsters[12].transform.position = Vector3.Lerp(startPosition, endPosition, t);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     // 마지막 위치 고정
    //     monsters[12].transform.position = endPosition;
    // }
}
