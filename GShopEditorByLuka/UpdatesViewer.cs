using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GShopEditorByLuka
{
    public partial class UpdatesViewer : LBLIBRARY.Components.UpdatesForm
    {
        public UpdatesViewer(int Langage)
        {
            InitializeComponent();
            SetLanguage = Langage;
        }
    }
}
