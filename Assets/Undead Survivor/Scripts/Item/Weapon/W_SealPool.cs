using UnityEngine;
using System.Collections;

public class W_SealPool : Weapon
{
    [SerializeField] W_Seal sealPrefab;
    [SerializeField] ActiveItemData Active;

    [Header("Spawn")]
    [SerializeField] Vector2 spawnOffset = new Vector2(0.3f, 0.5f);
    [SerializeField] Vector2 punchOffset = new Vector2(1.2f, -0.3f);

    [Header("Input")]
    [SerializeField] KeyCode launchKey = KeyCode.LeftShift;

    W_Seal seal;   // 항상 1개만 존재
    private bool bPunch = false;
    private bool bActiveOn = false;
    //void OnEnable()
    //{
    //    StartCoroutine(AttackLoop());
    //}
    //void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    //IEnumerator AttackLoop()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(cooldown);
    //        SpawnSeal();
    //    }
    //}

    protected override void Awake()
    {
        base.Awake();
        LevelUp.bActiveOn[Active] = true;
    }
    protected override IEnumerator AttackRoutine()
    {
        if (seal == null)
        {
            seal = Instantiate(sealPrefab);   // 플레이어 자식으로 두지 않기
            seal.UpdateDamage(Damage);
            seal.transform.SetParent(transform, false);
        }

        if (!seal.IsActive)
        {
            int dir = GetMouseDir();
            Vector2 pos = (Vector2)player.transform.position + new Vector2(spawnOffset.x * dir, spawnOffset.y);
            seal.Spawn(pos, dir);
        }
        yield return null;

    }
    protected override void UpdateData()
    {
        base.UpdateData();
        seal.UpdateDamage(Damage);
    }
    protected override void Update()
    {
        bPunch = (Input.GetKeyDown(launchKey) && (seal != null && seal.CanLaunch));

        if (bPunch && bActiveOn)
        {
            // 누르는 순간의 마우스 기준으로 방향 결정
            int dir = GetMouseDir();
            Vector2 pos = (Vector2)player.transform.position + new Vector2(punchOffset.x * dir, punchOffset.y);
            seal.Launch(dir,pos);
        }
        base.Update();
    }

    int GetMouseDir()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouse.x < player.transform.position.x ? -1 : 1;
    }

    public override void SetActive()
    {
        bActiveOn = true;
    }
}
