
public class NoteController : Item
{
    public GameData[] GameDataInDetail;
    
    public override string GetDetail()
    {
        string result;
        
        if (GameDataInDetail.Length > 0)
        {
            Singleton _singleton = Singleton.Instance;
            string[] dataTarget = new string[GameDataInDetail.Length];

            for (int i = 0; i < dataTarget.Length; i++)
            {
                switch (GameDataInDetail[i])
                {
                    case GameData.PlayerName:
                        dataTarget[i] = _singleton.GetUsername();
                        break;
                }
            }

            result = string.Format(ItemDetail, dataTarget);
        }
        else
        {
            result = ItemDetail;
        }

        return result;
    }
}
