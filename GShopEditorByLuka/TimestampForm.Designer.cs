namespace GShopEditorByLuka
{
    partial class TimestampForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimestampForm));
            this.label1 = new System.Windows.Forms.Label();
            this.Year = new LBLIBRARY.Components.NumericUpDownEx();
            this.Month = new LBLIBRARY.Components.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.Day = new LBLIBRARY.Components.NumericUpDownEx();
            this.label3 = new System.Windows.Forms.Label();
            this.Hour = new LBLIBRARY.Components.NumericUpDownEx();
            this.label4 = new System.Windows.Forms.Label();
            this.Minute = new LBLIBRARY.Components.NumericUpDownEx();
            this.label5 = new System.Windows.Forms.Label();
            this.Second = new LBLIBRARY.Components.NumericUpDownEx();
            this.label6 = new System.Windows.Forms.Label();
            this.Accept = new System.Windows.Forms.Button();
            this.DateTimeNow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(0, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year:";
            // 
            // Year
            // 
            this.Year.BackColor = System.Drawing.Color.White;
            this.Year.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Year.Increment = 1;
            this.Year.Location = new System.Drawing.Point(59, 23);
            this.Year.MaximalValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Year.MinimalValue = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.Year.Name = "Year";
            this.Year.SetBlack = false;
            this.Year.SetDecimalPlaces = 0;
            this.Year.SetThousandsSeparator = false;
            this.Year.Size = new System.Drawing.Size(91, 21);
            this.Year.TabIndex = 32;
            this.Year.TextBoxBackGroundColor = System.Drawing.SystemColors.Window;
            this.Year.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Year.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.Year.TextBoxName = "Flags_numeric";
            this.Year.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // Month
            // 
            this.Month.BackColor = System.Drawing.Color.White;
            this.Month.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Month.Increment = 1;
            this.Month.Location = new System.Drawing.Point(59, 46);
            this.Month.MaximalValue = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.Month.MinimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Month.Name = "Month";
            this.Month.SetBlack = false;
            this.Month.SetDecimalPlaces = 0;
            this.Month.SetThousandsSeparator = false;
            this.Month.Size = new System.Drawing.Size(91, 21);
            this.Month.TabIndex = 34;
            this.Month.TextBoxBackGroundColor = System.Drawing.SystemColors.Window;
            this.Month.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Month.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.Month.TextBoxName = "Flags_numeric";
            this.Month.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(0, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 33;
            this.label2.Text = "Month:";
            // 
            // Day
            // 
            this.Day.BackColor = System.Drawing.Color.White;
            this.Day.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Day.Increment = 1;
            this.Day.Location = new System.Drawing.Point(59, 69);
            this.Day.MaximalValue = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.Day.MinimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Day.Name = "Day";
            this.Day.SetBlack = false;
            this.Day.SetDecimalPlaces = 0;
            this.Day.SetThousandsSeparator = false;
            this.Day.Size = new System.Drawing.Size(91, 21);
            this.Day.TabIndex = 34;
            this.Day.TextBoxBackGroundColor = System.Drawing.SystemColors.Window;
            this.Day.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Day.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.Day.TextBoxName = "Flags_numeric";
            this.Day.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(0, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 19);
            this.label3.TabIndex = 33;
            this.label3.Text = "Day:";
            // 
            // Hour
            // 
            this.Hour.BackColor = System.Drawing.Color.White;
            this.Hour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Hour.Increment = 1;
            this.Hour.Location = new System.Drawing.Point(59, 92);
            this.Hour.MaximalValue = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Hour.MinimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Hour.Name = "Hour";
            this.Hour.SetBlack = false;
            this.Hour.SetDecimalPlaces = 0;
            this.Hour.SetThousandsSeparator = false;
            this.Hour.Size = new System.Drawing.Size(91, 21);
            this.Hour.TabIndex = 34;
            this.Hour.TextBoxBackGroundColor = System.Drawing.SystemColors.Window;
            this.Hour.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Hour.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.Hour.TextBoxName = "Flags_numeric";
            this.Hour.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(0, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 19);
            this.label4.TabIndex = 33;
            this.label4.Text = "Hour:";
            // 
            // Minute
            // 
            this.Minute.BackColor = System.Drawing.Color.White;
            this.Minute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Minute.Increment = 1;
            this.Minute.Location = new System.Drawing.Point(59, 115);
            this.Minute.MaximalValue = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Minute.MinimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Minute.Name = "Minute";
            this.Minute.SetBlack = false;
            this.Minute.SetDecimalPlaces = 0;
            this.Minute.SetThousandsSeparator = false;
            this.Minute.Size = new System.Drawing.Size(91, 21);
            this.Minute.TabIndex = 34;
            this.Minute.TextBoxBackGroundColor = System.Drawing.SystemColors.Window;
            this.Minute.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Minute.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.Minute.TextBoxName = "Flags_numeric";
            this.Minute.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(0, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 19);
            this.label5.TabIndex = 33;
            this.label5.Text = "Minute:";
            // 
            // Second
            // 
            this.Second.BackColor = System.Drawing.Color.White;
            this.Second.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Second.Increment = 1;
            this.Second.Location = new System.Drawing.Point(59, 138);
            this.Second.MaximalValue = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Second.MinimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Second.Name = "Second";
            this.Second.SetBlack = false;
            this.Second.SetDecimalPlaces = 0;
            this.Second.SetThousandsSeparator = false;
            this.Second.Size = new System.Drawing.Size(91, 21);
            this.Second.TabIndex = 34;
            this.Second.TextBoxBackGroundColor = System.Drawing.SystemColors.Window;
            this.Second.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Second.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.Second.TextBoxName = "Flags_numeric";
            this.Second.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(0, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 19);
            this.label6.TabIndex = 33;
            this.label6.Text = "Second:";
            // 
            // Accept
            // 
            this.Accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Accept.Image = global::GShopEditorByLuka.Properties.Resources.accept;
            this.Accept.Location = new System.Drawing.Point(0, 162);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(154, 39);
            this.Accept.TabIndex = 35;
            this.Accept.Text = "Confirm";
            this.Accept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // DateTimeNow
            // 
            this.DateTimeNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DateTimeNow.Location = new System.Drawing.Point(0, 1);
            this.DateTimeNow.Name = "DateTimeNow";
            this.DateTimeNow.Size = new System.Drawing.Size(154, 22);
            this.DateTimeNow.TabIndex = 36;
            this.DateTimeNow.Text = "Current Date Time";
            this.DateTimeNow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DateTimeNow.UseVisualStyleBackColor = true;
            this.DateTimeNow.Click += new System.EventHandler(this.DateTimeNow_Click);
            // 
            // TimestampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 202);
            this.Controls.Add(this.DateTimeNow);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.Second);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Minute);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Hour);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Day);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Month);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Year);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimestampForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timestamp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private LBLIBRARY.Components.NumericUpDownEx Year;
        private LBLIBRARY.Components.NumericUpDownEx Month;
        private System.Windows.Forms.Label label2;
        private LBLIBRARY.Components.NumericUpDownEx Day;
        private System.Windows.Forms.Label label3;
        private LBLIBRARY.Components.NumericUpDownEx Hour;
        private System.Windows.Forms.Label label4;
        private LBLIBRARY.Components.NumericUpDownEx Minute;
        private System.Windows.Forms.Label label5;
        private LBLIBRARY.Components.NumericUpDownEx Second;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button DateTimeNow;
    }
}