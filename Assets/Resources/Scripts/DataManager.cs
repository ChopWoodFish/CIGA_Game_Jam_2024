using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager
{
    private static List<MapInitBlockSO> listMapSO = new List<MapInitBlockSO>();

    public static MapInitBlockSO GetMapInitBlockSO(int mapIndex)
    {
        if (listMapSO.Count == 0)
        {
            listMapSO = Resources.LoadAll<MapInitBlockSO>("SO").ToList();
        }
        
        var so = listMapSO.Find(x => x.mapIndex == mapIndex);
        return so;
    }

    private static GlobalData globalSO;

    public static GlobalData GetGlobalDataSO()
    {
        if (globalSO == null)
        {
            globalSO = Resources.Load<GlobalData>("SO/GlobalData");
        }
        
        return globalSO;
    }

    private static BlockItemWeightSO blockItemWeightSO;
    public static BlockItemWeightSO GetBlockItemWeightSO()
    {
        if (blockItemWeightSO == null)
        {
            blockItemWeightSO = Resources.Load<BlockItemWeightSO>("SO/BlockItemWeightData");
        }
        
        return blockItemWeightSO;
    }
}