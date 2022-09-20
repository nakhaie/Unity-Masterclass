using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public enum GameData
    {
        PlayerName
    }
    
    public enum InteractType
    {
        Read = 0,
        Take = 1,
        Condition = 2
    }
    
    public InteractType ItemInteractType;
    public string ItemName;

    [Multiline(6)]
    public string ItemDetail;

    public abstract string GetDetail();

    public virtual void ConditionDone()
    {
        Debug.Log("Done!");
    }
}

