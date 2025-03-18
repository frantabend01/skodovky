namespace Skodovky
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            comboBox1 = new ComboBox();
            dataGridView2 = new DataGridView();
            errorLabel = new Label();
            uploadXML = new Button();
            darkMode = new CheckBox();
            exportCSV = new Button();
            Reset = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(168, 128);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(683, 150);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellFormatting += dataGridView_CellFormatting;
            // 
            // comboBox1
            // 
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(14, 352);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(144, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(168, 324);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(683, 150);
            dataGridView2.TabIndex = 4;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            dataGridView2.CellFormatting += dataGridView_CellFormatting;
            // 
            // errorLabel
            // 
            errorLabel.AutoSize = true;
            errorLabel.Location = new Point(360, 509);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(38, 15);
            errorLabel.TabIndex = 5;
            errorLabel.Text = "label1";
            errorLabel.Click += label1_Click;
            // 
            // uploadXML
            // 
            uploadXML.Location = new Point(23, 22);
            uploadXML.Name = "uploadXML";
            uploadXML.Size = new Size(126, 33);
            uploadXML.TabIndex = 6;
            uploadXML.Text = "Nahrát XML soubor";
            uploadXML.UseVisualStyleBackColor = true;
            uploadXML.Click += selectXMLFile;
            // 
            // darkMode
            // 
            darkMode.AutoSize = true;
            darkMode.Location = new Point(759, 22);
            darkMode.Name = "darkMode";
            darkMode.Size = new Size(84, 19);
            darkMode.TabIndex = 7;
            darkMode.Text = "Dark Mode";
            darkMode.UseVisualStyleBackColor = true;
            darkMode.CheckedChanged += darkMode_CheckedChanged;
            // 
            // exportCSV
            // 
            exportCSV.Location = new Point(310, 22);
            exportCSV.Name = "exportCSV";
            exportCSV.Size = new Size(126, 33);
            exportCSV.TabIndex = 8;
            exportCSV.Text = "Export CSV";
            exportCSV.UseVisualStyleBackColor = true;
            exportCSV.Click += this.exportToCSV;
            // 
            // Reset
            // 
            Reset.Location = new Point(168, 22);
            Reset.Name = "Reset";
            Reset.Size = new Size(122, 33);
            Reset.TabIndex = 9;
            Reset.Text = "Reset";
            Reset.UseVisualStyleBackColor = true;
            Reset.Click += Reset_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(877, 549);
            Controls.Add(Reset);
            Controls.Add(exportCSV);
            Controls.Add(darkMode);
            Controls.Add(uploadXML);
            Controls.Add(errorLabel);
            Controls.Add(dataGridView2);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private DataGridView dataGridView2;
        private Label errorLabel;
        private Button uploadXML;
        private CheckBox darkMode;
        private Button exportCSV;
        private Button Reset;
    }
}
