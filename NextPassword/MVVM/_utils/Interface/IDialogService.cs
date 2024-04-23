using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM._utils.Interface
{
    // An interface that can be used to display messages in a modal format
    public interface IDialogService
    {
        void ShowMessage(string message);
    }
}
