using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class RegulationVersionViewModel
    {
        public string VersionDate { get; set; }
        public string VersionId { get; set; }
    }

    public class RegulationVersionComparer : IEqualityComparer<RegulationVersionViewModel>
    {
        public bool Equals([AllowNull] RegulationVersionViewModel x, [AllowNull] RegulationVersionViewModel y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.VersionDate == y.VersionDate && x.VersionId == y.VersionId;
        }

        public int GetHashCode([DisallowNull] RegulationVersionViewModel obj)
        {
            int hashVersionDate = obj.VersionDate == null ? 0 : obj.VersionDate.GetHashCode();
            int hashVersionId = obj.VersionId == null ? 0 : obj.VersionId.GetHashCode();

            return hashVersionDate ^ hashVersionId;
        }
    }
}
