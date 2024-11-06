using UnityEngine;
using System.Collections;

public class NPCSpawn : MonoBehaviour
{
    //���� ���� �迭
    public Transform[] points;
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
        //Hierarchy View�� Spawn Point�� ã�� ������ �ִ� ��� Transform ������Ʈ�� ã�ƿ�
        points = GameObject.Find("NPCSpawnPoint").GetComponentsInChildren<Transform>();

        if (points.Length > 0)
        {
            //���� ���� �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(this.CreateMonster());
        }
    }

    IEnumerator CreateMonster()
    {
        //���� ���� �ñ��� ���� ����
        while (!isGameOver)
        {
            //���� ������ ���� ���� ����
            int bearCount = (int)GameObject.FindGameObjectsWithTag("Bear").Length;
            int goatCount = (int)GameObject.FindGameObjectsWithTag("Goat").Length;
            int sheepCount = (int)GameObject.FindGameObjectsWithTag("Sheep").Length;

            if (bearCount < maxBear)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(createTime);

                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(1, points.Length);
                //������ ���� ����
                Instantiate(bearPrefab, points[idx].position, points[idx].rotation);
            }
            else if (goatCount < maxGoat)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(createTime);

                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(1, points.Length);
                //������ ���� ����
                Instantiate(goatPrefab, points[idx].position, points[idx].rotation);
            }
            else if (sheepCount < maxSheep)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(createTime);

                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(1, points.Length);
                //������ ���� ����
                Instantiate(sheepPrefab, points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
}