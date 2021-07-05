using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    public bool skyrimBar;
    public bool alwaysShowName;
    public bool alwaysShowHPBar;
    public bool isBoss = false;
    public bool disabled;
    [SerializeField] Status status;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI subTitleText;
    public TextMeshProUGUI HpText;

    public bool activated;


    public GameObject container;
    public GameObject nameContainer;

    public Image bar;
    AI AI;



    private void Start()
    {
        if (status == null)
        {
            status = GetComponentInParent<Status>();
            AI = GetComponentInParent<AI>();
        }
        if (AI != null) AI.detectEvent += DisplayInfo;


        if (container != null && !alwaysShowHPBar)
            container.SetActive(false);

        if (nameContainer != null && !alwaysShowName)
            nameContainer.SetActive(false);

        status.healthEvent += UpdateBar;
        status.deathEvent += DisableHPBar;
        UpdateBar();
        SetName();
    }

    private void Update()
    {

        if (disabled) return;

        if (!alwaysShowHPBar && GameManager.Instance.showHPBar)
            if (status.rawStats.currentHealth < status.rawStats.maxHealth)
                container.SetActive(true);

        if (nameContainer != null && alwaysShowName)
            nameContainer.SetActive(true);
        UpdateBar();
       // SetName();
    }

    void DisplayInfo()
    {
        if (!activated || !isBoss) return;
        activated = true;
        container.SetActive(true);
        nameContainer.SetActive(true);
    }

    void DisableHPBar()
    {
        disabled = true;
        if (container != null)
        {
            container.SetActive(false);
            nameContainer.SetActive(false);
            //ou to if (subTitleText != null) subTitleText.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (status != null)
            status.healthEvent -= UpdateBar;
    }

    void SetName()
    {
        if (nameText == null) return;
            nameText.text = status.character.characterName;
        if (subTitleText != null) subTitleText.text = status.character.subTitle;
    }

    // Update is called once per frame
    void UpdateBar()
    {
        HpText.text = "" + status.rawStats.currentHealth + "/" + status.rawStats.maxHealth;
        if (!skyrimBar)
            bar.fillAmount = (float)status.rawStats.currentHealth / status.rawStats.maxHealth;
        else
        {
            bar.transform.localScale = new Vector3((float)status.rawStats.currentHealth / status.rawStats.maxHealth, 1, 1);
        }
    }
}
