using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM._utils.Interface
{
    public interface IHasId
    {
        int Id { get; set; }
        object ID { get; }
    }
}
