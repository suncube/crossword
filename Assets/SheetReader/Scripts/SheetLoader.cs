
using UnityEngine;

namespace SC.SheetReader
{
    [CreateAssetMenu(fileName = "Loader", menuName = "SunCube/Loader")]
    public class SheetLoader : ScriptableObject
    {
        [Header("Sheet Description")]
        public SheetReaderInfo SheetInfo;

        [Header("Data Parcer")]
        public SheetParser Parser;

        public void LoadAndParse()
        {

        }

    }

}