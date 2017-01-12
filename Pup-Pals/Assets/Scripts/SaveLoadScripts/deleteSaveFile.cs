using UnityEngine;
using System.Collections;
using System.IO;

public class deleteSaveFile : MonoBehaviour
{

    public int saveFileId;

    public void DeleteSaveGame()
    {
        File.Delete(Application.persistentDataPath + "/" + saveFileId + ".dat");
        File.Delete(Application.persistentDataPath + "/" + saveFileId + ".txt");
        loadSaveInfo.safeInfo.getInfo();
    }


}
