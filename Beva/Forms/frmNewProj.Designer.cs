﻿using System.Windows.Forms;
using System.ComponentModel;
using Beva.FormData;

namespace Beva.Forms
{
    partial class FrmNewProj
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.panel1 = new Panel();
            this.chbRoofType = new CheckBox();
            this.txtHeight = new TextBox();
            this.label7 = new Label();
            this.txtWidth = new TextBox();
            this.label8 = new Label();
            this.txtLength = new TextBox();
            this.label9 = new Label();
            this.label10 = new Label();
            this.txtZ = new TextBox();
            this.label6 = new Label();
            this.txtY = new TextBox();
            this.label5 = new Label();
            this.txtX = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.cbRoofType = new ComboBox();
            this.label2 = new Label();
            this.cbWallType = new ComboBox();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.btnCancel = new Button();
            this.btnOk = new Button();
            this.label11 = new Label();
            this.chbSlab = new CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chbSlab);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.chbRoofType);
            this.panel1.Controls.Add(this.txtHeight);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtWidth);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtLength);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtZ);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtY);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtX);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbRoofType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbWallType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 210);
            this.panel1.TabIndex = 19;
            // 
            // chbRoofType
            // 
            this.chbRoofType.AutoSize = true;
            this.chbRoofType.Checked = true;
            this.chbRoofType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRoofType.Location = new System.Drawing.Point(149, 70);
            this.chbRoofType.Name = "chbRoofType";
            this.chbRoofType.Size = new System.Drawing.Size(15, 14);
            this.chbRoofType.TabIndex = 37;
            this.chbRoofType.UseVisualStyleBackColor = true;
            this.chbRoofType.CheckedChanged += new System.EventHandler(this.chbRoofType_CheckedChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(512, 165);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 20);
            this.txtHeight.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(460, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "HEIGHT";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(356, 165);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(100, 20);
            this.txtWidth.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "WIDTH";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(204, 165);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 20);
            this.txtLength.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(149, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "LENGTH";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "BUILDING DIMENSIONS";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(421, 117);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(100, 20);
            this.txtZ.TabIndex = 29;
            this.txtZ.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(401, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Z";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(295, 117);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 20);
            this.txtY.TabIndex = 27;
            this.txtY.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Y";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(169, 116);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 20);
            this.txtX.TabIndex = 25;
            this.txtX.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "INSERTION POINT";
            // 
            // cbRoofType
            // 
            this.cbRoofType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoofType.FormattingEnabled = true;
            this.cbRoofType.Location = new System.Drawing.Point(170, 67);
            this.cbRoofType.Name = "cbRoofType";
            this.cbRoofType.Size = new System.Drawing.Size(167, 21);
            this.cbRoofType.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "ROOF TYPE";
            // 
            // cbWallType
            // 
            this.cbWallType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWallType.FormattingEnabled = true;
            this.cbWallType.Location = new System.Drawing.Point(149, 18);
            this.cbWallType.Name = "cbWallType";
            this.cbWallType.Size = new System.Drawing.Size(188, 21);
            this.cbWallType.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "CONSTRUCTION TYPE";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 62);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(537, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(436, 23);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(467, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "SLAB";
            // 
            // chbSlab
            // 
            this.chbSlab.AutoSize = true;
            this.chbSlab.Checked = true;
            this.chbSlab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSlab.Location = new System.Drawing.Point(507, 70);
            this.chbSlab.Name = "chbSlab";
            this.chbSlab.Size = new System.Drawing.Size(15, 14);
            this.chbSlab.TabIndex = 39;
            this.chbSlab.UseVisualStyleBackColor = true;
            // 
            // frmNewProj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 265);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewProj";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BEVA - MODEL CREATION FORM";
            this.Load += new System.EventHandler(this.frmNewProj_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private CheckBox chbRoofType;
        private TextBox txtHeight;
        private Label label7;
        private TextBox txtWidth;
        private Label label8;
        private TextBox txtLength;
        private Label label9;
        private Label label10;
        private TextBox txtZ;
        private Label label6;
        private TextBox txtY;
        private Label label5;
        private TextBox txtX;
        private Label label4;
        private Label label3;
        private ComboBox cbRoofType;
        private Label label2;
        private ComboBox cbWallType;
        private Label label1;
        private GroupBox groupBox1;
        private Button btnCancel;
        private Button btnOk;
        private CheckBox chbSlab;
        private Label label11;
    }
}