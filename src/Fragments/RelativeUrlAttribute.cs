using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fragments
{
    public class RelativeUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is string[] urls) || !urls.Any())
            {
                return true;
            }

            if (urls.All(x => Uri.TryCreate(x, UriKind.Relative, out _)))
            {
                return true;
            }

            return false;
        }
    }
}