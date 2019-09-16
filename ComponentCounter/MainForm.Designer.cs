namespace ComponentCounter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.btnGetData = new System.Windows.Forms.Button();
            this.dgvDataFromDB = new System.Windows.Forms.DataGridView();
            this.cbxLine = new System.Windows.Forms.ComboBox();
            this.lblLinia = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataFromDB)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(69, 24);
            this.dtpDateFrom.MinDate = new System.DateTime(2019, 9, 12, 0, 0, 0, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(115, 20);
            this.dtpDateFrom.TabIndex = 0;
            // 
            // dtpTimeFrom
            // 
            this.dtpTimeFrom.CustomFormat = "HH:mm";
            this.dtpTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeFrom.Location = new System.Drawing.Point(190, 24);
            this.dtpTimeFrom.Name = "dtpTimeFrom";
            this.dtpTimeFrom.ShowUpDown = true;
            this.dtpTimeFrom.Size = new System.Drawing.Size(53, 20);
            this.dtpTimeFrom.TabIndex = 1;
            // 
            // dtpTimeTo
            // 
            this.dtpTimeTo.CustomFormat = "HH:mm";
            this.dtpTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeTo.Location = new System.Drawing.Point(190, 50);
            this.dtpTimeTo.Name = "dtpTimeTo";
            this.dtpTimeTo.ShowUpDown = true;
            this.dtpTimeTo.Size = new System.Drawing.Size(53, 20);
            this.dtpTimeTo.TabIndex = 3;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(69, 50);
            this.dtpDateTo.MinDate = new System.DateTime(2019, 9, 12, 0, 0, 0, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(115, 20);
            this.dtpDateTo.TabIndex = 2;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(261, 24);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 46);
            this.btnGetData.TabIndex = 4;
            this.btnGetData.Text = "Pobierz dane";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.BtnGetData_Click);
            // 
            // dgvDataFromDB
            // 
            this.dgvDataFromDB.AllowUserToAddRows = false;
            this.dgvDataFromDB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDataFromDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataFromDB.Location = new System.Drawing.Point(30, 127);
            this.dgvDataFromDB.Name = "dgvDataFromDB";
            this.dgvDataFromDB.ReadOnly = true;
            this.dgvDataFromDB.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDataFromDB.Size = new System.Drawing.Size(306, 199);
            this.dgvDataFromDB.TabIndex = 5;
            // 
            // cbxLine
            // 
            this.cbxLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLine.FormattingEnabled = true;
            this.cbxLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxLine.Location = new System.Drawing.Point(122, 87);
            this.cbxLine.Name = "cbxLine";
            this.cbxLine.Size = new System.Drawing.Size(121, 21);
            this.cbxLine.TabIndex = 6;
            // 
            // lblLinia
            // 
            this.lblLinia.AutoSize = true;
            this.lblLinia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLinia.Location = new System.Drawing.Point(39, 87);
            this.lblLinia.Name = "lblLinia";
            this.lblLinia.Size = new System.Drawing.Size(52, 20);
            this.lblLinia.TabIndex = 7;
            this.lblLinia.Text = "Linia:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 355);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLinia);
            this.Controls.Add(this.cbxLine);
            this.Controls.Add(this.dgvDataFromDB);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.dtpTimeTo);
            this.Controls.Add(this.dtpDateTo);
            this.Controls.Add(this.dtpTimeFrom);
            this.Controls.Add(this.dtpDateFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "O-Ring Counter";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataFromDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpTimeFrom;
        private System.Windows.Forms.DateTimePicker dtpTimeTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.DataGridView dgvDataFromDB;
        private System.Windows.Forms.ComboBox cbxLine;
        private System.Windows.Forms.Label lblLinia;
        private System.Windows.Forms.Label label1;
    }
}

