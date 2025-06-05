using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.ValueObjects
{
    public record Sku
    {
        private readonly string _value;

        public Sku(string value)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch
                (value, @"^[A-Z]{3}-[0-9]{4}$"))
                throw new ArgumentException("SKU must be in format XXX-0000");
            _value = value;
        }

        public override string ToString() => _value;
    }
}
