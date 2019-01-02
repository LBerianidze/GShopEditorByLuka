using LBLIBRARY;
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
    public partial class Error_search : Form
    {
        public Error_search(Form1 fm, FileGshop its, LBLIBRARY.PWHelper.Elements us,bool ShowTip)
        {
            this.ShowTip = ShowTip;
            this.read = its;
            this.Main_form = fm;
            this.Elements_lists = us;
            InitializeComponent();
        }
        FileGshop read;
        Form1 Main_form;
        LBLIBRARY.PWHelper.Elements Elements_lists;
        int Action = -1;
        int Language = 1;
        int Version;
        bool ShowTip;
        List<Item> ErrorItems = new List<Item>();
        public void RefreshLanguage(int Language, int Version)
        {
            this.Version = Version;
            this.Language = Language;
            Error_combobox.Items.Clear();
            if (Language == 1)
            {
                Error_combobox.Items.AddRange(new object[]{
                                              "Показать товары которые отсутствуют в elements.data",
                                              "Показать товары количество которых равно нулю либо превышает допустимое",
                                              "Показать товары в опциях которых указана неверная цена",
                                              "Показать товары подкатегория которых не существует",
                                              "Показать товары в которых установлено неверное значение \"Статус\"" });
                if (Version >= 1)
                {
                    Error_combobox.Items.Add("Показать товары в которых установлено неверное значение \"Контроль\"");
                }
                if (Version >= 2)
                {
                    Error_combobox.Items.AddRange(new object[]{
                                              "Показать подарки которые отсутствуют в elements.data",
                                              "Показать подарки количество которых равно нулю либо превышает допустимое" });
                }
                if (Version >= 4)
                {
                    Error_combobox.Items.Add("Показать товары продавцы которых отсутствуют в elements.data");
                }
                GotoSelectedItem.Text = "Перейти к выбранным товарам";
                ImproveMistakes.Text = "Исправить ошибки в выбранных товарох";
                DeleteButton.Text = "Удалить выбранные товары";
            }
            else
            {
                Error_combobox.Items.AddRange(new object[]{
                                              "Show items which does not exist in elements.data",
                                              "Show items which amount equals zero or is higher than allowable",
                                              "Show items where options contain wrong price",
                                              "Show items which subcategories do not exist",
                                              "Show items where \"Status\" has incorrect value" });
                if (Version >= 1)
                {
                    Error_combobox.Items.Add("Show items where \"Control\" has incorrect value");
                }
                if (Version >= 2)
                {
                    Error_combobox.Items.AddRange(new object[]{
                                              "Show gifts which does not exist in elements.data",
                                              "Show gifts which amount equals zero or is higher than allowable" });
                }
                if (Version >= 4)
                {
                    Error_combobox.Items.Add("Show items which sellers do not exist in elements.data");
                }
                GotoSelectedItem.Text = "Move to selected items";
                ImproveMistakes.Text = "Fix Errors";
                DeleteButton.Text = "Delete selected items";
            }
        }
        private void Error_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Error_combobox.SelectedIndex)
            {
                case 0:
                    {
                        Action = 0;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Elements_lists != null)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (Elements_lists.Items.FindIndex(z => z.Id == read.List_items[i].Id) == -1)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                }
                            }
                            string ErrorMessage = GetErrorMessage("ID");
                            for (int i = 0; i < ErrorItems.Count; i++)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + ErrorItems[i].Id);
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        Action = 1;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Elements_lists != null)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (read.List_items[i].Amount == 0)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                    continue;
                                }
                                int ItemIndex = Elements_lists.Items.FindIndex(z => z.Id == read.List_items[i].Id);
                                if (ItemIndex != -1)
                                {
                                    if (read.List_items[i].Amount > Elements_lists.Items[ItemIndex].MaxAmount)
                                    {
                                        ErrorItems.Add(read.List_items[i]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (read.List_items[i].Amount == 0)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                }
                            }
                        }
                        string ErrorMessage = GetErrorMessage("AMOUNT");
                        for (int i = 0; i < ErrorItems.Count; i++)
                        {
                            Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + ErrorItems[i].Amount);
                        }
                        break;
                    }
                case 2:
                    {
                        Action = 2;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Elements_lists != null)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (read.List_items[i].Sales[0].Price == 0 || read.List_items[i].Sales[1].Price > 0 || read.List_items[i].Sales[2].Price > 0 || read.List_items[i].Sales[3].Price > 0)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                    continue;
                                }
                            }
                            string ErrorMessage = GetErrorMessage("PRICE");
                            for (int i = 0; i < ErrorItems.Count; i++)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, string.Format("{0} 1){1} 2){2} 3){3} 4){4}", ErrorMessage,
                                     ErrorItems[i].Sales[0].Price,
                                     ErrorItems[i].Sales[1].Price,
                                     ErrorItems[i].Sales[2].Price,
                                     ErrorItems[i].Sales[3].Price));
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        Action = 3;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        for (int i = 0; i < read.List_items.Count; i++)
                        {
                            if (read.List_categories[read.List_items[i].Item_category].Amount < read.List_items[i].Item_sub_category + 1)
                            {
                                ErrorItems.Add(read.List_items[i]);
                            }
                        }
                        for (int i = 0; i < ErrorItems.Count; i++)
                        {
                            if (Language == 1)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, string.Format("Подкатегория {0} категории {1} не найдена", ErrorItems[i].Item_sub_category, ErrorItems[i].Item_category));
                            }
                            else
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, string.Format("Subcategory {0} of category {1} not found", ErrorItems[i].Item_sub_category, ErrorItems[i].Item_category));
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        Action = 4;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        int MaxStatus = 12;
                        if (Version >= 5)
                        {
                            MaxStatus = 19;
                        }
                        for (int i = 0; i < read.List_items.Count; i++)
                        {
                            if (Version == 0)
                            {
                                if (read.List_items[i].Status > 12)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                }
                            }
                            else
                            {
                                if (read.List_items[i].Sales[0].Status > MaxStatus || read.List_items[i].Sales[1].Status > 0 || read.List_items[i].Sales[2].Status > 0 || read.List_items[i].Sales[3].Status > 0)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                }
                            }
                        }
                        string ErrorMessage = GetErrorMessage("STATUS");
                        for (int i = 0; i < ErrorItems.Count; i++)
                        {
                            if (Version == 0)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + Main_form.GetStatusString(ErrorItems[i].Status));
                            }
                            else
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + Main_form.GetStatusString(ErrorItems[i].Sales[0].Status) + "," + Main_form.GetStatusString(ErrorItems[i].Sales[1].Status) + "," + Main_form.GetStatusString(ErrorItems[i].Sales[2].Status) + "," + Main_form.GetStatusString(ErrorItems[i].Sales[3].Status));
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        Action = 5;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Version >= 1)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (read.List_items[i].Sales[0].Control == 0 || read.List_items[i].Sales[1].Control == -1 || read.List_items[i].Sales[2].Control == -1 || read.List_items[i].Sales[3].Control == -1)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                }
                            }
                            string ErrorMessage = GetErrorMessage("CONTROL");
                            for (int i = 0; i < ErrorItems.Count; i++)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + ControlName(ErrorItems[i].Sales[0].Control) + "," + ControlName(ErrorItems[i].Sales[1].Control) + "," + ControlName(ErrorItems[i].Sales[2].Control) + "," + ControlName(ErrorItems[i].Sales[3].Control));
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        Action = 6;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Elements_lists != null)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (read.List_items[i].Gift_id != 0)
                                {
                                    if (Elements_lists.Items.FindIndex(z => z.Id == read.List_items[i].Gift_id) == -1)
                                    {
                                        ErrorItems.Add(read.List_items[i]);
                                    }
                                }
                            }
                            string ErrorMessage = GetErrorMessage("GIFT");
                            for (int i = 0; i < ErrorItems.Count; i++)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + ErrorItems[i].Gift_id);
                            }
                        }
                        break;
                    }
                case 7:
                    {
                        Action = 7;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Elements_lists != null)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                if (read.List_items[i].Gift_id != 0)
                                {
                                    if (read.List_items[i].Gift_amount == 0)
                                    {
                                        ErrorItems.Add(read.List_items[i]);
                                        continue;
                                    }
                                    int ItemIndex = Elements_lists.Items.FindIndex(z => z.Id == read.List_items[i].Gift_id);
                                    if (ItemIndex != -1)
                                    {
                                        if (read.List_items[i].Gift_amount > Elements_lists.Items[ItemIndex].MaxAmount)
                                        {
                                            ErrorItems.Add(read.List_items[i]);
                                        }
                                    }
                                    continue;
                                }
                                if (read.List_items[i].Gift_id == 0 && read.List_items[i].Gift_amount > 0)
                                {
                                    ErrorItems.Add(read.List_items[i]);
                                }
                            }
                        }
                        string ErrorMessage = GetErrorMessage("AMOUNT");
                        for (int i = 0; i < ErrorItems.Count; i++)
                        {
                            Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + ErrorItems[i].Gift_amount);
                        }
                        break;
                    }
                case 8:
                    {
                        Action = 8;
                        Items_grid.Rows.Clear();
                        ErrorItems = new List<Item>();
                        if (Elements_lists != null)
                        {
                            for (int i = 0; i < read.List_items.Count; i++)
                            {
                                for (int z = 0; z < 8; z++)
                                {
                                    if (read.List_items[i].OwnerNpcs[z] != 0)
                                    {
                                        if (Elements_lists.NpcsList.Items.FindIndex(p => p.Id == read.List_items[i].OwnerNpcs[z]) == -1)
                                        {
                                            ErrorItems.Add(read.List_items[i]);
                                            break;
                                        }
                                    }
                                }
                            }
                            string ErrorMessage = GetErrorMessage("NPC");
                            for (int i = 0; i < ErrorItems.Count; i++)
                            {
                                Items_grid.Rows.Add(i + 1, ErrorItems[i].Id, ErrorItems[i].Name, ErrorMessage + string.Join(", ", ErrorItems[i].OwnerNpcs));
                            }
                        }
                        break;
                    }
            }

        }
        public string ControlName(int Index)
        {
            if (Language == 1)
            {
                if (Index == -1)
                {
                    return "Клиент";
                }
                else
                {
                    return "Сервер";
                }
            }
            else
            {
                if (Index == -1)
                {
                    return "Client";
                }
                else
                {
                    return "Server";
                }
            }
        }
        public string GetErrorMessage(string ErrorType)
        {
            if (ErrorType == "ID")
            {
                if (Language == 1)
                {
                    return "ID не найден: ";
                }
                else
                {
                    return "ID not found: ";
                }
            }
            else if (ErrorType == "AMOUNT")
            {
                if (Language == 1)
                {
                    return "Количество равно 0 либо больше допустимого: ";
                }
                else
                {
                    return "Amount equals zero or is higher than allowable: ";
                }
            }
            else if (ErrorType == "PRICE")
            {
                if (Language == 1)
                {
                    return "Неверная цена в опциях: ";
                }
                else
                {
                    return "Incorrect price in options: ";
                }
            }
            else if (ErrorType == "STATUS")
            {
                if (Language == 1)
                {
                    return "Неверный статус в опциях: ";
                }
                else
                {
                    return "Incorrect status in options: ";
                }
            }
            else if (ErrorType == "CONTROL")
            {
                if (Language == 1)
                {
                    return "Неверный контроль в опциях: ";
                }
                else
                {
                    return "Incorrect control in options: ";
                }
            }
            else if (ErrorType == "GIFT")
            {
                if (Language == 1)
                {
                    return "ID подарка не найден: ";
                }
                else
                {
                    return "Gift ID not found: ";
                }
            }
            else if (ErrorType == "GIFTAMOUNT")
            {
                if (Language == 1)
                {
                    return "Недопустимое количество подарка: ";
                }
                else
                {
                    return "Unallowable gift amount: ";
                }
            }
            else if (ErrorType == "NPC")
            {
                if (Language == 1)
                {
                    return "Нпс не найден: ";
                }
                else
                {
                    return "Npc not found: ";
                }
            }
            return "";
        }

        private void GotoSelectedItem_Click(object sender, EventArgs e)
        {
            List<int> Sel = new List<int>();
            foreach (DataGridViewRow item in Items_grid.SelectedRows)
            {
                Sel.Add(read.List_items.IndexOf(ErrorItems[item.Index]));
            }
            Main_form.SelectErrorItems(Sel,false);
        }

        private void Items_grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<int> Sel = new List<int>();
            foreach (DataGridViewRow item in Items_grid.SelectedRows)
            {
                Sel.Add(read.List_items.IndexOf(ErrorItems[item.Index]));
            }
            Main_form.SelectErrorItems(Sel,false);
        }

        private void ImproveMistakes_Click(object sender, EventArgs e)
        {
            if (Action == 0 || Action == 6)
            {
                if (Language == 1)
                {
                    MessageBox.Show("Исправление ошибок этого типа невозможно.Исправьте вручную,либо воспользуйтесь функцией \"Удалить\"...!!", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("This type of errors could not be improved.Fix by your own,or use function \"Delete\"...!!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Action == 1)
            {
                foreach (DataGridViewRow item in Items_grid.SelectedRows)
                {
                    int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                    if (read.List_items[Ind].Amount == 0)
                    {
                        read.List_items[Ind].Amount = 1;
                    }
                    else
                    {
                        if (Elements_lists != null)
                        {
                            int ItemIndex = Elements_lists.Items.FindIndex(z => z.Id == read.List_items[Ind].Id);
                            if (ItemIndex != -1)
                            {
                                if (read.List_items[Ind].Amount > Elements_lists.Items[ItemIndex].MaxAmount)
                                {
                                    read.List_items[Ind].Amount = Elements_lists.Items[ItemIndex].MaxAmount;
                                }
                            }
                        }
                    }
                }
                List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows.RemoveAt(Rows[i]);
                    ErrorItems.RemoveAt(Rows[i]);
                }
                if (Elements_lists != null)
                {
                    if (Language == 1)
                    {
                        MessageBox.Show("Все ошибки связанные с количеством устранены.Предметам количество которых превосходило допустимое,установлено их максимально допустимое значение...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("All errors related to amount were fixed.Items which amount was higher than allowable,set maximal allowable amount...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (Language == 1)
                    {
                        MessageBox.Show("Выбранные предметы которые превышали максимальнео допустимое количество,не были исправлена,так как elements.data не загружен...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Selected items which amount was higher than allowable,were nor fixed,because elements.data is not loaded...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (Action == 2)
            {
                foreach (DataGridViewRow item in Items_grid.SelectedRows)
                {
                    int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                    if (read.List_items[Ind].Sales[0].Price == 0)
                    {
                        read.List_items[Ind].Sales[0].Price = 1;
                    }
                    read.List_items[Ind].Sales[1].Price = 0;
                    read.List_items[Ind].Sales[2].Price = 0;
                    read.List_items[Ind].Sales[3].Price = 0;
                }
                List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows.RemoveAt(Rows[i]);
                    ErrorItems.RemoveAt(Rows[i]);
                }
                if (Language == 1)
                {
                    MessageBox.Show("Все выбранные ошибки связанные с ценой исправлены...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("All selected errors related to price were fixed...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Action == 3)
            {
                foreach (DataGridViewRow item in Items_grid.SelectedRows)
                {
                    int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                    int sb = read.List_categories.FindIndex(z => z.Amount > 0);
                    if (sb != -1)
                    {
                        read.List_items[Ind].Item_sub_category = sb;
                    }
                    else
                    {
                        read.List_categories[0].Sub_categories.Add("New");
                        read.List_categories[0].Amount++;
                        read.List_items[Ind].Item_category = 0;
                        read.List_items[Ind].Item_sub_category = 0;
                    }
                }
                List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows.RemoveAt(Rows[i]);
                    ErrorItems.RemoveAt(Rows[i]);
                }
                if (Language == 1)
                {
                    MessageBox.Show("Все выбранные ошибки связанные с отсутствием подкатегорий исправлены...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("All selected errors related to missing subcategories were fixed...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Action == 4)
            {
                foreach (DataGridViewRow item in Items_grid.SelectedRows)
                {
                    int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                    if (read.List_items[Ind].Status > 12)
                    {
                        read.List_items[Ind].Status = 0;
                    }
                    if (read.List_items[Ind].Sales[0].Status > 12 && read.Version < 5)
                    {
                        read.List_items[Ind].Sales[0].Status = 0;
                    }
                    if (read.List_items[Ind].Sales[0].Status > 19 && read.Version >= 5)
                    {
                        read.List_items[Ind].Sales[0].Status = 0;
                    }
                    read.List_items[Ind].Sales[1].Status = 0;
                    read.List_items[Ind].Sales[2].Status = 0;
                    read.List_items[Ind].Sales[3].Status = 0;
                }
                List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows.RemoveAt(Rows[i]);
                    ErrorItems.RemoveAt(Rows[i]);
                }
                if (Language == 1)
                {
                    MessageBox.Show("Все выбранные ошибки связанные с неверным значением \"статус\" исправлена...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("All selected errors related to wrong \"status\" values were improved...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Action == 5)
            {
                foreach (DataGridViewRow item in Items_grid.SelectedRows)
                {
                    int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                    if (read.List_items[Ind].Sales[0].Control != -1)
                    {
                        read.List_items[Ind].Sales[0].Control = -1;
                    }
                    read.List_items[Ind].Sales[1].Control = 0;
                    read.List_items[Ind].Sales[2].Control = 0;
                    read.List_items[Ind].Sales[3].Control = 0;
                }
                List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows.RemoveAt(Rows[i]);
                    ErrorItems.RemoveAt(Rows[i]);
                }
                if (Language == 1)
                {
                    MessageBox.Show("Все выбранные ошибки связанные с неверным значением \"Контроль\" исправлены...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("All selected errors related to wrong \"Control\" values were improved...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Action == 7)
            {
                foreach (DataGridViewRow item in Items_grid.SelectedRows)
                {
                    int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                    if (read.List_items[Ind].Gift_amount == 0)
                    {
                        read.List_items[Ind].Gift_amount = 1;
                    }
                    else
                    {
                        if (Elements_lists != null)
                        {
                            int ItemIndex = Elements_lists.Items.FindIndex(z => z.Id == read.List_items[Ind].Gift_id);
                            if (ItemIndex != -1)
                            {
                                if (read.List_items[Ind].Gift_amount > Elements_lists.Items[ItemIndex].MaxAmount)
                                {
                                    read.List_items[Ind].Gift_amount = Elements_lists.Items[ItemIndex].MaxAmount;
                                }
                            }
                        }
                    }
                }
                List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows.RemoveAt(Rows[i]);
                    ErrorItems.RemoveAt(Rows[i]);
                }
                if (Elements_lists != null)
                {
                    if (Language == 1)
                    {
                        MessageBox.Show("Все выбранные ошибки связанные с количеством подарков устранены.Предметам количество которых превосходило допустимое,установлено их максимально допустимое значение...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("All selected errors related to gifts amount were fixed.Items which amount was higher than allowable,set maximal allowable amount...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (Language == 1)
                    {
                        MessageBox.Show("Подарки которые превышали максимально допустимое количество не были исправлена,так как elements.data не загружен...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("All errors related to gifts amount were fixed.Items which amount was higher than allowable,set maximal allowable amount...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (Action == 8)
            {
                string Message = "Ошибочные продавцы будут заменены на 0.Желаете продолжить?";
                if (Language == 2)
                {
                    Message = "Error sellers Id's will be replaced by 0.Do you want to continue?";
                }
                var k = MessageBox.Show(Message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (k == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in Items_grid.SelectedRows)
                    {
                        int Ind = read.List_items.IndexOf(ErrorItems[item.Index]);
                        for (int i = 0; i < 8; i++)
                        {
                            if (read.List_items[Ind].OwnerNpcs[i] != 0)
                            {
                                int Index = Elements_lists.NpcsList.Items.FindIndex(f => f.Id == read.List_items[Ind].OwnerNpcs[i]);
                                if (Index == -1)
                                {
                                    read.List_items[Ind].OwnerNpcs[i] = 0;
                                }
                            }
                        }
                    }
                    List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                    for (int i = 0; i < Rows.Count; i++)
                    {
                        Items_grid.Rows.RemoveAt(Rows[i]);
                        ErrorItems.RemoveAt(Rows[i]);
                    }
                    if (Language == 1)
                    {
                        MessageBox.Show("Все выбранные ошибки связанные с Npc-продавцами исправлены...!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("All selected errors related to Npc-sellers were fixed...!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void Error_search_Shown(object sender, EventArgs e)
        {
            if (ShowTip == true)
            {
                if (Language == 1)
                {
                    MessageBox.Show("Обратите внимание,для некоторых действий необходимо загрузить Elements.data.\n1)Поиск предметов(Подарков) Id которых не существует в elements.data \n\n2)Фикс предметов(Подарков) количество которых превышает допустимое \n\n3)Поиск Npc-продавцов товара", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Make attention,for some searches should be loaded Elements.data.That are:\n1)Search items(gifts) which ID does not exist elements.data \n2)Fix items(gifts) which amount is higher than allowable\n3)Search  items Npc-sellers", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            List<int> Sel = new List<int>();
            foreach (DataGridViewRow item in Items_grid.SelectedRows)
            {
                Sel.Add(read.List_items.IndexOf(ErrorItems[item.Index]));
            }
            Main_form.SelectErrorItems(Sel,true);
            List<int> Rows = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
            for (int i = 0; i < Rows.Count; i++)
            {
                Items_grid.Rows.RemoveAt(Rows[i]);
                ErrorItems.RemoveAt(Rows[i]);
            }
        }
    }
}
