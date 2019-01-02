using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBLIBRARY;

namespace GShopEditorByLuka
{
    public partial class GshopsInteraction : Form
    {
        public GshopsInteraction(FileGshop gshop1, FileGshop gshop2, Form1 fm, LBLIBRARY.PWHelper.Elements el)
        {
            this.gshop1 = gshop1;
            this.gshop2 = gshop2;
            this.fm = fm;
            this.elem = el;
            InitializeComponent();
            RefreshShopInformation();
            Gshop1SubCategories.Tag = new Stopwatch();
            Gshop2SubCategories.Tag = new Stopwatch();
            if (fm.Gshop1.Version >= 4)
            {
                max1 =9;
            }
            if(fm.Gshop2.Version>=4)
            {
                max2 = 9;
            }
        }
        int max1 = 8;
        int max2 = 8;
        FileGshop gshop1;
        FileGshop gshop2;
        Form1 fm;
        LBLIBRARY.PWHelper.Elements elem;
        int Category1;
        int SubCategory1;
        int Category2;
        int SubCategory2;
        List<Item> Gshop1Items;
        List<Item> Gshop2Items;
        string DragSender;
        public void RefreshShopInformation()
        {
            Gshop1Categories.Items.Clear();
            Gshop2Categories.Items.Clear();
            Gshop1SubCategories.Items.Clear();
            Gshop2SubCategories.Items.Clear();
            Gshop1ItemsGrid.Rows.Clear();
            Gshop2ItemsGrid.Rows.Clear();
            Gshop1Categories.Items.AddRange(gshop1.List_categories.Select(z => z.Category_name).ToArray());
            Gshop2Categories.Items.AddRange(gshop2.List_categories.Select(z => z.Category_name).ToArray());
            Category1 = -1;
            SubCategory1 = -1;
            Category2 = -1;
            SubCategory2 = -1;
        }
        private void Gshop1Categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category1 = Gshop1Categories.SelectedIndex;
            Gshop1SubCategories.Items.Clear();
            Gshop1SubCategories.Items.AddRange(gshop1.List_categories[Category1].Sub_categories.ToArray());
        }
        private void Gshop2Categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category2 = Gshop2Categories.SelectedIndex;
            Gshop2SubCategories.Items.Clear();
            Gshop2SubCategories.Items.AddRange(gshop2.List_categories[Category2].Sub_categories.ToArray());
        }
        private void Gshop1SubCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubCategory1 = Gshop1SubCategories.SelectedIndex;
            Gshop1ItemsGrid.Rows.Clear();
            int i = 1;
            Gshop1Items = gshop1.List_items.Where(z => z.Item_category == Category1 && z.Item_sub_category == SubCategory1).ToList();
            foreach (var item in Gshop1Items)
            {
                Image bm = Properties.Resources.SmallQuestionMark;
                if (elem != null)
                {
                    int ind = elem.Items.FindIndex(z => z.Id == item.Id);
                    if (ind != -1)
                    {
                        bm = elem.Items[ind].IconImage;
                    }
                }
                Gshop1ItemsGrid.Rows.Add(i, item.Id, bm, item.Name);
                i++;
            }
        }
        private void Gshop2SubCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

            SubCategory2 = Gshop2SubCategories.SelectedIndex;
            Gshop2ItemsGrid.Rows.Clear();
            int i = 1;
            Gshop2Items = gshop2.List_items.Where(z => z.Item_category == Category2 && z.Item_sub_category == SubCategory2).ToList();
            foreach (var item in Gshop2Items)
            {
                Image bm = Properties.Resources.SmallQuestionMark;
                if (elem != null)
                {
                    int ind = elem.Items.FindIndex(z => z.Id == item.Id);
                    if (ind != -1)
                    {
                        bm = elem.Items[ind].IconImage;
                    }
                }
                Gshop2ItemsGrid.Rows.Add(i, item.Id, bm, item.Name);
                i++;
            }
        }
        private void Gshop1Items_DragDrop(object sender, DragEventArgs e)
        {
            if (DragSender == "Gshop2Grid")
            {
                if (Category1 != -1 && SubCategory1 != -1)
                {
                    var rowToMove = (e.Data.GetData(typeof(DataGridViewSelectedRowCollection)) as DataGridViewSelectedRowCollection).Cast<DataGridViewRow>().OrderBy(z=>z.Index).ToList();
                    foreach (DataGridViewRow item in rowToMove)
                    {
                        Gshop1ItemsGrid.Rows.Add(Gshop1ItemsGrid.Rows.Count + 1, item.Cells[1].Value, item.Cells[2].Value, item.Cells[3].Value);
                        Item it = Gshop2Items.ElementAt(item.Index);
                        it.Item_category = Category1;
                        it.Item_sub_category = SubCategory1;
                        gshop1.List_items.Add(it);
                        gshop1.Amount++;
                    }
                    Gshop1ItemsGrid.FirstDisplayedScrollingRowIndex = Gshop1ItemsGrid.RowCount - 1;
                    Gshop1Items = gshop1.List_items.Where(z => z.Item_category == Category1 && z.Item_sub_category == SubCategory1).ToList();
                    RefreshItemsPlaces(gshop1);
                    fm.SetSubCategoriesNames(fm.Category_index);
                }
            }
        }
        private void Gshop2Items_DragDrop(object sender, DragEventArgs e)
        {
            if (DragSender == "Gshop1Grid")
            {
                if (Category2 != -1 && SubCategory2 != -1)
                {
                    var rowToMove = (e.Data.GetData(typeof(DataGridViewSelectedRowCollection)) as DataGridViewSelectedRowCollection).Cast<DataGridViewRow>().OrderBy(z => z.Index).ToList();
                    foreach (DataGridViewRow item in rowToMove)
                    {
                        Gshop2ItemsGrid.Rows.Add(Gshop2ItemsGrid.Rows.Count + 1, item.Cells[1].Value, item.Cells[2].Value, item.Cells[3].Value);
                        Item it = Gshop1Items.ElementAt(item.Index);
                        it.Item_category = Category2;
                        it.Item_sub_category = SubCategory2;
                        gshop2.List_items.Add(it);
                        gshop2.Amount++;
                    }
                    Gshop2ItemsGrid.FirstDisplayedScrollingRowIndex = Gshop2ItemsGrid.RowCount - 1;
                    Gshop2Items = gshop1.List_items.Where(z => z.Item_category == Category2 && z.Item_sub_category == SubCategory2).ToList();
                    RefreshItemsPlaces(gshop2);
                }
            }
        }
        private void Gshop1Items_MouseDown(object sender, MouseEventArgs e)
        {
            DragSender = "Gshop1Grid";
        }
        private void Gshop2Items_MouseDown(object sender, MouseEventArgs e)
        {
            DragSender = "Gshop2Grid";
        }
        private void ImportToSecond_Click(object sender, EventArgs e)
        {
            if (Category2 != -1 && SubCategory2 != -1)
            {
                var k = Gshop1ItemsGrid.SelectedRows.Cast<DataGridViewRow>().OrderBy(z => z.Index).ToList();
                foreach (DataGridViewRow item in k)
                {
                    Gshop2ItemsGrid.Rows.Add(Gshop2ItemsGrid.Rows.Count + 1, item.Cells[1].Value, item.Cells[2].Value, item.Cells[3].Value);
                    Item it = Gshop1Items.ElementAt(item.Index);
                    it.Item_category = Category2;
                    it.Item_sub_category = SubCategory2;
                    gshop2.List_items.Add(it);
                    gshop2.Amount++;
                }
                Gshop2ItemsGrid.FirstDisplayedScrollingRowIndex = Gshop2ItemsGrid.RowCount - 1;
                Gshop2Items = gshop1.List_items.Where(z => z.Item_category == Category2 && z.Item_sub_category == SubCategory2).ToList();
                RefreshItemsPlaces(gshop2);
            }
        }
        private void ImportToFirst_Click(object sender, EventArgs e)
        {
            if (Category1 != -1 && SubCategory1 != -1)
            {
                var k = Gshop2ItemsGrid.SelectedRows.Cast<DataGridViewRow>().OrderBy(z => z.Index).ToList();
                foreach (DataGridViewRow item in k)
                {
                    Gshop1ItemsGrid.Rows.Add(Gshop1ItemsGrid.Rows.Count + 1, item.Cells[1].Value, item.Cells[2].Value, item.Cells[3].Value);
                    Item it = Gshop2Items.ElementAt(item.Index);
                    it.Item_category = Category1;
                    it.Item_sub_category = SubCategory1;
                    gshop1.List_items.Add(it);
                    gshop1.Amount++;
                }
                Gshop1ItemsGrid.FirstDisplayedScrollingRowIndex = Gshop1ItemsGrid.RowCount - 1;
                Gshop1Items = gshop1.List_items.Where(z => z.Item_category == Category1 && z.Item_sub_category == SubCategory1).ToList();
                RefreshItemsPlaces(gshop1);
                fm.SetSubCategoriesNames(fm.Category_index);
            }
        }
        private void MoveSubCategoryToFirst_Click(object sender, EventArgs e)
        {
            if (Gshop2Categories.SelectedIndex != -1 && Gshop2SubCategories.SelectedIndex != -1 && Gshop1Categories.SelectedIndex != -1)
            {
                if (gshop1.List_categories[Category1].Amount < max1)
                {
                    gshop1.List_categories[Category1].Sub_categories.Add(gshop2.List_categories[Category2].Sub_categories[SubCategory2]);
                    gshop1.List_categories[Category1].Amount++;
                    List<Item> Items = new List<Item>();
                    foreach (var item in gshop2.List_items.Where(z => z.Item_category == Category2 && z.Item_sub_category == SubCategory2).ToList())
                    {
                        Items.Add(item.Clone());
                    }
                    int am = gshop1.List_categories[Category1].Amount - 1;
                    Items.ForEach(z => z.Item_category = Category1);
                    Items.ForEach(z => z.Item_sub_category = am);
                    gshop1.List_items.AddRange(Items);
                    gshop1.Amount += Items.Count;
                    Gshop1SubCategories.Items.Add(gshop2.List_categories[Category2].Sub_categories[SubCategory2]);
                    RefreshItemsPlaces(gshop1);
                    fm.SetSubCategoriesNames(fm.Category_index);
                }
            }
        }
        private void MoveSubCategoryToSecond_Click(object sender, EventArgs e)
        {
            if (Gshop1Categories.SelectedIndex != -1 && Gshop1SubCategories.SelectedIndex != -1 && Gshop2Categories.SelectedIndex != -1)
            {
                if (gshop2.List_categories[Category2].Amount < max2)
                {
                    gshop2.List_categories[Category2].Sub_categories.Add(gshop1.List_categories[Category1].Sub_categories[SubCategory1]);
                    gshop2.List_categories[Category2].Amount++;
                    List<Item> Items = new List<Item>();
                    foreach(var item in gshop1.List_items.Where(z => z.Item_category == Category1 && z.Item_sub_category == SubCategory1).ToList())
                    {
                        Items.Add(item.Clone());
                    }
                    int am = gshop2.List_categories[Category2].Amount - 1;
                    Items.ForEach(z => z.Item_category = Category2);
                    Items.ForEach(z => z.Item_sub_category = am);
                    gshop2.List_items.AddRange(Items);
                    gshop2.Amount += Items.Count;
                    Gshop2SubCategories.Items.Add(gshop1.List_categories[Category1].Sub_categories[SubCategory1]);
                    RefreshItemsPlaces(gshop2);
                }
            }
        }
        private void Gshop1SubCategories_MouseDown(object sender, MouseEventArgs e)
        {
            ((sender as ListBox).Tag as Stopwatch).Start();
            if (e.Button == MouseButtons.Left)
            {
                DragSender = "Gshop1SubCategories";
            }
        }
        private void Gshop2SubCategories_MouseDown(object sender, MouseEventArgs e)
        {
            ((sender as ListBox).Tag as Stopwatch).Start();
            if (e.Button == MouseButtons.Left)
            {
                DragSender = "Gshop2SubCategories";
            }
        }
        private void Gshop1SubCategories_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void Gshop2SubCategories_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void Gshop2SubCategories_MouseMove(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            if (watch.ElapsedMilliseconds > 75)
            {
                Gshop2SubCategories.DoDragDrop(Gshop2SubCategories.SelectedItem, DragDropEffects.Move);
            }
            watch.Reset();
        }
        private void Gshop1SubCategories_MouseMove(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            if (watch.ElapsedMilliseconds > 75)
            {
                Gshop1SubCategories.DoDragDrop(Gshop1SubCategories.SelectedItem, DragDropEffects.Move);
            }
            watch.Reset();
        }
        private void Gshop1SubCategories_MouseUp(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            watch.Reset();
        }
        private void Gshop2SubCategories_MouseUp(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            watch.Reset();
        }
        private void Gshop2SubCategories_DragDrop(object sender, DragEventArgs e)
        {
            if (DragSender == "Gshop1SubCategories")
            {
                if (gshop2.List_categories[Category2].Amount < max2)
                {
                    Gshop2SubCategories.Items.Add(Gshop1SubCategories.Items[SubCategory1]);
                    gshop2.List_categories[Category2].Sub_categories.Add(Gshop1SubCategories.Items[SubCategory1].ToString());
                    gshop2.List_categories[Category2].Amount++;
                    List<Item> Items = new List<Item>();
                    foreach (var item in gshop1.List_items.Where(z => z.Item_category == Category1 && z.Item_sub_category == SubCategory1).ToList())
                    {
                        Items.Add(item.Clone());
                    }
                    Items.ForEach(z => z.Item_category = Category2);
                    Items.ForEach(z => z.Item_sub_category = gshop2.List_categories[Category2].Amount - 1);
                    gshop2.List_items.AddRange(Items);
                    RefreshItemsPlaces(gshop2);
                }
            }
        }
        private void Gshop1SubCategories_DragDrop(object sender, DragEventArgs e)
        {
            if (DragSender == "Gshop2SubCategories")
            {
                if (gshop1.List_categories[Category1].Amount < max1)
                {
                    Gshop1SubCategories.Items.Add(Gshop2SubCategories.Items[SubCategory2]);
                    gshop1.List_categories[Category1].Sub_categories.Add(Gshop2SubCategories.Items[SubCategory2].ToString());
                    gshop1.List_categories[Category1].Amount++;
                    List<Item> Items = new List<Item>();
                    foreach (var item in gshop2.List_items.Where(z => z.Item_category == Category2 && z.Item_sub_category == SubCategory2).ToList())
                    {
                        Items.Add(item.Clone());
                    }
                    Items.ForEach(z => z.Item_category = Category1);
                    Items.ForEach(z=>z.Item_sub_category = gshop1.List_categories[Category1].Amount - 1);
                    gshop1.List_items.AddRange(Items);
                    RefreshItemsPlaces(gshop1);
                    fm.SetSubCategoriesNames(fm.Category_index);
                }
            }
        }
        void RefreshItemsPlaces(FileGshop Shop)
        {
            Shop.Max_place = Shop.Amount - 1;
            for (int i = 0; i < Shop.List_items.Count; i++)
            {
                Shop.List_items[i].Place = i;
            }
        }
        private void Information_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы так же можете использовать Drag&Drop для перемещения подкатегорий и товаров!!...\r" +
                "//\r"+
                "You can also use Drag&Drop to move subcategories and items!!...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
