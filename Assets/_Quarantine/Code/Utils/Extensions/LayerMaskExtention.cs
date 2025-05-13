using UnityEngine;

namespace _Quarantine.Code.Utils.Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, int layer) =>
            ((1 << layer) & mask) != 0;
    }
}