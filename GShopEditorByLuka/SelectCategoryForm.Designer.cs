namespace GShopEditorByLuka
{
    partial class SelectCategoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCategoryForm));
            this.Categories_listbox = new System.Windows.Forms.ListBox();
            this.Categories_context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChangeCategoryName_button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SubCategories_listbox = new System.Windows.Forms.ListBox();
            this.Sub_categories = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add_SubCategory_button = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeSubCategoryName_button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUsingDragAndDropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AcceptChangingCategories_button = new System.Windows.Forms.Button();
            this.SubCategory_textbox = new System.Windows.Forms.TextBox();
            this.CategoryNameChange_textbox = new System.Windows.Forms.TextBox();
            this.Categories_context.SuspendLayout();
            this.Sub_categories.SuspendLayout();
            this.SuspendLayout();
            // 
            // Categories_listbox
            // 
            this.Categories_listbox.AllowDrop = true;
            this.Categories_listbox.ContextMenuStrip = this.Categories_context;
            this.Categories_listbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Categories_listbox.FormattingEnabled = true;
            this.Categories_listbox.ItemHeight = 20;
            this.Categories_listbox.Location = new System.Drawing.Point(1, 0);
            this.Categories_listbox.Name = "Categories_listbox";
            this.Categories_listbox.Size = new System.Drawing.Size(111, 184);
            this.Categories_listbox.TabIndex = 0;
            this.Categories_listbox.SelectedIndexChanged += new System.EventHandler(this.Categories_listbox_SelectedIndexChanged);
            this.Categories_listbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Categories_listbox_DragDrop);
            this.Categories_listbox.DragOver += new System.Windows.Forms.DragEventHandler(this.Categories_listbox_DragOver);
            this.Categories_listbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Categories_listbox_MouseDown);
            this.Categories_listbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Categories_listbox_MouseMove);
            this.Categories_listbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Categories_listbox_MouseUp);
            // 
            // Categories_context
            // 
            this.Categories_context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeCategoryName_button,
            this.toolStripMenuItem1});
            this.Categories_context.Name = "Sub_categories";
            this.Categories_context.Size = new System.Drawing.Size(218, 52);
            // 
            // ChangeCategoryName_button
            // 
            this.ChangeCategoryName_button.Image = global::GShopEditorByLuka.Properties.Resources.rename;
            this.ChangeCategoryName_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ChangeCategoryName_button.Name = "ChangeCategoryName_button";
            this.ChangeCategoryName_button.Size = new System.Drawing.Size(217, 24);
            this.ChangeCategoryName_button.Text = "Изменить название";
            this.ChangeCategoryName_button.Click += new System.EventHandler(this.ChangeCategoryName);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::GShopEditorByLuka.Properties.Resources.move;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 24);
            this.toolStripMenuItem1.Text = "Move: Use Drag-And-Drop";
            // 
            // SubCategories_listbox
            // 
            this.SubCategories_listbox.AllowDrop = true;
            this.SubCategories_listbox.ContextMenuStrip = this.Sub_categories;
            this.SubCategories_listbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubCategories_listbox.FormattingEnabled = true;
            this.SubCategories_listbox.ItemHeight = 20;
            this.SubCategories_listbox.Location = new System.Drawing.Point(118, 0);
            this.SubCategories_listbox.Name = "SubCategories_listbox";
            this.SubCategories_listbox.Size = new System.Drawing.Size(111, 184);
            this.SubCategories_listbox.TabIndex = 3;
            this.SubCategories_listbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.SubCategories_listbox_DragDrop);
            this.SubCategories_listbox.DragOver += new System.Windows.Forms.DragEventHandler(this.SubCategories_listbox_DragOver);
            this.SubCategories_listbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SubCategories_listbox_MouseDown);
            this.SubCategories_listbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SubCategories_listbox_MouseMove);
            this.SubCategories_listbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SubCategories_listbox_MouseUp);
            // 
            // Sub_categories
            // 
            this.Sub_categories.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_SubCategory_button,
            this.ChangeSubCategoryName_button,
            this.toolStripSeparator1,
            this.moveUsingDragAndDropToolStripMenuItem});
            this.Sub_categories.Name = "Sub_categories";
            this.Sub_categories.Size = new System.Drawing.Size(218, 82);
            // 
            // Add_SubCategory_button
            // 
            this.Add_SubCategory_button.Image = global::GShopEditorByLuka.Properties.Resources.Clone;
            this.Add_SubCategory_button.Name = "Add_SubCategory_button";
            this.Add_SubCategory_button.Size = new System.Drawing.Size(217, 24);
            this.Add_SubCategory_button.Text = "Добавить";
            this.Add_SubCategory_button.Click += new System.EventHandler(this.AddSubCategory);
            // 
            // ChangeSubCategoryName_button
            // 
            this.ChangeSubCategoryName_button.Image = global::GShopEditorByLuka.Properties.Resources.rename;
            this.ChangeSubCategoryName_button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ChangeSubCategoryName_button.Name = "ChangeSubCategoryName_button";
            this.ChangeSubCategoryName_button.Size = new System.Drawing.Size(217, 24);
            this.ChangeSubCategoryName_button.Text = "Изменить название";
            this.ChangeSubCategoryName_button.Click += new System.EventHandler(this.ChangeSubCategoryName);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // moveUsingDragAndDropToolStripMenuItem
            // 
            this.moveUsingDragAndDropToolStripMenuItem.Image = global::GShopEditorByLuka.Properties.Resources.move;
            this.moveUsingDragAndDropToolStripMenuItem.Name = "moveUsingDragAndDropToolStripMenuItem";
            this.moveUsingDragAndDropToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.moveUsingDragAndDropToolStripMenuItem.Text = "Move: Use Drag-And-Drop";
            // 
            // AcceptChangingCategories_button
            // 
            this.AcceptChangingCategories_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptChangingCategories_button.Image = global::GShopEditorByLuka.Properties.Resources.accept;
            this.AcceptChangingCategories_button.Location = new System.Drawing.Point(1, 185);
            this.AcceptChangingCategories_button.Name = "AcceptChangingCategories_button";
            this.AcceptChangingCategories_button.Size = new System.Drawing.Size(228, 39);
            this.AcceptChangingCategories_button.TabIndex = 2;
            this.AcceptChangingCategories_button.Text = "Подтвердить";
            this.AcceptChangingCategories_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AcceptChangingCategories_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AcceptChangingCategories_button.UseVisualStyleBackColor = true;
            this.AcceptChangingCategories_button.Click += new System.EventHandler(this.Accept);
            // 
            // SubCategory_textbox
            // 
            this.SubCategory_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubCategory_textbox.Location = new System.Drawing.Point(118, 62);
            this.SubCategory_textbox.MaxLength = 10;
            this.SubCategory_textbox.Multiline = true;
            this.SubCategory_textbox.Name = "SubCategory_textbox";
            this.SubCategory_textbox.Size = new System.Drawing.Size(111, 22);
            this.SubCategory_textbox.TabIndex = 4;
            this.SubCategory_textbox.Visible = false;
            this.SubCategory_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubCategory_textbox_KeyDown);
            this.SubCategory_textbox.Leave += new System.EventHandler(this.SubCategory_textbox_Leave);
            // 
            // CategoryNameChange_textbox
            // 
            this.CategoryNameChange_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryNameChange_textbox.Location = new System.Drawing.Point(1, 62);
            this.CategoryNameChange_textbox.MaxLength = 10;
            this.CategoryNameChange_textbox.Multiline = true;
            this.CategoryNameChange_textbox.Name = "CategoryNameChange_textbox";
            this.CategoryNameChange_textbox.Size = new System.Drawing.Size(111, 22);
            this.CategoryNameChange_textbox.TabIndex = 6;
            this.CategoryNameChange_textbox.Visible = false;
            this.CategoryNameChange_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CategoryNameChange_textbox_KeyDown);
            this.CategoryNameChange_textbox.Leave += new System.EventHandler(this.CategoryNameChange_textbox_Leave);
            // 
            // SelectCategoryForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 224);
            this.Controls.Add(this.CategoryNameChange_textbox);
            this.Controls.Add(this.SubCategory_textbox);
            this.Controls.Add(this.SubCategories_listbox);
            this.Controls.Add(this.AcceptChangingCategories_button);
            this.Controls.Add(this.Categories_listbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectCategoryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Категории";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectCategoryForm_FormClosed);
            this.Categories_context.ResumeLayout(false);
            this.Sub_categories.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptChangingCategories_button;
        public System.Windows.Forms.ListBox Categories_listbox;
        public System.Windows.Forms.ListBox SubCategories_listbox;
        private System.Windows.Forms.ContextMenuStrip Sub_categories;
        private System.Windows.Forms.ToolStripMenuItem Add_SubCategory_button;
        private System.Windows.Forms.ToolStripMenuItem ChangeSubCategoryName_button;
        private System.Windows.Forms.TextBox SubCategory_textbox;
        private System.Windows.Forms.ContextMenuStrip Categories_context;
        private System.Windows.Forms.ToolStripMenuItem ChangeCategoryName_button;
        private System.Windows.Forms.TextBox CategoryNameChange_textbox;
        private System.Windows.Forms.ToolStripMenuItem moveUsingDragAndDropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}