namespace GShopEditorByLuka
{
    partial class GshopsInteraction
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GshopsInteraction));
            this.Gshop1Categories = new System.Windows.Forms.ListBox();
            this.Gshop1SubCategories = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Gshop2SubCategories = new System.Windows.Forms.ListBox();
            this.Gshop2Categories = new System.Windows.Forms.ListBox();
            this.Gshop1ItemsGrid = new LBLIBRARY.Components.DraggableDataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gshop2ItemsGrid = new LBLIBRARY.Components.DraggableDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Information = new LBLIBRARY.Components.ButtonC();
            this.MoveSubCategoryToFirst = new LBLIBRARY.Components.ButtonC();
            this.MoveSubCategoryToSecond = new LBLIBRARY.Components.ButtonC();
            this.ImportToFirst = new LBLIBRARY.Components.ButtonC();
            this.ImportToSecond = new LBLIBRARY.Components.ButtonC();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gshop1ItemsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gshop2ItemsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Gshop1Categories
            // 
            this.Gshop1Categories.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.Gshop1Categories.FormattingEnabled = true;
            this.Gshop1Categories.ItemHeight = 16;
            this.Gshop1Categories.Location = new System.Drawing.Point(0, 1);
            this.Gshop1Categories.Name = "Gshop1Categories";
            this.Gshop1Categories.Size = new System.Drawing.Size(161, 132);
            this.Gshop1Categories.TabIndex = 0;
            this.Gshop1Categories.SelectedIndexChanged += new System.EventHandler(this.Gshop1Categories_SelectedIndexChanged);
            // 
            // Gshop1SubCategories
            // 
            this.Gshop1SubCategories.AllowDrop = true;
            this.Gshop1SubCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.Gshop1SubCategories.FormattingEnabled = true;
            this.Gshop1SubCategories.ItemHeight = 16;
            this.Gshop1SubCategories.Location = new System.Drawing.Point(163, 1);
            this.Gshop1SubCategories.Name = "Gshop1SubCategories";
            this.Gshop1SubCategories.Size = new System.Drawing.Size(161, 132);
            this.Gshop1SubCategories.TabIndex = 2;
            this.Gshop1SubCategories.SelectedIndexChanged += new System.EventHandler(this.Gshop1SubCategories_SelectedIndexChanged);
            this.Gshop1SubCategories.DragDrop += new System.Windows.Forms.DragEventHandler(this.Gshop1SubCategories_DragDrop);
            this.Gshop1SubCategories.DragOver += new System.Windows.Forms.DragEventHandler(this.Gshop1SubCategories_DragOver);
            this.Gshop1SubCategories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Gshop1SubCategories_MouseDown);
            this.Gshop1SubCategories.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Gshop1SubCategories_MouseMove);
            this.Gshop1SubCategories.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Gshop1SubCategories_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Information);
            this.panel1.Controls.Add(this.MoveSubCategoryToFirst);
            this.panel1.Controls.Add(this.MoveSubCategoryToSecond);
            this.panel1.Controls.Add(this.ImportToFirst);
            this.panel1.Controls.Add(this.ImportToSecond);
            this.panel1.Location = new System.Drawing.Point(326, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(31, 493);
            this.panel1.TabIndex = 3;
            // 
            // Gshop2SubCategories
            // 
            this.Gshop2SubCategories.AllowDrop = true;
            this.Gshop2SubCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.Gshop2SubCategories.FormattingEnabled = true;
            this.Gshop2SubCategories.ItemHeight = 16;
            this.Gshop2SubCategories.Location = new System.Drawing.Point(523, 1);
            this.Gshop2SubCategories.Name = "Gshop2SubCategories";
            this.Gshop2SubCategories.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Gshop2SubCategories.Size = new System.Drawing.Size(161, 132);
            this.Gshop2SubCategories.TabIndex = 15;
            this.Gshop2SubCategories.SelectedIndexChanged += new System.EventHandler(this.Gshop2SubCategories_SelectedIndexChanged);
            this.Gshop2SubCategories.DragDrop += new System.Windows.Forms.DragEventHandler(this.Gshop2SubCategories_DragDrop);
            this.Gshop2SubCategories.DragOver += new System.Windows.Forms.DragEventHandler(this.Gshop2SubCategories_DragOver);
            this.Gshop2SubCategories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Gshop2SubCategories_MouseDown);
            this.Gshop2SubCategories.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Gshop2SubCategories_MouseMove);
            this.Gshop2SubCategories.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Gshop2SubCategories_MouseUp);
            // 
            // Gshop2Categories
            // 
            this.Gshop2Categories.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.Gshop2Categories.FormattingEnabled = true;
            this.Gshop2Categories.ItemHeight = 16;
            this.Gshop2Categories.Location = new System.Drawing.Point(360, 1);
            this.Gshop2Categories.Name = "Gshop2Categories";
            this.Gshop2Categories.Size = new System.Drawing.Size(161, 132);
            this.Gshop2Categories.TabIndex = 14;
            this.Gshop2Categories.SelectedIndexChanged += new System.EventHandler(this.Gshop2Categories_SelectedIndexChanged);
            // 
            // Gshop1ItemsGrid
            // 
            this.Gshop1ItemsGrid.AllowDrop = true;
            this.Gshop1ItemsGrid.AllowUserToAddRows = false;
            this.Gshop1ItemsGrid.AllowUserToDeleteRows = false;
            this.Gshop1ItemsGrid.AllowUserToResizeColumns = false;
            this.Gshop1ItemsGrid.AllowUserToResizeRows = false;
            this.Gshop1ItemsGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Gshop1ItemsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Gshop1ItemsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Gshop1ItemsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gshop1ItemsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Gshop1ItemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gshop1ItemsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Gshop1ItemsGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.Gshop1ItemsGrid.Location = new System.Drawing.Point(0, 135);
            this.Gshop1ItemsGrid.Name = "Gshop1ItemsGrid";
            this.Gshop1ItemsGrid.ReadOnly = true;
            this.Gshop1ItemsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Gshop1ItemsGrid.RowHeadersVisible = false;
            this.Gshop1ItemsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Gshop1ItemsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Gshop1ItemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Gshop1ItemsGrid.ShowCellErrors = false;
            this.Gshop1ItemsGrid.ShowCellToolTips = false;
            this.Gshop1ItemsGrid.ShowEditingIcon = false;
            this.Gshop1ItemsGrid.ShowRowErrors = false;
            this.Gshop1ItemsGrid.Size = new System.Drawing.Size(324, 359);
            this.Gshop1ItemsGrid.TabIndex = 17;
            this.Gshop1ItemsGrid.Tag = stopwatch1;
            this.Gshop1ItemsGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.Gshop1Items_DragDrop);
            this.Gshop1ItemsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Gshop1Items_MouseDown);
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn4.HeaderText = "#";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 40;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn5.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Icon";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn6.Width = 32;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn7.FillWeight = 119.0432F;
            this.dataGridViewTextBoxColumn7.HeaderText = "                             Name";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 201;
            // 
            // Gshop2ItemsGrid
            // 
            this.Gshop2ItemsGrid.AllowDrop = true;
            this.Gshop2ItemsGrid.AllowUserToAddRows = false;
            this.Gshop2ItemsGrid.AllowUserToDeleteRows = false;
            this.Gshop2ItemsGrid.AllowUserToResizeColumns = false;
            this.Gshop2ItemsGrid.AllowUserToResizeRows = false;
            this.Gshop2ItemsGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Gshop2ItemsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Gshop2ItemsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Gshop2ItemsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gshop2ItemsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Gshop2ItemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gshop2ItemsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column3,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Gshop2ItemsGrid.DefaultCellStyle = dataGridViewCellStyle10;
            this.Gshop2ItemsGrid.Location = new System.Drawing.Point(360, 137);
            this.Gshop2ItemsGrid.Name = "Gshop2ItemsGrid";
            this.Gshop2ItemsGrid.ReadOnly = true;
            this.Gshop2ItemsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Gshop2ItemsGrid.RowHeadersVisible = false;
            this.Gshop2ItemsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Gshop2ItemsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Gshop2ItemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Gshop2ItemsGrid.ShowCellErrors = false;
            this.Gshop2ItemsGrid.ShowCellToolTips = false;
            this.Gshop2ItemsGrid.ShowEditingIcon = false;
            this.Gshop2ItemsGrid.ShowRowErrors = false;
            this.Gshop2ItemsGrid.Size = new System.Drawing.Size(324, 359);
            this.Gshop2ItemsGrid.TabIndex = 16;
            this.Gshop2ItemsGrid.Tag = stopwatch2;
            this.Gshop2ItemsGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.Gshop2Items_DragDrop);
            this.Gshop2ItemsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Gshop2Items_MouseDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn1.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn2.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Icon";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column3.Width = 32;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.FillWeight = 119.0432F;
            this.dataGridViewTextBoxColumn3.HeaderText = "                             Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 201;
            // 
            // Information
            // 
            this.Information.BackColor = System.Drawing.SystemColors.Control;
            this.Information.BorderColor = System.Drawing.Color.Transparent;
            this.Information.BorderWidth = 0;
            this.Information.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.Information.Text = "";
            this.Information.EndColor = System.Drawing.SystemColors.Control;
            this.Information.FlatAppearance.BorderSize = 0;
            this.Information.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Information.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Information.GradientAngle = 90;
            this.Information.Image = global::GShopEditorByLuka.Properties.Resources.info;
            this.Information.Image_Location = new System.Drawing.Point(0, 1);
            this.Information.ImageToHeight = true;
            this.Information.Location = new System.Drawing.Point(0, 464);
            this.Information.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.Information.MouseClickColor2 = System.Drawing.Color.Red;
            this.Information.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.Information.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.Information.Name = "Information";
            this.Information.ShowButtontext = true;
            this.Information.Size = new System.Drawing.Size(32, 29);
            this.Information.StartColor = System.Drawing.SystemColors.Control;
            this.Information.TabIndex = 4;
            this.Information.Text = "buttonZ2";
            this.Information.TextLocation_X = 9;
            this.Information.TextLocation_Y = 14;
            this.Information.Transparent1 = 250;
            this.Information.Transparent2 = 250;
            this.Information.UseVisualStyleBackColor = false;
            this.Information.Click += new System.EventHandler(this.Information_Click);
            // 
            // MoveSubCategoryToFirst
            // 
            this.MoveSubCategoryToFirst.BackColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToFirst.BorderColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToFirst.BorderWidth = 1;
            this.MoveSubCategoryToFirst.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.MoveSubCategoryToFirst.Text = "";
            this.MoveSubCategoryToFirst.EndColor = System.Drawing.Color.MidnightBlue;
            this.MoveSubCategoryToFirst.FlatAppearance.BorderSize = 0;
            this.MoveSubCategoryToFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MoveSubCategoryToFirst.GradientAngle = 90;
            this.MoveSubCategoryToFirst.Image = global::GShopEditorByLuka.Properties.Resources.Left;
            this.MoveSubCategoryToFirst.Image_Location = new System.Drawing.Point(3, 2);
            this.MoveSubCategoryToFirst.ImageToHeight = false;
            this.MoveSubCategoryToFirst.Location = new System.Drawing.Point(0, 30);
            this.MoveSubCategoryToFirst.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.MoveSubCategoryToFirst.MouseClickColor2 = System.Drawing.Color.Red;
            this.MoveSubCategoryToFirst.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.MoveSubCategoryToFirst.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.MoveSubCategoryToFirst.Name = "MoveSubCategoryToFirst";
            this.MoveSubCategoryToFirst.ShowButtontext = true;
            this.MoveSubCategoryToFirst.Size = new System.Drawing.Size(31, 28);
            this.MoveSubCategoryToFirst.StartColor = System.Drawing.Color.DodgerBlue;
            this.MoveSubCategoryToFirst.TabIndex = 3;
            this.MoveSubCategoryToFirst.Text = "buttonZ2";
            this.MoveSubCategoryToFirst.TextLocation_X = 9;
            this.MoveSubCategoryToFirst.TextLocation_Y = 14;
            this.MoveSubCategoryToFirst.Transparent1 = 250;
            this.MoveSubCategoryToFirst.Transparent2 = 250;
            this.MoveSubCategoryToFirst.UseVisualStyleBackColor = false;
            this.MoveSubCategoryToFirst.Click += new System.EventHandler(this.MoveSubCategoryToFirst_Click);
            // 
            // MoveSubCategoryToSecond
            // 
            this.MoveSubCategoryToSecond.BackColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToSecond.BorderColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToSecond.BorderWidth = 1;
            this.MoveSubCategoryToSecond.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.MoveSubCategoryToSecond.Text = "";
            this.MoveSubCategoryToSecond.EndColor = System.Drawing.Color.MidnightBlue;
            this.MoveSubCategoryToSecond.FlatAppearance.BorderSize = 0;
            this.MoveSubCategoryToSecond.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToSecond.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.MoveSubCategoryToSecond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MoveSubCategoryToSecond.GradientAngle = 90;
            this.MoveSubCategoryToSecond.Image = global::GShopEditorByLuka.Properties.Resources.Right;
            this.MoveSubCategoryToSecond.Image_Location = new System.Drawing.Point(3, 2);
            this.MoveSubCategoryToSecond.ImageToHeight = false;
            this.MoveSubCategoryToSecond.Location = new System.Drawing.Point(0, 0);
            this.MoveSubCategoryToSecond.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.MoveSubCategoryToSecond.MouseClickColor2 = System.Drawing.Color.Red;
            this.MoveSubCategoryToSecond.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.MoveSubCategoryToSecond.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.MoveSubCategoryToSecond.Name = "MoveSubCategoryToSecond";
            this.MoveSubCategoryToSecond.ShowButtontext = true;
            this.MoveSubCategoryToSecond.Size = new System.Drawing.Size(31, 28);
            this.MoveSubCategoryToSecond.StartColor = System.Drawing.Color.DodgerBlue;
            this.MoveSubCategoryToSecond.TabIndex = 2;
            this.MoveSubCategoryToSecond.Text = "buttonZ1";
            this.MoveSubCategoryToSecond.TextLocation_X = 9;
            this.MoveSubCategoryToSecond.TextLocation_Y = 14;
            this.MoveSubCategoryToSecond.Transparent1 = 250;
            this.MoveSubCategoryToSecond.Transparent2 = 250;
            this.MoveSubCategoryToSecond.UseVisualStyleBackColor = false;
            this.MoveSubCategoryToSecond.Click += new System.EventHandler(this.MoveSubCategoryToSecond_Click);
            // 
            // ImportToFirst
            // 
            this.ImportToFirst.BackColor = System.Drawing.Color.Transparent;
            this.ImportToFirst.BorderColor = System.Drawing.Color.Transparent;
            this.ImportToFirst.BorderWidth = 1;
            this.ImportToFirst.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.ImportToFirst.Text = "";
            this.ImportToFirst.EndColor = System.Drawing.Color.MidnightBlue;
            this.ImportToFirst.FlatAppearance.BorderSize = 0;
            this.ImportToFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ImportToFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ImportToFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportToFirst.GradientAngle = 90;
            this.ImportToFirst.Image = global::GShopEditorByLuka.Properties.Resources.Left;
            this.ImportToFirst.Image_Location = new System.Drawing.Point(3, 2);
            this.ImportToFirst.ImageToHeight = false;
            this.ImportToFirst.Location = new System.Drawing.Point(0, 244);
            this.ImportToFirst.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.ImportToFirst.MouseClickColor2 = System.Drawing.Color.Red;
            this.ImportToFirst.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.ImportToFirst.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.ImportToFirst.Name = "ImportToFirst";
            this.ImportToFirst.ShowButtontext = true;
            this.ImportToFirst.Size = new System.Drawing.Size(31, 28);
            this.ImportToFirst.StartColor = System.Drawing.Color.DodgerBlue;
            this.ImportToFirst.TabIndex = 1;
            this.ImportToFirst.Text = "buttonZ2";
            this.ImportToFirst.TextLocation_X = 9;
            this.ImportToFirst.TextLocation_Y = 14;
            this.ImportToFirst.Transparent1 = 250;
            this.ImportToFirst.Transparent2 = 250;
            this.ImportToFirst.UseVisualStyleBackColor = false;
            this.ImportToFirst.Click += new System.EventHandler(this.ImportToFirst_Click);
            // 
            // ImportToSecond
            // 
            this.ImportToSecond.BackColor = System.Drawing.Color.Transparent;
            this.ImportToSecond.BorderColor = System.Drawing.Color.Transparent;
            this.ImportToSecond.BorderWidth = 1;
            this.ImportToSecond.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.ImportToSecond.Text = "";
            this.ImportToSecond.EndColor = System.Drawing.Color.MidnightBlue;
            this.ImportToSecond.FlatAppearance.BorderSize = 0;
            this.ImportToSecond.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ImportToSecond.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ImportToSecond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportToSecond.GradientAngle = 90;
            this.ImportToSecond.Image = global::GShopEditorByLuka.Properties.Resources.Right;
            this.ImportToSecond.Image_Location = new System.Drawing.Point(3, 2);
            this.ImportToSecond.ImageToHeight = false;
            this.ImportToSecond.Location = new System.Drawing.Point(0, 214);
            this.ImportToSecond.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.ImportToSecond.MouseClickColor2 = System.Drawing.Color.Red;
            this.ImportToSecond.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.ImportToSecond.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.ImportToSecond.Name = "ImportToSecond";
            this.ImportToSecond.ShowButtontext = true;
            this.ImportToSecond.Size = new System.Drawing.Size(31, 28);
            this.ImportToSecond.StartColor = System.Drawing.Color.DodgerBlue;
            this.ImportToSecond.TabIndex = 0;
            this.ImportToSecond.Text = "buttonZ1";
            this.ImportToSecond.TextLocation_X = 9;
            this.ImportToSecond.TextLocation_Y = 14;
            this.ImportToSecond.Transparent1 = 250;
            this.ImportToSecond.Transparent2 = 250;
            this.ImportToSecond.UseVisualStyleBackColor = false;
            this.ImportToSecond.Click += new System.EventHandler(this.ImportToSecond_Click);
            // 
            // GshopsInteraction
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 495);
            this.Controls.Add(this.Gshop1ItemsGrid);
            this.Controls.Add(this.Gshop2ItemsGrid);
            this.Controls.Add(this.Gshop2SubCategories);
            this.Controls.Add(this.Gshop2Categories);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Gshop1SubCategories);
            this.Controls.Add(this.Gshop1Categories);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GshopsInteraction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gshops Interaction";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gshop1ItemsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gshop2ItemsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Gshop1Categories;
        private System.Windows.Forms.ListBox Gshop1SubCategories;
        private System.Windows.Forms.Panel panel1;
        private LBLIBRARY.Components.DraggableDataGridView Gshop2ItemsGrid;
        private System.Windows.Forms.ListBox Gshop2SubCategories;
        private System.Windows.Forms.ListBox Gshop2Categories;
        private LBLIBRARY.Components.DraggableDataGridView Gshop1ItemsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private LBLIBRARY.Components.ButtonC ImportToSecond;
        private LBLIBRARY.Components.ButtonC ImportToFirst;
        private LBLIBRARY.Components.ButtonC MoveSubCategoryToFirst;
        private LBLIBRARY.Components.ButtonC MoveSubCategoryToSecond;
        private LBLIBRARY.Components.ButtonC Information;
    }
}