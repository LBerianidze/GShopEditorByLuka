using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using Dropbox.Api;
using Dropbox.Api.Files;
using LBLIBRARY;

namespace GShopEditorByLuka
{
    public partial class Form1 : Form
    {
        [DllImport("Kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesRead);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public Form1()
        {
            InitializeComponent();
            OpenedWhenStart = false;
        }
        public Form1(string Path)
        {
            InitializeComponent();
            OpenGshopData(Path);
            OpenedWhenStart = true;
        }
        bool OpenedWhenStart = false;
        public int New_category = -1;
        public int New_sub_category = -1;
        public int Category_index;
        public int Sub_category_index;
        int RowIndex;
        int posa;
        int Language = 1;
        int ColorValue = 1;
        int Position;
        public int Gshop1Version;
        int Gshop2Version = 0;
        int Explanation_location;
        bool ElementsIconsLoaded = false;
        bool OpenErrorSearchTip = true;
        string LastLoadedFilePath;
        string SecondLastLoadedFilePath;
        decimal Value;
        public bool IsLinking;
        public int[] InListAmount;
        public FileGshop Gshop1;
        public FileGshop Gshop2;
        IEnumerable<DataGridViewRow> s;
        IEnumerable<Item> Items;
        public LBLIBRARY.PWHelper.Elements Elem;
        Option OptionForm;
        public List<LBLIBRARY.PWHelper.ShopIcon> Surfaces_images;
        public Bitmap LinkedImage;
        public List<Indexes> SelectedRowIndexes;
        Select_id Selected_exs;
        Error_search Error_fm;
        SelectCategoryForm CategoriesForm;
        IconChooser IconsForm;
        private List<LBLIBRARY.PWHelper.Desc> item_ext_desc;
        ToolTip t = new ToolTip();
        GshopsInteraction Interaction;
        static string EditorVersion = "2.3.1";
        bool IsFirstRunAfterUpdate;
        bool SearchFirstClick;

        public List<PWHelper.Desc> Item_ext_desc { get => item_ext_desc; set => item_ext_desc = value; }

        public void ElementsLoaded(bool Status)
        {
            ElementsIconsLoaded = Status;
            if (Elem != null)
            {
                int NameIndex = Elem.Items.FindIndex(z => z.Id == Id_numeric.Value);
                if (NameIndex != -1)
                {
                    ItemRealName.BeginInvoke(new MethodInvoker(delegate
                    {
                        ItemRealName.Text = Elem.Items[NameIndex].Name;
                    }));
                }
                for (int i = 0; i < OwnersGrid.Rows.Count; i++)
                {
                    string Nam = "Unknown";
                    if (Elem != null)
                    {
                        int f = Elem.NpcsList.Items.FindIndex(z => z.Id == Convert.ToInt32(OwnersGrid.Rows[i].Cells[1].Value));
                        if (f != -1)
                        {
                            Nam = Elem.NpcsList.Items[f].Name;
                        }
                    }
                    OwnersGrid.Rows[i].Cells[2].Value = Nam;
                }
                InListAmount = Elem.InListAmount;
                Selected_exs = new Select_id(this, Elem, InListAmount, Status);
            }
            if (Position != -1 && Items_grid.Rows.Count != 0 && Status == true)
            {
                int ind = Elem.Items.FindIndex(i => i.Id == Gshop1.List_items[Position].Id);
                if (ind != -1)
                {
                    Id_image.Image = Elem.Items[ind].Standard_image;
                }
            }
            try
            {
                if (Items_grid.Rows.Count != 0 && Status == true)
                {
                    foreach (DataGridViewRow dg in Items_grid.Rows)
                    {
                        int ind = Elem.Items.FindIndex(i => i.Id == Convert.ToInt32(dg.Cells[1].Value));
                        if (ind != -1)
                        {
                            Items_grid.BeginInvoke(new MethodInvoker(delegate
                            {
                                dg.Cells[2].Value = Elem.Items[ind].IconImage;
                            }));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RefreshCurrentShopImage()
        {
            if (Position != -1 && Items_grid.Rows.Count != 0)
            {
                int ind = Surfaces_images.FindIndex(i => i.Name.ToLower() == Gshop1.List_items[Position].Icon.ToLower());
                if (ind != -1)
                    ShopIcon_picturebox.Image = Surfaces_images[ind].Icon;
            }
        }
        public void SetNewID(int Id, string Item_name, Image RowImage, Image Picurebox_image, int Decision)
        {
            if (Decision == 0)
            {
                this.Id_numeric.Value = Id;
                Id_image.Image = Picurebox_image;
                Items_grid.CurrentRow.Cells[2].Value = RowImage;
                ControlsLeave(Id_numeric, null);
            }
            if (Decision == 1)
            {
                this.Gift_id_numeric.Value = Convert.ToDecimal(Id);
                Gift_image.Image = Picurebox_image;
                ControlsLeave(Gift_id_numeric, null);
            }
        }
        private void OpenGshopData_Click(object sender, EventArgs e)
        {
            string f = sender.ToString();
            if (f == "1.2.6 - 1.4.2")
                Gshop1Version = 0;
            else if (f == "1.4.2 v27")
                Gshop1Version = 1;
            else if (f == "1.4.2 - 1.4.3")
                Gshop1Version = 2;
            else if (f == "1.4.4 - 1.5.1")
                Gshop1Version = 3;
            else if (f == "1.5.2 - 1.5.3")
                Gshop1Version = 4;
            else if (f == "1.5.5 Ru.Official")
                Gshop1Version = 5;
            else if (f == "1.5.5 CN.Official")
                Gshop1Version = 6;
            else
                Gshop1Version = 0;
            if (Dialog_gshop.ShowDialog() == DialogResult.OK)
            {
                OpenGshopData(Dialog_gshop.FileName);
            }
            else
            {
                Dialog_gshop.FileName = null;
            }
        }
        public void OpenGshopData(string path)
        {
            try
            {
                if (File.Exists(path) && path.Contains(".data"))
                {
                    Gshop1 = new FileGshop();
                    Items_grid.Rows.Clear();
                    Gshop1.ReadFile(path, Gshop1Version);
                    if (Gshop1.List_items == null)
                    {
                        if (Language == 1)
                        {
                            MessageBox.Show("Редактор не смог определить версию загружаемого файла Gshop.data.\nПопробуйте открыть файл выбрав подходящую версию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Editor could not determine Gshop.data version.Try open with selection of loading file version", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    RefreshCategoriesNames();
                    string v = "";
                    if (Gshop1.Version == 0) v = "Version 1.2.6";
                    else if (Gshop1.Version == 1) v = "Version 1.4.2 v27";
                    else if (Gshop1.Version == 2) v = "Version 1.4.2";
                    else if (Gshop1.Version == 3) v = "Version 1.4.4";
                    else if (Gshop1.Version == 4) v = "Version 1.5.2";
                    else if (Gshop1.Version == 5) v = "Version 1.5.5 Russian Official";
                    else if (Gshop1.Version == 6) v = "Version 1.5.5 China Official";
                    this.Text = path + "   -   " + v + "    -    " + "Gshop Editor By Luka v" + EditorVersion;
                    if (v != "")
                    {
                        LastLoadedFilePath = path;
                    }
                    TimeStampButton.Text = Gshop1.TimeStamp.ToString();
                    SetControlsInability();
                    All_category.Checked = true;
                    AllCategoriesButtonClick(null, null);
                    ItemsGridChangeIndex(null, null);
                    CategoriesForm = new SelectCategoryForm(this, Gshop1.List_categories);
                    LastLoadedFiles.DropDownItems.Insert(0, new ToolStripMenuItem() { Text = path });
                    SortLastLoadedFiles();
                }
                else
                {
                    Dialog_gshop.FileName = null;
                }
            }
            catch { }
        }
        private void SetControlsInability()
        {
            Control[] cos = new Control[] {Item_name_textbox,Icon_name_textbox,Id_numeric,Icon_name_textbox,Gift_amount_numeric,Gift_id_numeric,ILogPrice_numeric
                                           ,Gift_time_numeric,Sale_combobox,Status_combobox,Control_combobox,AfterbuyTimelimit_numeric,Days_numeric,
                                            Flags_numeric,Selling_end_time_pick,Selling_start_time_pick,VipLevel_numeric,Periodlimit_numeric,Frequency_numeric,Class_combobox,OwnersGrid};
            foreach (var s in cos)
            {
                s.Enabled = false;
            }
            if (Gshop1.List_items != null && Gshop1.List_categories != null)
            {
                if (Gshop1.Version >= 0)
                {
                    Id_numeric.Enabled = true;
                    Amount_numeric.Enabled = true;
                    Price_numeric.Enabled = true;
                    Icon_name_textbox.Enabled = true;
                    Item_name_textbox.Enabled = true;
                    Sale_combobox.Enabled = true;
                    AfterbuyTimelimit_numeric.Enabled = true;
                    Selling_end_time_pick.Enabled = true;
                    Explanation_textbox.Enabled = true;
                    Status_combobox.Enabled = true;
                    #region FillStatus
                    Status_combobox.Items.Clear();
                    if (Language == 1)
                    {
                        Status_combobox.Items.AddRange(new string[] {"Обычный",
"Новое",
"Популярное",
"Продаваемое",
"Скидка 10%",
"Скидка 20%",
"Скидка 30%",
"Скидка 40%",
"Скидка 50%",
"Скидка 60%",
"Скидка 70%",
"Скидка 80%",
"Скидка 90%"
});
                    }
                    else if (Language == 2)
                    {
                        Status_combobox.Items.AddRange(new string[] {"Default",
"New",
"Popular",
"Often selling",
"Sale 10%",
"Sale 20%",
"Sale 30%",
"Sale 40%",
"Sale 50%",
"Sale 60%",
"Sale 70%",
"Sale 80%",
"Sale 90%"});
                    }
                    #endregion
                }
                if (Gshop1.Version >= 1)
                {
                    Selling_start_time_pick.Enabled = true;
                    Days_numeric.Enabled = true;
                    Flags_numeric.Enabled = true;
                    Control_combobox.Enabled = true;
                }
                if (Gshop1.Version >= 2)
                {
                    Gift_id_numeric.Enabled = true;
                    Gift_amount_numeric.Enabled = true;
                    Gift_time_numeric.Enabled = true;
                }
                if (Gshop1.Version >= 3)
                {
                    ILogPrice_numeric.Enabled = true;
                }
                if (Gshop1.Version >= 4)
                {
                    OwnersGrid.Enabled = true;
                }
                if (Gshop1.Version >= 5)
                {
                    VipLevel_numeric.Enabled = true;
                    Periodlimit_numeric.Enabled = true;
                    Frequency_numeric.Enabled = true;
                    Status_combobox.Items.AddRange(new string[] { "Vip0", "Vip1", "Vip2", "Vip3", "Vip4", "Vip5", "Vip6" });
                }
                if (Gshop1.Version >= 6)
                    Class_combobox.Enabled = true;

            }
        }
        private void ShowOptionsWindow(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                OptionForm.RefreshLanguage(Language);
                OptionForm.ShowDialog();
                this.Focus();
            }
            else
                if (Language == 1)
                MessageBox.Show("Gshop.data не загружен!!...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (Language == 2)
                MessageBox.Show("Gshop.data is not loaded!!...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SetDescColor(object sender, EventArgs e)
        {
            if (SetColor_Window.ShowDialog() == DialogResult.OK)
            {
                string color = String.Format("{0:X2}{1:X2}{2:X2}", SetColor_Window.Color.R, SetColor_Window.Color.G, SetColor_Window.Color.B);
                Explanation_textbox.Text = Explanation_textbox.Text.Insert(Explanation_location, "^" + color);
            }
        }
        public void RefreshCategoriesNames()
        {
            if (Gshop1.List_categories != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    Control[] sd = Controls.Find("Category" + i.ToString(), true);
                    sd[0].Text = Gshop1.List_categories[i].Category_name;
                }
            }
        }
        private void SubCategoriesMouseDown(object sender, MouseEventArgs e)
        {
            if (Gshop1 != null)
            {
                Control Contr = sender as Control;
                var Radio = Contr as RadioButton;
                Radio.Checked = true;
                Sub_category_index = int.Parse(Contr.Name.Substring(12, 1));
                Items_grid.Rows.Clear();
                if (Gshop1.List_items.Count != 0 && Category_index > -1)
                {
                    int Index = 1;
                    var Items = Gshop1.List_items.Where(i => i.Item_category == Category_index && i.Item_sub_category == Sub_category_index);
                    if (Items != null)
                    {
                        foreach (var x in Items)
                        {
                            Items_grid.Rows.Add(Index, x.Id, GetImageById(x.Id), x.Name, x.Amount, Convert.ToDecimal(x.Sales[0].Price) / 100);
                            if (ChangeStyleColor.Checked == true & Elem != null)
                            {
                                int id = Elem.ElementsLists[21].Items.FindIndex(v => v.Id == x.Id);
                                if (id != -1)
                                {
                                    Items_grid.Rows[Items_grid.Rows.Count - 1].Cells[3].Style.ForeColor = (int)Elem.ElementsLists[21].Items[id].Values[10] == 1 ? Color.LightPink : Color.LightGreen;
                                }
                            }
                            Index++;
                        }
                    }
                    ItemsGridChangeIndex(null, null);
                    SearchFirstClick = true;
                    SeachTextChanged(null, null);
                }
            }
        }
        private void AllCategoriesButtonClick(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                Category_index = -1;
                Sub_category_index = -1;
                if (Gshop1.List_items != null)
                {
                    Items_grid.ScrollBars = ScrollBars.None;
                    Items_grid.Rows.Clear();
                    for (int i = 0; i < Gshop1.List_items.Count; i++)
                    {
                        Items_grid.Rows.Add(i + 1, Gshop1.List_items[i].Id, GetImageById(Gshop1.List_items[i].Id), Gshop1.List_items[i].Name, Gshop1.List_items[i].Amount, Convert.ToDecimal(Gshop1.List_items[i].Sales[0].Price) / 100);
                        if (ChangeStyleColor.Checked == true & Elem != null)
                        {
                            int id = Elem.ElementsLists[21].Items.FindIndex(v => v.Id == Gshop1.List_items[i].Id);
                            if (id != -1)
                            {
                                Items_grid.Rows[Items_grid.Rows.Count - 1].Cells[3].Style.ForeColor = (int)Elem.ElementsLists[21].Items[id].Values[10] == 1 ? Color.LightPink : Color.LightGreen;
                            }
                        }
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Control[] sd = Controls.Find("Sub_category" + i.ToString(), true);
                        sd[0].Enabled = false;
                        sd[0].Text = null;
                    }
                    Items_grid.ScrollBars = ScrollBars.Vertical;
                    All_items_in_category.Checked = true;
                    ItemsGridChangeIndex(null, null);
                }
            }
        }
        private void AllSubCategororiesButtonClick(object sender, EventArgs e)
        {
            Items_grid.ScrollBars = ScrollBars.None;
            Items_grid.Rows.Clear();
            if (Gshop1 != null && Category_index > -1)
            {
                Sub_category_index = -1;
                int Index = 1;
                var Items = Gshop1.List_items.Where(i => i.Item_category == Category_index);
                if (Items != null)
                {
                    foreach (var x in Items)
                    {
                        Items_grid.Rows.Add(Index, x.Id, GetImageById(x.Id), x.Name, x.Amount, Convert.ToDecimal(x.Sales[0].Price) / 100);
                        if (ChangeStyleColor.Checked == true & Elem != null)
                        {
                            int id = Elem.ElementsLists[21].Items.FindIndex(v => v.Id == x.Id);
                            if (id != -1)
                            {
                                Items_grid.Rows[Items_grid.Rows.Count - 1].Cells[3].Style.ForeColor = (int)Elem.ElementsLists[21].Items[id].Values[10] == 1 ? Color.LightPink : Color.LightGreen;
                            }
                        }
                        Index++;
                    }
                }
                SearchFirstClick = true;
                SeachTextChanged(null, null);
            }
            Items_grid.ScrollBars = ScrollBars.Vertical;
        }
        private void ItemsGridChangeIndex(object sender, EventArgs e)
        {
            GC.Collect();
            #region GetIndexes
            Sale_combobox.SelectedIndex = 0;
            SelectedRowIndexes = new List<Indexes>(Items_grid.SelectedRows.Count);
            if (Items_grid.CurrentRow != null && Gshop1.List_items.Count != 0)
            {
                RowIndex = Items_grid.CurrentRow.Index;
                if (All_category.Checked == true)
                {
                    Position = Gshop1.List_items[RowIndex].Place;
                    foreach (DataGridViewRow dg in Items_grid.SelectedRows)
                    {
                        Indexes Index = new Indexes()
                        {
                            Position = Gshop1.List_items[dg.Index].Place,
                            RowIndex = dg.Index
                        };
                        SelectedRowIndexes.Add(Index);
                    }
                }
                else if (All_items_in_category.Checked == true && All_category.Checked == false)
                {
                    Items = Gshop1.List_items.Where(y => y.Item_category == Category_index);
                    Position = Gshop1.List_items.IndexOf(Items.ElementAt(RowIndex));
                    foreach (DataGridViewRow dg in Items_grid.SelectedRows)
                    {
                        Indexes Index = new Indexes()
                        {
                            Position = Gshop1.List_items.IndexOf(Items.ElementAt(dg.Index)),
                            RowIndex = dg.Index
                        };
                        SelectedRowIndexes.Add(Index);
                    }
                }
                else
                {
                    Items = Gshop1.List_items.Where(y => y.Item_category == Category_index && y.Item_sub_category == Sub_category_index);
                    Position = Gshop1.List_items.IndexOf(Items.ElementAt(RowIndex));
                    Indexes Index = new Indexes();
                    foreach (DataGridViewRow dg in Items_grid.SelectedRows)
                    {
                        Index.Position = Gshop1.List_items.IndexOf(Items.ElementAt(dg.Index));
                        Index.RowIndex = dg.Index;
                        SelectedRowIndexes.Add(Index);
                    }
                }
                if (Position >= Gshop1.List_items.Count)
                {
                    Position = Gshop1.List_items.Count - 1;
                }
                //if(Items_grid.SortOrder!=SortOrder.None)
                //{
                //    int Sorted = Items_grid.SortedColumn.Index;
                //    var type = Items_grid.SortOrder;
                //    if(Sorted==1)
                //    {
                //        if(type==SortOrder.Ascending)
                //        {
                //        }
                //    }
                //}
                #endregion
                this.Id_numeric.Value = Gshop1.List_items[Position].Id;
                Item_name_textbox.Text = Gshop1.List_items[Position].Name;
                Icon_name_textbox.Text = Gshop1.List_items[Position].Icon;
                #region From_elements
                if (Elem != null)
                {
                    int y = Elem.Items.FindIndex(i => i.Id == Gshop1.List_items[Position].Id);
                    if (y != -1)
                    {
                        ItemRealName.Text = Elem.Items[y].Name;
                        Id_image.Image = Elem.Items[y].Standard_image;
                    }
                    else
                    {
                        Id_image.Image = Properties.Resources._32x32_Question;
                        if (Language == 1)
                            ItemRealName.Text = "Не найдено....";
                        else if (Language == 2)
                            ItemRealName.Text = "Not found....";
                    }
                }
                #endregion
                OwnersGrid.Rows.Clear();
                Amount_numeric.Value = Gshop1.List_items[Position].Amount;
                Price_numeric.Value = (Convert.ToDecimal(Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Price) / 100);
                Control_combobox.SelectedIndex = Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Control + 1;
                AfterbuyTimelimit_numeric.Value = Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].During;

                Selling_start_time_pick.Text = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Selling_start_time).ToString();
                Selling_end_time_pick.Text = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Selling_end_time).ToString();

                Explanation_textbox.Text = Gshop1.List_items[Position].Explanation;
                Days_numeric.Value = Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Day;
                Flags_numeric.Value = Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Flags;
                Gift_id_numeric.Value = Gshop1.List_items[Position].Gift_id;
                Gift_amount_numeric.Value = Gshop1.List_items[Position].Gift_amount;
                ILogPrice_numeric.Value = Convert.ToDecimal(Gshop1.List_items[Position].ILogPrice) / 100;
                Gift_time_numeric.Value = Gshop1.List_items[Position].Gift_time;
                if (Elem != null)
                {
                    if (Gshop1.List_items[Position].Gift_id != 0)
                    {
                        int y = Elem.Items.FindIndex(i => i.Id == Gshop1.List_items[Position].Gift_id);
                        if (y != -1)
                        {
                            GiftName.Text = Elem.Items[y].Name;
                            Gift_image.Image = Elem.Items[y].Standard_image;
                        }
                        else
                        {
                            Gift_image.Image = Properties.Resources._32x32_Question;
                            if (Language == 1)
                            {
                                GiftName.Text = "Не найдено....";
                            }
                            else if (Language == 2)
                            {
                                GiftName.Text = "Not found....";
                            }
                        }
                    }
                    else
                    {
                        Gift_image.Image = Properties.Resources._32x32_Question;
                        GiftName.Text = "";
                    }
                }
                Category_name_textbox.Text = Gshop1.List_categories[Gshop1.List_items[Position].Item_category].Category_name;
                if (Gshop1.List_categories[Gshop1.List_items[Position].Item_category].Sub_categories.Count > Gshop1.List_items[Position].Item_sub_category && Gshop1.List_items[Position].Item_sub_category != -1)
                {
                    Subcategory_name_textbox.Text = Gshop1.List_categories[Gshop1.List_items[Position].Item_category].Sub_categories[Gshop1.List_items[Position].Item_sub_category];
                }
                else
                {
                    Subcategory_name_textbox.Text = Language == 1 ? "Подкатегория не существует" : "Subcategory does not exist";
                }

                if (Surfaces_images != null)
                {
                    int im = Surfaces_images.FindIndex(F => F.Name == Gshop1.List_items[Position].Icon.ToLower());
                    if (im != -1)
                    {
                        ShopIcon_picturebox.Image = Surfaces_images[im].Icon;
                    }
                    else
                    {
                        ShopIcon_picturebox.Image = Properties.Resources.BiGQuestionMark;
                    }
                }
                if (Gshop1.Version >= 4)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        string Nam = "Unknown";
                        if (Elem != null)
                        {
                            int f = Elem.NpcsList.Items.FindIndex(z => z.Id == Gshop1.List_items[Position].OwnerNpcs[i]);
                            if (f != -1)
                            {
                                Nam = Elem.NpcsList.Items[f].Name;
                            }
                        }
                        OwnersGrid.Rows.Add(i + 1, Gshop1.List_items[Position].OwnerNpcs[i], Nam);
                    }
                }
                if (Gshop1.Version >= 5)
                {
                    VipLevel_numeric.Value = Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Vip_lvl;
                    Periodlimit_numeric.Value = Gshop1.List_items[Position].Period_limit;
                    Frequency_numeric.Value = Gshop1.List_items[Position].Avail_frequency;
                }
                if (Gshop1.Version >= 6)
                {
                    Class_combobox.SelectedIndex = Gshop1.List_items[Position].Class + 1;
                }
                if ((Gshop1.Version > 0 && Gshop1.Version <= 4 && Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Status > 12) || Gshop1.List_items[Position].Status > 12)
                {
                    if (Language == 1)
                        MessageBox.Show("Значение Status недопустимо для этой версии gshop.data.Проверьте файл на ошибки Status-а в окне фильтра и поиска ошибок.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Status value is wrong for this version gshop.data.Check file on status errors in  filter and bug search window", "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Status_combobox.SelectedIndex = Gshop1.Version > 0 ? Gshop1.List_items[Position].Sales[Sale_combobox.SelectedIndex].Status : Gshop1.List_items[Position].Status;//Тернарное сравнение
                }
            }
        }
        public Image GetImageById(int Item_id)
        {
            if (ElementsIconsLoaded == true)
            {
                int y = Elem.Items.FindIndex(i => i.Id == Item_id);
                if (y != -1)
                {
                    return Elem.Items[y].IconImage;
                }
                else
                {
                    return Properties.Resources.SmallQuestionMark;
                }
            }
            else
                return Properties.Resources.SmallQuestionMark;
        }
        private void SetOptionTime(object sender, EventArgs e)
        {
            switch (SetOptionTimeCombobox.SelectedIndex)
            {
                case 0:
                    AfterbuyTimelimit_numeric.Value = 60;
                    break;
                case 1:
                    AfterbuyTimelimit_numeric.Value = 3600;
                    break;
                case 2:
                    AfterbuyTimelimit_numeric.Value = 86400;
                    break;
                case 3:
                    AfterbuyTimelimit_numeric.Value = 604800;
                    break;
                case 4:
                    AfterbuyTimelimit_numeric.Value = 2592000;
                    break;
                case 5:
                    AfterbuyTimelimit_numeric.Value = 31536000;
                    break;
            }
            foreach (var item in SelectedRowIndexes)
            {
                Gshop1.List_items[item.Position].Sales[Sale_combobox.SelectedIndex].During = (int)AfterbuyTimelimit_numeric.Value;
            }
        }
        private void RenameCategory(object sender, EventArgs e)
        {
            Control[] Radion_control = Controls.Find("Category" + Category_index.ToString(), true);
            Category_textbox.Text = Radion_control[0].Text;
            Category_textbox.Location = new Point(Radion_control[0].Location.X + 1, Radion_control[0].Location.Y);
            Category_textbox.Visible = true;
            Category_textbox.Focus();
        }
        private void CategoryMouseDown(object sender, MouseEventArgs e)
        {
            if (Gshop1 != null)
            {
                if (Gshop1.List_items != null)
                {
                    Items_grid.Rows.Clear();
                    Items_grid.ScrollBars = ScrollBars.None;
                    var Radio = sender as RadioButton;
                    Radio.Checked = true;
                    Category_index = int.Parse(Radio.Name.Substring(8, 1));
                    SetSubCategoriesNames(Category_index);
                    All_items_in_category.Checked = true;
                    AllSubCategororiesButtonClick(sender, e);
                    Items_grid.ScrollBars = ScrollBars.Vertical;
                    ItemsGridChangeIndex(null, null);
                }
            }

        }
        private void CategoryRenameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CategoryRenameTextBoxLeave(null, null);
            }
        }
        private void CategoryRenameTextBoxLeave(object sender, EventArgs e)
        {
            Gshop1.List_categories[Category_index].Category_name = Category_textbox.Text;
            Control[] d = Controls.Find("Category" + Category_index.ToString(), true);
            d[0].Text = Category_textbox.Text;
            Category_textbox.Visible = false;
            Category_textbox.Text = null;
            ItemsGridChangeIndex(null, null);
        }
        private void SaveGshopData(object sender, EventArgs e)
        {
            #region GetVersion
            int Vrs = Gshop1.Version;
            string Contr = sender.ToString();
            if (Contr == "1.2.6 - 1.4.2")
                Vrs = 0;
            else if (Contr == "1.4.2 v27")
                Vrs = 1;
            else if (Contr == "1.4.2 - 1.4.3")
                Vrs = 2;
            else if (Contr == "1.4.4 - 1.5.1")
                Vrs = 3;
            else if (Contr == "1.5.2 - 1.5.3")
                Vrs = 4;
            else if (Contr == "1.5.5 Ru.Official")
                Vrs = 5;
            else if (Contr == "1.5.5 CN.Official")
                Vrs = 6;
            else
                Vrs = Gshop1.Version;
            #endregion
            if (Gshop1 != null)
            {
                try
                {
                    if (LastLoadedFilePath.Split('\\').Last().Split('.')[0].Contains("1"))
                    {
                        Dialog_save_gshop.FileName = "gshop1";
                    }
                    else if (LastLoadedFilePath.Split('\\').Last().Split('.')[0].Contains("2"))
                    {
                        Dialog_save_gshop.FileName = "gshop2";
                    }
                    else
                    {
                        Dialog_save_gshop.FileName = "gshop";
                    }
                }
                catch
                {
                    Dialog_save_gshop.FileName = "gshop";
                }
                if (Gshop1.List_categories != null && Gshop1.List_items != null)
                {
                    if (Dialog_save_gshop.ShowDialog() == DialogResult.OK)
                    {
                        if (Backup.Checked == true)
                        {
                            string n = LastLoadedFilePath.Split('\\').Last().Split('.')[0];
                            string DirectoryCreatePath = Dialog_save_gshop.FileName.Substring(0, Dialog_save_gshop.FileName.LastIndexOf('\\')) + "\\" + n + "_backup";
                            if (!Directory.Exists(DirectoryCreatePath))
                            {
                                Directory.CreateDirectory(DirectoryCreatePath);
                            }
                            DateTime dt = DateTime.Now;
                            string dts = dt.Day + "." + dt.Month + "." + dt.Year + " " + dt.Hour + "-" + dt.Minute + "-" + dt.Second;
                            string directoryPath = Path.GetDirectoryName(Dialog_save_gshop.FileName);
                            string f = string.Format(DirectoryCreatePath + "\\{0}[{1}].data", n, dts);
                            if (File.Exists(Dialog_save_gshop.FileName))
                            {
                                File.Copy(Dialog_save_gshop.FileName, f, true);
                            }
                        }
                        BinaryWriter bw = new BinaryWriter(File.Create(Dialog_save_gshop.FileName));
                        Gshop1.WriteFile(bw, Vrs);
                        bw.Close();
                        if (Language == 1)
                            MessageBox.Show("Файл успешно сохранён", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (Language == 2)
                        {
                            MessageBox.Show("File has been saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (Language == 1)
                        MessageBox.Show("Загруженный файл имел неверный формат!!!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Loaded file has got wrong format!!!", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (Language == 1)
                    MessageBox.Show("Нечего сохранять!!!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Nothing to save!!!", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SetSubCategoriesNames(int CategoryIndex)
        {
            if (CategoryIndex != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    RadioButton[] sd = Controls.Find("Sub_category" + i.ToString(), true).Cast<RadioButton>().ToArray();
                    sd[0].Image = null;
                    sd[0].Text = null;
                    sd[0].Enabled = true;
                }
                for (int i = 0; i < Gshop1.List_categories[CategoryIndex].Amount; i++)
                {
                    if (i != 8)
                    {
                        RadioButton[] sd = Controls.Find("Sub_category" + i.ToString(), true).Cast<RadioButton>().ToArray();
                        sd[0].Text = Gshop1.List_categories[Category_index].Sub_categories[i] + "(" + Gshop1.List_items.Where(z => z.Item_category == Category_index && z.Item_sub_category == i).Count().ToString() + ")";
                        sd[0].Image = Properties.Resources.Under_category;
                    }
                }
            }
        }
        private void CloneItems(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                if (Gshop1.List_items != null && Gshop1.List_categories != null)
                {
                    if (SelectedRowIndexes != null && Items_grid.CurrentRow != null && Gshop1.List_items.Count > 0)
                    {
                        SelectedRowIndexes = SelectedRowIndexes.OrderBy(i => i.RowIndex).ToList();
                        foreach (var d in SelectedRowIndexes)
                        {
                            Item item = Gshop1.List_items[d.Position].Clone();
                            Gshop1.Max_place++;
                            Gshop1.Amount++;
                            item.Place = Gshop1.Max_place;
                            Gshop1.List_items.Add(item);
                            Items_grid.Rows.Add(Items_grid.Rows.Count, item.Id, Items_grid.Rows[d.RowIndex].Cells[2].Value, item.Name, item.Amount, Convert.ToDecimal(item.Sales[0].Price) / 100);
                            Items_grid.Rows[d.RowIndex].Selected = false;
                            Items_grid.Rows[Items_grid.Rows.Count - 1].Selected = true;
                        }
                        Items_grid.FirstDisplayedScrollingRowIndex = Items_grid.Rows.Count - 1;
                        ItemsGridChangeIndex(null, null);
                        if (All_items_in_category.Checked == false && All_category.Checked == false)
                        {
                            RefreshSelectedSubCategoryName(Gshop1.List_items[SelectedRowIndexes[SelectedRowIndexes.Count - 1].Position].Item_category, Gshop1.List_items[SelectedRowIndexes[SelectedRowIndexes.Count - 1].Position].Item_sub_category);
                        }
                        else if (All_items_in_category.Checked == true && All_category.Checked == false)
                        {
                            SetSubCategoriesNames(Gshop1.List_items[SelectedRowIndexes[SelectedRowIndexes.Count - 1].Position].Item_category);
                        }
                    }
                    else
                    {
                        Item it = new Item();
                        Gshop1.Max_place++;
                        Gshop1.Amount++;
                        if (Language == 1)
                            it.Name = "Новое";
                        else if (Language == 2)
                            it.Name = "New";
                        it.Icon = "";
                        it.Explanation = "";
                        it.Place = Gshop1.Max_place;
                        it.Id = 2000;
                        it.Amount = 1;
                        it.Sales = new List<Sale>(4);
                        Sale sl = new Sale();
                        it.Sales.Add(sl);
                        it.Sales.Add(sl);
                        it.Sales.Add(sl);
                        it.Sales.Add(sl);
                        it.Sales[0].Price = 1;
                        it.Sales[0].Control = -1;
                        it.Sales[1].Control = 0;
                        it.Sales[2].Control = 0;
                        it.Sales[3].Control = 0;
                        it.OwnerNpcs = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                        it.Item_category = 0;
                        it.Item_sub_category = 0;
                        #region ФильтрКатегорий
                        if (Category_index != -1)
                        {
                            it.Item_category = Category_index;
                        }
                        if (Sub_category_index == -1 && Category_index != -1)
                        {
                            if (Gshop1.List_categories[Category_index].Amount == 0)
                            {
                                Gshop1.List_categories[Category_index].Amount += 1;
                                Gshop1.List_categories[Category_index].Sub_categories.Add("Новое");
                                if (All_items_in_category.Checked == true && All_category.Checked == false)
                                {
                                    Sub_category0.Image = GShopEditorByLuka.Properties.Resources.Under_category;
                                    Sub_category0.Text = "Новое";
                                }
                            }
                        }
                        else
                        {
                            if (Sub_category_index != -1)
                            {
                                it.Item_sub_category = Sub_category_index;
                            }
                            if (Category_index != -1)
                            {
                                if (Gshop1.List_categories[Category_index].Amount < Sub_category_index + 1)
                                {
                                    for (int i = Gshop1.List_categories[Category_index].Amount; i < Sub_category_index + 1; i++)
                                    {
                                        Gshop1.List_categories[Category_index].Amount += 1;
                                        Gshop1.List_categories[Category_index].Sub_categories.Add("Новое");
                                        SetSubCategoriesNames(Category_index);
                                    }
                                }
                            }
                        }
                        if (it.Item_category == 0 && it.Item_sub_category == 0)
                        {
                            if (Gshop1.List_categories[0].Amount == 0)
                            {
                                if (Language == 1)
                                    Gshop1.List_categories[0].Sub_categories.Add("Новое");
                                else if (Language == 2)
                                    Gshop1.List_categories[0].Sub_categories.Add("New");
                                Gshop1.List_categories[0].Amount++;
                            }
                        }
                        #endregion
                        Gshop1.List_items.Add(it);
                        Items_grid.Rows.Add(1, it.Id, GShopEditorByLuka.Properties.Resources.SmallQuestionMark, it.Name, it.Amount, Convert.ToDecimal(it.Sales[0].Price) / 100);
                        ItemsGridChangeIndex(null, null);
                        if (All_category.Checked == false && All_items_in_category.Checked == true)
                            SetSubCategoriesNames(it.Item_category);
                        else if (All_items_in_category.Checked == false && All_category.Checked == false)
                            RefreshSelectedSubCategoryName(it.Item_category, it.Item_sub_category);
                    }
                }

            }
        }
        void RefreshSelectedSubCategoryName(int ca, int su)
        {
            var co = Controls.Find("Sub_category" + su.ToString(), true);
            co[0].Text = Gshop1.List_categories[ca].Sub_categories[su] + "(" + Gshop1.List_items.Where(c => c.Item_category == ca && c.Item_sub_category == su).Count().ToString() + ")";
        }
        private void DeleteItems(object sender, EventArgs e)
        {
            if (Items_grid.CurrentRow != null)
            {
                SelectedRowIndexes = SelectedRowIndexes.OrderByDescending(i => i.RowIndex).ToList();
                foreach (var k in SelectedRowIndexes)
                {
                    int d = Gshop1.List_items.FindIndex(i => i.Place == k.Position);
                    Gshop1.List_items.RemoveAt(d);
                    Items_grid.Rows.RemoveAt(k.RowIndex);
                    --Gshop1.Max_place;
                    --Gshop1.Amount;
                }
                for (int i = 0; i < Gshop1.Amount; i++)
                {
                    Gshop1.List_items[i].Place = i;
                }
                for (int SC = 0; SC < Items_grid.Rows.Count; SC++)
                {
                    Items_grid.Rows[SC].Cells[0].Value = SC + 1;
                }
                if (All_category.Checked == false)
                    SetSubCategoriesNames(Category_index);
            }
            ItemsGridChangeIndex(null, null);
        }
        private void IdNumericsDoubleClick(object sender, EventArgs e)
        {
            if (Selected_exs != null)
            {
                Selected_exs.RefreshLanguage(Language);
                Control co = sender as Control;
                if (co.Name == "Id_image" || co.Name == "Id_numeric")
                {
                    Selected_exs.Items_grid.MultiSelect = false;
                    List<LBLIBRARY.PWHelper.Elements.List> d = Elem.ElementsLists.Where(i => i.Items.Where(v => v.Id == Id_numeric.Value).Count() > 0).ToList();
                    if (d.Count != 0)
                    {
                        Selected_exs.Selectindex(Elem.ElementsLists.IndexOf(d[0]), (int)Id_numeric.Value);
                    }
                    Selected_exs.Decision = 0;
                    Selected_exs.ShowDialog();
                }
                else if (co.Name == "Gift_image" || co.Name == "Gift_id_numeric")
                {
                    Selected_exs.Items_grid.MultiSelect = false;
                    var d = Elem.Items.FindIndex(i => i.Id == Gift_id_numeric.Value);
                    var s = Array.FindIndex(InListAmount, i => i > d);
                    Selected_exs.Selectindex(s, (int)Gift_id_numeric.Value);
                    Selected_exs.Decision = 1;
                    Selected_exs.ShowDialog();
                }

            }
            else
            {
                DialogResult dg = DialogResult.Cancel;
                if (Language == 1)
                    dg = MessageBox.Show("Elements.data не загружен!\r                Загрузить?", "Elements.data", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                else if (Language == 2)
                    dg = MessageBox.Show("Elements.data isn't loaded!\r                Do you want to load it?", "Elements.data", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dg == DialogResult.Yes)
                    ShowOptionsWindow(null, null);
                else
                    return;
            }

        }
        private void UpItems(object sender, EventArgs e)
        {
            if (Gshop1 != null && Items_grid.CurrentRow != null)
            {
                var dgs = Items_grid.SelectedRows.Cast<DataGridViewRow>().OrderBy(i => i.Index).ToList();
                var colle = Items_grid.SelectedRows.Cast<DataGridViewRow>().OrderBy(i => i.Index).ToList();
                if (All_category.Checked == true)
                {
                    bool k = true;
                    if (dgs[0].Index == 0) k = false;
                    if (k == true)
                    {
                        foreach (DataGridViewRow Row in dgs)
                        {
                            int Current = Row.Index;
                            int Previous = Row.Index - 1;
                            Gshop1.List_items[Current].Place = Previous;
                            Gshop1.List_items[Previous].Place = Current;
                            Items_grid.Rows.Remove(Row);
                            Items_grid.Rows.Insert(Previous, Row);
                            Gshop1.List_items = Gshop1.List_items.OrderBy(i => i.Place).ToList();

                        }
                        if (Items_grid.FirstDisplayedScrollingRowIndex == dgs.ElementAt(dgs.Count() - 1).Index + 1)
                        {
                            Items_grid.FirstDisplayedScrollingRowIndex = Items_grid.Rows[dgs.ElementAt(dgs.Count() - 1).Index].Index;
                        }
                    }
                }
                else if (All_items_in_category.Checked == true && All_category.Checked == false)
                {
                    List<Item> SortedItems = Gshop1.List_items.Where(i => i.Item_category == Category_index).ToList();
                    if (dgs[0].Index != 0)
                    {
                        foreach (DataGridViewRow Row in dgs)
                        {
                            int Current = Row.Index;
                            int Previous = Row.Index - 1;
                            int Current1 = SortedItems[Current].Place;
                            int Previous1 = SortedItems[Previous].Place;
                            Gshop1.List_items[Current1].Place = Previous1;
                            Gshop1.List_items[Previous1].Place = Current1;
                            SortedItems[Current].Place = Previous1;
                            SortedItems[Previous].Place = Current1;
                            Items_grid.Rows.Remove(Row);
                            Items_grid.Rows.Insert(Previous, Row);
                            SortedItems = SortedItems.OrderBy(i => i.Place).ToList(); ;
                            Gshop1.List_items = Gshop1.List_items.OrderBy(i => i.Place).ToList();
                        }
                    }
                }
                else
                {
                    List<Item> SortedItems = Gshop1.List_items.Where(i => i.Item_category == Category_index && i.Item_sub_category == Sub_category_index).ToList();
                    if (dgs[0].Index != 0)
                    {
                        foreach (DataGridViewRow Row in dgs)
                        {
                            int Current = Row.Index;
                            int Previous = Row.Index - 1;
                            int Current1 = SortedItems[Current].Place;
                            int Previous1 = SortedItems[Previous].Place;
                            Gshop1.List_items[Current1].Place = Previous1;
                            Gshop1.List_items[Previous1].Place = Current1;
                            SortedItems[Current].Place = Previous1;
                            SortedItems[Previous].Place = Current1;
                            Items_grid.Rows.Remove(Row);
                            Items_grid.Rows.Insert(Previous, Row);
                            SortedItems = SortedItems.OrderBy(i => i.Place).ToList(); ;
                            Gshop1.List_items = Gshop1.List_items.OrderBy(i => i.Place).ToList();
                        }

                    }
                }
                for (int i = 0; i < Items_grid.Rows.Count; i++)
                {
                    Items_grid.Rows[i].Cells[0].Value = i + 1;
                }
                Items_grid.CurrentCell = Items_grid.Rows[dgs[dgs.Count() - 1].Index].Cells[1];
                foreach (DataGridViewRow row in colle)
                {

                    Items_grid.Rows[row.Index].Selected = true;
                }
            }
        }
        private void DownItems(object sender, EventArgs e)
        {
            var dgs = Items_grid.SelectedRows.Cast<DataGridViewRow>().OrderByDescending(i => i.Index).ToList();
            var colle = Items_grid.SelectedRows.Cast<DataGridViewRow>().OrderByDescending(i => i.Index).ToList();
            if (dgs[0].Index != Items_grid.Rows.Count - 1)
            {
                if (All_category.Checked == true)
                {
                    foreach (DataGridViewRow Row in dgs)
                    {
                        int Current = Row.Index;
                        int Next = Row.Index + 1;
                        Gshop1.List_items[Current].Place = Next;
                        Gshop1.List_items[Next].Place = Current;
                        Items_grid.Rows.Remove(Row);
                        Items_grid.Rows.Insert(Next, Row);
                        Gshop1.List_items = Gshop1.List_items.OrderBy(i => i.Place).ToList();

                    }
                    if (Items_grid.FirstDisplayedScrollingRowIndex == dgs.ElementAt(dgs.Count() - 1).Index + 1)
                        Items_grid.FirstDisplayedScrollingRowIndex = Items_grid.Rows[dgs.ElementAt(dgs.Count() - 1).Index].Index;
                    Items_grid.CurrentCell = Items_grid.Rows[dgs.ElementAt(dgs.Count() - 1).Index].Cells[1];
                }
                else if (All_items_in_category.Checked == true && All_category.Checked == false)
                {
                    List<Item> SortedItems = Gshop1.List_items.Where(i => i.Item_category == Category_index).ToList();
                    foreach (DataGridViewRow Row in dgs)
                    {
                        int Current = Row.Index;
                        int Next = Row.Index + 1;
                        int Current1 = SortedItems[Current].Place;
                        int Previous1 = SortedItems[Next].Place;
                        Gshop1.List_items[Current1].Place = Previous1;
                        Gshop1.List_items[Previous1].Place = Current1;
                        SortedItems[Current].Place = Previous1;
                        SortedItems[Next].Place = Current1;
                        Items_grid.Rows.Remove(Row);
                        Items_grid.Rows.Insert(Next, Row);
                        SortedItems = SortedItems.OrderBy(i => i.Place).ToList(); ;
                        Gshop1.List_items = Gshop1.List_items.OrderBy(i => i.Place).ToList();
                    }
                }
                else
                {
                    List<Item> SortedItems = Gshop1.List_items.Where(i => i.Item_category == Category_index && i.Item_sub_category == Sub_category_index).ToList();
                    foreach (DataGridViewRow Row in dgs)
                    {
                        int Current = Row.Index;
                        int Next = Row.Index + 1;
                        int Current1 = SortedItems[Current].Place;
                        int Previous1 = SortedItems[Next].Place;
                        Gshop1.List_items[Current1].Place = Previous1;
                        Gshop1.List_items[Previous1].Place = Current1;
                        SortedItems[Current].Place = Previous1;
                        SortedItems[Next].Place = Current1;
                        Items_grid.Rows.Remove(Row);
                        Items_grid.Rows.Insert(Next, Row);
                        SortedItems = SortedItems.OrderBy(i => i.Place).ToList(); ;
                        Gshop1.List_items = Gshop1.List_items.OrderBy(i => i.Place).ToList();

                    }
                }
                Items_grid.CurrentCell = Items_grid.Rows[dgs[dgs.Count() - 1].Index].Cells[1];
                foreach (DataGridViewRow row in colle)
                {

                    Items_grid.Rows[row.Index].Selected = true;
                }
            }
        }
        private void SeachTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Search_textbox.Text))
                {
                    posa = 0;
                    s = Items_grid.Rows.Cast<DataGridViewRow>().Where(i => i.Cells[1].Value.ToString().Contains(Search_textbox.Text) || i.Cells[3].Value.ToString().ToLower().Contains(Search_textbox.Text.ToLower()));
                    if (s.Count() != 0 && !SearchFirstClick)
                    {
                        Items_grid.CurrentCell = Items_grid.Rows[s.ElementAt(0).Index].Cells[1];
                        Items_grid.FirstDisplayedScrollingRowIndex = Items_grid.Rows[s.ElementAt(0).Index].Index;
                        posa++;
                    }
                    SearchFirstClick = false;
                }
            }
            catch { }
        }
        private void ContinueSearch(object sender, EventArgs e)
        {
            try
            {
                if (s != null)
                {
                    if (posa != s.Count())
                    {
                        Items_grid.CurrentCell = Items_grid.Rows[s.ElementAt(posa).Index].Cells[1];
                        Items_grid.FirstDisplayedScrollingRowIndex = Items_grid.Rows[s.ElementAt(posa).Index].Index;
                        posa++;
                    }
                }
            }
            catch { }
        }
        private void RenameSubCategoryClick(object sender, EventArgs e)
        {
            Control[] Radion_control = Controls.Find("Sub_category" + Sub_category_index.ToString(), true);
            Sub_category_name_textbox.Text = Radion_control[0].Text.Split(new char[] { '(' }).ElementAt(0);
            Sub_category_name_textbox.Location = new Point(Radion_control[0].Location.X + 1, Radion_control[0].Location.Y);
            Sub_category_name_textbox.Visible = true;
            Sub_category_name_textbox.Focus();
        }
        private void SubCategories_menu_Opening(object sender, CancelEventArgs e)
        {
            if (Gshop1 != null)
            {
                if (Gshop1.List_categories != null)
                {
                    Control[] Textbox_control = Controls.Find("Sub_category" + Sub_category_index.ToString() + "_textbox", true);
                    Control[] Radion_control = Controls.Find("Sub_category" + Sub_category_index.ToString(), true);
                    if (Sub_category_index + 1 <= Gshop1.List_categories[Category_index].Amount)
                    {
                        Add_sub_button.Visible = false;
                        RenameSubCategory_button.Visible = true;
                        Delete_sub_button.Visible = true;
                        Delete_sub_withitems_button.Visible = true;
                        AfterRenameseparator.Visible = true;
                        AfterAddSeparator.Visible = false;
                    }
                    else
                    {
                        Add_sub_button.Visible = true;
                        RenameSubCategory_button.Visible = false;
                        Delete_sub_button.Visible = false;
                        Delete_sub_withitems_button.Visible = false;
                        AfterRenameseparator.Visible = false;
                        AfterAddSeparator.Visible = false;
                    }
                }
            }
        }
        private void SubCategoryRenameTextBoxLeave(object sender, EventArgs e)
        {
            Control[] d = Controls.Find("Sub_category" + Sub_category_index.ToString(), true);
            Gshop1.List_categories[Category_index].Sub_categories[Sub_category_index] = Sub_category_name_textbox.Text;
            d[0].Text = Sub_category_name_textbox.Text;
            Sub_category_name_textbox.Visible = false;
            Sub_category_name_textbox.Text = null;
            ItemsGridChangeIndex(null, null);
            SetSubCategoriesNames(Category_index);
        }
        private void SubCategoryRenameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubCategoryRenameTextBoxLeave(null, null);
            }
        }
        private void AddSubCategory(object sender, EventArgs e)
        {
            for (int i = Gshop1.List_categories[Category_index].Amount; i < Sub_category_index + 1; i++)
            {
                Gshop1.List_categories[Category_index].Amount += 1;
                Gshop1.List_categories[Category_index].Sub_categories.Add("Новое");
                RadioButton[] Radion_control = Controls.Find("Sub_category" + i.ToString(), true).Cast<RadioButton>().ToArray();
                Radion_control[0].Image = GShopEditorByLuka.Properties.Resources.Under_category;
                Radion_control[0].Text = "Новое";
            }
        }
        private void SaleComboboxChangeIndex(object sender, EventArgs e)
        {
            if (Gshop1 != null && Items_grid.Rows.Count != 0)
            {
                if (SelectedRowIndexes != null)
                {
                    if (SelectedRowIndexes.Count > 0)
                    {
                        SelectedRowIndexes = SelectedRowIndexes.OrderByDescending(i => i.RowIndex).ToList();
                        int f = SelectedRowIndexes[SelectedRowIndexes.Count - 1].Position;
                        Price_numeric.Value = (Convert.ToDecimal(Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Price) / 100);
                        if (Gshop1.Version > 0)
                        {
                            if (Status_combobox.Items.Count >= Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Status + 1)
                            {
                                Status_combobox.SelectedIndex = Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Status;
                            }
                        }
                        Control_combobox.SelectedIndex = Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Control + 1;
                        Days_numeric.Value = Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Day;
                        Flags_numeric.Value = Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Flags;
                        AfterbuyTimelimit_numeric.Value = Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].During;
                        Selling_start_time_pick.Text = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Selling_start_time).ToString();
                        Selling_end_time_pick.Text = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Selling_end_time).ToString();
                        if (Gshop1.Version >= 5)
                        {
                            VipLevel_numeric.Value = Gshop1.List_items[f].Sales[Sale_combobox.SelectedIndex].Vip_lvl;
                        }
                    }
                }
            }
        }
        private void DeleteSubCategoryWithItems(object sender, EventArgs e)
        {
            Gshop1.List_categories[Category_index].Amount--;
            Gshop1.List_categories[Category_index].Sub_categories.RemoveAt(Sub_category_index);
            Gshop1.List_items.RemoveAll(i => i.Item_category == Category_index && i.Item_sub_category == Sub_category_index);
            Gshop1.Amount = Gshop1.List_items.Count;
            for (int i = Sub_category_index + 1; i < Gshop1.List_categories[Category_index].Amount + 1; i++)
            {
                Gshop1.List_items.Where(x => x.Item_category == Category_index && x.Item_sub_category == i).ToList().ForEach(d => d.Item_sub_category--);
            }
            SetSubCategoriesNames(Category_index);
            if (Sub_category_index != 0)
            {
                Control[] co1 = Controls.Find("Sub_category" + (Sub_category_index - 1).ToString(), true);
                SubCategoriesMouseDown(co1[0], null);
            }
            else
                SubCategoriesMouseDown(Sub_category0, null);
            for (int i = 0; i < Gshop1.Amount; i++)
            {
                Gshop1.List_items[i].Place = i;
            }
        }
        public void AddItemsElementsData(List<AddNewItems> a)
        {
            Selected_exs.Hide();
            if (Category_index == -1 && Sub_category_index == -1)
            {
                CategoriesForm.Categories_list = Gshop1.List_categories;
                CategoriesForm.RefreshInformation(-1, -1, 2, Language);
                CategoriesForm.ShowDialog(this);
            }
            else if (Category_index != -1 && Sub_category_index == -1)
            {
                CategoriesForm.Categories_list = Gshop1.List_categories;
                CategoriesForm.RefreshInformation(Category_index, -1, 3, Language);
                CategoriesForm.Categories_listbox.Enabled = false;
                CategoriesForm.ShowDialog(this);
                CategoriesForm.Categories_listbox.Enabled = true;
            }
            if (All_items_in_category.Checked == true && All_category.Checked == false)
            {
                Category_index = New_category;
            }
            foreach (var f in a)
            {
                Gshop1.Max_place++;
                Gshop1.Amount++;
                Item it = new Item()
                {
                    Id = f.Id,
                    Place = Gshop1.Max_place,
                    Name = f.Name,
                    Amount = 1,
                    Icon = "surfaces\\百宝阁\\完美大礼包2大.dds",
                    Explanation = "",
                    Sales = new List<Sale>(4)
                };
                Sale s = new Sale();
                it.Sales.Add(s);
                Sale n = new Sale()
                {
                    Control = 0,
                    Price = 0
                };
                it.Sales.Add(n);
                it.Sales.Add(n);
                it.Sales.Add(n);
                it.Sales[0].Price = 1;
                it.Sales[0].Control = -1;
                it.OwnerNpcs = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                if (All_category.Checked == true)
                {
                    it.Item_category = New_category;
                    it.Item_sub_category = New_sub_category;
                }
                else if (All_items_in_category.Checked == true && All_category.Checked == false)
                {
                    it.Item_category = New_category;
                    it.Item_sub_category = New_sub_category;
                }
                else
                {
                    it.Item_category = Category_index;
                    it.Item_sub_category = Sub_category_index;
                }
                Gshop1.List_items.Add(it);
                Items_grid.Rows.Add(Items_grid.Rows.Count, f.Id, f.Im, f.Name, 1, "0,01");
            }
            if (All_category.Checked == false)
                SetSubCategoriesNames(Category_index);
            Items_grid.CurrentCell = Items_grid.Rows[Items_grid.Rows.Count - 1].Cells[1];
            for (int i = Items_grid.Rows.Count - a.Count; i < Items_grid.Rows.Count; i++)
            {
                Items_grid.Rows[i].Selected = true;
            }
            ItemsGridChangeIndex(null, null);
        }
        private void AddItemsElementsDataClick(object sender, EventArgs e)
        {
            if (Selected_exs != null)
            {
                Selected_exs.RefreshLanguage(Language);
                int d = Elem.ElementsLists.FindIndex(i => i.Items.Where(r => r.Id == Id_numeric.Value).Count() > 0);
                if (d != -1)
                {
                    Selected_exs.Selectindex(d, (int)Id_numeric.Value);
                }
                Selected_exs.Items_grid.MultiSelect = true;
                Selected_exs.Decision = 2;
                Selected_exs.ShowDialog();
            }
            else
            {
                DialogResult dg = System.Windows.Forms.DialogResult.Cancel;
                if (Language == 1)
                    dg = MessageBox.Show("Elements.data не загружен!\r                Загрузить?", "Elements.data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                else if (Language == 2)
                    dg = MessageBox.Show("Elements.data isn't load!\rDo you want to load it?", "Elements.data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dg == DialogResult.Yes)
                    ShowOptionsWindow(null, null);
            }
        }
        private void ExplanationLeave(object sender, EventArgs e)
        {
            Explanation_location = Explanation_textbox.SelectionStart;
            foreach (var d in SelectedRowIndexes)
            {
                Gshop1.List_items[d.Position].Explanation = Explanation_textbox.Text;
            }
        }
        private void ItemCategoryNameDoubleClick(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                if (Gshop1.List_categories != null && Items_grid.CurrentRow != null)
                {
                    CategoriesForm.Categories_list = Gshop1.List_categories;
                    CategoriesForm.read = Gshop1.List_items;
                    CategoriesForm.RefreshInformation(Gshop1.List_items[Position].Item_category, Gshop1.List_items[Position].Item_sub_category, 1, Language);
                    CategoriesForm.ShowDialog(this);
                }
            }
            else
            {
                DialogResult dg = DialogResult.Abort;
                if (Language == 1)
                    dg = MessageBox.Show("Gshop.data не загружен.\r       Загрузить?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                else if (Language == 2)
                    dg = MessageBox.Show("Gshop.data isn't loaded\r       Do you want to load it?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dg == DialogResult.Yes)
                    OpenGshopData_Click(OpenGshop_data1, null);
            }
        }
        public void SetItemCategory(int Category_i, int SubCategori_i, int Act)
        {
            if (Act != 4)
            {
                List<Item> its = Gshop1.List_items.Where(i => i.Item_category == Category_i && i.Item_sub_category == SubCategori_i).ToList();
                foreach (var d in SelectedRowIndexes)
                {
                    Gshop1.List_items[d.Position].Item_category = Category_i;
                    Gshop1.List_items[d.Position].Item_sub_category = SubCategori_i;
                }
            }
            Control[] Cat_control = Controls.Find("Category" + Category_i.ToString(), true);
            Control[] Sub_control = Controls.Find("Sub_category" + SubCategori_i.ToString(), true);
            var u = SelectedRowIndexes;
            CategoryMouseDown(Cat_control[0], null);
            SubCategoriesMouseDown(Sub_control[0], null);
            if (Act != 4)
            {
                List<Item> itsk = Gshop1.List_items.Where(i => i.Item_category == Category_i && i.Item_sub_category == SubCategori_i).ToList();
                Items_grid.ClearSelection();
                Items_grid.CurrentCell = Items_grid.Rows[itsk.FindIndex(b => b.Place == u[u.Count - 1].Position)].Cells[1];
                foreach (var item in u)
                {
                    Items_grid.Rows[itsk.FindIndex(b => b.Place == item.Position)].Selected = true;
                }
            }
            ItemsGridChangeIndex(null, null);
        }
        private void SaveGshopSevData(object sender, EventArgs e)
        {
            int Verssev;
            string sender_name = sender.ToString();
            if (sender_name == "1.2.6 - 1.4.2") Verssev = 0;
            else if (sender_name == "1.4.2 v27") Verssev = 1;
            else if (sender_name == "1.4.2 - 1.4.3") Verssev = 2;
            else if (sender_name == "1.4.4 - 1.5.1") Verssev = 3;
            else if (sender_name == "1.5.2 - 1.5.3") Verssev = 4;
            else if (sender_name == "1.5.5 Ru.Official") Verssev = 5;
            else if (sender_name == "1.5.5 CN.Official") Verssev = 6;
            else Verssev = Gshop1.Version;
            if (Verssev != 0)
            {
                try
                {
                    if (LastLoadedFilePath.Split('\\').Last().Split('.')[0].Contains("1"))
                    {
                        SevDialog.FileName = "gshopsev1";
                    }
                    else if (LastLoadedFilePath.Split('\\').Last().Split('.')[0].Contains("2"))
                    {
                        SevDialog.FileName = "gshopsev2";
                    }
                    else
                    {
                        SevDialog.FileName = "gshopsev";
                    }
                }
                catch
                {
                    SevDialog.FileName = "gshopsev";
                }

                if (SevDialog.ShowDialog() == DialogResult.OK)
                {
                    BinaryWriter bw = new BinaryWriter(File.Create(SevDialog.FileName));
                    Gshop1.WriteSev(bw, Verssev);
                    bw.Close();
                    if (Language == 1)
                        MessageBox.Show("gshopsev.data успешно сохранен.!!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (Language == 2)
                        MessageBox.Show("gshopsev.data has been successfully saved.!!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (Language == 1)
                    MessageBox.Show("Используйте gshop.data для версий 1.3.6-1.4.2.", "Информация о сохранении", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (Language == 2)
                    MessageBox.Show("Use gshop.data for 1.3.6-1.4.2 versions", "Saving information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ControlsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control Contr = sender as Control;
                switch (Contr.Name)
                {
                    #region Id
                    case "Id_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Id = (int)Id_numeric.Value;
                                Items_grid.Rows[d.RowIndex].Cells[1].Value = (int)Id_numeric.Value;
                                int h = Elem.Items.FindIndex(p => p.Id == (int)Id_numeric.Value);
                                bool ok = false;
                                Image im = null;
                                if (h != -1)
                                {
                                    im = Elem.Items[h].IconImage;
                                    ok = true;
                                }
                                if (ChangeNameWithID.Checked == true)
                                {
                                    if (Elem != null)
                                    {
                                        int ind = Elem.Items.FindIndex(i => i.Id == Id_numeric.Value);
                                        if (ind != -1)
                                        {

                                            Items_grid.Rows[d.RowIndex].Cells[3].Value = Elem.Items[ind].Name;
                                            Gshop1.List_items[d.Position].Name = Elem.Items[ind].Name;
                                            Item_name_textbox.Text = Elem.Items[ind].Name;
                                        }
                                    }
                                }
                                if (Elem != null)
                                {
                                    if (ok == true)
                                    {
                                        Items_grid.Rows[d.RowIndex].Cells[2].Value = im;
                                    }
                                }
                                if (ChangeDescWithId.Checked == true && Item_ext_desc != null)
                                {
                                    int ind = Item_ext_desc.FindIndex(v => v.Id == Id_numeric.Value);
                                    if (ind != -1)
                                    {
                                        Gshop1.List_items[d.Position].Explanation = Item_ext_desc[ind].Description;
                                    }
                                }
                            }
                            ItemsGridChangeIndex(null, null);
                            break;
                        }
                    #endregion
                    #region Icon_name
                    case "Icon_name_textbox":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Icon = Icon_name_textbox.Text;
                        }
                        if (Surfaces_images != null)
                        {
                            int im = Surfaces_images.FindIndex(F => F.Name == Icon_name_textbox.Text.ToLower());
                            if (im != -1)
                                ShopIcon_picturebox.Image = Surfaces_images[im].Icon;
                            else
                                ShopIcon_picturebox.Image = GShopEditorByLuka.Properties.Resources.BiGQuestionMark;
                        }
                        break;
                    #endregion
                    #region Amount
                    case "Amount_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Amount = Convert.ToInt32(Amount_numeric.Value);
                                Items_grid.Rows[d.RowIndex].Cells[4].Value = Convert.ToInt32(Amount_numeric.Value);
                            }
                            break;
                        }
                    #endregion
                    #region Price
                    case "Price_numeric":
                        {
                            string val = Convert.ToDouble(Math.Round(Price_numeric.Value, 2)).ToString();
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Price = Convert.ToInt32(Price_numeric.Value * 100);
                                if (Sale_combobox.SelectedIndex == 0)
                                {
                                    Items_grid.Rows[d.RowIndex].Cells[5].Value = val;
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Time_limit
                    case "AfterbuyTimelimit_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].During = Convert.ToInt32(AfterbuyTimelimit_numeric.Value);
                            }
                            break;
                        }
                    #endregion
                    #region Days
                    case "Days_numeric":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Day = Convert.ToInt32(Days_numeric.Value);
                        }
                        break;
                    #endregion
                    #region flags
                    case "Flags_numeric":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Flags = Convert.ToInt32(this.Flags_numeric.Value);
                        }
                        break;
                    #endregion
                    #region ItemName
                    case "Item_name_textbox":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Name = Item_name_textbox.Text;
                            Items_grid.Rows[d.RowIndex].Cells[3].Value = Item_name_textbox.Text;
                        }
                        break;
                    #endregion
                    #region Gift_id
                    case "Gift_id_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Gift_id = (int)this.Gift_id_numeric.Value;
                            }
                            if (Elem != null)
                            {
                                int f = Elem.Items.FindIndex(i => i.Id == (int)Gift_id_numeric.Value);
                                if (f != -1)
                                    Gift_image.Image = Elem.Items[f].Standard_image;
                            }
                            break;
                        }
                    #endregion
                    #region Gift_amount
                    case "Gift_amount_numeric":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Gift_amount = (int)Gift_amount_numeric.Value;
                        }
                        break;
                    #endregion
                    #region ILogPrice
                    case "ILogPrice_numeric":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].ILogPrice = (int)ILogPrice_numeric.Value * 100;
                        }
                        break;
                    #endregion
                    #region Gift_time
                    case "Gift_time_numeric":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Gift_time = (int)Gift_time_numeric.Value;
                        }
                        break;
                    #endregion
                    #region class
                    case "Class_combobox":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Class = Class_combobox.SelectedIndex - 1;
                            }
                            break;
                        }
                    #endregion
                    #region PeriodLimit
                    case "Periodlimit_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Period_limit = (int)Periodlimit_numeric.Value;
                            }
                            break;
                        }
                    #endregion
                    #region Frequency
                    case "Frequency_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Avail_frequency = (int)Frequency_numeric.Value;
                            }
                            break;
                        }
                    #endregion
                    #region Price
                    case "VipLevel_numeric":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Vip_lvl = (int)VipLevel_numeric.Value;
                            }
                            break;
                        }
                    #endregion
                    #region Status
                    case "Status_combobox":
                        {
                            if (Gshop1.Version > 0)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Status = Status_combobox.SelectedIndex;
                                }
                            }
                            else
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Status = Status_combobox.SelectedIndex;
                                }
                            break;
                        }
                    #endregion
                    #region Control
                    case "Control_combobox":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Control = Control_combobox.SelectedIndex - 1;
                            }
                            break;
                        }
                        #endregion
                        #region Selling_end_time
                        //case "Selling_end_time_pick":
                        //    {
                        //        foreach (var d in SelectedRowIndexes)
                        //        {
                        //            Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Selling_end_time = (Int32)(Selling_end_time_pick.Value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        //        }
                        //        break;
                        //    }
                        #endregion
                        #region Selling_start_time
                        // case "Selling_start_time_pick":
                        //     {
                        //         foreach (var d in SelectedRowIndexes)
                        //         {
                        //             Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Selling_start_time = (Int32)(Selling_start_time_pick.Value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        //         }
                        //         break;
                        //     }
                        #endregion
                }
            }
        }
        private void ControlsLeave(object sender, EventArgs e)
        {
            if (SelectedRowIndexes != null)
            {
                Control Contr = sender as Control;
                switch (Contr.Name)
                {
                    #region Id
                    case "Id_numeric":
                        {
                            if (Id_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Id = (int)Id_numeric.Value;
                                    Items_grid.Rows[d.RowIndex].Cells[1].Value = (int)Id_numeric.Value;
                                    if (Elem != null)
                                    {
                                        int Ind = Elem.Items.FindIndex(p => p.Id == (int)Id_numeric.Value);
                                        Image image = Properties.Resources.SmallQuestionMark;
                                        if (Ind != -1)
                                        {
                                            image = Elem.Items[Ind].IconImage;
                                        }
                                        Items_grid.Rows[d.RowIndex].Cells[2].Value = image;
                                        if (ChangeNameWithID.Checked)
                                        {
                                            Gshop1.List_items[d.Position].Name = Elem.Items[Ind].Name;
                                            Items_grid.Rows[d.RowIndex].Cells[3].Value = Elem.Items[Ind].Name;
                                        }
                                    }
                                    if (ChangeDescWithId.Checked == true && Item_ext_desc != null)
                                    {
                                        int ind = Item_ext_desc.FindIndex(v => v.Id == Id_numeric.Value);
                                        if (ind != -1)
                                        {
                                            Gshop1.List_items[d.Position].Explanation = Item_ext_desc[ind].Description;
                                        }
                                    }
                                }
                                ItemsGridChangeIndex(null, null);
                            }
                            break;
                        }
                    #endregion
                    #region Icon_name
                    case "Icon_name_textbox":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Icon = Icon_name_textbox.Text;
                        }
                        if (Surfaces_images != null)
                        {
                            int im = Surfaces_images.FindIndex(F => F.Name == Icon_name_textbox.Text.ToLower());
                            if (im != -1)
                                ShopIcon_picturebox.Image = Surfaces_images[im].Icon;
                            else
                                ShopIcon_picturebox.Image = GShopEditorByLuka.Properties.Resources.BiGQuestionMark;
                        }
                        break;
                    #endregion
                    #region Amount
                    case "Amount_numeric":
                        {
                            if (Amount_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Amount = Convert.ToInt32(Amount_numeric.Value);
                                    Items_grid.Rows[d.RowIndex].Cells[4].Value = Convert.ToInt32(Amount_numeric.Value);
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Price
                    case "Price_numeric":
                        {
                            if (Convert.ToDecimal(Price_numeric.Value) != Value)
                            {
                                string val = Convert.ToDouble(Math.Round(Price_numeric.Value, 2)).ToString();
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Price = Convert.ToInt32(Price_numeric.Value * 100);
                                    if (Sale_combobox.SelectedIndex == 0)
                                    {
                                        Items_grid.Rows[d.RowIndex].Cells[5].Value = val;
                                    }
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Time_limit
                    case "AfterbuyTimelimit_numeric":
                        {
                            if (AfterbuyTimelimit_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].During = Convert.ToInt32(AfterbuyTimelimit_numeric.Value);
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Days
                    case "Days_numeric":
                        if (Convert.ToDecimal(Days_numeric.Value) != Value)
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Day = Convert.ToInt32(Days_numeric.Value);
                            }
                        }
                        break;
                    #endregion
                    #region flags
                    case "Flags_numeric":
                        if (Convert.ToDecimal(Flags_numeric.Value) != Value)
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Flags = Convert.ToInt32(this.Flags_numeric.Value);
                            }
                        }
                        break;
                    #endregion
                    #region ItemName
                    case "Item_name_textbox":
                        foreach (var d in SelectedRowIndexes)
                        {
                            Gshop1.List_items[d.Position].Name = Item_name_textbox.Text;
                            Items_grid.Rows[d.RowIndex].Cells[3].Value = Item_name_textbox.Text;
                        }
                        break;
                    #endregion
                    #region Gift_id
                    case "Gift_id_numeric":
                        {
                            if (Gift_id_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Gift_id = (int)this.Gift_id_numeric.Value;
                                }
                                if (Elem != null)
                                {
                                    int f = Elem.Items.FindIndex(i => i.Id == (int)Gift_id_numeric.Value);
                                    if (f != -1)
                                        Gift_image.Image = Elem.Items[f].Standard_image;
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Gift_amount
                    case "Gift_amount_numeric":
                        if (Gift_amount_numeric.Value != Value)
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Gift_amount = (int)Gift_amount_numeric.Value;
                            }
                        }
                        break;
                    #endregion
                    #region ILogPrice
                    case "ILogPrice_numeric":
                        if (ILogPrice_numeric.Value != Value)
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].ILogPrice = (int)ILogPrice_numeric.Value * 100;
                            }
                        }
                        break;
                    #endregion
                    #region Gift_time
                    case "Gift_time_numeric":
                        if (Gift_time_numeric.Value != Value)
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Gift_time = (int)Gift_time_numeric.Value;
                            }
                        }
                        break;
                    #endregion
                    #region class
                    case "Class_combobox":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Class = Class_combobox.SelectedIndex - 1;
                            }
                            break;
                        }
                    #endregion
                    #region PeriodLimit
                    case "Periodlimit_numeric":
                        {
                            if (Periodlimit_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Period_limit = (int)Periodlimit_numeric.Value;
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Frequency
                    case "Frequency_numeric":
                        {
                            if (Frequency_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Avail_frequency = (int)Frequency_numeric.Value;
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Price
                    case "VipLevel_numeric":
                        {
                            if (VipLevel_numeric.Value != Value)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Vip_lvl = (int)VipLevel_numeric.Value;
                                }
                            }
                            break;
                        }
                    #endregion
                    #region Status
                    case "Status_combobox":
                        {
                            if (Gshop1.Version > 0)
                            {
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Status = Status_combobox.SelectedIndex;
                                }
                            }
                            else
                                foreach (var d in SelectedRowIndexes)
                                {
                                    Gshop1.List_items[d.Position].Status = Status_combobox.SelectedIndex;
                                }
                            break;
                        }
                    #endregion
                    #region Control
                    case "Control_combobox":
                        {
                            foreach (var d in SelectedRowIndexes)
                            {
                                Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Control = Control_combobox.SelectedIndex - 1;
                            }
                            break;
                        }
                        #endregion
                        #region Selling_end_time
                        //case "Selling_end_time_pick":
                        //    {
                        //        foreach (var d in SelectedRowIndexes)
                        //        {
                        //            Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Selling_end_time = (Int32)(Selling_end_time_pick.Value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        //        }
                        //        break;
                        //    }
                        #endregion
                        #region Selling_start_time
                        //case "Selling_start_time_pick":
                        //    {
                        //        foreach (var d in SelectedRowIndexes)
                        //        {
                        //            Gshop1.List_items[d.Position].Sales[Sale_combobox.SelectedIndex].Selling_start_time = (Int32)(Selling_start_time_pick.Value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        //        }
                        //        break;
                        //    }
                        #endregion
                }
            }
        }
        private void ControlsEnter(object sender, EventArgs e)
        {
            Value = Convert.ToDecimal((sender as Control).Text);
        }
        private void SetTimeStamp(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                TimestampForm st = new TimestampForm(TimeStampButton.Text, this);
                SetTime(st.WaitForValue());
            }
        }
        public void SetTime(int[] dt)
        {
            try
            {
                var d = new DateTime(dt[0], dt[1], dt[2], dt[3], dt[4], dt[5]);
                Gshop1.TimeStamp = d;
                TimeStampButton.Text = d.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void ShopIconsPictureBoxDoubleClick(object sender, EventArgs e)
        {
            if (IsLinking == true)
            {
                if (Language == 1)
                    MessageBox.Show("Идет обработка изображения.Подождите немного", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (Language == 2)
                    MessageBox.Show("Image is processing.Wait for a while", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (LinkedImage == null)
            {
                DialogResult dg = System.Windows.Forms.DialogResult.Abort;
                if (Language == 1)
                    dg = MessageBox.Show("Surfaces.pck не загружен!\r                Загрузить?", "Surfaces.pck", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                else if (Language == 2)
                    dg = MessageBox.Show("Surfaces.pck isn't load!\r                Do you want to load it?", "Surfaces.pck", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dg == DialogResult.Yes)
                    ShowOptionsWindow(null, null);
                else
                    return;
            }
            else
            {
                int ImageIndex = Surfaces_images.FindIndex(i => i.Name.ToLower() == Icon_name_textbox.Text.ToLower());
                if (ImageIndex != -1)
                    IconsForm.FindIcon(ImageIndex);
                IconsForm.ShowDialog(this);
            }
        }
        public void CreateIconsForm()
        {
            IconsForm = new IconChooser(this, LinkedImage, Surfaces_images, false);
            IsLinking = false;
        }
        public void SetShopIconImage(string Icon_name, Image Icon)
        {
            ShopIcon_picturebox.Image = Icon;
            Icon_name_textbox.Text = Icon_name;
            ControlsLeave(Icon_name_textbox, null);
        }
        private void ChangeLanguage(object sender, EventArgs e)
        {
            Control co = sender as Control;
            #region English
            if (co.Name == "English")
            {
                Language = 2;
                ColorCombobox.Items.Clear();
                ColorCombobox.Items.AddRange(new string[] { "Clear", "Dark" });
                ChangeDescWithId.Text = "Set description from Item_ext_desc.txt when changed ID";
                Category_edit_window.Text = "Edit categories";
                ChangeNameWithID.Text = "Change item name when ID changed";
                ExportButton.ToolTipText = "Export selected items";
                Import_button.ToolTipText = "Import items";
                Export_toolstrip.Text = "Export items";
                GotoItem.Text = "Move to selected item";
                CreateNewFile.Text = "Create new gshop.data file";
                SetExplanation.Text = "Description from Item_ext_desc.txt";
                label1.Text = "     Icon:";
                File_TSMI.Text = "File";
                OpenFileToolStrip.Text = "Open Gshop.data";
                OpenGshopData_button.Text = "Open Gshop.data as";
                Save_button.Text = "Save Gshop.data";
                Save_as_button.Text = "Save Gshop.data as";
                Gshop_sev_button.Text = "Save Gshopsev.data";
                Gshopsev_save_as_button.Text = "Save Gshopsev.data as";
                LastLoadedFiles.Text = "Recent Files";
                Exit_button.Text = "Exit";
                OptionsTSMI.Text = "Options";
                ExtraFiles.Text = "Extra Files";
                Clone_button.Text = "Clone";
                Delete_items_button.Text = "Delete";
                Clone_button1.Text = "Clone";
                AddFromelementsdata_button1.Text = "Add from Elements.data";
                Delete_items_button1.Text = "Delete";
                Replace_items_button.Text = "Move Up-Down";
                Rows_up_button.Text = "Move up";
                Rows_down_button.Text = "Move down";
                CategoryChange_button.Text = "Change category";
                Category_rename.Text = "Rename category";
                Delete_all_items_in_category.Text = "Delete all items in category";
                Add_sub_button.Text = "Add sub_category";
                RenameSubCategory_button.Text = "Rename sub_category";
                Delete_sub_button.Text = "Delete only sub_category";
                Delete_sub_withitems_button.Text = "Delete sub_category with items";
                Item_groupbox.Text = "Item";
                label3.Text = "          Amount:";
                label18.Text = "  Option:";
                label6.Text = "  Category:";
                label9.Text = " Sub_category:";
                Sale_groupbox.Text = "Sale options";
                label4.Text = "Price:";
                label5.Text = " Status:";
                label7.Text = "     Control:";
                label12.Text = "  Day:";
                label13.Text = "   Flags:";
                Gift_groupbox.Text = "Gift options";
                label15.Text = "        Amount:";
                label16.Text = "  Price:";
                label17.Text = "    Time:";
                groupBox4.Text = "New 1.5.2+";
                Search_textbox.Text = "Search:Enter name or ID";
                Set_RealName_button.Text = "Name from Elements.data";
                All_category.Text = "[All]";
                All_items_in_category.Text = "[All]";
                Help_toolstrip.Text = "Help";
                ExtraTSMI.Text = "Extra";
                Show_filter_window.Text = "Search errors";
                OpenGshop_data1.ToolTipText = "Open Gshop.data";
                OpenFileAs.ToolTipText = "Open Gshop.data as version:";
                Save_file_1.ToolTipText = "Save file";
                SaveFileAs.ToolTipText = "Save Gshop.data as version:";
                toolStripButton2.ToolTipText = "Save Gshopsev.data";
                toolStripDropDownButton1.ToolTipText = "Save Gshopsev.data as version:";
                Backup.Text = "Create backup files";
                groupBox3.Text = "Selling start & end time";
                groupBox4.Text = "Item sellers";
                groupBox2.Text = "Time limit after bought";
                label10.Text = "     Start:";
                label11.Text = "    End:";
                label20.Text = "                       Time limit:";
                label22.Text = "  Class:";
                #region ClassCombobox
                int cci = Class_combobox.SelectedIndex;
                Class_combobox.Items.Clear();
                Class_combobox.Items.AddRange(new string[] {"All",
"Blademaster",
"Wizard",
"Psychic",
"Venomancer",
"Barbarian",
"Assassin",
"Archer",
"Cleric",
"Seeker",
"Mystic",
"Duskblade",
"Stormbringer"});
                Class_combobox.SelectedIndex = cci;
                #endregion
                #region comboboxes
                int Si = Sale_combobox.SelectedIndex;
                Sale_combobox.Items.Clear();
                Sale_combobox.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3", "Option 4" });
                Sale_combobox.SelectedIndex = Si;
                int Sti = Status_combobox.SelectedIndex;
                Status_combobox.Items.Clear();
                Status_combobox.Items.AddRange(new string[] {"Default",
"New",
"Popular",
"Often selling",
"Sale 10%",
"Sale 20%",
"Sale 30%",
"Sale 40%",
"Sale 50%",
"Sale 60%",
"Sale 70%",
"Sale 80%",
"Sale 90%"});
                if (Gshop1 != null)
                {
                    if (Gshop1.Version >= 5)
                        Status_combobox.Items.AddRange(new string[] { "Vip0", "Vip1", "Vip2", "Vip3", "Vip4", "Vip5", "Vip6" });
                }
                Status_combobox.SelectedIndex = Sti;
                int Ci = Control_combobox.SelectedIndex;
                Control_combobox.Items.Clear();
                Control_combobox.Items.AddRange(new string[] { "Client", "Server" });
                Control_combobox.SelectedIndex = Ci;
                ItemsGridChangeIndex(null, null);
                #endregion
                SetAllPricesToOneSilver.Text = "Set all items price to one silver";
                ChangeNameOnOriginal.Text = "Set all items name from Elements.data";
                ChangeNameOnOriginalWithAmount.Text = "Set all items name from elements.data with amount (n)";
                ExtraGshop.Text = "Second gshop.data";
                ExtraGshopOpen.Text = "Open Gshop.data";
                ExtraGshopOpenAs.Text = "Open Gshop.data as";
                ExtraGshopSave.Text = "Save Gshop.data";
                ExtraGshopSaveAs.Text = "Save Gshop.data as";
                ExtraGshopsevSave.Text = "Save Gshopsev.data";
                ExtraGshopsevSaveAs.Text = "Save Gshopsev.data as";
                InteractionWithSecondGshop.Text = "Interaction with second gshop.data";
                SetAlltemsDescription.Text = "Set all items description from Item_ext_desc";
                SetAllItemsMaximalAmount.Text = "Set all items maximal amount";
                SetItemRealNameWithAmount.Text = "Name from Elements.data with amount(n)";
                SetItemMaximalAmount.Text = "Maximal amount from Elements.data";
                label17.Text = "Time:";
                int ind = SetOptionTimeCombobox.SelectedIndex;
                SetOptionTimeCombobox.Items.Clear();
                SetOptionTimeCombobox.Items.AddRange(new object[]
                {
                 "Minute",
                 "Hour",
                 "Day",
                 "Week",
                 "Month",
                 "Year"
                });
                SetOptionTimeCombobox.SelectedIndex = ind;
                label23.Text = "Set time:";
                UpdateEditor.Text = "Click to update editor";
                label8.Text = "Time:";
                label8.Location = new Point(73, label8.Location.Y);
                label23.Location = new Point(57, label23.Location.Y);
                ChangeStyleColor.Text = "Change styles color in list";
            }
            #endregion
            #region Russian
            else if (co.Name == "Russian")
            {
                Language = 1;
                ColorCombobox.Items.Clear();
                ColorCombobox.Items.AddRange(new string[] { "Светлый", "Темный" });
                Import_button.ToolTipText = "Импортировать товары";
                ChangeNameWithID.Text = "Изменять название при смене ID";
                ChangeDescWithId.Text = "Изменять описание из Item_ext_desc.txt при смене ID";
                Export_toolstrip.Text = "Экспортировать товары";
                ExportButton.ToolTipText = "Экспортировать выбранные товары";
                Category_edit_window.Text = "Редактирование категорий";
                LastLoadedFiles.Text = "Недавние файлы";
                GotoItem.Text = "Перейти к выбранному товару";
                CreateNewFile.Text = "Создать новый файл gshop.data";
                SetExplanation.Text = "Описание из Item_ext_desc.txt";
                label1.Text = "Иконка:";
                File_TSMI.Text = "Файл";
                OpenFileToolStrip.Text = "Открыть Gshop.data";
                OpenGshopData_button.Text = "Открыть Gshop.data как";
                Save_button.Text = "Сохранить Gshop.data";
                Save_as_button.Text = "Сохранить Gshop.data как";
                Gshop_sev_button.Text = "Сохранить Gshopsev.data";
                Gshopsev_save_as_button.Text = "Сохранить Gshopsev.data как";
                Exit_button.Text = "Выход";
                OptionsTSMI.Text = "Настройки";
                ExtraFiles.Text = "Дополнительные файлы";
                Clone_button.Text = "Клонировать";
                Delete_items_button.Text = "Удалить";
                Clone_button1.Text = "Клонировать";
                AddFromelementsdata_button1.Text = "Добавить с Elements.data";
                Delete_items_button1.Text = "Удалить";
                Replace_items_button.Text = "Переместить";
                Rows_up_button.Text = "Выше";
                Rows_down_button.Text = "Ниже";
                CategoryChange_button.Text = "Изменить категорию";
                Category_rename.Text = "Переименовать категорию";
                Delete_all_items_in_category.Text = "Удалить все предметы в категории";
                Add_sub_button.Text = "Добавить подкатегорию";
                RenameSubCategory_button.Text = "Переименовать подкатегорию";
                Delete_sub_button.Text = "Удалить только подкатегорию";
                Delete_sub_withitems_button.Text = "Удалить подкатегорию вместе с предметами";
                Item_groupbox.Text = "Товар";
                label3.Text = " Количество:";
                label18.Text = " Опция:";
                label6.Text = "Категория:";
                label9.Text = "Подкатегория:";
                Sale_groupbox.Text = "Настройка скидки";
                label4.Text = "Цена:";
                label5.Text = "Статус:";
                label7.Text = "Контроль:";
                label12.Text = "День:";
                label13.Text = "Флаги:";
                label10.Text = "Начало продажи:";
                label11.Text = "Конец продажи:";
                Gift_groupbox.Text = "Подарок";
                label15.Text = "Количество:";
                label16.Text = "Цена:";
                label17.Text = "Время:";
                groupBox4.Text = "Новое 1.5.2+";
                All_category.Text = "[Все]";
                All_items_in_category.Text = "[Все]";
                #region Comboboxes
                int Si = Sale_combobox.SelectedIndex;
                Sale_combobox.Items.Clear();
                Sale_combobox.Items.AddRange(new string[] { "Опция 1", "Опция 2", "Опция 3", "Опция 4" });
                Sale_combobox.SelectedIndex = Si;
                int Sti = Status_combobox.SelectedIndex;
                Status_combobox.Items.Clear();
                Status_combobox.Items.AddRange(new string[] {"Обычный",
"Новое",
"Популярное",
"Продаваемое",
"Скидка 10%",
"Скидка 20%",
"Скидка 30%",
"Скидка 40%",
"Скидка 50%",
"Скидка 60%",
"Скидка 70%",
"Скидка 80%",
"Скидка 90%"});
                if (Gshop1 != null)
                {
                    if (Gshop1.Version >= 5)
                    {
                        Status_combobox.Items.AddRange(new string[] { "Vip0", "Vip1", "Vip2", "Vip3", "Vip4", "Vip5", "Vip6" });
                    }
                }
                Status_combobox.SelectedIndex = Sti;
                int Ci = Control_combobox.SelectedIndex;
                Control_combobox.Items.Clear();
                Control_combobox.Items.AddRange(new string[] { "Клиент", "Сервер" });
                Control_combobox.SelectedIndex = Ci;
                ItemsGridChangeIndex(null, null);
                #endregion
                Search_textbox.Text = "Поиск:Введите название или ID";
                Set_RealName_button.Text = "Название из Elements.data";
                label20.Text = "Ограничение времени:";
                label22.Text = "Класс:";
                #region ClassCombobox
                int cci = Class_combobox.SelectedIndex;
                Class_combobox.Items.Clear();
                Class_combobox.Items.AddRange(new string[] {"Все",
"Воин",
"Маг",
"Шаман",
"Друид",
"Оборотень",
"Ассасин",
"Лучник",
"Жрец",
"Страж",
"Мистик",
"Призрак",
"Жнец"});
                Class_combobox.SelectedIndex = cci;
                #endregion
                Help_toolstrip.Text = "Помощь";
                ExtraTSMI.Text = "Дополнительно";
                OpenGshop_data1.ToolTipText = "Открыть Gshop.data";
                OpenFileAs.ToolTipText = "Открыть Gshop.data как версию:";
                Save_file_1.ToolTipText = "Сохранить файл";
                SaveFileAs.ToolTipText = "Сохранить Gshop.data как версию:";
                toolStripButton2.ToolTipText = "Сохранить Gshopsev.data";
                toolStripDropDownButton1.ToolTipText = "Сохранить Gshopsev.data как версию:";
                Show_filter_window.Text = "Поиск ошибок";
                Backup.Text = "Создавать резервные копии файла";
                groupBox3.Text = "Начало & конец продажи";
                groupBox4.Text = "Продавцы товара";
                groupBox2.Text = "Срок действия после покупки";
                label10.Text = "Начало:";
                label11.Text = "Конец:";
                SetAllPricesToOneSilver.Text = "Задать всем товарам цену в одну единицу";
                ChangeNameOnOriginal.Text = "Задать всем товарам название из Elements.data";
                ChangeNameOnOriginalWithAmount.Text = "Задать всем товарам название из Elements.data с кол-вом (n)";
                ExtraGshop.Text = "Дополнительный gshop.data";
                ExtraGshopOpen.Text = "Открыть Gshop.data";
                ExtraGshopOpenAs.Text = "Открыть Gshop.data как";
                ExtraGshopSave.Text = "Сохранить Gshop.data";
                ExtraGshopSaveAs.Text = "Сохранить Gshop.data как";
                ExtraGshopsevSave.Text = "Сохранить Gshopsev.data";
                ExtraGshopsevSaveAs.Text = "Сохранить Gshopsev.data как";
                InteractionWithSecondGshop.Text = "Взаимодействие с дополнительным gshop.data";
                SetAlltemsDescription.Text = "Установить всем товарам описание из Item_ext_desc";
                SetAllItemsMaximalAmount.Text = "Установить всем товарам максимальное количество";
                SetItemRealNameWithAmount.Text = "Название из Elements.data с кол-вом(n)";
                SetItemMaximalAmount.Text = "Максимальное количество из Elements.data";
                label17.Text = "Время:";
                int ind = SetOptionTimeCombobox.SelectedIndex;
                SetOptionTimeCombobox.Items.Clear();
                SetOptionTimeCombobox.Items.AddRange(new object[]
                {
                 "Минута",
                 "Час",
                 "День",
                 "Неделя",
                 "Месяц",
                 "Год"
                });
                SetOptionTimeCombobox.SelectedIndex = ind;
                label23.Text = "Установить время:"; label23.Location = new Point(2, label23.Location.Y);
                UpdateEditor.Text = "Нажмите для обновления редактора";
                label8.Text = "Время:";
                label8.Location = new Point(64, label8.Location.Y);
                label23.Location = new Point(2, label23.Location.Y);
                ChangeStyleColor.Text = "Изменять цвет стилей в списке";
            }
            #endregion
        }
        public void RenameSubCategorisFromSelectCategoryForm(int category, int action)
        {
            if (action == 1)
            {
                Control[] cos = Controls.Find("Category" + category.ToString(), true);
                var f = cos[0] as RadioButton;
                f.Image = GShopEditorByLuka.Properties.Resources.Categories;
                cos[0].Text = Gshop1.List_categories[category].Category_name;
            }
            else if (action == 2 || action == 3)
            {
                if (Category_index == category)
                {
                    SetSubCategoriesNames(Category_index);
                }
            }
        }
        private void SetNameFromElementsData(object sender, EventArgs e)
        {
            if (Elem != null)
            {
                foreach (var item in SelectedRowIndexes)
                {
                    int ind = Elem.Items.FindIndex(i => i.Id == Convert.ToInt32(Items_grid.Rows[item.RowIndex].Cells[1].Value));
                    if (ind != -1)
                    {
                        Gshop1.List_items[item.Position].Name = Elem.Items[ind].Name;
                        Items_grid.Rows[item.RowIndex].Cells[3].Value = Elem.Items[ind].Name;
                    }
                }
                ItemsGridChangeIndex(null, null);
            }
        }
        private void ProgrammeInformation(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Perfect world: Gshop.data editor \rVersion: {0}   16.04.2018\rKn1fe-zone.ru\rSkype:Luka007789\r                                               © Luka", EditorVersion), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start("https://kn1fe-zone.ru/index.php?members/luka.1491/");
        }
        private void ShowErrorsWindow(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                Error_fm = new Error_search(this, Gshop1, Elem, OpenErrorSearchTip);
                Error_fm.RefreshLanguage(Language, Gshop1.Version);
                Error_fm.Show(this);
                OpenErrorSearchTip = false;
            }
            else
            {
                if (Language == 1)
                    MessageBox.Show("Gshop.data не загружен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (Language == 2)
                    MessageBox.Show("Gshop.data isn't loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public string GetStatusString(int Index)
        {
            if (Status_combobox.Items.Count >= Index + 1)
            {
                return Status_combobox.Items[Index].ToString();
            }
            else
            {
                return "Unknown";
            }
        }
        public void SelectErrorItems(List<int> Rows, bool Del)
        {
            if (All_category.Checked == true)
            {
                Items_grid.CurrentCell = Items_grid.Rows[Rows[Rows.Count() - 1]].Cells[1];
                for (int i = 0; i < Rows.Count; i++)
                {
                    Items_grid.Rows[Rows[i]].Selected = true;
                }
                ItemsGridChangeIndex(null, null);
                if (Del == true)
                {
                    List<int> ls = Items_grid.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList().OrderByDescending(t => t).ToList();
                    foreach (var n in ls)
                    {
                        Items_grid.Rows.RemoveAt(n);
                        Gshop1.List_items.RemoveAt(n);
                        Gshop1.Amount--;
                    }
                }
            }
            else
            {
                if (Language == 1)
                {
                    MessageBox.Show("Прежде чем совершить это действие,вам нужно выбрать категорию [Все]");
                }
                else
                {
                    MessageBox.Show("Before you confirm this action,you need to select category [All]");
                }
            }
        }
        private void DeleteSubCategory(object sender, EventArgs e)
        {
            if (Gshop1.List_categories[Category_index].Amount != 1)
            {
                Gshop1.List_categories[Category_index].Amount--;
                Gshop1.List_categories[Category_index].Sub_categories.RemoveAt(Sub_category_index);
                Gshop1.List_items.Where(x => x.Item_category == Category_index && x.Item_sub_category == Sub_category_index).ToList().ForEach(d => d.Item_sub_category = 0);
                for (int i = Sub_category_index + 1; i < Gshop1.List_categories[Category_index].Amount + 1; i++)
                {
                    Gshop1.List_items.Where(x => x.Item_category == Category_index && x.Item_sub_category == i).ToList().ForEach(d => d.Item_sub_category--);
                }
                SetSubCategoriesNames(Category_index);
                if (Sub_category_index != 0)
                {
                    Control[] co1 = Controls.Find("Sub_category" + (Sub_category_index - 1).ToString(), true);
                    SubCategoriesMouseDown(co1[0], null);
                }
                else
                    SubCategoriesMouseDown(Sub_category0, null);
            }
            else
            {
                DialogResult dg = DialogResult.Abort;
                if (Language == 1)
                    dg = MessageBox.Show("Это единственная подкатегория в этой категории,при ее удалении все предметы находящиеся в ней будут удалены!Хотите выполнить это действие?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                else if (Language == 2)
                    dg = MessageBox.Show("This is single subcategory in this category,after deleting all items located in it will be deleted!Do you want execute this action?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dg == DialogResult.Yes)
                {
                    DeleteSubCategoryWithItems(null, null);
                }
            }
        }
        private void SetItemDescription(object sender, EventArgs e)
        {
            if (Item_ext_desc != null)
            {
                foreach (var item in SelectedRowIndexes)
                {
                    int i = Item_ext_desc.FindIndex(d => d.Id == Gshop1.List_items[item.Position].Id);
                    if (i != -1)
                        Gshop1.List_items[item.Position].Explanation = Item_ext_desc[i].Description;
                }
                ItemsGridChangeIndex(null, null);
            }
            else
            {
                DialogResult dg = System.Windows.Forms.DialogResult.Abort;
                if (Language == 1)
                    dg = MessageBox.Show("Configs.pck не загружен!\r                Загрузить?", "Configs.pck", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                else if (Language == 2)
                    dg = MessageBox.Show("Configs.pck isn't loaded!\r                Do you want to load it?", "Configs.pck", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dg == DialogResult.Yes)
                    ShowOptionsWindow(null, null);
                else
                    return;
            }
        }
        private void HistoryGshopOpen(object sender, EventArgs e)
        {
            Items_grid.Rows.Clear();
            string f = sender.ToString();
            if (f == "1.2.6 - 1.4.2")
                Gshop1Version = 0;
            else if (f == "1.4.2 v27")
                Gshop1Version = 1;
            else if (f == "1.4.2 - 1.4.3")
                Gshop1Version = 2;
            else if (f == "1.4.4 - 1.5.1")
                Gshop1Version = 3;
            else if (f == "1.5.2 - 1.5.3")
                Gshop1Version = 4;
            else if (f == "1.5.5 Ru.Official")
                Gshop1Version = 5;
            else if (f == "1.5.5 CN.Official")
                Gshop1Version = 6;
            Gshop1 = new FileGshop()
            {
                Max_place = -1,
                Amount = 0,
                Version = Gshop1Version,
                List_items = new List<Item>(),
                List_categories = new List<Categories>(8)
            };
            for (int i = 0; i < 8; i++)
            {
                Categories ct = new Categories();
                if (Language == 1)
                    ct.Category_name = "Новое";
                else if (Language == 2)
                    ct.Category_name = "New";
                ct.Sub_categories = new List<string>();
                ct.Amount = 0;
                Gshop1.List_categories.Add(ct);
            }
            SetControlsInability();
            AllCategoriesButtonClick(null, null);
            All_category.Checked = true;
            RefreshCategoriesNames();
            CategoriesForm = new SelectCategoryForm(this, Gshop1.List_categories);
            string v = "";
            if (Gshop1.Version == 0) v = "Version 1.2.6";
            else if (Gshop1.Version == 1) v = "Version 1.4.2 v27";
            else if (Gshop1.Version == 2) v = "Version 1.4.2";
            else if (Gshop1.Version == 3) v = "Version 1.4.4";
            else if (Gshop1.Version == 4) v = "Version 1.5.2";
            else if (Gshop1.Version == 5) v = "Version 1.5.5 Russian Official";
            else if (Gshop1.Version == 6) v = "Version 1.5.5 China Official";
            this.Text = "Creating gshop.data" + "   -   " + v + "    -    " + "Gshop Editor By Luka v" + EditorVersion;
        }
        private void GoToItemLocation(object sender, EventArgs e)
        {
            if (Gshop1 != null && Items_grid.CurrentRow != null)
            {
                int C = Gshop1.List_items[Position].Item_category;
                int S = Gshop1.List_items[Position].Item_sub_category;
                int f = Position;
                if (C != -1 && S != -1)
                {
                    var CB = Controls.Find("Category" + C.ToString(), true);
                    var SB = Controls.Find("Sub_category" + S.ToString(), true);
                    if (C != Category_index)
                    {
                        CategoryMouseDown(CB[0], null);
                    }
                    if (S != Sub_category_index)
                        SubCategoriesMouseDown(SB[0], null);
                    var k = Gshop1.List_items.Where(i => i.Item_category == C && i.Item_sub_category == S).ToList();
                    var it = k.FindIndex(b => b.Place == Gshop1.List_items[f].Place);
                    if (it != -1)
                        Items_grid.CurrentCell = Items_grid.Rows[it].Cells[1];
                }
            }
        }
        private void FormClose(object sender, FormClosedEventArgs e)
        {
            XmlTextWriter vt = new XmlTextWriter(string.Format(Application.StartupPath + "\\Gshop Editor Settings.xml"), Encoding.UTF8)
            {
                Formatting = Formatting.Indented,
                IndentChar = '\t',
                Indentation = 1,
                QuoteChar = '\''

            };
            #region Default
            vt.WriteStartDocument();
            vt.WriteStartElement("root");
            vt.WriteStartAttribute("ProductName");
            vt.WriteString("Gshop Editor by Luka");
            vt.WriteEndAttribute();
            vt.WriteStartElement("Settings");
            vt.WriteElementString("Version", EditorVersion);
            #endregion
            #region gshop
            if (LastLoadedFilePath != "Gshop File" && LastLoadedFilePath != null)
                vt.WriteElementString("LastPath", LastLoadedFilePath);
            else vt.WriteElementString("LastPath", "Not Loaded");
            if (SecondLastLoadedFilePath != "Gshop File" && SecondLastLoadedFilePath != null)
            {
                vt.WriteElementString("SecondGshopLastPath", SecondLastLoadedFilePath);
            }
            else
            {
                vt.WriteElementString("SecondGshopLastPath", "Not Loaded");
            }
            #endregion
            #region Elements
            if (OptionForm.ElementsPath != null)
                vt.WriteElementString("ElementsDataPath", OptionForm.ElementsPath);
            else
                vt.WriteElementString("ElementsDataPath", "Not Loaded");
            #endregion
            #region Surfaces
            if (OptionForm.SurfacesPath != null)
                vt.WriteElementString("SurfacesPckPath", OptionForm.SurfacesPath);
            else
                vt.WriteElementString("SurfacesPckPath", "Not Loaded");
            #endregion
            #region configs
            if (OptionForm.ConfigsPath != null)
                vt.WriteElementString("ConfigsPckPath", OptionForm.ConfigsPath);
            else
                vt.WriteElementString("ConfigsPckPath", "Not Loaded");
            #endregion
            string lng = "";
            if (Language == 1) lng = "Russian";
            else if (Language == 2) lng = "English";
            vt.WriteElementString("Language", lng);
            vt.WriteElementString("CreateBackUpFile", Convert.ToBoolean(Backup.CheckState).ToString());
            string Col = "";
            if (ColorCombobox.SelectedIndex == 0) Col = "Clear";
            else if (ColorCombobox.SelectedIndex == 1) Col = "Dark";
            else Col = "Clear";
            vt.WriteElementString("Color", Col);
            vt.WriteElementString("ChangeNameWhenIDChanged", Convert.ToBoolean(ChangeNameWithID.CheckState).ToString());
            vt.WriteElementString("ChangeDescriptionWhenIDChanged", Convert.ToBoolean(ChangeDescWithId.CheckState).ToString());
            vt.WriteElementString("ChangeStyleRowColor", Convert.ToBoolean(ChangeStyleColor.CheckState).ToString());
            vt.WriteElementString("IsFirstRunAfterUpdate", IsFirstRunAfterUpdate.ToString());
            vt.WriteEndElement();
            vt.WriteStartElement("LastLoadedFiles");
            vt.WriteElementString("Count", (LastLoadedFiles.DropDownItems.Count).ToString());
            for (int i = 0; i < LastLoadedFiles.DropDownItems.Count && i < 20; i++)
            {
                vt.WriteElementString("path_" + (i + 1), LastLoadedFiles.DropDownItems[i].Text);
            }
            vt.WriteEndElement();
            vt.Close();
            Environment.Exit(0);
        }
        public class AdvancedCursorsFromEmbededResources
        {
            [DllImport("user32.dll")]
            static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);
            public static Cursor Create(byte[] resource)
            {
                IntPtr myNew_Animated_hCursor;
                myNew_Animated_hCursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
                if (myNew_Animated_hCursor.ToInt32() == 0)
                    return Cursors.Default;
                return new Cursor(myNew_Animated_hCursor);
            }
        }
        async Task<bool> GetVersion()
        {
            DropboxClient dbx = new DropboxClient("LmM_SdDQsqAAAAAAAAAADAvnE4fiMSNLYWtJ0mAXHBv0xqMTzi0S4-gfFG4ZVCHu");
            var full = await dbx.Users.GetCurrentAccountAsync();
            var list = await dbx.Files.ListFolderAsync(string.Empty);
            if (EditorVersion != GetString(dbx).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool HasConnection()
        {
            try
            {
                System.Net.IPHostEntry i = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
        async static Task<string> GetString(DropboxClient dbx)
        {
            var response = await dbx.Files.DownloadAsync("/Version.txt");
            string srt = await response.GetContentAsStringAsync();
            List<string> strs = srt.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            return strs[0];
        }
        private void FormLoad(object sender, EventArgs e)
        {
            try
            {
                var Curs = AdvancedCursorsFromEmbededResources.Create(Properties.Resources.Game);
                Items_grid.Cursor = Curs;
            }
            catch { }
            OptionForm = new Option(this);
            try
            {
                if (File.Exists(Application.StartupPath + "\\Gshop Editor Settings.xml"))
                {
                    using (System.Xml.XmlTextReader rg = new XmlTextReader(string.Format(Application.StartupPath + "\\Gshop Editor Settings.xml")))
                    {
                        rg.ReadToFollowing("LastPath");
                        string FilePath = rg.ReadElementContentAsString();
                        if (FilePath != "Gshop File" && File.Exists(FilePath))
                        {
                            if (!OpenedWhenStart)
                            {
                                OpenGshopData(FilePath);
                            }
                        }
                        rg.ReadToFollowing("SecondGshopLastPath");
                        string Fp = rg.ReadElementContentAsString();
                        if (Fp != "Gshop File" && File.Exists(Fp))
                        {
                            OpenSecondGshop(Fp);
                        }
                        rg.ReadToFollowing("ElementsDataPath");
                        OptionForm.ElementsPath = rg.ReadElementContentAsString();
                        rg.ReadToFollowing("SurfacesPckPath");
                        OptionForm.SurfacesPath = rg.ReadElementContentAsString();
                        rg.ReadToFollowing("ConfigsPckPath");
                        OptionForm.ConfigsPath = rg.ReadElementContentAsString();
                        OptionForm.Setpath(OptionForm.ElementsPath, OptionForm.SurfacesPath, OptionForm.ConfigsPath);
                        rg.ReadToFollowing("Language");
                        string Lang = rg.ReadElementContentAsString();
                        if (Lang == "Russian")
                        {
                            Russian.Checked = true;
                        }
                        else if (Lang == "English")
                        {
                            English.Checked = true;
                        }
                        rg.ReadToFollowing("CreateBackUpFile");
                        Backup.Checked = Convert.ToBoolean(rg.ReadElementContentAsString());
                        rg.ReadToFollowing("Color");
                        string Color = rg.ReadElementContentAsString();
                        if (Color == "Dark")
                        {
                            ColorCombobox.SelectedIndex = 1;
                        }
                        else
                        {
                            ColorCombobox.SelectedIndex = 0;
                        }
                        ColorComboboxIndexChange(null, null);
                        rg.ReadToFollowing("ChangeNameWhenIDChanged");
                        ChangeNameWithID.Checked = Convert.ToBoolean(rg.ReadElementContentAsString());
                        rg.ReadToFollowing("ChangeDescriptionWhenIDChanged");
                        ChangeDescWithId.Checked = Convert.ToBoolean(rg.ReadElementContentAsString());
                        rg.ReadToFollowing("ChangeStyleRowColor");
                        ChangeStyleColor.Checked = Convert.ToBoolean(rg.ReadElementContentAsString());
                        rg.ReadToFollowing("IsFirstRunAfterUpdate");
                        IsFirstRunAfterUpdate = Convert.ToBoolean(rg.ReadElementContentAsString());
                        rg.ReadToFollowing("Count");
                        int Count = Convert.ToInt32(rg.ReadElementContentAsString());
                        for (int i = 0; i < Count && i < 20; i++)
                        {
                            rg.ReadToFollowing($"path_{(i + 1)}");
                            LastLoadedFiles.DropDownItems.Add(rg.ReadElementContentAsString());
                        }
                        SortLastLoadedFiles();
                    }
                }

                Thread th = new Thread(delegate ()
                {
                    try
                    {
                        if (HasConnection() == true)
                        {
                            if (GetVersion().Result == true)
                            {
                                this.BeginInvoke(new MethodInvoker(delegate
                                    {
                                        UpdateEditor.Visible = true;
                                        FlashingButton.Start();
                                    }));
                            }
                        }
                    }
                    catch { }
                    if (IsFirstRunAfterUpdate == true)
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            UpdatesViewer upd = new UpdatesViewer(Language);
                            upd.ShowDialog(this);
                            IsFirstRunAfterUpdate = false;
                        }));
                    }

                });
                th.Start();


            }
            catch (Exception)
            {
                if (ColorCombobox.SelectedIndex == -1)
                    ColorCombobox.SelectedIndex = 0;
            }
        }
        private void SortLastLoadedFiles()
        {
            List<string> Its = new List<string>();
            for (int i = 0; i < LastLoadedFiles.DropDownItems.Count; i++)
            {
                Its.Add(LastLoadedFiles.DropDownItems[i].Text);
            }
            Its = Its.GroupBy(t => t).Select(y => y.FirstOrDefault()).ToList();
            LastLoadedFiles.DropDownItems.Clear();
            for (int i = 0; i < Its.Count; i++)
            {
                LastLoadedFiles.DropDownItems.Add(Its[i]);
                LastLoadedFiles.DropDownItems[i].Click += new EventHandler(OpenFromHistory);
            }
        }
        private void OpenFromHistory(object sender, EventArgs e)
        {
            Gshop1Version = 0;
            OpenGshopData(sender.ToString());
        }
        private void ShowCategoriesWindow(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                if (Gshop1.List_categories != null)
                {
                    CategoriesForm.Categories_list = Gshop1.List_categories;
                    CategoriesForm.read = Gshop1.List_items;
                    CategoriesForm.RefreshInformation(Gshop1.List_items[Position].Item_category, Gshop1.List_items[Position].Item_sub_category, 4, Language);
                    CategoriesForm.ShowDialog(this);
                }
            }
            else
            {
                DialogResult dg = DialogResult.Abort;
                if (Language == 1)
                {
                    dg = MessageBox.Show("Gshop.data не загружен.\r       Загрузить?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                else if (Language == 2)
                {
                    dg = MessageBox.Show("Gshop.data isn't load\r       Do you want to load it?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                if (dg == DialogResult.Yes)
                {
                    OpenGshopData_Click(OpenGshop_data1, null);
                }
            }
        }
        private void GetAllGroupBoxes(Control container, ref List<Control> ControlList)
        {
            foreach (Control c in container.Controls)
            {
                GetAllGroupBoxes(c, ref ControlList);
                if (c is GroupBox)
                {
                    ControlList.Add(c);
                }
            }
        }
        private void GetAllNumerics(Control container, ref List<Control> ControlList)
        {
            foreach (Control c in container.Controls)
            {
                GetAllNumerics(c, ref ControlList);
                if (c is LBLIBRARY.Components.NumericUpDownEx)
                {
                    ControlList.Add(c);
                }
            }
        }
        private void GetAllLabels(Control container, ref List<Control> ControlList)
        {
            foreach (Control c in container.Controls)
            {
                GetAllLabels(c, ref ControlList);
                if (c is Label)
                {
                    ControlList.Add(c);
                }
            }
        }
        private void GetAllComboboxes(Control container, ref List<Control> ControlList)
        {
            foreach (Control c in container.Controls)
            {
                GetAllComboboxes(c, ref ControlList);
                if (c is LBLIBRARY.Components.ComboBoxA)
                {
                    ControlList.Add(c);
                }
            }
        }
        private void GetAllRadioButtons(Control Container, ref List<Control> ControlList)
        {
            foreach (Control c in Container.Controls)
            {
                GetAllRadioButtons(c, ref ControlList);
                if (c is RadioButton)
                {
                    ControlList.Add(c);
                }
            }
        }
        private void ColorComboboxIndexChange(object sender, EventArgs e)
        {
            List<Control> GroupBoxes = new List<Control>();
            GetAllGroupBoxes(this, ref GroupBoxes);
            List<Control> Numerics = new List<Control>();
            GetAllNumerics(this, ref Numerics);
            List<Control> Labels = new List<Control>();
            GetAllLabels(this, ref Labels);
            List<Control> Comboboxes = new List<Control>();
            GetAllComboboxes(this, ref Labels);
            List<Control> RadioButtons = new List<Control>();
            GetAllRadioButtons(panel1, ref RadioButtons);
            GetAllRadioButtons(panel2, ref RadioButtons);
            if (ColorCombobox.SelectedIndex == 0)
            {
                this.BackColor = Color.FromArgb(238, 245, 250);
                toolStrip1.BackColor = Color.FromArgb(238, 245, 250);
                menuStrip1.SetColorBlack = false;
                foreach (var item in GroupBoxes)
                {
                    item.BackColor = Color.FromArgb(238, 245, 250);
                    item.ForeColor = SystemColors.ControlText;
                }
                foreach (LBLIBRARY.Components.NumericUpDownEx item in Numerics)
                {
                    item.SetBlack = false;
                }
                foreach (var item in Labels)
                {
                    item.ForeColor = SystemColors.ControlText;
                }
                Sale_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.False;
                Control_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.False;
                Status_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.False;
                SetOptionTimeCombobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.False;
                Class_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.False;
                Explanation_textbox.BackColor = Color.White;
                Explanation_textbox.ForeColor = SystemColors.ControlText;
                English.BackColor = SystemColors.Control;
                English.ForeColor = SystemColors.ControlText;
                Russian.BackColor = SystemColors.Control;
                Russian.ForeColor = SystemColors.ControlText;
                TimeStampButton.ForeColor = SystemColors.ControlText;
                ColorCombobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.False;
                Items_grid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                Items_grid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                OwnersGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                OwnersGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                foreach (RadioButton item in RadioButtons)
                {
                    item.BackColor = Color.FromArgb(223, 223, 223);
                    item.FlatAppearance.BorderColor = Color.Silver;
                    item.FlatAppearance.CheckedBackColor = Color.FromArgb(115, 204, 233);
                    item.ForeColor = SystemColors.ControlText;
                }
                Item_name_textbox.BackColor = Color.FromArgb(64, 64, 64);
                Item_name_textbox.ForeColor = Color.FromArgb(192, 255, 192);
                ItemRealName.BackColor = Color.FromArgb(64, 64, 64);
                ItemRealName.ForeColor = Color.FromArgb(192, 255, 192);
                GiftName.BackColor = Color.FromArgb(64, 64, 64);
                GiftName.ForeColor = Color.FromArgb(192, 255, 192);
                Id_numeric.TextBoxBackGroundColor = Color.FromArgb(255, 255, 192);
                Gift_id_numeric.TextBoxBackGroundColor = Color.FromArgb(255, 255, 192);
                Search_textbox.BackColor = Color.FromArgb(192, 255, 192);
                Search_textbox.ForeColor = SystemColors.WindowText;
                Button_cont_search.BackColor = Color.FromArgb(224, 224, 224);
                Search_textbox.BackColor = Color.FromArgb(192, 255, 192);
                SetRealName_button.BackColor = Color.FromArgb(224, 224, 224);
                Clone_button.BackColor = SystemColors.Control;
                Clone_button.FlatAppearance.BorderColor = Color.Gray;
                Clone_button.ForeColor = SystemColors.ControlText;

                Delete_items_button.BackColor = SystemColors.Control;
                Delete_items_button.FlatAppearance.BorderColor = Color.Gray;
                Delete_items_button.ForeColor = SystemColors.ControlText;

                Add_from_elements_data_button.BackColor = SystemColors.Control;
                Add_from_elements_data_button.FlatAppearance.BorderColor = Color.Gray;
                Add_from_elements_data_button.ForeColor = SystemColors.ControlText;

                SetDescription.BackColor = Color.FromArgb(238, 245, 250);
                SetColor_button.BackColor = Color.FromArgb(238, 245, 250);

                Category_name_textbox.BackColor = Color.FromArgb(255, 224, 192);
                Subcategory_name_textbox.BackColor = Color.FromArgb(255, 224, 192);
                Icon_name_textbox.BackColor = Color.FromArgb(255, 224, 192);
                Category_name_textbox.ForeColor = Color.Black;
                Subcategory_name_textbox.ForeColor = Color.Black;
                Icon_name_textbox.ForeColor = Color.Black;
                Selling_start_time_pick.BorderColor = Color.Gray;
                Selling_start_time_pick.StartColor = Color.White;
                Selling_start_time_pick.EndColor = Color.White;
                Selling_start_time_pick.ForeColor = Color.Black;
                Selling_end_time_pick.BorderColor = Color.Gray;
                Selling_end_time_pick.StartColor = Color.White;
                Selling_end_time_pick.EndColor = Color.White;
                Selling_end_time_pick.ForeColor = Color.Black;
                OwnersGrid.BackgroundColor = Color.FromArgb(238, 245, 250);
                OwnersGrid.DefaultCellStyle.BackColor = Color.FromArgb(238, 245, 250);
                OwnersGrid.DefaultCellStyle.ForeColor = SystemColors.WindowText;
            }
            else if (ColorCombobox.SelectedIndex == 1)
            {
                Color q = Color.FromArgb(20, 20, 20);
                Color w = Color.FromArgb(235, 235, 235);
                Color r = Color.FromArgb(45, 45, 45);
                this.BackColor = q;
                menuStrip1.SetColorBlack = true;
                foreach (var item in GroupBoxes)
                {
                    item.BackColor = q;
                    item.ForeColor = w;
                }
                foreach (LBLIBRARY.Components.NumericUpDownEx item in Numerics)
                {
                    item.SetBlack = true;
                }
                foreach (var item in Labels)
                {
                    item.ForeColor = w;
                }
                Sale_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.True;
                Control_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.True;
                Status_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.True;
                SetOptionTimeCombobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.True;
                Class_combobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.True;
                Explanation_textbox.BackColor = Color.FromArgb(35, 35, 35);
                Explanation_textbox.ForeColor = w;
                English.BackColor = q;
                English.ForeColor = w;
                Russian.BackColor = q;
                Russian.ForeColor = w;
                toolStrip1.SetColorBlack = true;
                TimeStampButton.ForeColor = w;
                ColorCombobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.True;
                Items_grid.ColumnHeadersDefaultCellStyle.BackColor = q;
                Items_grid.ColumnHeadersDefaultCellStyle.ForeColor = w;
                OwnersGrid.ColumnHeadersDefaultCellStyle.BackColor = q;
                OwnersGrid.ColumnHeadersDefaultCellStyle.ForeColor = w;
                foreach (RadioButton item in RadioButtons)
                {
                    item.BackColor = Color.FromArgb(28, 28, 28);
                    item.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
                    item.FlatAppearance.CheckedBackColor = Color.FromArgb(100, 100, 100);
                    item.ForeColor = w;
                }
                Item_name_textbox.BackColor = Color.FromArgb(64, 64, 64);
                Item_name_textbox.ForeColor = Color.FromArgb(192, 255, 192);
                ItemRealName.BackColor = Color.FromArgb(64, 64, 64);
                ItemRealName.ForeColor = Color.FromArgb(192, 255, 192);
                GiftName.BackColor = Color.FromArgb(64, 64, 64);
                GiftName.ForeColor = Color.FromArgb(192, 255, 192);
                Id_numeric.TextBoxBackGroundColor = Color.FromArgb(255, 255, 192);
                Id_numeric.TextBoxForeColor = SystemColors.ControlText;
                Gift_id_numeric.TextBoxBackGroundColor = Color.FromArgb(255, 255, 192);
                Gift_id_numeric.TextBoxForeColor = SystemColors.ControlText;
                Search_textbox.BackColor = Color.FromArgb(192, 255, 192);
                Search_textbox.ForeColor = SystemColors.WindowText;
                Button_cont_search.BackColor = Color.FromArgb(30, 30, 30);
                Search_textbox.BackColor = Color.FromArgb(152, 215, 172);
                Clone_button.BackColor = Color.FromArgb(30, 30, 30);
                Clone_button.FlatAppearance.BorderColor = Color.Silver;
                Clone_button.ForeColor = w;
                Delete_items_button.BackColor = Color.FromArgb(30, 30, 30);
                Delete_items_button.FlatAppearance.BorderColor = Color.Silver;
                Delete_items_button.ForeColor = w;
                Add_from_elements_data_button.BackColor = Color.FromArgb(30, 30, 30);
                Add_from_elements_data_button.FlatAppearance.BorderColor = Color.Silver;
                Add_from_elements_data_button.ForeColor = w;
                SetRealName_button.BackColor = r;
                SetDescription.BackColor = r;
                SetColor_button.BackColor = r;
                Category_name_textbox.BackColor = r;
                Subcategory_name_textbox.BackColor = r;
                Icon_name_textbox.BackColor = r;
                Category_name_textbox.ForeColor = w;
                Subcategory_name_textbox.ForeColor = w;
                Icon_name_textbox.ForeColor = w;
                Selling_start_time_pick.BorderColor = Color.FromArgb(150, 150, 150);
                Selling_start_time_pick.StartColor = Color.FromArgb(30, 30, 30);
                Selling_start_time_pick.EndColor = Color.FromArgb(30, 30, 30);
                Selling_start_time_pick.ForeColor = w;
                Selling_end_time_pick.BorderColor = Color.FromArgb(150, 150, 150);
                Selling_end_time_pick.StartColor = Color.FromArgb(30, 30, 30);
                Selling_end_time_pick.EndColor = Color.FromArgb(30, 30, 30);
                Selling_end_time_pick.ForeColor = w;
                OwnersGrid.BackgroundColor = Color.FromArgb(28, 28, 28);
                OwnersGrid.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
                OwnersGrid.DefaultCellStyle.ForeColor = w;
            }
        }
        private void Export(object sender, EventArgs e)
        {
            if (Gshop1 != null && SelectedRowIndexes != null && Items_grid.CurrentRow != null)
            {
                ExportDialog.FileName = "PWShopItems" + string.Format(" ({0})", SelectedRowIndexes.Count);
                if (ExportDialog.ShowDialog(this) == DialogResult.OK)
                {
                    BinaryWriter bw = new BinaryWriter(File.Create(ExportDialog.FileName));
                    SelectedRowIndexes = SelectedRowIndexes.OrderBy(g => g.Position).ToList();
                    bw.Write(Gshop1.GetBytes("Gshop Editor by Luka", 40, Encoding.Unicode));
                    bw.Write(SelectedRowIndexes.Count);
                    foreach (var d in SelectedRowIndexes)
                    {
                        bw.Write(Gshop1.List_items[d.Position].Place);
                        bw.Write(Gshop1.List_items[d.Position].Item_category);
                        bw.Write(Gshop1.List_items[d.Position].Item_sub_category);
                        bw.Write(Gshop1.GetBytes(Gshop1.List_items[d.Position].Icon, 128, Encoding.GetEncoding(936)));
                        bw.Write(Gshop1.List_items[d.Position].Id);
                        bw.Write(Gshop1.List_items[d.Position].Amount);
                        for (int i = 0; i < 4; i++)
                        {
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Price);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Selling_end_time);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].During);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Selling_start_time);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Control);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Day);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Status);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Flags);
                            bw.Write(Gshop1.List_items[d.Position].Sales[i].Vip_lvl);
                        }
                        bw.Write(Gshop1.List_items[d.Position].Status);
                        bw.Write(Gshop1.GetBytes(Gshop1.List_items[d.Position].Explanation, 1024, Encoding.Unicode));
                        bw.Write(Gshop1.GetBytes(Gshop1.List_items[d.Position].Name, 64, Encoding.Unicode));
                        bw.Write(Gshop1.List_items[d.Position].Gift_id);
                        bw.Write(Gshop1.List_items[d.Position].Gift_amount);
                        bw.Write(Gshop1.List_items[d.Position].Gift_time);
                        bw.Write(Gshop1.List_items[d.Position].ILogPrice);
                        for (int f = 0; f < 8; f++)
                        {
                            bw.Write(Gshop1.List_items[d.Position].OwnerNpcs[f]);
                        }
                        bw.Write(Gshop1.List_items[d.Position].Period_limit);
                        bw.Write(Gshop1.List_items[d.Position].Avail_frequency);
                        bw.Write(Gshop1.List_items[d.Position].Class);
                    }
                    bw.Close();
                    if (Language == 1)
                        MessageBox.Show(string.Format("Успешно экспортировано {0} предметов!!", SelectedRowIndexes.Count), "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (Language == 2)
                        MessageBox.Show(string.Format("Successfully exported {0} items!!", SelectedRowIndexes.Count), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (Language == 1)
                    MessageBox.Show("Невозможно выполнить действие.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (Language == 2)
                    MessageBox.Show("Can't execute action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Import(object sender, EventArgs e)
        {
            if (ImportDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    BinaryReader br = new BinaryReader(File.Open(ImportDialog.FileName, FileMode.Open));
                    br.ReadBytes(40);
                    int Amount = br.ReadInt32();
                    for (int i = 0; i < Amount; i++)
                    {
                        Gshop1.Amount++;
                        Gshop1.Max_place++;
                        Item it = new Item();
                        br.ReadInt32();
                        it.Place = Gshop1.Max_place;
                        it.Item_category = br.ReadInt32();
                        it.Item_sub_category = br.ReadInt32();
                        it.Icon = Encoding.GetEncoding(936).GetString(br.ReadBytes(128));
                        it.Id = br.ReadInt32();
                        it.Amount = br.ReadInt32();
                        it.Sales = new List<Sale>(4);
                        for (int j = 0; j < 4; j++)
                        {
                            Sale sl = new Sale()
                            {
                                Price = br.ReadInt32(),
                                Selling_end_time = br.ReadInt32(),
                                During = br.ReadInt32(),
                                Selling_start_time = br.ReadInt32(),
                                Control = br.ReadInt32(),
                                Day = br.ReadInt32(),
                                Status = br.ReadInt32(),
                                Flags = br.ReadInt32(),
                                Vip_lvl = br.ReadInt32()
                            };
                            it.Sales.Add(sl);
                        }
                        it.Status = br.ReadInt32();
                        it.Explanation = Encoding.Unicode.GetString(br.ReadBytes(1024)).TrimEnd('\0');
                        it.Name = Encoding.Unicode.GetString(br.ReadBytes(64)).Split('\0')[0];
                        it.Gift_id = br.ReadInt32();
                        it.Gift_amount = br.ReadInt32();
                        it.Gift_time = br.ReadInt32();
                        it.ILogPrice = br.ReadInt32();
                        it.OwnerNpcs = new int[8];
                        for (int g = 0; g < 8; g++)
                        {
                            it.OwnerNpcs[g] = br.ReadInt32();
                        }
                        it.Period_limit = br.ReadInt32();
                        it.Avail_frequency = br.ReadInt32();
                        it.Class = br.ReadInt32();
                        if (All_category.Checked == false && All_items_in_category.Checked == true)
                        {
                            it.Item_category = Category_index;
                            it.Item_sub_category = 0;
                            if (Gshop1.List_categories[it.Item_category].Amount == 0)
                            {
                                Gshop1.List_categories[it.Item_category].Amount++;
                                Gshop1.List_categories[it.Item_category].Sub_categories.Add("Новое");
                                SetSubCategoriesNames(it.Item_category);
                            }
                        }
                        else if (All_items_in_category.Checked == false && All_category.Checked == false)
                        {
                            it.Item_category = Category_index;
                            it.Item_sub_category = Sub_category_index;
                        }
                        Gshop1.List_items.Add(it);
                        Items_grid.Rows.Add(Items_grid.Rows.Count, it.Id, GetImageById(it.Id), it.Name, it.Amount, Convert.ToDecimal(it.Sales[0].Price) / 100);
                    }
                    for (int v = 0; v < Gshop1.List_items.Count; v++)
                    {
                        Gshop1.List_items[v].Place = v;
                    }
                    DialogResult dg = DialogResult.Abort;
                    if (Language == 1)
                    {
                        dg = MessageBox.Show(string.Format("Успешно импортировано {0} предетов!!\rХотите перейти к импортированным предметам?", Amount), "Успех", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    else if (Language == 2)
                    {
                        dg = MessageBox.Show(string.Format("Successfully imported {0} items!!\rDo you want to move to imported items?", Amount), "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }

                    if (dg == DialogResult.Yes)
                    {
                        for (int k = 1; k < Amount; k++)
                        {
                            Items_grid.CurrentCell = Items_grid.Rows[Items_grid.Rows.Count - Amount].Cells[1];
                            Items_grid.Rows[Items_grid.Rows.Count - k].Selected = true;
                        }
                    }
                    if (Category_index != -1)
                    {
                        SetSubCategoriesNames(Category_index);
                    }

                    ItemsGridChangeIndex(null, null);
                    br.Close();
                }
                catch
                {
                    if (Language == 1)
                    {
                        MessageBox.Show("Попытка загрузить ошибочный файл!!", "Импорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Language == 2)
                    {
                        MessageBox.Show("Trying to load wrong file!!", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DeleteAllInCategory(object sender, EventArgs e)
        {
            Gshop1.Amount -= Gshop1.List_items.Where(v => v.Item_category == Category_index).Count();
            Gshop1.List_items.RemoveAll(v => v.Item_category == Category_index);
            Gshop1.List_categories[Category_index].Amount = 0;
            Gshop1.List_categories[Category_index].Sub_categories.Clear();
            SetSubCategoriesNames(Category_index);
            Items_grid.Rows.Clear();
            for (int i = 0; i < Gshop1.Amount; i++)
            {
                Gshop1.List_items[i].Place = i;
            }
        }
        private void ExplanationMouseMove(object sender, MouseEventArgs e)
        {
            string text = this.Explanation_textbox.Text;
            IntPtr handle = ((Control)sender).Handle;
            this.t.ShowToolTip(handle, text);
        }
        private void ExplanationMouseLeave(object sender, EventArgs e)
        {
            if ((t != null) && t.Visible)
            {
                this.t.Hide();
            }
        }
        private void ExplanationKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }
        private void ExplanationTextChanged(object sender, EventArgs e)
        {
            if (this.Explanation_textbox.Focused)
            {
                string text = this.Explanation_textbox.Text;
                IntPtr handle = ((Control)sender).Handle;
                this.t.ShowToolTip(handle, text);
            }
        }
        private void OwnersGridChangeValue(object sender, DataGridViewCellEventArgs e)
        {
            if (SelectedRowIndexes != null && e.ColumnIndex != 2 && e.ColumnIndex != 0)
            {
                foreach (var d in SelectedRowIndexes)
                {
                    int.TryParse(OwnersGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out Gshop1.List_items[d.Position].OwnerNpcs[e.RowIndex]);
                }
                if (Elem != null)
                {
                    int f = Elem.NpcsList.Items.FindIndex(z => z.Id == Convert.ToInt32(OwnersGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    if (f != -1)
                    {
                        OwnersGrid.Rows[e.RowIndex].Cells[2].Value = Elem.NpcsList.Items[f].Name;
                    }
                }
            }
        }
        private void SearchTextBoxMouseDown(object sender, MouseEventArgs e)
        {
            Search_textbox.SelectAll();
        }
        private void SetAllPriceOne(object sender, EventArgs e)
        {
            foreach (var item in Gshop1.List_items)
            {
                item.Sales[0].Price = 1;
            }
            foreach (DataGridViewRow dg in Items_grid.Rows)
            {
                dg.Cells[5].Value = "0,01";
            }
        }
        private void ChangeAllNameOnOriginal(object sender, EventArgs e)
        {
            if (Elem != null)
            {
                foreach (var item in Gshop1.List_items)
                {
                    int i = Elem.Items.FindIndex(z => z.Id == item.Id);
                    if (i != -1)
                    {
                        item.Name = Elem.Items[i].Name;
                    }
                }
                foreach (DataGridViewRow item in Items_grid.Rows)
                {
                    item.Cells[3].Value = Gshop1.List_items.Find(f => f.Id == Convert.ToInt32(item.Cells[1].Value)).Name;
                }
            }
        }
        private void ChangeAllNameOnOriginalWithAmount(object sender, EventArgs e)
        {
            if (Elem != null)
            {
                foreach (var item in Gshop1.List_items)
                {
                    int i = Elem.Items.FindIndex(z => z.Id == item.Id);
                    string p = "";
                    if (item.Amount > 1)
                    {
                        p = " (" + item.Amount + ")";
                    }
                    if (i != -1)
                    {
                        item.Name = Elem.Items[i].Name + p;
                    }
                }
                List<Indexes> RowsList = new List<Indexes>(Items_grid.SelectedRows.Count);
                IEnumerable<Item> its;
                if (Items_grid.CurrentRow != null && Gshop1.List_items.Count != 0)
                {
                    if (All_category.Checked == true)
                    {
                        foreach (DataGridViewRow dg in Items_grid.Rows)
                        {
                            Indexes Index = new Indexes()
                            {
                                Position = Gshop1.List_items[dg.Index].Place,
                                RowIndex = dg.Index
                            };
                            RowsList.Add(Index);
                        }
                    }
                    else if (All_items_in_category.Checked == true && All_category.Checked == false)
                    {
                        its = Gshop1.List_items.Where(y => y.Item_category == Category_index);
                        foreach (DataGridViewRow dg in Items_grid.Rows)
                        {
                            Indexes Index = new Indexes()
                            {
                                Position = Gshop1.List_items.IndexOf(its.ElementAt(dg.Index)),
                                RowIndex = dg.Index
                            };
                            RowsList.Add(Index);
                        }
                    }
                    else
                    {
                        its = Gshop1.List_items.Where(y => y.Item_category == Category_index && y.Item_sub_category == Sub_category_index);
                        foreach (DataGridViewRow dg in Items_grid.Rows)
                        {
                            Indexes Index = new Indexes()
                            {
                                Position = Gshop1.List_items.IndexOf(its.ElementAt(dg.Index)),
                                RowIndex = dg.Index
                            };
                            RowsList.Add(Index);
                        }
                    }
                    foreach (var d in RowsList)
                    {
                        Items_grid.Rows[d.RowIndex].Cells[3].Value = Gshop1.List_items[d.Position].Name;
                    }
                }
            }
        }
        private void SetAllItemsDescription_Click(object sender, EventArgs e)
        {
            if (Item_ext_desc != null)
            {
                foreach (var item in Gshop1.List_items)
                {
                    int i = Item_ext_desc.FindIndex(z => z.Id == item.Id);
                    if (i != -1)
                    {
                        item.Explanation = Item_ext_desc[i].Description;
                    }
                }
                ItemsGridChangeIndex(null, null);
            }
        }
        private void SetAllItemsMaximalAmount_Click(object sender, EventArgs e)
        {
            if (Elem != null)
            {
                foreach (var item in Gshop1.List_items)
                {
                    int i = Elem.Items.FindIndex(z => z.Id == item.Id);
                    if (i != -1)
                    {
                        item.Amount = Elem.Items[i].MaxAmount;
                    }
                }
                foreach (DataGridViewRow item in Items_grid.Rows)
                {
                    item.Cells[4].Value = Gshop1.List_items.Find(f => f.Id == Convert.ToInt32(item.Cells[1].Value)).Amount;
                }
                ItemsGridChangeIndex(null, null);
            }
        }
        private void FlashingButton_Tick(object sender, EventArgs e)
        {
            if (ColorValue == 1)
            {
                UpdateEditor.BackColor = Color.Yellow;
                ColorValue = 2;
            }
            else
            {
                UpdateEditor.BackColor = Color.Red;
                ColorValue = 1;
            }
        }
        private void UpdateEditor_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\update.exe");
            Environment.Exit(0);
        }
        private void SetItemRealNameWithAmount_Click(object sender, EventArgs e)
        {
            if (Elem != null)
            {
                foreach (var item in SelectedRowIndexes)
                {
                    int ind = Elem.Items.FindIndex(i => i.Id == Convert.ToInt32(Items_grid.Rows[item.RowIndex].Cells[1].Value));
                    if (ind != -1)
                    {
                        string Name = Elem.Items[ind].Name;
                        if (Gshop1.List_items[item.Position].Amount > 1)
                        {
                            Name += $"({ Gshop1.List_items[item.Position].Amount})";
                        }
                        Gshop1.List_items[item.Position].Name = Name;
                        Items_grid.Rows[item.RowIndex].Cells[3].Value = Name;
                    }
                }
                ItemsGridChangeIndex(null, null);
            }
        }
        private void SetItemMaximalAmount_Click(object sender, EventArgs e)
        {
            if (Elem != null)
            {
                foreach (var item in SelectedRowIndexes)
                {
                    int ind = Elem.Items.FindIndex(i => i.Id == Convert.ToInt32(Items_grid.Rows[item.RowIndex].Cells[1].Value));
                    if (ind != -1)
                    {
                        Gshop1.List_items[item.Position].Amount = Elem.Items[ind].MaxAmount;
                        Items_grid.Rows[item.RowIndex].Cells[4].Value = Elem.Items[ind].MaxAmount;
                    }
                }
                ItemsGridChangeIndex(null, null);
            }
        }
        public void OpenSecondGshop(string Path)
        {
            if (File.Exists(Path))
            {
                Gshop2 = new FileGshop();
                Gshop2.ReadFile(Path, Gshop2Version);
                string v = "Неизвестно";
                if (Gshop2.Version == 0) v = "Version 1.2.6";
                else if (Gshop2.Version == 1) v = "Version 1.4.2 v27";
                else if (Gshop2.Version == 2) v = "Version 1.4.2";
                else if (Gshop2.Version == 3) v = "Version 1.4.4";
                else if (Gshop2.Version == 4) v = "Version 1.5.2";
                else if (Gshop2.Version == 5) v = "Version 1.5.5 Russian Official";
                else if (Gshop2.Version == 6) v = "Version 1.5.5 China Official";
                if (v != "Неизвестно")
                {
                    SecondLastLoadedFilePath = Path;
                }
                Interaction = new GshopsInteraction(Gshop1, Gshop2, this, Elem);
            }
        }
        private void SecondGshopOpen_Click(object sender, EventArgs e)
        {
            if (Gshop1 != null)
            {
                string f = sender.ToString();
                if (f == "1.2.6 - 1.4.2")
                    Gshop2Version = 0;
                else if (f == "1.4.2 v27")
                    Gshop2Version = 1;
                else if (f == "1.4.2 - 1.4.3")
                    Gshop2Version = 2;
                else if (f == "1.4.4 - 1.5.1")
                    Gshop2Version = 3;
                else if (f == "1.5.2 - 1.5.3")
                    Gshop2Version = 4;
                else if (f == "1.5.5 Ru.Official")
                    Gshop2Version = 5;
                else if (f == "1.5.5 CN.Official")
                    Gshop2Version = 6;
                else
                    Gshop2Version = 0;
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    FileName = "Gshop File",
                    Filter = "GShop Files|gshop.data|All Data Files|*.data|All Files|*.*"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenSecondGshop(ofd.FileName);
                }
            }
            else
            {
                if (Language == 1)
                {
                    MessageBox.Show("Основной файл Gshop.data не загружен...!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Main Gshop.data file isn't loaded...!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private void InteractionWithSecondGshop_Click(object sender, EventArgs e)
        {
            if (Interaction != null)
            {
                Interaction.RefreshShopInformation();
                Interaction.ShowDialog(this);
            }
        }
        private void ExtraGshopSave_Click(object sender, EventArgs e)
        {
            #region GetVersion
            int Vrs = Gshop2.Version;
            string Contr = sender.ToString();
            if (Contr == "1.2.6 - 1.4.2")
                Vrs = 0;
            if (Contr == "1.4.2 v27")
                Vrs = 1;
            if (Contr == "1.4.2 - 1.4.3")
                Vrs = 2;
            else if (Contr == "1.4.4 - 1.5.1")
                Vrs = 3;
            else if (Contr == "1.5.2 - 1.5.3")
                Vrs = 4;
            else if (Contr == "1.5.5 Ru.Official")
                Vrs = 5;
            else if (Contr == "1.5.5 CN.Official")
                Vrs = 6;
            else
                Vrs = Gshop2.Version;
            #endregion
            if (Gshop2 != null)
            {
                if (Gshop2.List_categories != null && Gshop2.List_items != null)
                {
                    SaveFileDialog sfd = new SaveFileDialog()
                    {
                        Filter = "Anjelica engine shop file|gshop.data|All Files|*.*",
                        FileName = "gshop.data"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (Backup.Checked == true)
                        {
                            string DirectoryCreatePath = sfd.FileName.Substring(0, sfd.FileName.LastIndexOf('\\')) + "\\Extra_gshop_backup";
                            if (!Directory.Exists(DirectoryCreatePath))
                                Directory.CreateDirectory(DirectoryCreatePath);
                            string directoryPath = Path.GetDirectoryName(sfd.FileName);
                            string f = string.Format(DirectoryCreatePath + "\\gshop[{0}].data", DateTime.Now.ToString().Replace(":", "-").Replace("/", "\\"));
                            if (File.Exists(sfd.FileName))
                            {
                                File.Copy(sfd.FileName, f, true);
                            }
                        }
                        BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName));
                        Gshop2.WriteFile(bw, Vrs);
                        bw.Close();
                        if (Language == 1)
                            MessageBox.Show("Файл успешно сохранён", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (Language == 2)
                        {
                            MessageBox.Show("File has been saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (Language == 1)
                        MessageBox.Show("Загруженный файл имел неверный формат!!!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Loaded file has got wrong format!!!", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (Language == 1)
                    MessageBox.Show("Нечего сохранять!!!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Nothing to save!!!", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExtraGshopsevSave_Click(object sender, EventArgs e)
        {
            int Verssev;
            string sender_name = sender.ToString();
            if (sender_name == "1.2.6 - 1.4.2") Verssev = 0;
            else if (sender_name == "1.4.2 v27") Verssev = 1;
            else if (sender_name == "1.4.2 - 1.4.3") Verssev = 2;
            else if (sender_name == "1.4.4 - 1.5.1") Verssev = 3;
            else if (sender_name == "1.5.2 - 1.5.3") Verssev = 4;
            else if (sender_name == "1.5.5 Ru.Official") Verssev = 5;
            else if (sender_name == "1.5.5 CN.Official") Verssev = 6;
            else Verssev = Gshop2.Version;
            if (Verssev != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    FileName = "gshopsev.data",
                    Filter = "GShopsev Files|gshopsev.data|All Files|*.*"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName));
                    Gshop2.WriteSev(bw, Verssev);
                    bw.Close();
                    if (Language == 1)
                        MessageBox.Show("Gshop.sev успешно сохранен.!!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (Language == 2)
                        MessageBox.Show("Gshopsev.data has been successfully saved.!!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (Language == 1)
                    MessageBox.Show("Используйте gshop.data для версий 1.3.6-1.4.2.", "Информация о сохранении", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (Language == 2)
                    MessageBox.Show("Use gshop.data for 1.3.6-1.4.2 versions", "Saving information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Items_grid_Sorted(object sender, EventArgs e)
        {

        }
        private void Items_grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
            //    if (Items_grid.Rows.Count != 0 && e.RowIndex != -1)
            //    {
            //       
            //      //
            //    }
            //}
            //catch
            //{
            //
            //}
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (data != null)
            {
                OpenGshopData(data[0]);
            }
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void Items_grid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;


        }
        private void Items_grid_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (data != null)
            {
                OpenGshopData(data[0]);
            }
        }

        private void SaleStartTime_Click(object sender, EventArgs e)
        {
            if (SelectedRowIndexes == null) return;

            TimestampForm stf = new TimestampForm(Selling_start_time_pick.Text.ToString(), this);
            var l = stf.WaitForValue();
            if (l != null)
            {
                DateTime dt = new DateTime(l[0], l[1], l[2], l[3], l[4], l[5]);
                int total = (int)dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                foreach (var item in SelectedRowIndexes)
                {
                    Gshop1.List_items[item.Position].Sales[Sale_combobox.SelectedIndex].Selling_start_time = total;
                }
                Selling_start_time_pick.Text = dt.ToString();
            }

        }

        private void buttonC2_Click(object sender, EventArgs e)
        {
            if (SelectedRowIndexes == null) return;
            TimestampForm stf = new TimestampForm(Selling_end_time_pick.Text.ToString(), this);
            var l = stf.WaitForValue();
            if (l != null)
            {
                DateTime dt = new DateTime(l[0], l[1], l[2], l[3], l[4], l[5]);
                int total = (int)dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                foreach (var item in SelectedRowIndexes)
                {
                    Gshop1.List_items[item.Position].Sales[Sale_combobox.SelectedIndex].Selling_end_time = total;
                }
                Selling_end_time_pick.Text = dt.ToString();
            }
        }
    }
    public struct Indexes
    {
        public int RowIndex;
        public int Position;
    }
}
