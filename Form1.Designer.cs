namespace SupervisorDashboard
{
    partial class Form1
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
            this.addFloorBtn = new System.Windows.Forms.Button();
            this.flowLayoutRadio = new System.Windows.Forms.FlowLayoutPanel();
            this.toolsGroupBox = new System.Windows.Forms.GroupBox();
            this.superposeCheckBox = new System.Windows.Forms.CheckBox();
            this.placeSensorRadio = new System.Windows.Forms.RadioButton();
            this.drawPolyRadio = new System.Windows.Forms.RadioButton();
            this.sensorRadioGroup = new System.Windows.Forms.GroupBox();
            this.genericSensorRadio = new System.Windows.Forms.RadioButton();
            this.gasSensorRadio = new System.Windows.Forms.RadioButton();
            this.pressureSensorRadio = new System.Windows.Forms.RadioButton();
            this.tempSensorRadio = new System.Windows.Forms.RadioButton();
            this.sensorConfGroup = new System.Windows.Forms.GroupBox();
            this.providerNameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sensorPictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.upperNumeric = new System.Windows.Forms.NumericUpDown();
            this.lowerNumeric = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sensorNameBox = new System.Windows.Forms.TextBox();
            this.gridBox = new System.Windows.Forms.PictureBox();
            this.toolsGroupBox.SuspendLayout();
            this.sensorRadioGroup.SuspendLayout();
            this.sensorConfGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addFloorBtn
            // 
            this.addFloorBtn.Location = new System.Drawing.Point(304, 237);
            this.addFloorBtn.Name = "addFloorBtn";
            this.addFloorBtn.Size = new System.Drawing.Size(155, 23);
            this.addFloorBtn.TabIndex = 2;
            this.addFloorBtn.Text = "Add floor";
            this.addFloorBtn.UseVisualStyleBackColor = true;
            this.addFloorBtn.Click += new System.EventHandler(this.addFloorBtn_Click);
            // 
            // flowLayoutRadio
            // 
            this.flowLayoutRadio.AutoScroll = true;
            this.flowLayoutRadio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutRadio.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutRadio.Location = new System.Drawing.Point(304, 124);
            this.flowLayoutRadio.Name = "flowLayoutRadio";
            this.flowLayoutRadio.Size = new System.Drawing.Size(155, 107);
            this.flowLayoutRadio.TabIndex = 0;
            this.flowLayoutRadio.WrapContents = false;
            // 
            // toolsGroupBox
            // 
            this.toolsGroupBox.Controls.Add(this.superposeCheckBox);
            this.toolsGroupBox.Controls.Add(this.placeSensorRadio);
            this.toolsGroupBox.Controls.Add(this.drawPolyRadio);
            this.toolsGroupBox.Location = new System.Drawing.Point(304, 12);
            this.toolsGroupBox.Name = "toolsGroupBox";
            this.toolsGroupBox.Size = new System.Drawing.Size(155, 100);
            this.toolsGroupBox.TabIndex = 3;
            this.toolsGroupBox.TabStop = false;
            this.toolsGroupBox.Text = "Toolbox";
            // 
            // superposeCheckBox
            // 
            this.superposeCheckBox.AutoSize = true;
            this.superposeCheckBox.Location = new System.Drawing.Point(6, 65);
            this.superposeCheckBox.Name = "superposeCheckBox";
            this.superposeCheckBox.Size = new System.Drawing.Size(105, 17);
            this.superposeCheckBox.TabIndex = 2;
            this.superposeCheckBox.Text = "Superpose floors";
            this.superposeCheckBox.UseVisualStyleBackColor = true;
            this.superposeCheckBox.CheckedChanged += new System.EventHandler(this.superposeCheckBox_CheckedChanged);
            // 
            // placeSensorRadio
            // 
            this.placeSensorRadio.AutoSize = true;
            this.placeSensorRadio.Location = new System.Drawing.Point(6, 42);
            this.placeSensorRadio.Name = "placeSensorRadio";
            this.placeSensorRadio.Size = new System.Drawing.Size(86, 17);
            this.placeSensorRadio.TabIndex = 1;
            this.placeSensorRadio.Text = "Place sensor";
            this.placeSensorRadio.UseVisualStyleBackColor = true;
            this.placeSensorRadio.CheckedChanged += new System.EventHandler(this.placeSensorRadio_CheckedChanged);
            // 
            // drawPolyRadio
            // 
            this.drawPolyRadio.AutoSize = true;
            this.drawPolyRadio.Checked = true;
            this.drawPolyRadio.Location = new System.Drawing.Point(6, 19);
            this.drawPolyRadio.Name = "drawPolyRadio";
            this.drawPolyRadio.Size = new System.Drawing.Size(72, 17);
            this.drawPolyRadio.TabIndex = 0;
            this.drawPolyRadio.TabStop = true;
            this.drawPolyRadio.Text = "Draw poly";
            this.drawPolyRadio.UseVisualStyleBackColor = true;
            // 
            // sensorRadioGroup
            // 
            this.sensorRadioGroup.Controls.Add(this.genericSensorRadio);
            this.sensorRadioGroup.Controls.Add(this.gasSensorRadio);
            this.sensorRadioGroup.Controls.Add(this.pressureSensorRadio);
            this.sensorRadioGroup.Controls.Add(this.tempSensorRadio);
            this.sensorRadioGroup.Enabled = false;
            this.sensorRadioGroup.Location = new System.Drawing.Point(12, 266);
            this.sensorRadioGroup.Name = "sensorRadioGroup";
            this.sensorRadioGroup.Size = new System.Drawing.Size(118, 126);
            this.sensorRadioGroup.TabIndex = 3;
            this.sensorRadioGroup.TabStop = false;
            this.sensorRadioGroup.Text = "Sensors";
            // 
            // genericSensorRadio
            // 
            this.genericSensorRadio.AutoSize = true;
            this.genericSensorRadio.Checked = true;
            this.genericSensorRadio.Location = new System.Drawing.Point(6, 95);
            this.genericSensorRadio.Name = "genericSensorRadio";
            this.genericSensorRadio.Size = new System.Drawing.Size(62, 17);
            this.genericSensorRadio.TabIndex = 7;
            this.genericSensorRadio.TabStop = true;
            this.genericSensorRadio.Text = "Generic";
            this.genericSensorRadio.UseVisualStyleBackColor = true;
            this.genericSensorRadio.CheckedChanged += new System.EventHandler(this.genericSensorRadio_CheckedChanged);
            // 
            // gasSensorRadio
            // 
            this.gasSensorRadio.AutoSize = true;
            this.gasSensorRadio.Location = new System.Drawing.Point(6, 70);
            this.gasSensorRadio.Name = "gasSensorRadio";
            this.gasSensorRadio.Size = new System.Drawing.Size(95, 17);
            this.gasSensorRadio.TabIndex = 6;
            this.gasSensorRadio.Text = "Methane Gase";
            this.gasSensorRadio.UseVisualStyleBackColor = true;
            this.gasSensorRadio.CheckedChanged += new System.EventHandler(this.methaneSensorRadio_CheckedChanged);
            // 
            // pressureSensorRadio
            // 
            this.pressureSensorRadio.AutoSize = true;
            this.pressureSensorRadio.Location = new System.Drawing.Point(6, 45);
            this.pressureSensorRadio.Name = "pressureSensorRadio";
            this.pressureSensorRadio.Size = new System.Drawing.Size(66, 17);
            this.pressureSensorRadio.TabIndex = 5;
            this.pressureSensorRadio.Text = "Pressure";
            this.pressureSensorRadio.UseVisualStyleBackColor = true;
            this.pressureSensorRadio.CheckedChanged += new System.EventHandler(this.pressureSensorRadio_CheckedChanged);
            // 
            // tempSensorRadio
            // 
            this.tempSensorRadio.AutoSize = true;
            this.tempSensorRadio.Location = new System.Drawing.Point(6, 21);
            this.tempSensorRadio.Name = "tempSensorRadio";
            this.tempSensorRadio.Size = new System.Drawing.Size(85, 17);
            this.tempSensorRadio.TabIndex = 4;
            this.tempSensorRadio.Tag = "";
            this.tempSensorRadio.Text = "Temperature";
            this.tempSensorRadio.UseVisualStyleBackColor = true;
            this.tempSensorRadio.CheckedChanged += new System.EventHandler(this.tempSensorRadio_CheckedChanged);
            // 
            // sensorConfGroup
            // 
            this.sensorConfGroup.Controls.Add(this.providerNameBox);
            this.sensorConfGroup.Controls.Add(this.label5);
            this.sensorConfGroup.Controls.Add(this.sensorPictureBox);
            this.sensorConfGroup.Controls.Add(this.label4);
            this.sensorConfGroup.Controls.Add(this.upperNumeric);
            this.sensorConfGroup.Controls.Add(this.lowerNumeric);
            this.sensorConfGroup.Controls.Add(this.textBox2);
            this.sensorConfGroup.Controls.Add(this.label3);
            this.sensorConfGroup.Controls.Add(this.label2);
            this.sensorConfGroup.Controls.Add(this.label1);
            this.sensorConfGroup.Controls.Add(this.sensorNameBox);
            this.sensorConfGroup.Enabled = false;
            this.sensorConfGroup.Location = new System.Drawing.Point(136, 266);
            this.sensorConfGroup.Name = "sensorConfGroup";
            this.sensorConfGroup.Size = new System.Drawing.Size(323, 126);
            this.sensorConfGroup.TabIndex = 4;
            this.sensorConfGroup.TabStop = false;
            this.sensorConfGroup.Text = "Sensor Configuration";
            // 
            // providerNameBox
            // 
            this.providerNameBox.Location = new System.Drawing.Point(85, 69);
            this.providerNameBox.Name = "providerNameBox";
            this.providerNameBox.Size = new System.Drawing.Size(133, 20);
            this.providerNameBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Provider";
            // 
            // sensorPictureBox
            // 
            this.sensorPictureBox.Image = global::SupervisorDashboard.Properties.Resources.generic_sensor;
            this.sensorPictureBox.Location = new System.Drawing.Point(224, 20);
            this.sensorPictureBox.Name = "sensorPictureBox";
            this.sensorPictureBox.Size = new System.Drawing.Size(93, 95);
            this.sensorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sensorPictureBox.TabIndex = 5;
            this.sensorPictureBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "and";
            // 
            // upperNumeric
            // 
            this.upperNumeric.Location = new System.Drawing.Point(168, 95);
            this.upperNumeric.Name = "upperNumeric";
            this.upperNumeric.Size = new System.Drawing.Size(50, 20);
            this.upperNumeric.TabIndex = 6;
            // 
            // lowerNumeric
            // 
            this.lowerNumeric.Location = new System.Drawing.Point(85, 95);
            this.lowerNumeric.Name = "lowerNumeric";
            this.lowerNumeric.Size = new System.Drawing.Size(46, 20);
            this.lowerNumeric.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(85, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(133, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Safe between";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "GUID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // sensorNameBox
            // 
            this.sensorNameBox.Location = new System.Drawing.Point(85, 20);
            this.sensorNameBox.Name = "sensorNameBox";
            this.sensorNameBox.Size = new System.Drawing.Size(133, 20);
            this.sensorNameBox.TabIndex = 0;
            // 
            // gridBox
            // 
            this.gridBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridBox.Location = new System.Drawing.Point(12, 12);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(286, 248);
            this.gridBox.TabIndex = 0;
            this.gridBox.TabStop = false;
            this.gridBox.Paint += new System.Windows.Forms.PaintEventHandler(this.gridBox_Paint);
            this.gridBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridBox_MouseDown);
            this.gridBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridBox_MouseMove_NotDrawing);
            this.gridBox.Resize += new System.EventHandler(this.gridBox_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 404);
            this.Controls.Add(this.sensorConfGroup);
            this.Controls.Add(this.sensorRadioGroup);
            this.Controls.Add(this.toolsGroupBox);
            this.Controls.Add(this.flowLayoutRadio);
            this.Controls.Add(this.addFloorBtn);
            this.Controls.Add(this.gridBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolsGroupBox.ResumeLayout(false);
            this.toolsGroupBox.PerformLayout();
            this.sensorRadioGroup.ResumeLayout(false);
            this.sensorRadioGroup.PerformLayout();
            this.sensorConfGroup.ResumeLayout(false);
            this.sensorConfGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gridBox;
        private System.Windows.Forms.Button addFloorBtn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutRadio;
        private System.Windows.Forms.GroupBox toolsGroupBox;
        private System.Windows.Forms.RadioButton placeSensorRadio;
        private System.Windows.Forms.RadioButton drawPolyRadio;
        private System.Windows.Forms.CheckBox superposeCheckBox;
        private System.Windows.Forms.GroupBox sensorRadioGroup;
        private System.Windows.Forms.RadioButton gasSensorRadio;
        private System.Windows.Forms.RadioButton pressureSensorRadio;
        private System.Windows.Forms.RadioButton tempSensorRadio;
        private System.Windows.Forms.GroupBox sensorConfGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown upperNumeric;
        private System.Windows.Forms.NumericUpDown lowerNumeric;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sensorNameBox;
        private System.Windows.Forms.PictureBox sensorPictureBox;
        private System.Windows.Forms.RadioButton genericSensorRadio;
        private System.Windows.Forms.TextBox providerNameBox;
        private System.Windows.Forms.Label label5;
    }
}

