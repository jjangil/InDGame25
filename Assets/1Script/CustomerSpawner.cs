using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs; // 여러 손님 프리팹 배열
    public Transform spawnPoint; // 손님이 생성될 위
    public float spawnInterval = 1f; // 손님 생성 간격
    public int maxCustomers = 7; // 최대 손님 수
    private int currentCustomerCount = 0; // 현재 손님 수

    private void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            if (currentCustomerCount < maxCustomers)
            {
                int randIndex = Random.Range(0, customerPrefabs.Length);
                GameObject selectedPrefab = customerPrefabs[randIndex];

                GameObject newCustomer = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
                currentCustomerCount++;
                newCustomer.GetComponent<Custom>().OnCustomerLeave += DecreaseCustomerCount;
            }

            yield return new WaitForSeconds(spawnInterval); // 항상 대기
        }
    }


    // 손님이 떠날 때 호출하여 손님 수를 감소시킴
    public void DecreaseCustomerCount()
    {
        currentCustomerCount--;
    }
}
