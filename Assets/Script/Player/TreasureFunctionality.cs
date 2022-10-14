using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureFunctionality : MonoBehaviour
{
    [SerializeField]
    GameObject tOpen, tEmpty, tCLosed;
    [HideInInspector]
    public bool isOpen;
    int randomValue;
    DatabaseManager db;
    // Start is called before the first frame update
    void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
    }

   public void TreatureOpen()
    {
        if (!isOpen)
        {
            TreasureSetting(true, false, false);
            StartCoroutine(GoldAnim());
            isOpen = true;
        }
    }
    public void TreasureSecond()
    {
        if(!isOpen)
        {
            StartCoroutine(GoldSecond());
            isOpen = true;
        }
    }
   IEnumerator GoldSecond()
    {
        yield return new WaitForSeconds(0.25f);
        TresureSecond(false, true);
        randomValue = Random.Range(0, 10);
        Globals.shopMerchant.Gold += randomValue;
        db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
    }
    IEnumerator GoldAnim()
    {
        yield return new WaitForSeconds(0.2f);
        TreasureSetting(false, false, true);
        randomValue = Random.Range(0, 10);
        Globals.shopMerchant.Gold  += randomValue;
        db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
    }
    void TreasureSetting(bool o,bool c,bool e)
    {
        tOpen.SetActive(o);
        tEmpty.SetActive(e);
        tCLosed.SetActive(c);
    }
    void TresureSecond(bool o,bool e)
    {
        tOpen.SetActive(o);
        tEmpty.SetActive(e);
    }
}
