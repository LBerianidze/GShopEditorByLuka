namespace GShopEditorByLuka
{
    partial class Error_search
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Error_search));
            this.Error_combobox = new System.Windows.Forms.ComboBox();
            this.Items_grid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grid_context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GotoSelectedItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImproveMistakes = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteButton = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Items_grid)).BeginInit();
            this.Grid_context.SuspendLayout();
            this.SuspendLayout();
            // 
            // Error_combobox
            // 
            this.Error_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Error_combobox.BackColor = System.Drawing.Color.White;
            this.Error_combobox.Cursor = System.Windows.Forms.Cursors.Default;
            this.Error_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Error_combobox.FormattingEnabled = true;
            this.Error_combobox.Location = new System.Drawing.Point(1, 1);
            this.Error_combobox.Name = "Error_combobox";
            this.Error_combobox.Size = new System.Drawing.Size(783, 21);
            this.Error_combobox.TabIndex = 0;
            this.Error_combobox.SelectedIndexChanged += new System.EventHandler(this.Error_combobox_SelectedIndexChanged);
            // 
            // Items_grid
            // 
            this.Items_grid.AllowDrop = true;
            this.Items_grid.AllowUserToAddRows = false;
            this.Items_grid.AllowUserToDeleteRows = false;
            this.Items_grid.AllowUserToResizeColumns = false;
            this.Items_grid.AllowUserToResizeRows = false;
            this.Items_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Items_grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Items_grid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Items_grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Items_grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Items_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Items_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Items_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5});
            this.Items_grid.ContextMenuStrip = this.Grid_context;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Items_grid.DefaultCellStyle = dataGridViewCellStyle5;
            this.Items_grid.EnableHeadersVisualStyles = false;
            this.Items_grid.Location = new System.Drawing.Point(0, 22);
            this.Items_grid.Name = "Items_grid";
            this.Items_grid.ReadOnly = true;
            this.Items_grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Items_grid.RowHeadersVisible = false;
            this.Items_grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Items_grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Items_grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Items_grid.ShowCellErrors = false;
            this.Items_grid.ShowCellToolTips = false;
            this.Items_grid.ShowEditingIcon = false;
            this.Items_grid.ShowRowErrors = false;
            this.Items_grid.Size = new System.Drawing.Size(784, 475);
            this.Items_grid.TabIndex = 13;
            this.Items_grid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Items_grid_CellMouseDoubleClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 6.912442F;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 45;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FillWeight = 44.7233F;
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column2.Width = 60;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.FillWeight = 119.0432F;
            this.Column4.HeaderText = "Name";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 245;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Error";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 432;
            // 
            // Grid_context
            // 
            this.Grid_context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GotoSelectedItem,
            this.ImproveMistakes,
            this.DeleteButton});
            this.Grid_context.Name = "Grid_context";
            this.Grid_context.Size = new System.Drawing.Size(295, 92);
            // 
            // GotoSelectedItem
            // 
            this.GotoSelectedItem.BackColor = System.Drawing.SystemColors.Control;
            this.GotoSelectedItem.ForeColor = System.Drawing.Color.Black;
            this.GotoSelectedItem.Image = global::GShopEditorByLuka.Properties.Resources.user_walk;
            this.GotoSelectedItem.Name = "GotoSelectedItem";
            this.GotoSelectedItem.Size = new System.Drawing.Size(294, 22);
            this.GotoSelectedItem.Text = "Перейти к выбранным товарам";
            this.GotoSelectedItem.Click += new System.EventHandler(this.GotoSelectedItem_Click);
            // 
            // ImproveMistakes
            // 
            this.ImproveMistakes.BackColor = System.Drawing.SystemColors.Control;
            this.ImproveMistakes.ForeColor = System.Drawing.Color.Black;
            this.ImproveMistakes.Image = global::GShopEditorByLuka.Properties.Resources.Fix;
            this.ImproveMistakes.Name = "ImproveMistakes";
            this.ImproveMistakes.Size = new System.Drawing.Size(294, 22);
            this.ImproveMistakes.Text = "Исправить ошибки выбранных товаров";
            this.ImproveMistakes.Click += new System.EventHandler(this.ImproveMistakes_Click);
            // 
            // Delete
            // 
            this.DeleteButton.Image = global::GShopEditorByLuka.Properties.Resources.Remove;
            this.DeleteButton.Name = "Delete";
            this.DeleteButton.Size = new System.Drawing.Size(294, 22);
            this.DeleteButton.Text = "Удалить выбранные товары";
            this.DeleteButton.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Error_search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(784, 499);
            this.Controls.Add(this.Items_grid);
            this.Controls.Add(this.Error_combobox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Error_search";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search errors";
            this.Shown += new System.EventHandler(this.Error_search_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Items_grid)).EndInit();
            this.Grid_context.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox Error_combobox;
        private System.Windows.Forms.DataGridView Items_grid;
        private System.Windows.Forms.ContextMenuStrip Grid_context;
        private System.Windows.Forms.ToolStripMenuItem GotoSelectedItem;
        private System.Windows.Forms.ToolStripMenuItem ImproveMistakes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ToolStripMenuItem DeleteButton;
    }
}