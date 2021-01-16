using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100;
    // prop을 모두 저장
    private List<GameObject> props = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();
        for(int i = 0; i < count; i++)
        {
            Spawn();
        }
        // 생성된 프롭들의 개별 box Collider를 끈다
        area.enabled = false;
    }

    // 생성용 함수
    // 미리 만들어놓은 프랩 중 랜덤 위치에 프롭을 찍어냄
    private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];
        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        props.Add(instance);
    }

    // 랜덤한 vector3 좌표를 찍어주는 함수
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        // 50 2 50
        Vector3 size = area.size;
        // 가로 길이의 절반 왼쪽, 오른쪽
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    public void Reset()
    {
        for(int i = 0; i < props.Count; i++)
        {
            props[i].transform.position = GetRandomPosition();
            props[i].SetActive(true);
        }
    }
}
