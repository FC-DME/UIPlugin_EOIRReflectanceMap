namespace FC.UIPlugin.EOIRReflectanceMap
{
    partial class UserInterface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Run = new System.Windows.Forms.Button();
            this.groupBox_Setup = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton_HideLines = new System.Windows.Forms.RadioButton();
            this.radioButton_ShowLines = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_HideAssets = new System.Windows.Forms.RadioButton();
            this.radioButton_ShowAssets = new System.Windows.Forms.RadioButton();
            this.groupBox_ImageSetup = new System.Windows.Forms.GroupBox();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ImageName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog_Image = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBox_AddToConfig = new System.Windows.Forms.CheckBox();
            this.button_Help = new System.Windows.Forms.Button();
            this.groupBox_Setup.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox_ImageSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Run
            // 
            this.button_Run.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_Run.Location = new System.Drawing.Point(113, 250);
            this.button_Run.Name = "button_Run";
            this.button_Run.Size = new System.Drawing.Size(222, 37);
            this.button_Run.TabIndex = 0;
            this.button_Run.Text = "Create reflectance map";
            this.button_Run.UseVisualStyleBackColor = true;
            this.button_Run.Click += new System.EventHandler(this.button_Run_Click);
            // 
            // groupBox_Setup
            // 
            this.groupBox_Setup.Controls.Add(this.panel2);
            this.groupBox_Setup.Controls.Add(this.panel1);
            this.groupBox_Setup.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Setup.Name = "groupBox_Setup";
            this.groupBox_Setup.Size = new System.Drawing.Size(454, 117);
            this.groupBox_Setup.TabIndex = 1;
            this.groupBox_Setup.TabStop = false;
            this.groupBox_Setup.Text = "2D Window Setup";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton_HideLines);
            this.panel2.Controls.Add(this.radioButton_ShowLines);
            this.panel2.Location = new System.Drawing.Point(6, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(442, 31);
            this.panel2.TabIndex = 2;
            // 
            // radioButton_HideLines
            // 
            this.radioButton_HideLines.AutoSize = true;
            this.radioButton_HideLines.Location = new System.Drawing.Point(167, 3);
            this.radioButton_HideLines.Name = "radioButton_HideLines";
            this.radioButton_HideLines.Size = new System.Drawing.Size(127, 20);
            this.radioButton_HideLines.TabIndex = 1;
            this.radioButton_HideLines.Text = "Hide lat/lon lines";
            this.radioButton_HideLines.UseVisualStyleBackColor = true;
            // 
            // radioButton_ShowLines
            // 
            this.radioButton_ShowLines.AutoSize = true;
            this.radioButton_ShowLines.Checked = true;
            this.radioButton_ShowLines.Location = new System.Drawing.Point(3, 3);
            this.radioButton_ShowLines.Name = "radioButton_ShowLines";
            this.radioButton_ShowLines.Size = new System.Drawing.Size(131, 20);
            this.radioButton_ShowLines.TabIndex = 0;
            this.radioButton_ShowLines.TabStop = true;
            this.radioButton_ShowLines.Text = "Show lat/lon lines";
            this.radioButton_ShowLines.UseVisualStyleBackColor = true;
            this.radioButton_ShowLines.CheckedChanged += new System.EventHandler(this.radioButton_ShowLines_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_HideAssets);
            this.panel1.Controls.Add(this.radioButton_ShowAssets);
            this.panel1.Location = new System.Drawing.Point(6, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 31);
            this.panel1.TabIndex = 1;
            // 
            // radioButton_HideAssets
            // 
            this.radioButton_HideAssets.AutoSize = true;
            this.radioButton_HideAssets.Location = new System.Drawing.Point(167, 3);
            this.radioButton_HideAssets.Name = "radioButton_HideAssets";
            this.radioButton_HideAssets.Size = new System.Drawing.Size(121, 20);
            this.radioButton_HideAssets.TabIndex = 1;
            this.radioButton_HideAssets.Text = "Hide all objects";
            this.radioButton_HideAssets.UseVisualStyleBackColor = true;
            // 
            // radioButton_ShowAssets
            // 
            this.radioButton_ShowAssets.AutoSize = true;
            this.radioButton_ShowAssets.Checked = true;
            this.radioButton_ShowAssets.Location = new System.Drawing.Point(3, 3);
            this.radioButton_ShowAssets.Name = "radioButton_ShowAssets";
            this.radioButton_ShowAssets.Size = new System.Drawing.Size(125, 20);
            this.radioButton_ShowAssets.TabIndex = 0;
            this.radioButton_ShowAssets.TabStop = true;
            this.radioButton_ShowAssets.Text = "Show all objects";
            this.radioButton_ShowAssets.UseVisualStyleBackColor = true;
            this.radioButton_ShowAssets.CheckedChanged += new System.EventHandler(this.radioButton_ShowAssets_CheckedChanged);
            // 
            // groupBox_ImageSetup
            // 
            this.groupBox_ImageSetup.Controls.Add(this.button_Browse);
            this.groupBox_ImageSetup.Controls.Add(this.textBox_Path);
            this.groupBox_ImageSetup.Controls.Add(this.label2);
            this.groupBox_ImageSetup.Controls.Add(this.textBox_ImageName);
            this.groupBox_ImageSetup.Controls.Add(this.label1);
            this.groupBox_ImageSetup.Location = new System.Drawing.Point(3, 126);
            this.groupBox_ImageSetup.Name = "groupBox_ImageSetup";
            this.groupBox_ImageSetup.Size = new System.Drawing.Size(454, 92);
            this.groupBox_ImageSetup.TabIndex = 2;
            this.groupBox_ImageSetup.TabStop = false;
            this.groupBox_ImageSetup.Text = "Image Setup";
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(359, 54);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(87, 24);
            this.button_Browse.TabIndex = 4;
            this.button_Browse.Text = "Browse...";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_Path
            // 
            this.textBox_Path.Location = new System.Drawing.Point(94, 54);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.ReadOnly = true;
            this.textBox_Path.Size = new System.Drawing.Size(259, 22);
            this.textBox_Path.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Image path:";
            // 
            // textBox_ImageName
            // 
            this.textBox_ImageName.Location = new System.Drawing.Point(94, 26);
            this.textBox_ImageName.Name = "textBox_ImageName";
            this.textBox_ImageName.Size = new System.Drawing.Size(259, 22);
            this.textBox_ImageName.TabIndex = 1;
            this.textBox_ImageName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ImageName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image name:";
            // 
            // checkBox_AddToConfig
            // 
            this.checkBox_AddToConfig.AutoSize = true;
            this.checkBox_AddToConfig.Checked = true;
            this.checkBox_AddToConfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AddToConfig.Location = new System.Drawing.Point(14, 224);
            this.checkBox_AddToConfig.Name = "checkBox_AddToConfig";
            this.checkBox_AddToConfig.Size = new System.Drawing.Size(427, 20);
            this.checkBox_AddToConfig.TabIndex = 3;
            this.checkBox_AddToConfig.Text = "Add reflectance map to the EOIR configuration for this STK scenario";
            this.checkBox_AddToConfig.UseVisualStyleBackColor = true;
            // 
            // button_Help
            // 
            this.button_Help.Location = new System.Drawing.Point(394, 261);
            this.button_Help.Margin = new System.Windows.Forms.Padding(0);
            this.button_Help.Name = "button_Help";
            this.button_Help.Size = new System.Drawing.Size(63, 25);
            this.button_Help.TabIndex = 4;
            this.button_Help.Text = "Help";
            this.button_Help.UseVisualStyleBackColor = true;
            this.button_Help.Click += new System.EventHandler(this.button_Help_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_Help);
            this.Controls.Add(this.checkBox_AddToConfig);
            this.Controls.Add(this.groupBox_ImageSetup);
            this.Controls.Add(this.groupBox_Setup);
            this.Controls.Add(this.button_Run);
            this.Name = "UserInterface";
            this.Size = new System.Drawing.Size(465, 298);
            this.groupBox_Setup.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox_ImageSetup.ResumeLayout(false);
            this.groupBox_ImageSetup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Run;
        private System.Windows.Forms.GroupBox groupBox_Setup;
        private System.Windows.Forms.RadioButton radioButton_ShowAssets;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton_HideAssets;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_HideLines;
        private System.Windows.Forms.RadioButton radioButton_ShowLines;
        private System.Windows.Forms.GroupBox groupBox_ImageSetup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ImageName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_Image;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.CheckBox checkBox_AddToConfig;
        private System.Windows.Forms.Button button_Help;
    }
}
