namespace Snagger2
{
    partial class frmSnagger
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.splitCont = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.lstView = new System.Windows.Forms.ListView();
            this.Element = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flpSide = new System.Windows.Forms.FlowLayoutPanel();
            this.lblIce = new System.Windows.Forms.Label();
            this.lblInternal = new System.Windows.Forms.Label();
            this.txtInternal = new System.Windows.Forms.TextBox();
            this.lblIceWarnings = new System.Windows.Forms.Label();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitCont)).BeginInit();
            this.splitCont.Panel1.SuspendLayout();
            this.splitCont.Panel2.SuspendLayout();
            this.splitCont.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.flpSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Location = new System.Drawing.Point(12, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(87, 36);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(12, 59);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 36);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(137, 80);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 15);
            this.lblTo.TabIndex = 5;
            this.lblTo.Text = "To:";
            this.lblTo.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(121, 57);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(39, 15);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "From:";
            this.lblFrom.Visible = false;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.BackColor = System.Drawing.Color.Transparent;
            this.lblEnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.Location = new System.Drawing.Point(130, 34);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(30, 15);
            this.lblEnd.TabIndex = 7;
            this.lblEnd.Text = "End:";
            this.lblEnd.Visible = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.BackColor = System.Drawing.Color.Transparent;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(122, 11);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(38, 15);
            this.lblStart.TabIndex = 8;
            this.lblStart.Text = "Start:";
            this.lblStart.Visible = false;
            // 
            // txtTo
            // 
            this.txtTo.BackColor = System.Drawing.SystemColors.Control;
            this.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTo.Location = new System.Drawing.Point(166, 80);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(588, 16);
            this.txtTo.TabIndex = 9;
            // 
            // txtFrom
            // 
            this.txtFrom.BackColor = System.Drawing.SystemColors.Control;
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.Location = new System.Drawing.Point(166, 57);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(588, 16);
            this.txtFrom.TabIndex = 10;
            // 
            // txtEnd
            // 
            this.txtEnd.BackColor = System.Drawing.SystemColors.Control;
            this.txtEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEnd.Location = new System.Drawing.Point(166, 34);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(588, 16);
            this.txtEnd.TabIndex = 11;
            // 
            // txtStart
            // 
            this.txtStart.BackColor = System.Drawing.SystemColors.Control;
            this.txtStart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStart.Location = new System.Drawing.Point(166, 11);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(588, 16);
            this.txtStart.TabIndex = 12;
            // 
            // splitCont
            // 
            this.splitCont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitCont.Location = new System.Drawing.Point(12, 111);
            this.splitCont.Name = "splitCont";
            // 
            // splitCont.Panel1
            // 
            this.splitCont.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitCont.Panel1.Controls.Add(this.treeView);
            // 
            // splitCont.Panel2
            // 
            this.splitCont.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitCont.Panel2.Controls.Add(this.lstView);
            this.splitCont.Size = new System.Drawing.Size(877, 517);
            this.splitCont.SplitterDistance = 245;
            this.splitCont.SplitterWidth = 5;
            this.splitCont.TabIndex = 13;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.ShowNodeToolTips = true;
            this.treeView.Size = new System.Drawing.Size(245, 517);
            this.treeView.TabIndex = 0;
            this.treeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_DrawNode);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewNode_Click);
            // 
            // lstView
            // 
            this.lstView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Element,
            this.Value});
            this.lstView.ContextMenuStrip = this.contextMenu;
            this.lstView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstView.FullRowSelect = true;
            this.lstView.GridLines = true;
            this.lstView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstView.HideSelection = false;
            this.lstView.Location = new System.Drawing.Point(0, 0);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(627, 517);
            this.lstView.TabIndex = 0;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            this.lstView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RightClickListItem);
            // 
            // Element
            // 
            this.Element.Text = "Element";
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 100;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyWithToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(204, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyToolStripMenuItem.Text = "Copy Values";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.ContextMenuCopyValues_Click);
            // 
            // copyWithToolStripMenuItem
            // 
            this.copyWithToolStripMenuItem.Name = "copyWithToolStripMenuItem";
            this.copyWithToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyWithToolStripMenuItem.Text = "Copy Values && Elements";
            this.copyWithToolStripMenuItem.Click += new System.EventHandler(this.copyWithToolStripMenuItem_Click);
            // 
            // flpSide
            // 
            this.flpSide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpSide.AutoScroll = true;
            this.flpSide.Controls.Add(this.lblIce);
            this.flpSide.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSide.Location = new System.Drawing.Point(895, 111);
            this.flpSide.Name = "flpSide";
            this.flpSide.Size = new System.Drawing.Size(280, 517);
            this.flpSide.TabIndex = 15;
            this.flpSide.WrapContents = false;
            // 
            // lblIce
            // 
            this.lblIce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIce.Location = new System.Drawing.Point(3, 0);
            this.lblIce.Name = "lblIce";
            this.lblIce.Size = new System.Drawing.Size(274, 15);
            this.lblIce.TabIndex = 1;
            this.lblIce.Text = "ICE Warning Flags:";
            this.lblIce.Visible = false;
            // 
            // lblInternal
            // 
            this.lblInternal.AutoSize = true;
            this.lblInternal.BackColor = System.Drawing.Color.Transparent;
            this.lblInternal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternal.Location = new System.Drawing.Point(835, 89);
            this.lblInternal.Name = "lblInternal";
            this.lblInternal.Size = new System.Drawing.Size(54, 15);
            this.lblInternal.TabIndex = 17;
            this.lblInternal.Text = "Internal:";
            this.lblInternal.Visible = false;
            // 
            // txtInternal
            // 
            this.txtInternal.BackColor = System.Drawing.SystemColors.Control;
            this.txtInternal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInternal.Location = new System.Drawing.Point(895, 89);
            this.txtInternal.Name = "txtInternal";
            this.txtInternal.Size = new System.Drawing.Size(58, 16);
            this.txtInternal.TabIndex = 16;
            // 
            // lblIceWarnings
            // 
            this.lblIceWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIceWarnings.AutoSize = true;
            this.lblIceWarnings.Location = new System.Drawing.Point(1150, 22);
            this.lblIceWarnings.MaximumSize = new System.Drawing.Size(265, 0);
            this.lblIceWarnings.Name = "lblIceWarnings";
            this.lblIceWarnings.Size = new System.Drawing.Size(0, 15);
            this.lblIceWarnings.TabIndex = 18;
            this.lblIceWarnings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ofdOpen
            // 
            this.ofdOpen.Filter = "Uccapilog|*.uccapilog;*.uccapilog.bak|All Files|*.*";
            this.ofdOpen.Multiselect = true;
            this.ofdOpen.SupportMultiDottedExtensions = true;
            // 
            // frmSnagger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 640);
            this.Controls.Add(this.lblIceWarnings);
            this.Controls.Add(this.lblInternal);
            this.Controls.Add(this.txtInternal);
            this.Controls.Add(this.flpSide);
            this.Controls.Add(this.splitCont);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOpen);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSnagger";
            this.Text = "Snagger";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmSnagger_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmSnagger_DragEnter);
            this.splitCont.Panel1.ResumeLayout(false);
            this.splitCont.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCont)).EndInit();
            this.splitCont.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.flpSide.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.SplitContainer splitCont;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.ColumnHeader Element;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.FlowLayoutPanel flpSide;
        private System.Windows.Forms.Label lblIce;
        private System.Windows.Forms.Label lblInternal;
        private System.Windows.Forms.TextBox txtInternal;
        private System.Windows.Forms.Label lblIceWarnings;
        private System.Windows.Forms.OpenFileDialog ofdOpen;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyWithToolStripMenuItem;
    }
}

