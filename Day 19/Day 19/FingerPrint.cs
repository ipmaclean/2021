using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_19
{
    public class FingerPrint: IEqualityComparer<FingerPrint>, IEquatable<FingerPrint>
    {
        public int ManhattenDistance { get; set; }
        public int MinOffset { get; set; }
        public int MaxOffset { get; set; }

        public FingerPrint(int manhattenDistance, int minOffset, int maxOffset)
        {
            ManhattenDistance = manhattenDistance;
            MinOffset = minOffset;
            MaxOffset = maxOffset;
        }

        public bool Equals(FingerPrint x, FingerPrint y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ManhattenDistance == y.ManhattenDistance &&
                   x.MinOffset == y.MinOffset &&
                   x.MaxOffset == y.MaxOffset;
        }

        public int GetHashCode([DisallowNull] FingerPrint fingerPrint)
            => (fingerPrint.ManhattenDistance, fingerPrint.MinOffset, fingerPrint.MaxOffset).GetHashCode();

        public bool Equals(FingerPrint other)
        {
            if (object.ReferenceEquals(this, other))
                return true;

            if (this is null || other is null)
                return false;

            return ManhattenDistance == other.ManhattenDistance &&
                   MinOffset == other.MinOffset &&
                   MaxOffset == other.MaxOffset;
        }
    }
}
