/// <summary>
///   Original Author: Joe Zachary
///   Further Authors: H. James de St. Germain
///   
///   Dates          : 2012-ish - Original 
///                    2020     - Updated for use with ASP Core
///                    
///   This code represents a Windows Form element for a Spreadsheet
///   
///   This code is the "auto-generated" portion of the SimpleSpreadsheetGUI.
///   See the SimpleSpreadsheetGUI.cs for "hand-written" code.
///  
/// </summary>

using SpreadsheetGrid_Framework;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace CS3500_Spreadsheet_GUI_Example
{
    partial class SimpleSpreadsheetGUI
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainControlArea = new System.Windows.Forms.FlowLayoutPanel();
            this.NameLabel = new System.Windows.Forms.Label();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.ContentsLabel = new System.Windows.Forms.Label();
            this.contentsTextbox = new System.Windows.Forms.TextBox();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.valueTextbox = new System.Windows.Forms.TextBox();
            this.formulaLabel = new System.Windows.Forms.Label();
            this.formulaTextbox = new System.Windows.Forms.TextBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.bgProgressBar = new System.Windows.Forms.ProgressBar();
            this.getDependentsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grid_widget = new SpreadsheetGrid_Framework.SpreadsheetGridWidget();
            this.menuStrip.SuspendLayout();
            this.MainControlArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(811, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // MainControlArea
            // 
            this.MainControlArea.AutoSize = true;
            this.MainControlArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainControlArea.BackColor = System.Drawing.Color.Coral;
            this.MainControlArea.Controls.Add(this.NameLabel);
            this.MainControlArea.Controls.Add(this.nameTextbox);
            this.MainControlArea.Controls.Add(this.ContentsLabel);
            this.MainControlArea.Controls.Add(this.contentsTextbox);
            this.MainControlArea.Controls.Add(this.ValueLabel);
            this.MainControlArea.Controls.Add(this.valueTextbox);
            this.MainControlArea.Controls.Add(this.formulaLabel);
            this.MainControlArea.Controls.Add(this.formulaTextbox);
            this.MainControlArea.Controls.Add(this.ProgressLabel);
            this.MainControlArea.Controls.Add(this.bgProgressBar);
            this.MainControlArea.Controls.Add(this.getDependentsButton);
            this.MainControlArea.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainControlArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainControlArea.Location = new System.Drawing.Point(4, 4);
            this.MainControlArea.Margin = new System.Windows.Forms.Padding(4);
            this.MainControlArea.MinimumSize = new System.Drawing.Size(133, 123);
            this.MainControlArea.Name = "MainControlArea";
            this.MainControlArea.Size = new System.Drawing.Size(803, 123);
            this.MainControlArea.TabIndex = 4;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(3, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(49, 17);
            this.NameLabel.TabIndex = 3;
            this.NameLabel.Text = "Name:";
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(58, 3);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.ReadOnly = true;
            this.nameTextbox.Size = new System.Drawing.Size(100, 22);
            this.nameTextbox.TabIndex = 4;
            // 
            // ContentsLabel
            // 
            this.ContentsLabel.AutoSize = true;
            this.ContentsLabel.Location = new System.Drawing.Point(164, 0);
            this.ContentsLabel.Name = "ContentsLabel";
            this.ContentsLabel.Size = new System.Drawing.Size(68, 17);
            this.ContentsLabel.TabIndex = 4;
            this.ContentsLabel.Text = "Contents:";
            // 
            // contentsTextbox
            // 
            this.contentsTextbox.Location = new System.Drawing.Point(238, 3);
            this.contentsTextbox.Name = "contentsTextbox";
            this.contentsTextbox.ReadOnly = true;
            this.contentsTextbox.Size = new System.Drawing.Size(272, 22);
            this.contentsTextbox.TabIndex = 5;
            // 
            // ValueLabel
            // 
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Location = new System.Drawing.Point(516, 0);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(48, 17);
            this.ValueLabel.TabIndex = 6;
            this.ValueLabel.Text = "Value:";
            // 
            // valueTextbox
            // 
            this.valueTextbox.Location = new System.Drawing.Point(570, 3);
            this.valueTextbox.Name = "valueTextbox";
            this.valueTextbox.ReadOnly = true;
            this.valueTextbox.Size = new System.Drawing.Size(199, 22);
            this.valueTextbox.TabIndex = 7;
            // 
            // formulaLabel
            // 
            this.formulaLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.formulaLabel.AutoSize = true;
            this.formulaLabel.Location = new System.Drawing.Point(3, 34);
            this.formulaLabel.Name = "formulaLabel";
            this.formulaLabel.Size = new System.Drawing.Size(63, 17);
            this.formulaLabel.TabIndex = 8;
            this.formulaLabel.Text = "Formula:";
            // 
            // formulaTextbox
            // 
            this.formulaTextbox.Location = new System.Drawing.Point(72, 31);
            this.formulaTextbox.Name = "formulaTextbox";
            this.formulaTextbox.Size = new System.Drawing.Size(406, 22);
            this.formulaTextbox.TabIndex = 9;
            this.formulaTextbox.TextChanged += new System.EventHandler(this.formulaTextbox_TextChanged);
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(484, 28);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(69, 17);
            this.ProgressLabel.TabIndex = 11;
            this.ProgressLabel.Text = "Progress:";
            // 
            // bgProgressBar
            // 
            this.bgProgressBar.Location = new System.Drawing.Point(559, 31);
            this.bgProgressBar.MarqueeAnimationSpeed = 20;
            this.bgProgressBar.Name = "bgProgressBar";
            this.bgProgressBar.Size = new System.Drawing.Size(160, 23);
            this.bgProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.bgProgressBar.TabIndex = 10;
            // 
            // getDependentsButton
            // 
            this.getDependentsButton.Location = new System.Drawing.Point(3, 60);
            this.getDependentsButton.Name = "getDependentsButton";
            this.getDependentsButton.Size = new System.Drawing.Size(132, 23);
            this.getDependentsButton.TabIndex = 12;
            this.getDependentsButton.Text = "Show Dependents";
            this.getDependentsButton.UseVisualStyleBackColor = true;
            this.getDependentsButton.Click += new System.EventHandler(this.getDependentsButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.MainControlArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grid_widget, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(811, 416);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // grid_widget
            // 
            this.grid_widget.AutoSize = true;
            this.grid_widget.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.grid_widget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_widget.Location = new System.Drawing.Point(4, 127);
            this.grid_widget.Margin = new System.Windows.Forms.Padding(4);
            this.grid_widget.MaximumSize = new System.Drawing.Size(2800, 2462);
            this.grid_widget.Name = "grid_widget";
            this.grid_widget.Size = new System.Drawing.Size(803, 285);
            this.grid_widget.TabIndex = 0;
            // 
            // SimpleSpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 444);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SimpleSpreadsheetGUI";
            this.Text = "Simple Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimpleSpreadsheetGUI_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.MainControlArea.ResumeLayout(false);
            this.MainControlArea.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //This is where we are holding our Spreadsheet object.

        private SpreadsheetGridWidget grid_widget;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

        private FlowLayoutPanel MainControlArea;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox nameTextbox;
        private Label NameLabel;
        private Label ContentsLabel;
        private TextBox contentsTextbox;
        private Label ValueLabel;
        private TextBox valueTextbox;
        private Label formulaLabel;
        private TextBox formulaTextbox;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Label ProgressLabel;
        private ProgressBar bgProgressBar;
        private Button getDependentsButton;
    }
}

