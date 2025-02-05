using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Kino;

public class EventManager : MonoBehaviour
{
    private SceneMove sceneMove;

    public Camera mainCamera;  // 메인 카메라
    public Button btn;
    private dialogueManager dialogueManager;
    private Vector3 originalCameraPos;

    private Animator deerAnimator;
    public GameObject Dear;
    public GameObject StrangeDear;

    //0일차 귀신들
    public GameObject wallMonseter1;
    public GameObject wallMonseter2;
    public GameObject wall;

    //1일차 귀신들
    public GameObject neckMonster1;
    public GameObject legMonster;
    public GameObject maskMonseter1;
    public GameObject maskMonseter2;

    //2일차 귀신들
    public GameObject maskLeg;
    public GameObject[] maskMonster3;
    public GameObject neckMonster2;
    public GameObject neckMonster3;
    public GameObject neckFace;
    public GameObject Face;

    public GameObject blackPanel;
    public GameObject lastFace;
    public GameObject[] hands;


    public GameObject[] footPrint;

    public TMP_Text dialogueText;  // TextMeshPro 텍스트 컴포넌트

    private Sequence mySequence;

    //기타
    public Sprite[] monsterSprite;  // 바뀔 귀신의 이미지
    public static bool isInEvent = false;

    private AnalogGlitch analogGlitch;

    void Awake(){
        dialogueManager = FindObjectOfType<dialogueManager>();
    }

    void Start()
    {
        originalCameraPos = new Vector3(0, 0, -10);
        isInEvent = false;
        sceneMove = FindObjectOfType<SceneMove>();
        analogGlitch = Camera.main.GetComponent<AnalogGlitch>();
    }

    void Update() 
    {

    }
    
    public IEnumerator CameraShake(float shakeDuration, float shakeIntensity)
    {
        for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            mainCamera.transform.position = originalCameraPos + new Vector3(offsetX, 0, 0);
            yield return null;
        }

        mainCamera.transform.position = originalCameraPos;
    }

    public void Event0_0()
    {
        dialogueManager.SelectDialogue(0);
        dialogueManager.DisplayCurrentDialogue();
    }
    public void Event0_3()
    {
        if (!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            Dear.SetActive(true);
            Invoke("DeactivateND", 1f);
        }
    }

    //벽귀신 등장 이벤트
    public void Event0_10(){
        Debug.Log("이벤트 0-10");
        wallMonseter1.SetActive(true);
    }

    //벽귀신 뒤로 사라지는 이벤트
    public IEnumerator Event0_11(){
        Debug.Log("이벤트 0-11");
        if (!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            wallMonseter1.SetActive(false);
            wallMonseter2.SetActive(true);
            wall.SetActive(true);
            
            AudioManager.Instance.PlaySFX("Shock1");

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
        wallMonseter2.SetActive(false);
        btn.interactable = true;
        wall.SetActive(false);
    }

    // 시퀀스를 재생하는 메서드
    public void Event0_13()
    {
        // 이전 시퀀스를 정리하고 새 시퀀스 생성
        if (mySequence != null) mySequence.Kill();

        mySequence = DOTween.Sequence();
        mySequence.OnStart(() => {
            btn.interactable = false;

        }).AppendCallback(() => { footPrint[0].SetActive(true); AudioManager.Instance.PlaySFX("TalGhostWalking"); })  // 객체 활성화
        .AppendInterval(1f)                         // 2초 동안 유지
        .AppendCallback(() => { footPrint[1].SetActive(true); AudioManager.Instance.PlaySFX("TalGhostWalking"); }) // 객체 활성화
        .AppendInterval(1f)
        .AppendCallback(() => { footPrint[2].SetActive(true); AudioManager.Instance.PlaySFX("TalGhostWalking"); }) // 객체 활성화
        .AppendInterval(1f)
        .OnComplete(() => {
            btn.interactable = true;
        });

        mySequence.Restart(); // 시퀀스를 다시 시작
    }

    public void Event0_14()
    {
        footPrint[0].SetActive(false);
        footPrint[1].SetActive(false);
        footPrint[2].SetActive(false);
    }

    //홍살문 등장 하고 다이얼로그
    public void Event0_15(){
        Debug.Log("이벤트 0-15");
        // 다이얼로그 호출
        dialogueManager.SelectDialogue(1);
        dialogueManager.DisplayCurrentDialogue();
    }

    //목귀신 흔들리는 이벤트
    public IEnumerator Event1_2()
    {   
        Debug.Log("이벤트 1-2");
        if (!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            //목귀신 등장후 1.5초 기다림
            neckMonster1.SetActive(true);
            yield return new WaitForSeconds(1.5f);

            //1.5초후 카메라 무빙 시작
            //2초 동안 카메라 흔들기 
            float shakeDuration = 2f;
            float shakeIntensity = 0.2f;
            for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
            {
                float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
                float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
                mainCamera.transform.position = originalCameraPos + new Vector3(offsetX, offsetY, 0);
                yield return null;
            }
            //2초후 날 쳐다보는 목 귀신 이미지로 변경 (0.5초동안 지속)
            SpriteRenderer mstSpriter = neckMonster1.GetComponent<SpriteRenderer>();
            mstSpriter.sprite = monsterSprite[0];
            shakeDuration = 1f;
            for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
            {
                float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
                float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
                mainCamera.transform.position = originalCameraPos + new Vector3(offsetX, offsetY, 0);
                yield return null;
            }

            // 카메라 위치 복원
            mainCamera.transform.position = originalCameraPos;
            neckMonster1.SetActive(false);
            
        }
        isInEvent = false;
        btn.interactable = true;
    }

    public void Event1_4() 
    {
        if (!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            Dear.SetActive(true);
            Invoke("DeactivateND", 1f);
        }
    }

    //다리 귀신 흔들리는 이벤트
    public IEnumerator Event1_6()
    {   
        Debug.Log("이벤트 1-6");
        if (!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            legMonster.SetActive(true);


            AudioManager.Instance.PlaySFX("Squeak");

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
            mstSpriter.sprite = monsterSprite[1];
            mstSpriter.transform.position = originalPosition;
            mstSpriter.transform.rotation = originalRotation;
            
            // 카메라 약한 흔들림 시작
            StartCoroutine(CameraShake(0.5f, 0.5f));

            // 부드럽게 아래로 떨어지는 효과
            AudioManager.Instance.PlaySFX("BodyDump");
            float fallSpeed = 100f;  // 떨어지는 속도
            legMonster.transform.position = new Vector3 (0, 12, 0);
            while (legMonster.transform.position.y > -13f)
            {
                legMonster.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
                yield return null;
            }
        }
        isInEvent = false;
        btn.interactable = true;
    }

    //탈귀신 등장 작은 버전
    public void Event1_9(){
        Debug.Log("이벤트 1-9");
        maskMonseter1.SetActive(true);
    }

    //탈귀신 등장 큰 버전
    public void Event1_10(){
        Debug.Log("이벤트 1-10");
        maskMonseter1.SetActive(false);
        maskMonseter2.SetActive(true);
    }

    //탈귀신 쫒아옴
    public void Event1_11(){
        Debug.Log("이벤트 1-11");
        maskMonseter2.SetActive(false);
        dialogueManager.SelectDialogue(2);
        dialogueManager.DisplayCurrentDialogue();
        
        //철퍽 재생
        AudioManager.Instance.PlaySFX("TalGhostWalking");
        AudioManager.Instance.PlayMusic("backBreathe");
    }

    //탈귀신 쫒아옴
    public void Event1_12(){
        Debug.Log("이벤트 1-12");
        AudioManager.Instance.PlaySFX("TalGhostWalking");
        Invoke("PlaySound", 0.4f);
    }

    //탈귀신 쫒아옴
    public void Event1_13(){
        Debug.Log("이벤트 1-13");
        //철퍽 철퍽 철퍽 재생
        AudioManager.Instance.PlaySFX("TalGhostWalking");
        Invoke("PlaySound", 0.2f);
        Invoke("PlaySound", 0.4f);


    }
    void PlaySound()
    {
        AudioManager.Instance.PlaySFX("TalGhostWalking");
    }

    //홍살문 등장 후 다이얼로그
    public void Event1_14(){
        Debug.Log("이벤트 1-14");
        //다이얼로그 호출
        dialogueManager.SelectDialogue(3);
        dialogueManager.DisplayCurrentDialogue();
        AudioManager.Instance.PlayMusic("2bgm");
    }
    public void Event2_05() {

        if (!isInEvent)
        {
            isInEvent = true;
            btn.interactable = false;

            StrangeDear.SetActive(true);
            Invoke("DeactivateSD", 1.7f);
        }
    }

    void DeactivateND()
    {   
        Dear.SetActive(false);
        isInEvent = false;
        btn.interactable = true;
    }
    void DeactivateSD() {
        StrangeDear.SetActive(false);
        isInEvent = false;
        btn.interactable = true;
    }

    //절벽 등장, 2초후 다이얼로그 등장
    public IEnumerator Event2_7(){
        Debug.Log("이벤트 2-7");
        if(!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            AudioManager.Instance.PlayMusic("WindBlow");
            // 2초 대기
            yield return new WaitForSeconds(2f);
            //다이얼로그 호출
            dialogueManager.SelectDialogue(4);
            dialogueManager.DisplayCurrentDialogue();
            //페이드인아웃
        }        
        isInEvent = false;
        btn.interactable = true;

    }

    //탈귀신 다리만 등장
    public IEnumerator Event2_8(){
        Debug.Log("이벤트 2-8");
        if(!isInEvent){
            isInEvent = true;
            btn.interactable = false;
            AudioManager.Instance.PlayMusic("2bgm");
            maskLeg.SetActive(true);
            //웃는 소리 출력
            AudioManager.Instance.PlaySFX("TalGhostLaugh");
            yield return new WaitForSeconds(2f);
            //철
            //페이드인아웃 처리
            maskLeg.SetActive(false);
            AudioManager.Instance.PlaySFX("TalGhostWalking");
            Invoke("PlaySound", 0.4f);
            yield return new WaitForSeconds(1f);
        }
        isInEvent = false;
        btn.interactable = true;
    }

    public void Event2_9(){
        Debug.Log("이벤트 2-9");
        // 이전 시퀀스를 정리하고 새 시퀀스 생성
        if (mySequence != null) mySequence.Kill();

        mySequence = DOTween.Sequence();
        mySequence.OnStart(() => {
            btn.interactable = false;
            
            analogGlitch.scanLineJitter = 0.35f;
            analogGlitch.verticalJump = 0.15f;
            analogGlitch.horizontalShake = 0.065f;
            analogGlitch.colorDrift = 0.2f;

            AudioManager.Instance.PlayMusic("noise");
            
        }).AppendCallback(() => {
            maskMonster3[0].SetActive(true);
            AudioManager.Instance.PlaySFX("TalGhostLaugh");
            AudioManager.Instance.PlaySFX("btn");
        })  // 객체 활성화
        .AppendInterval(3f)                         // 1초 동안 유지
        .AppendCallback(() => { 
            maskMonster3[0].SetActive(false); 
            maskMonster3[1].SetActive(true);
            analogGlitch.scanLineJitter = 0.55f;
            AudioManager.Instance.PlaySFX("btn");


        }) // 객체 활성화
        .AppendInterval(2f)
        .AppendCallback(() => { 
            maskMonster3[1].SetActive(false); 
            maskMonster3[2].SetActive(true);
            analogGlitch.scanLineJitter = 0.85f;
            AudioManager.Instance.PlaySFX("btn");
            AudioManager.Instance.PlaySFX("shock1");

        }) // 객체 활성화
        .AppendInterval(3f)
        .AppendCallback(() => {
            // maskMonster3[1]의 첫 번째 자식인 머리 오브젝트를 2초 동안 아래로 굴러떨어지게 만듬
            Transform head = maskMonster3[2].transform.GetChild(0); // 첫 번째 자식 (머리 오브젝트)

            // 포물선 모양의 경로로 이동 (x = -t^2 형태, t는 시간)
            Vector3 startPos = head.position;
            float fallDuration = 2f;  // 2초 동안 떨어지도록 설정
            float maxX = 3f;  // x축으로 이동할 최대 거리
            AudioManager.Instance.PlaySFX("neckCracked");

            DOTween.To(() => 0f, x => {
                // -x^2 형태로 이동
                float y = -Mathf.Pow(x, 4);
                head.position = new Vector3(startPos.x - x, startPos.y + y, startPos.z); // 포물선 이동
            }, maxX, fallDuration).SetEase(Ease.InOutSine); // Ease 효과를 추가하여 부드럽게

            // 동시에 회전시키기
            head.DORotate(new Vector3(0f, 0f, 10f), fallDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine);  // 회전 애니메이션에 Ease 효과 추가
        })
        .AppendInterval(4f)
    .OnComplete(() => {

        // AnalogGlitch 효과를 천천히 0으로 줄이기
        DOTween.To(() => analogGlitch.scanLineJitter, x => analogGlitch.scanLineJitter = x, 0f, 2f);
        DOTween.To(() => analogGlitch.verticalJump, x => analogGlitch.verticalJump = x, 0f, 2f);
        DOTween.To(() => analogGlitch.horizontalShake, x => analogGlitch.horizontalShake = x, 0f, 2f);
        DOTween.To(() => analogGlitch.colorDrift, x => analogGlitch.colorDrift = x, 0f, 2f);

        maskMonster3[2].SetActive(false);
        btn.interactable = true;

        AudioManager.Instance.PlayMusic("2bgm");
    });

        mySequence.Restart(); // 시퀀스를 다시 시작
    }

    //목귀신 기립 작은 버전
    public void Event2_12(){
        Debug.Log("이벤트 2-12");
        neckMonster2.SetActive(true);
    }

    //목귀신 기립 큰 버전
    public void Event2_13(){
        Debug.Log("이벤트 2-13");
        neckMonster2.SetActive(false);
        neckMonster3.SetActive(true);
        //다이얼로그 불러와야함
        dialogueManager.SelectDialogue(5);
        dialogueManager.DisplayCurrentDialogue();
    }

    //목 귀신 얼굴 내려오는 이벤트
    public IEnumerator Event2_14(){
        Debug.Log("이벤트 2-14");
        if(!isInEvent){
            AudioManager.Instance.PlaySFX("NeckGhostFace");
            neckMonster3.SetActive(false);
            btn.interactable = false;
            neckFace.SetActive(true);
            Vector3 startPosition = new Vector3(0f, 8f, 0);   // 처음 위치 (씬 밖)
            Vector3 endPosition = new Vector3(0, 2.0f, 0);     // 최종 위치 (씬 안)
            float duration = 5.0f;                              // 이동 시간 (조정 가능)
            float elapsedTime = 0f;

            // 초기 위치 설정
            neckFace.transform.position = startPosition;
            // 1.5초 대기
            yield return new WaitForSeconds(1.5f);
            // 카메라 흔들림 5초 동안
                StartCoroutine(CameraShake(5f, 0.1f)); //5초, 강도 0.1
            // 지정된 시간 동안 천천히 이동
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;  // 시간 비율
                neckFace.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 마지막 위치 고정
            neckFace.transform.position = endPosition;
            StartCoroutine(Typing("왜 무 시 해 ?"));
            yield return new WaitForSeconds(4f);
            dialogueText.text = "";
            neckFace.SetActive(false);
        }
        isInEvent = true;
        btn.interactable = true;
    }

    public void Event2_16()
    {
        Debug.Log("이벤트 2-16");
        AudioManager.Instance.PlaySFX("faceAppear");
        Face.SetActive(true);
    }

    public void Event2_17()
    {
        Debug.Log("이벤트 2-17");

        Face.SetActive(false);
    }

    public IEnumerator Typing(string message){
        dialogueText.text = "";  // 초기 텍스트 비우기
        float typingSpeed = 0.3f;

        for (int i = 0; i < message.Length; i++)
        {
            dialogueText.text += message[i];  // 한 글자씩 추가
            if (message[i] != ' ')
            {
                AudioManager.Instance.PlaySFX("WordImpact");
                mainCamera.transform.DOShakePosition(1f, new Vector3(1, 0, 0), 5, 0, false, true, ShakeRandomnessMode.Full);
            }
            yield return new WaitForSeconds(typingSpeed);  // 지연 시간 설정
        }
    }

    public void Event3_1(){
        if(mySequence != null) mySequence.Kill();
        Vector3 targetPos = new Vector3(6, -3, 0);

        mySequence = DOTween.Sequence();
        mySequence.OnStart(() => {
            btn.interactable = false;
            lastFace.SetActive(true);
            AudioManager.Instance.PlaySFX("DeerShock");
            AudioManager.Instance.PlaySFX("frontBreathe");
            analogGlitch.verticalJump = 0.1f;
            analogGlitch.scanLineJitter = 0.5f;
        }).Append(lastFace.transform.DOMove(targetPos, 0.3f)).Append(lastFace.transform.DOPunchPosition(new Vector3(0, 1, 0), 4, 10, 1, false))
        .OnComplete(() => {
            Event3_2();
        });

        mySequence.Restart(); // 시퀀스를 다시 시작
    }
    //-10.89/ 6.27 -> 최종타겟

    public void Event3_2()
    {
        if (mySequence != null) mySequence.Kill();

        mySequence = DOTween.Sequence();
        mySequence
            .Append(lastFace.transform.DOMove(new Vector3(6.1f, -5, 0), 2f))
            .Join(blackPanel.GetComponent<Image>().DOFade(0.33f, 2f))
            .Join(lastFace.transform.DOScale(2,2))
        .OnComplete(() => {
            Event3_3();
            analogGlitch.verticalJump = 0.50f;

        });

        mySequence.Restart(); // 시퀀스를 다시 시작
    }

    public void Event3_3()
    {
        if (mySequence != null) mySequence.Kill();

        mySequence = DOTween.Sequence();
        mySequence
            .Append(lastFace.transform.DOMove(new Vector3(6.2f, -8, 0), 2f))
            .Join(blackPanel.GetComponent<Image>().DOFade(0.66f, 2f))
            .Join(lastFace.transform.DOScale(3, 2))
        .OnComplete(() => {
            Event3_4();
            analogGlitch.verticalJump = 0.5f;
            analogGlitch.scanLineJitter = 0.9f;
        });

        mySequence.Restart(); // 시퀀스를 다시 시작
    }
    public void Event3_4()
    {
        if (mySequence != null) mySequence.Kill();

        mySequence = DOTween.Sequence();
        mySequence
            .Append(lastFace.transform.DOMove(new Vector3(6.27f, -10.89f, 0), 2f))
            .Join(blackPanel.GetComponent<Image>().DOFade(0.80f, 2f))
            .Join(lastFace.transform.DOScale(4, 2))
        .OnComplete(() => {
            sceneMove.ChangeScene();

        });

        mySequence.Restart(); // 시퀀스를 다시 시작
    }


}
