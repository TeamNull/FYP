using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public int id;
    public int unit;
    public int price;
    public string itemName = "default";
    public string description="default";

    //public GameObject<TextAreaAttribute> description;

    public void UpdateUnit(int increaseUnit)
    {
        unit += increaseUnit;
        return;
    }

    public void ResetUnitToZero() {
        unit = 0;
    }

    public virtual void ApplyAction() {
        //Debug.Log("applyaction in item");
    }
}
