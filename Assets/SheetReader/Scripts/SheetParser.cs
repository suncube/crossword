
using System.Collections.Generic;
using UnityEngine;

namespace SC.SheetReader
{
    [CreateAssetMenu(fileName = "DefaultParser", menuName = "SunCube/DefaultParser")]
    public class SheetParser : ScriptableObject
    {
        public virtual void Parse(IList<IList<object>> data)
        {
        }
    }


}