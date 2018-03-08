using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public int id;
    public int unit;

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
