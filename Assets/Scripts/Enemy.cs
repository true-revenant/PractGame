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
        // получаем меш головы
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
        // проверка изменился ли цвет
        while (meshRenderer.material.color != deathColor)
        {
            // плавный переход от одного цвета в другой
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, deathColor, 0.1f);
            // ожидание след выполнения функции Update()
            yield return null;
        }
        // если изменился, то не заходим больше в цикл, ожидаем 3 секунды и уничтожаем объект
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
