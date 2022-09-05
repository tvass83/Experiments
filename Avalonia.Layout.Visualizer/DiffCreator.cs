using Avalonia.Layout.Visualizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalonia.Layout.Visualizer
{
    public static class DiffCreator
    {
        public static Dictionary<string, string> CalcDiff(this ParsedObject refObj, ParsedObject other)
        {
            return refObj.Attributes.Zip(other.Attributes, (kvp1, kvp2) =>
            {
                if (kvp1.Key != kvp2.Key)
                {
                    throw new NotSupportedException();
                }

                if (kvp1.Value != kvp2.Value)
                {
                    return KeyValuePair.Create(kvp1.Key, $"{kvp1.Value} => {kvp2.Value}");
                }

                return KeyValuePair.Create<string, string>("dummy", null);

            })
                .Where(x => x.Value != null)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
