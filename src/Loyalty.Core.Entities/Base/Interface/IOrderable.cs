using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Core.Entities.Base.Interface
{
    public interface IOrderable
    {
        public bool IsAvailableForOrder { get; set; }
    }
}
