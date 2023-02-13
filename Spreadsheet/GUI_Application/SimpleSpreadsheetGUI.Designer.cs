/// <summary>
///   Authors: Samuel Hancock, Hyrum Schenk
///   
///   We, Hyrum Schenk and Samuel Hancock, certify that I wrote this code from scratch and did not copy it in part or whole from  
///   another source.  All references used in the completion of the assignment are cited in my README file.
///   
/// File Contents
/// <para>
///     This GUI provides a way to interact with the 
///     Spreadsheet class; to enter values to cells, evalute
///     formulas based on values in cells and view the contents and values of cells.
///   </para>
/// 
/// </summary>

using SpreadsheetGrid_Core;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace SpreadsheetGUI
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
            this.fileToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem   = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem           = new System.Windows.Forms.ToolStripMenuItem();
            this.helpPageMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
            this.MainControlArea        = new System.Windows.Forms.FlowLayoutPanel();
            this.NameLabel              = new System.Windows.Forms.Label();
            this.nameTextbox            = new System.Windows.Forms.TextBox();
            this.ContentsLabel          = new System.Windows.Forms.Label();
            this.contentsTextbox        = new System.Windows.Forms.TextBox();
            this.ValueLabel             = new System.Windows.Forms.Label();
            this.valueTextbox           = new System.Windows.Forms.TextBox();
            this.formulaLabel           = new System.Windows.Forms.Label();
            this.formulaTextbox         = new System.Windows.Forms.TextBox();
            this.ProgressLabel          = new System.Windows.Forms.Label();
            this.bgProgressBar          = new System.Windows.Forms.ProgressBar();
            this.grid_widget            = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.tableLayoutPanel1      = new System.Windows.Forms.TableLayoutPanel();
            this.openFileDialog         = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog         = new System.Windows.Forms.SaveFileDialog();
            this.getDependeesButton    = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.MainControlArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            this.bg_worker = new BackgroundWorker();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(772, 28);
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
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            //
            //helpMenuItem
            //
            this.helpMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.helpPageMenuItem
            });
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(46, 24);
            this.helpMenuItem.Text = "Help";
            //
            //helpPageMenuItem
            //
            this.helpPageMenuItem.Name = "HelpPage";
            this.helpPageMenuItem.Text = "Help Page";
            this.helpPageMenuItem.Size = new System.Drawing.Size(128, 26);
            this.helpPageMenuItem.Click += new System.EventHandler(this.HelpPageOnClick);
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
            this.MainControlArea.Controls.Add(this.getDependeesButton);
            this.MainControlArea.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainControlArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainControlArea.Location = new System.Drawing.Point(4, 4);
            this.MainControlArea.Margin = new System.Windows.Forms.Padding(4);
            this.MainControlArea.MinimumSize = new System.Drawing.Size(133, 123);
            this.MainControlArea.Name = "MainControlArea";
            this.MainControlArea.Size = new System.Drawing.Size(764, 123);
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
            this.nameTextbox.Size = new System.Drawing.Size(100, 22);
            this.nameTextbox.ReadOnly = true;
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
            this.valueTextbox.Location = new System.Drawing.Point(3, 31);
            this.valueTextbox.Name = "valueTextbox";
            this.valueTextbox.ReadOnly = true;
            this.valueTextbox.Size = new System.Drawing.Size(147, 22);
            this.valueTextbox.TabIndex = 7;
            // 
            // formulaLabel
            // 
            this.formulaLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.formulaLabel.AutoSize = true;
            this.formulaLabel.Location = new System.Drawing.Point(208, 33);
            this.formulaLabel.Name = "formulaLabel";
            this.formulaLabel.Size = new System.Drawing.Size(63, 17);
            this.formulaLabel.TabIndex = 8;
            this.formulaLabel.Text = "Formula:";
            // 
            // formulaTextbox
            // 
            this.formulaTextbox.Location = new System.Drawing.Point(277, 31);
            this.formulaTextbox.Name = "formulaTextbox";
            this.formulaTextbox.Size = new System.Drawing.Size(406, 22);
            this.formulaTextbox.TabIndex = 9;
            this.formulaTextbox.TextChanged += new System.EventHandler(this.formulaTextbox_TextChanged);
            this.formulaTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.formulaTextbox_KeyPress);
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
            this.grid_widget.Size = new System.Drawing.Size(764, 285);
            this.grid_widget.TabIndex = 0;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(grid_widget_KeyDown);
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
            this.bgProgressBar.Name = "bgProgressBar";
            this.bgProgressBar.Size = new System.Drawing.Size(160, 23);
            this.bgProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.bgProgressBar.MarqueeAnimationSpeed = 0;
            this.bgProgressBar.TabIndex = 10;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 416);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // getDependeesButton
            // 
            this.getDependeesButton.Location = new System.Drawing.Point(3, 60);
            this.getDependeesButton.Name = "getDependeesButton";
            this.getDependeesButton.Size = new System.Drawing.Size(132, 23);
            this.getDependeesButton.TabIndex = 12;
            this.getDependeesButton.Text = "Dependees";
            this.getDependeesButton.UseVisualStyleBackColor = true;
            this.getDependeesButton.Click += new System.EventHandler(this.getDependeesButton_Click);
            // 
            // SimpleSpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 444);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SimpleSpreadsheetGUI";
            this.Text = "Simple Spreadsheet";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.MainControlArea.ResumeLayout(false);
            this.MainControlArea.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();


            //formclosing
            this.FormClosing += SimpleSpreadsheetGUI_FormClosing;

            // background worker
            bg_worker.DoWork += EvaluateCell;
            bg_worker.RunWorkerCompleted += compute_done;
            bg_worker.ProgressChanged += compute_progress;
            bg_worker.WorkerReportsProgress = true;
            bg_worker.WorkerSupportsCancellation = true;

        }

        #endregion


        private SpreadsheetGridWidget grid_widget;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;

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
        private ToolStripMenuItem helpPageMenuItem;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Label ProgressLabel;
        private ProgressBar bgProgressBar;
        private Button getDependeesButton;

        private BackgroundWorker bg_worker;
    }
}

