using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBackup.Models
{
    public interface IEntity
    {
        string Id { get; set; }
    }
}
