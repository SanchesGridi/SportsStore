using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Extensions
{
    public static class CsTypesExtensions
    {
        public static StringBuilder AppendExistingLinesWithStartAdditions(this StringBuilder @this, IEnumerable<string> additions, IEnumerable<string> lines)
        {
            if (@this == null | additions == null | lines == null)
            {
                throw new ArgumentNullException("@This or Additions or Lines", "Some of the arguments are null!");
            }
            if (lines.Any(line => !string.IsNullOrWhiteSpace(line)))
            {
                if (additions.Count() != lines.Count())
                {
                    throw new InvalidOperationException("Additions count should be equal lines count!");
                }
                else
                {
                    var length = additions.Count();

                    for (var index = 0; index < length; index++)
                    {
                        var addition = additions.ElementAt(index);
                        var line = lines.ElementAt(index);

                        if (!string.IsNullOrWhiteSpace(addition) & !string.IsNullOrWhiteSpace(line))
                        {
                            @this.AppendLine($"{addition}{line}");
                        }
                        else
                        {
                            throw new ArgumentException("Check the additions or lines you add!", "Additions or Lines");
                        }
                    }
                }
            }

            return @this;
        }
    }
}
