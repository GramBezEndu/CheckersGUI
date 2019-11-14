using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGUI.Controls.Buttons
{
    public interface IButton : IDrawableComponent
    {
        EventHandler OnClick { get; set; }
        EventHandler OnSelectedChange { get; set; }
        bool Selected { get; set; }
    }
}
