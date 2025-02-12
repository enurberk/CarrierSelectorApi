using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Core.Utilities
{
    public static class ValidationHelper
    {
        public static void Validate(object obj)
        {
            var context = new ValidationContext(obj, null, null);
            Validator.ValidateObject(obj, context, true);
        }
    }
}
