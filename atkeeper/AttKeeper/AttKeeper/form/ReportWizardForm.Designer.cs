namespace AttKeeper.form
{
    partial class ReportWizardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportWizardForm));
            this.jEnrolleeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.departmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jEnrolleeBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labelSetDuration = new System.Windows.Forms.Label();
            this.groupBoxEnrollees = new System.Windows.Forms.GroupBox();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.buttonRemoveOne = new System.Windows.Forms.Button();
            this.buttonGetOne = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.buttonGetAll = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxDepartment = new System.Windows.Forms.ComboBox();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxDuration = new System.Windows.Forms.GroupBox();
            this.labelDuration = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.comboBoxEndDay = new System.Windows.Forms.ComboBox();
            this.comboBoxStartDay = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.jEnrolleeBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jEnrolleeBindingSource1)).BeginInit();
            this.groupBoxEnrollees.SuspendLayout();
            this.groupBoxDuration.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 462);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "Ready...";
            // 
            // buttonFinish
            // 
            this.buttonFinish.Location = new System.Drawing.Point(196, 433);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(75, 23);
            this.buttonFinish.TabIndex = 32;
            this.buttonFinish.Text = "&Finish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(88, 433);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 31;
            this.buttonNext.Text = "&Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(7, 433);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 30;
            this.buttonBack.Text = "&Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(551, 433);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 29;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // jEnrolleeBindingSource1
            // 
            this.jEnrolleeBindingSource1.DataSource = typeof(AttKeeper.core.EmployeeData);
            // 
            // labelSetDuration
            // 
            this.labelSetDuration.AutoSize = true;
            this.labelSetDuration.ForeColor = System.Drawing.Color.Red;
            this.labelSetDuration.Location = new System.Drawing.Point(7, 29);
            this.labelSetDuration.Name = "labelSetDuration";
            this.labelSetDuration.Size = new System.Drawing.Size(16, 13);
            this.labelSetDuration.TabIndex = 27;
            this.labelSetDuration.Text = "...";
            // 
            // groupBoxEnrollees
            // 
            this.groupBoxEnrollees.Controls.Add(this.labelSetDuration);
            this.groupBoxEnrollees.Controls.Add(this.buttonRemoveAll);
            this.groupBoxEnrollees.Controls.Add(this.buttonRemoveOne);
            this.groupBoxEnrollees.Controls.Add(this.buttonGetOne);
            this.groupBoxEnrollees.Controls.Add(this.listBox2);
            this.groupBoxEnrollees.Controls.Add(this.buttonGetAll);
            this.groupBoxEnrollees.Controls.Add(this.label8);
            this.groupBoxEnrollees.Controls.Add(this.listBox1);
            this.groupBoxEnrollees.Controls.Add(this.label7);
            this.groupBoxEnrollees.Controls.Add(this.comboBoxDepartment);
            this.groupBoxEnrollees.Location = new System.Drawing.Point(7, 52);
            this.groupBoxEnrollees.Name = "groupBoxEnrollees";
            this.groupBoxEnrollees.Size = new System.Drawing.Size(618, 375);
            this.groupBoxEnrollees.TabIndex = 34;
            this.groupBoxEnrollees.TabStop = false;
            this.groupBoxEnrollees.Text = "Select Enrollee(s):";
            this.groupBoxEnrollees.Visible = false;
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(271, 213);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(53, 23);
            this.buttonRemoveAll.TabIndex = 26;
            this.buttonRemoveAll.Text = "<<";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // buttonRemoveOne
            // 
            this.buttonRemoveOne.Location = new System.Drawing.Point(270, 184);
            this.buttonRemoveOne.Name = "buttonRemoveOne";
            this.buttonRemoveOne.Size = new System.Drawing.Size(53, 23);
            this.buttonRemoveOne.TabIndex = 25;
            this.buttonRemoveOne.Text = "<";
            this.buttonRemoveOne.UseVisualStyleBackColor = true;
            this.buttonRemoveOne.Click += new System.EventHandler(this.buttonRemoveOne_Click);
            // 
            // buttonGetOne
            // 
            this.buttonGetOne.Location = new System.Drawing.Point(270, 131);
            this.buttonGetOne.Name = "buttonGetOne";
            this.buttonGetOne.Size = new System.Drawing.Size(53, 23);
            this.buttonGetOne.TabIndex = 24;
            this.buttonGetOne.Text = ">";
            this.buttonGetOne.UseVisualStyleBackColor = true;
            this.buttonGetOne.Click += new System.EventHandler(this.buttonGetOne_Click);
            // 
            // listBox2
            // 
            this.listBox2.DataSource = this.jEnrolleeBindingSource1;
            this.listBox2.DisplayMember = "GetFullName";
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(5, 92);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(259, 277);
            this.listBox2.TabIndex = 23;
            this.listBox2.ValueMember = "EnrolleeNo";
            // 
            // buttonGetAll
            // 
            this.buttonGetAll.Location = new System.Drawing.Point(270, 102);
            this.buttonGetAll.Name = "buttonGetAll";
            this.buttonGetAll.Size = new System.Drawing.Size(53, 23);
            this.buttonGetAll.TabIndex = 22;
            this.buttonGetAll.Text = ">>";
            this.buttonGetAll.UseVisualStyleBackColor = true;
            this.buttonGetAll.Click += new System.EventHandler(this.buttonGetAll_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(327, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Selected Enrollee(s):";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(330, 92);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(282, 277);
            this.listBox1.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Dept.:";
            // 
            // comboBoxDepartment
            // 
            this.comboBoxDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDepartment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDepartment.DisplayMember = "DepartmentId";
            this.comboBoxDepartment.FormattingEnabled = true;
            this.comboBoxDepartment.Location = new System.Drawing.Point(48, 63);
            this.comboBoxDepartment.Name = "comboBoxDepartment";
            this.comboBoxDepartment.Size = new System.Drawing.Size(216, 21);
            this.comboBoxDepartment.TabIndex = 16;
            this.comboBoxDepartment.ValueMember = "DepartmentId";
            this.comboBoxDepartment.SelectedIndexChanged += new System.EventHandler(this.comboBoxDepartment_SelectedIndexChanged);
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(100, 27);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(135, 21);
            this.comboBoxMonth.TabIndex = 21;
            this.comboBoxMonth.SelectedIndexChanged += new System.EventHandler(this.comboBoxMonth_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 45);
            this.panel1.TabIndex = 27;
            // 
            // groupBoxDuration
            // 
            this.groupBoxDuration.Controls.Add(this.labelDuration);
            this.groupBoxDuration.Controls.Add(this.label6);
            this.groupBoxDuration.Controls.Add(this.label5);
            this.groupBoxDuration.Controls.Add(this.label3);
            this.groupBoxDuration.Controls.Add(this.label2);
            this.groupBoxDuration.Controls.Add(this.label1);
            this.groupBoxDuration.Controls.Add(this.comboBoxYear);
            this.groupBoxDuration.Controls.Add(this.comboBoxEndDay);
            this.groupBoxDuration.Controls.Add(this.comboBoxStartDay);
            this.groupBoxDuration.Controls.Add(this.comboBoxMonth);
            this.groupBoxDuration.Location = new System.Drawing.Point(8, 51);
            this.groupBoxDuration.Name = "groupBoxDuration";
            this.groupBoxDuration.Size = new System.Drawing.Size(618, 375);
            this.groupBoxDuration.TabIndex = 28;
            this.groupBoxDuration.TabStop = false;
            this.groupBoxDuration.Text = "Set Duration";
            // 
            // labelDuration
            // 
            this.labelDuration.AutoSize = true;
            this.labelDuration.ForeColor = System.Drawing.Color.Red;
            this.labelDuration.Location = new System.Drawing.Point(100, 153);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(16, 13);
            this.labelDuration.TabIndex = 31;
            this.labelDuration.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Duration:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Year:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "End (Day):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Start (Day):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Month:";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(100, 102);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(135, 21);
            this.comboBoxYear.TabIndex = 24;
            this.comboBoxYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxYear_SelectedIndexChanged);
            // 
            // comboBoxEndDay
            // 
            this.comboBoxEndDay.FormattingEnabled = true;
            this.comboBoxEndDay.Location = new System.Drawing.Point(100, 77);
            this.comboBoxEndDay.Name = "comboBoxEndDay";
            this.comboBoxEndDay.Size = new System.Drawing.Size(64, 21);
            this.comboBoxEndDay.TabIndex = 23;
            this.comboBoxEndDay.SelectedIndexChanged += new System.EventHandler(this.comboBoxEndDay_SelectedIndexChanged);
            // 
            // comboBoxStartDay
            // 
            this.comboBoxStartDay.FormattingEnabled = true;
            this.comboBoxStartDay.Location = new System.Drawing.Point(100, 51);
            this.comboBoxStartDay.Name = "comboBoxStartDay";
            this.comboBoxStartDay.Size = new System.Drawing.Size(64, 21);
            this.comboBoxStartDay.TabIndex = 22;
            this.comboBoxStartDay.SelectedIndexChanged += new System.EventHandler(this.comboBoxStartDay_SelectedIndexChanged);
            // 
            // ReportWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 484);
            this.Controls.Add(this.groupBoxEnrollees);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxDuration);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportWizardForm";
            this.Text = "Report Wizard";
            this.Load += new System.EventHandler(this.ReportWizardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jEnrolleeBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jEnrolleeBindingSource1)).EndInit();
            this.groupBoxEnrollees.ResumeLayout(false);
            this.groupBoxEnrollees.PerformLayout();
            this.groupBoxDuration.ResumeLayout(false);
            this.groupBoxDuration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource jEnrolleeBindingSource;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.BindingSource departmentBindingSource;
        private System.Windows.Forms.BindingSource jEnrolleeBindingSource1;
        private System.Windows.Forms.Label labelSetDuration;
        private System.Windows.Forms.GroupBox groupBoxEnrollees;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Button buttonRemoveOne;
        private System.Windows.Forms.Button buttonGetOne;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button buttonGetAll;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxDepartment;
        private System.Windows.Forms.ComboBox comboBoxMonth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxDuration;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.ComboBox comboBoxEndDay;
        private System.Windows.Forms.ComboBox comboBoxStartDay;
    }
}