using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LiveObj
{
    private Color deathColor;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        deathColor = Color.white;
        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;
        // �������� ��� ������
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        var playerGO = GameObject.Find("Player");
        if (playerGO != null)
        {
            Debug.Log("Player is found!");
        }
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log($"{name} : OUCH!!!");
        currentHP -= damage;
        if (currentHP <= 0 && IsAlive)
        {
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        // �������� ��������� �� ����
        while (meshRenderer.material.color != deathColor)
        {
            // ������� ������� �� ������ ����� � ������
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, deathColor, 0.1f);
            // �������� ���� ���������� ������� Update()
            yield return null;
        }
        // ���� ���������, �� �� ������� ������ � ����, ������� 3 ������� � ���������� ������
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
