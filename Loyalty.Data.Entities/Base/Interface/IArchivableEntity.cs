using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Data.Entities.Base.Interface
{
    public interface IArchivableEntity
    {
        bool IsArchived { get; set; }
    }
}
