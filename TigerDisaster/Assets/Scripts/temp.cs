using System.Collections;
using UnityEngine;

public class temp : MonoBehaviour
{
    public GameObject targetObject;  // Transform이 있는 GameObject를 받을 변수
    public float slideDuration = 3.0f; // 이동 시간
    public float targetXPosition = -6.7f; // 목표 위치

    void Start()
    {
        // targetObject에서 Transform을 가져옴
        Transform transform = targetObject.GetComponent<Transform>();
        if (transform != null)
        {
            StartCoroutine(SlideOutCoroutine(transform));
        }
        else
        {
            Debug.LogError("targetObject에 Transform이 없습니다.");
        }
    }

    IEnumerator SlideOutCoroutine(Transform transform)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(targetXPosition, startPosition.y, startPosition.z); // 목표 X 위치만 설정

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            // 정확한 비율로 이동하도록 보정
            float t = Mathf.Clamp01(elapsedTime / slideDuration);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막 위치 고정
        transform.position = endPosition;
    }
}
