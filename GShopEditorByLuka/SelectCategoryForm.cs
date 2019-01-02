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

namespace GShopEditorByLuka
{
    public partial class SelectCategoryForm : Form
    {
        public SelectCategoryForm(Form1 fm1, List<Categories> lk)
        {
            InitializeComponent();
            this.Categories_list = lk;
            this.fm = fm1;
            Categories_listbox.Tag = new Stopwatch();
            SubCategories_listbox.Tag = new Stopwatch();
            if(fm1.Gshop1.Version>=4)
            {
                max = 9;
            }
        }
        public List<Categories> Categories_list;
        public List<Item> read;
        Form1 fm;
        int Action;
        int Category;
        bool Clicked;
        int Sub_category;
        String Drag;
        int max = 8;
        private void Accept(object sender, EventArgs e)
        {
            if (Action == 1)
            {
                #region categories
                if (Categories_listbox.SelectedIndex == -1)
                {
                    Category = 0;
                }
                else
                {
                    Category = Categories_listbox.SelectedIndex;
                }
                if (SubCategories_listbox.SelectedIndex == -1)
                {
                    Sub_category = 0;
                }
                else
                {
                    Sub_category = SubCategories_listbox.SelectedIndex;
                }
                #endregion
                if (SubCategories_listbox.SelectedIndex != 8)
                    fm.SetItemCategory(Categories_listbox.SelectedIndex, SubCategories_listbox.SelectedIndex, 1);
                else
                    if (Lang == 1)
                    MessageBox.Show("Невозможно перейти к девятой категории.Для редактирования девятой категории,нужно переместить ее на 8 место или выше,отредактировать и вернуть обратно на 9 позицию","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else
                    MessageBox.Show("It's impossible to go to the ninth category.To edit the ninth category, you need to move it to the 8th place or higher, edit and return back to the 9th position", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
            }
            else if (Action == 2 || Action == 3)
            {
                fm.New_category = Categories_listbox.SelectedIndex;
                fm.New_sub_category = SubCategories_listbox.SelectedIndex;
                Clicked = true;
                this.Hide();
            }
            else if (Action ==4)
            {
                if(SubCategories_listbox.SelectedIndex!=8)
                fm.SetItemCategory(Categories_listbox.SelectedIndex, SubCategories_listbox.SelectedIndex,4);
                else
                    if (Lang == 1)
                    MessageBox.Show("Невозможно перейти к девятой категории.Для редактирования девятой категории,нужно переместить ее на 8 место или выше,отредактировать и вернуть обратно на 9 позицию", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("It's impossible to go to the ninth category.To edit the ninth category, you need to move it to the 8th place or higher, edit and return back to the 9th position", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
            }
        }
        int Lang;
        public void RefreshInformation(int Cat_index, int Sub_cat_index, int action, int Language)
        {
            #region Lang
            Lang = Language;
            if (Language == 1 && this.Text != "Категории")
            {
                this.Text = "Категории";
                Add_SubCategory_button.Text = "Добавить";
                ChangeSubCategoryName_button.Text = "Изменить название";
                ChangeCategoryName_button.Text = "Изменить название";
                AcceptChangingCategories_button.Text = "Подтвердить";
            }
            else if (Language == 2 && this.Text != "Categories")
            {
                this.Text = "Categories";
                Add_SubCategory_button.Text = "Add";
                ChangeCategoryName_button.Text = "Change name";
                ChangeSubCategoryName_button.Text = "Change name";
                AcceptChangingCategories_button.Text = "Confirm";
            }
            #endregion
            Clicked = false;
            Action = action;
            if (action == 1)
            {
                Categories_listbox.Items.Clear();
                SubCategories_listbox.Items.Clear();
                foreach (var d in Categories_list)
                {
                    Categories_listbox.Items.Add(d.Category_name);
                }
                Categories_listbox.SelectedIndex = Cat_index;
                if (Categories_list[Cat_index].Amount > 0)
                    SubCategories_listbox.SelectedIndex = Sub_cat_index;
            }
            else if (action == 2)
            {
                Categories_listbox.Items.Clear();
                SubCategories_listbox.Items.Clear();
                foreach (var d in Categories_list)
                {
                    Categories_listbox.Items.Add(d.Category_name);
                }
            }
            else if (action == 3)
            {
                Categories_listbox.Items.Clear();
                SubCategories_listbox.Items.Clear();
                foreach (var d in Categories_list)
                {
                    Categories_listbox.Items.Add(d.Category_name);
                }
                Categories_listbox.SelectedIndex = Cat_index;
            }
            else if (action == 4)
            {
                Categories_listbox.Items.Clear();
                SubCategories_listbox.Items.Clear();
                foreach (var d in Categories_list)
                {
                    Categories_listbox.Items.Add(d.Category_name);
                }
            }
        }
        private void Categories_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Categories_listbox.SelectedIndex != -1)
            {
                SubCategories_listbox.Items.Clear();
                foreach (var d in Categories_list[Categories_listbox.SelectedIndex].Sub_categories)
                {
                    SubCategories_listbox.Items.Add(d);
                }
                if (SubCategories_listbox.Items.Count != 0)
                    SubCategories_listbox.SelectedIndex = 0;
            }
        }
        private void SelectCategoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Clicked == false)
            {
                if (Action == 2)
                {
                    fm.New_category = 0;
                    fm.New_sub_category = 0;
                }
                else if (Action == 3)
                {
                    fm.New_category = fm.Category_index;
                    fm.New_sub_category = 0;
                }
            }
        }
        private void AddSubCategory(object sender, EventArgs e)
        {
            if (Categories_listbox.SelectedIndex != -1)
            {
                if (SubCategories_listbox.Items.Count < max)
                {
                    fm.Gshop1.List_categories[Categories_listbox.SelectedIndex].Amount += 1;
                    fm.Gshop1.List_categories[Categories_listbox.SelectedIndex].Sub_categories.Add("Новое");
                    SubCategories_listbox.Items.Add("Новое");
                    fm.RenameSubCategorisFromSelectCategoryForm(Categories_listbox.SelectedIndex, 3);
                }
            }
        }
        private void ChangeSubCategoryName(object sender, EventArgs e)
        {
            if (Categories_listbox.SelectedIndex != -1)
            {
                if (SubCategories_listbox.SelectedIndex != -1)
                {
                    SubCategory_textbox.Location = new Point(118, SubCategories_listbox.SelectedIndex * 20);
                    SubCategory_textbox.Text = SubCategories_listbox.SelectedItem.ToString();
                    SubCategory_textbox.Visible = true;
                    SubCategory_textbox.Focus();
                }
            }
        }
        private void ChangeCategoryName(object sender, EventArgs e)
        {
            if (Categories_listbox.SelectedIndex != -1)
            {
                CategoryNameChange_textbox.Location = new Point(1, Categories_listbox.SelectedIndex * 20);
                CategoryNameChange_textbox.Text = Categories_listbox.SelectedItem.ToString();
                CategoryNameChange_textbox.Visible = true;
                CategoryNameChange_textbox.Focus();
            }
        }
        private void SubCategories_listbox_MouseDown(object sender, MouseEventArgs e)
        {
            ((sender as ListBox).Tag as Stopwatch).Start();
            if (e.Button == MouseButtons.Left)
            {
                Drag = "Sub";
            }
        }
        private void Categories_listbox_MouseDown(object sender, MouseEventArgs e)
        {
            ((sender as ListBox).Tag as Stopwatch).Start();
            Categories_listbox_SelectedIndexChanged(null, null);
            if (e.Button == MouseButtons.Left)
            {
                Drag = "Cat";
            }
        }
        private void Categories_listbox_DragDrop(object sender, DragEventArgs e)
        {
            if (Drag == "Cat")
            {
                int SourceIndex = Categories_listbox.SelectedIndex;
                var SourceItem = Categories_listbox.Items[SourceIndex];
                Point point = Categories_listbox.PointToClient(new Point(e.X, e.Y));
                int TargetIndex = Categories_listbox.IndexFromPoint(point);
                if (TargetIndex == -1)
                {
                    TargetIndex = 0;
                }

                Categories_listbox.Items.Remove(SourceItem);
                this.Categories_listbox.Items.Insert(TargetIndex, SourceItem);
                Categories_listbox.SelectedIndex = TargetIndex;
                var Category = Categories_list[SourceIndex];
                Categories_list.RemoveAt(SourceIndex);
                Categories_list.Insert(TargetIndex, Category);
                if(SourceIndex >TargetIndex)
                {
                    read.Where(i => i.Item_category == SourceIndex).ToList().ForEach(d => d.Item_category = 555);
                    read.Where(d => d.Item_category >= TargetIndex && d.Item_category <= SourceIndex).ToList().ForEach(d => d.Item_category++);
                    read.Where(i => i.Item_category == 555).ToList().ForEach(d => d.Item_category = TargetIndex);
                }
                else if (TargetIndex > SourceIndex)
                {
                    read.Where(i => i.Item_category == SourceIndex).ToList().ForEach(d => d.Item_category = 555);
                    read.Where(d => d.Item_category >= SourceIndex && d.Item_category <= TargetIndex).ToList().ForEach(d => d.Item_category--);
                    read.Where(i => i.Item_category == 555).ToList().ForEach(d => d.Item_category = TargetIndex);
                }
                fm.RefreshCategoriesNames();
                Categories_listbox_SelectedIndexChanged(null, null);
            }
            else
            {
                int SubIndex = SubCategories_listbox.SelectedIndex;
                int SourceIndex = Categories_listbox.SelectedIndex;
                Point point = Categories_listbox.PointToClient(new Point(e.X, e.Y));
                int TargetIndex = this.Categories_listbox.IndexFromPoint(point);
                if (Categories_list[TargetIndex].Amount < max)
                {
                    var Item = Categories_list[SourceIndex].Sub_categories[SubIndex];
                    Categories_list[SourceIndex].Sub_categories.RemoveAt(SubIndex);
                    Categories_list[SourceIndex].Amount--;
                    SubCategories_listbox.Items.RemoveAt(SubIndex);
                    Categories_list[TargetIndex].Sub_categories.Insert(Categories_list[TargetIndex].Amount, Item);
                    Categories_list[TargetIndex].Amount++;
                    read.Where(z => z.Item_category == SourceIndex && z.Item_sub_category == SubIndex).ToList().ForEach(b => b.Item_sub_category = 500);
                    read.Where(z => z.Item_category == SourceIndex && z.Item_sub_category == 500).ToList().ForEach(b => b.Item_category = TargetIndex);
                    read.Where(z => z.Item_category == TargetIndex && z.Item_sub_category == 500).ToList().ForEach(b => b.Item_sub_category = Categories_list[TargetIndex].Amount-1);
                    read.Where(z => z.Item_category == SourceIndex && z.Item_sub_category >= SubIndex).ToList().ForEach(z => z.Item_sub_category--);
                    Categories_listbox.SelectedIndex = TargetIndex;
                    SubCategories_listbox.SelectedIndex = Categories_list[TargetIndex].Amount - 1;
                }
                else
                    if(Lang==1)
                    MessageBox.Show("Количество подкатегорий в этой категории достигло максимума!","Действие невозможно",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else
                    MessageBox.Show("The number of subcategories in this category has reached maximum!!", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void SubCategories_listbox_DragDrop(object sender, DragEventArgs e)
        {
            int c = Categories_listbox.SelectedIndex;
            int SourceIndex = SubCategories_listbox.SelectedIndex;
            var SourceItem = SubCategories_listbox.Items[SourceIndex];
            Point point = SubCategories_listbox.PointToClient(new Point(e.X, e.Y));
            int TargetIndex = this.SubCategories_listbox.IndexFromPoint(point);
            if (TargetIndex == -1)
                TargetIndex = 0;
            this.SubCategories_listbox.Items.Remove(SourceItem);
            this.SubCategories_listbox.Items.Insert(TargetIndex, SourceItem);
            this.SubCategories_listbox.SelectedIndex = TargetIndex;
            string sr = Categories_list[c].Sub_categories[SourceIndex];
            Categories_list[c].Sub_categories.RemoveAt(SourceIndex);
            Categories_list[c].Sub_categories.Insert(TargetIndex, sr);
            if (SourceIndex > TargetIndex)
            {
                read.Where(i => i.Item_category == c && i.Item_sub_category == SourceIndex).ToList().ForEach(d => d.Item_sub_category = 555);
                read.Where(d => d.Item_category == c && d.Item_sub_category >= TargetIndex && d.Item_sub_category <= SourceIndex).ToList().ForEach(d => d.Item_sub_category++);
                read.Where(i => i.Item_category == c && i.Item_sub_category == 555).ToList().ForEach(d => d.Item_sub_category = TargetIndex);
            }
            else
            {
                read.Where(i => i.Item_category == c && i.Item_sub_category == SourceIndex).ToList().ForEach(d => d.Item_sub_category = 555);
                read.Where(d => d.Item_category == c && d.Item_sub_category >= SourceIndex && d.Item_sub_category <= TargetIndex).ToList().ForEach(d => d.Item_sub_category--);
                read.Where(i => i.Item_category == c && i.Item_sub_category == 555).ToList().ForEach(d => d.Item_sub_category = TargetIndex);
            }
        }
        private void SubCategories_listbox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void Categories_listbox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void Categories_listbox_MouseUp(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            watch.Reset();
        }
        private void SubCategories_listbox_MouseUp(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            watch.Reset();
        }
        private void Categories_listbox_MouseMove(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            if (watch.ElapsedMilliseconds > 75)
            {
                Categories_listbox.DoDragDrop(Categories_listbox.SelectedItem, DragDropEffects.Move);
            }
            watch.Reset();
        }
        private void SubCategories_listbox_MouseMove(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as System.Windows.Forms.ListBox).Tag as Stopwatch);
            watch.Stop();
            if (watch.ElapsedMilliseconds > 75)
            {
                SubCategories_listbox.DoDragDrop(SubCategories_listbox.SelectedItem, DragDropEffects.Move);
            }
            watch.Reset();
        }
        private void SubCategory_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubCategory_textbox_Leave(null, null);
            }
        }
        private void SubCategory_textbox_Leave(object sender, EventArgs e)
        {
            Categories_list[Categories_listbox.SelectedIndex].Sub_categories[SubCategories_listbox.SelectedIndex] = SubCategory_textbox.Text;
            SubCategories_listbox.Items[SubCategories_listbox.SelectedIndex] = SubCategory_textbox.Text;
            SubCategory_textbox.Visible = false;
            fm.Gshop1.List_categories = Categories_list;
            fm.RenameSubCategorisFromSelectCategoryForm(Categories_listbox.SelectedIndex, 2);
        }
        private void CategoryNameChange_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CategoryNameChange_textbox_Leave(null, null);
            }
        }
        private void CategoryNameChange_textbox_Leave(object sender, EventArgs e)
        {
            Categories_list[Categories_listbox.SelectedIndex].Category_name = CategoryNameChange_textbox.Text;
            Categories_listbox.Items[Categories_listbox.SelectedIndex] = CategoryNameChange_textbox.Text;
            CategoryNameChange_textbox.Visible = false;
            fm.Gshop1.List_categories = Categories_list;
            fm.RenameSubCategorisFromSelectCategoryForm(Categories_listbox.SelectedIndex, 1);
        }
    }
}
