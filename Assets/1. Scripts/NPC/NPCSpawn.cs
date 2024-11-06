using UnityEngine;
using System.Collections;

public class NPCSpawn : MonoBehaviour
{
    //���� ���� �迭
    public Transform[] bearSpawnPoints;
    public Transform[] goatAndSheepSpawnPoints;
    //NPC �������� �Ҵ��� ����
    public GameObject bearPrefab;
    public GameObject goatPrefab;
    public GameObject sheepPrefab;

    //���� �ֱ�
    public float createTime;
    //NPC�ִ� ������
    public int maxBear = 3;
    public int maxGoat = 5;
    public int maxSheep = 5;
    //���� ���� ���� ����
    public bool isGameOver = false;

    // Use this for initialization
    void Start()
    {
        //Hierarchy View�� NPCSpawnPoint�� ã�� ������ �ִ� ��� Transform ������Ʈ�� ã�ƿ�
        bearSpawnPoints = GameObject.Find("BearSpawnPoint").GetComponentsInChildren<Transform>();
        goatAndSheepSpawnPoints = GameObject.Find("GoatAndSheepSpawnPoint").GetComponentsInChildren<Transform>();

        if (bearSpawnPoints.Length > 0)
        {
            //���� ���� �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(this.CreateBear());
        }

        if (goatAndSheepSpawnPoints.Length > 0)
        {
            StartCoroutine(this.CreateGoatAndSheep());
        }
    }

    IEnumerator CreateBear()
    {
        //���� ���� �ñ��� ���� ����
        while (!isGameOver)
        {
            //���� ������ NPC �� ����
            int bearCount = (int)GameObject.FindGameObjectsWithTag("Bear").Length;

            if (bearCount < maxBear)
            {
                //NPC�� ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(createTime);

                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(1, bearSpawnPoints.Length);
                //NPC�� ���� ����
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
        //���� ���� �ñ��� ���� ����
        while (!isGameOver)
        {
            //���� ������ NPC �� ����
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