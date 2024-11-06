using UnityEngine;
using System.Collections;

public class NPCSpawn : MonoBehaviour
{
    //스폰 지점 배열
    public Transform[] bearSpawnPoints;
    public Transform[] goatAndSheepSpawnPoints;
    //NPC 프리팹을 할당할 변수
    public GameObject bearPrefab;
    public GameObject goatPrefab;
    public GameObject sheepPrefab;

    //스폰 주기
    public float createTime;
    //NPC최대 마릿수
    public int maxBear = 3;
    public int maxGoat = 5;
    public int maxSheep = 5;
    //게임 종료 여부 변수
    public bool isGameOver = false;

    // Use this for initialization
    void Start()
    {
        //Hierarchy View의 NPCSpawnPoint를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        bearSpawnPoints = GameObject.Find("BearSpawnPoint").GetComponentsInChildren<Transform>();
        goatAndSheepSpawnPoints = GameObject.Find("GoatAndSheepSpawnPoint").GetComponentsInChildren<Transform>();

        if (bearSpawnPoints.Length > 0)
        {
            //몬스터 생성 코루틴 함수 호출
            StartCoroutine(this.CreateBear());
        }

        if (goatAndSheepSpawnPoints.Length > 0)
        {
            StartCoroutine(this.CreateGoatAndSheep());
        }
    }

    IEnumerator CreateBear()
    {
        //게임 종료 시까지 무한 루프
        while (!isGameOver)
        {
            //현재 생성된 NPC 수 산출
            int bearCount = (int)GameObject.FindGameObjectsWithTag("Bear").Length;

            if (bearCount < maxBear)
            {
                //NPC의 생성 주기 시간만큼 대기
                yield return new WaitForSeconds(createTime);

                //불규칙적인 위치 산출
                int idx = Random.Range(1, bearSpawnPoints.Length);
                //NPC의 동적 생성
                Instantiate(bearPrefab, bearSpawnPoints[idx].position, bearSpawnPoints[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator CreateGoatAndSheep()
    {
        //게임 종료 시까지 무한 루프
        while (!isGameOver)
        {
            //현재 생성된 NPC 수 산출
            int goatCount = (int)GameObject.FindGameObjectsWithTag("Goat").Length;
            int sheepCount = (int)GameObject.FindGameObjectsWithTag("Sheep").Length;

            if (goatCount < maxGoat)
            {
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, goatAndSheepSpawnPoints.Length);
                Instantiate(goatPrefab, goatAndSheepSpawnPoints[idx].position, goatAndSheepSpawnPoints[idx].rotation);
            }
            else if (sheepCount < maxSheep)
            {
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, goatAndSheepSpawnPoints.Length);
                Instantiate(sheepPrefab, goatAndSheepSpawnPoints[idx].position, goatAndSheepSpawnPoints[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
}