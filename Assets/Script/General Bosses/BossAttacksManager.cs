using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacksManager : MonoBehaviour
{
    [System.Serializable]
    public class Attack
    {
        public GameObject prefab;
        public float duration;
        public Vector3 direction;
    }

    public Attack[] attacks;
    public float initialInterval = 5f;
    public float intervalDecrement = 0.1f;
    public float minimumInterval = 1f;

    private float currentInterval;
    private int lastAttackIndex = -1;

    void Start()
    {
        currentInterval = initialInterval;
        StartCoroutine(ManageAttacks());
    }

    IEnumerator ManageAttacks()
    {
        while (true)
        {
            int currentAttackIndex = GetRandomAttackIndex();
            Attack currentAttack = attacks[currentAttackIndex];
            GameObject attackInstance = Instantiate(currentAttack.prefab, transform.position, Quaternion.identity);

            if (attackInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = currentAttack.direction;
            }
            else if (attackInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
            {
                rb2D.velocity = currentAttack.direction;
            }
            else
            {
                attackInstance.transform.Translate(currentAttack.direction);
            }

            yield return new WaitForSeconds(currentAttack.duration);
            Destroy(attackInstance);

            lastAttackIndex = currentAttackIndex;
            currentInterval = Mathf.Max(minimumInterval, currentInterval - intervalDecrement);
            yield return new WaitForSeconds(currentInterval);
        }
    }

    int GetRandomAttackIndex()
    {
        List<int> possibleIndices = new List<int>();
        for (int i = 0; i < attacks.Length; i++)
        {
            if (i != lastAttackIndex)
            {
                possibleIndices.Add(i);
            }
        }
        int randomIndex = Random.Range(0, possibleIndices.Count);
        return possibleIndices[randomIndex];
    }
}
