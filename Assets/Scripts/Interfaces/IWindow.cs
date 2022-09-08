using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IWindow
    {
        public void Show(IWindow prevWindow = null);
        public void Hide();
        public void OnAnimFinish();
    }
}
