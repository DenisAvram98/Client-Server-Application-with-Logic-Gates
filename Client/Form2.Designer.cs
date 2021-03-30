namespace Client
{
    partial class Form2
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("AND", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("OR", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("XOR", 2);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("NOT", 3);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("INPUT: 0", 4);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("INPUT: 1", 5);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("OUTPUT", 6);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.diagramView1 = new MindFusion.Diagramming.WinForms.DiagramView();
            this.diagram1 = new MindFusion.Diagramming.Diagram();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stergeTotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.generareRezultatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conectareLaServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 556);
            this.panel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(3, 33);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(313, 520);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AND.bmp");
            this.imageList1.Images.SetKeyName(1, "OR.bmp");
            this.imageList1.Images.SetKeyName(2, "XOR.bmp");
            this.imageList1.Images.SetKeyName(3, "NOT.bmp");
            this.imageList1.Images.SetKeyName(4, "START0.bmp");
            this.imageList1.Images.SetKeyName(5, "START1.bmp");
            this.imageList1.Images.SetKeyName(6, "Terminator.bmp");
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Toolbox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // diagramView1
            // 
            this.diagramView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramView1.Diagram = this.diagram1;
            this.diagramView1.LicenseKey = null;
            this.diagramView1.Location = new System.Drawing.Point(338, 40);
            this.diagramView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.diagramView1.Name = "diagramView1";
            this.diagramView1.Size = new System.Drawing.Size(783, 556);
            this.diagramView1.TabIndex = 1;
            this.diagramView1.Text = "diagramView1";
            // 
            // diagram1
            // 
            this.diagram1.AdjustmentHandlesSize = 8F;
            this.diagram1.AlignToGrid = false;
            this.diagram1.AllowLinksRepeat = false;
            this.diagram1.AllowSelfLoops = false;
            this.diagram1.AllowUnanchoredLinks = false;
            this.diagram1.BackBrush = new MindFusion.Drawing.SolidBrush("#FFE0E0E0");
            this.diagram1.DiagramStyle.FontSize = 10F;
            this.diagram1.LinkBaseShape = MindFusion.Diagramming.Shape.FromId("Triangle");
            this.diagram1.LinkSegments = 3;
            this.diagram1.LinkShape = MindFusion.Diagramming.LinkShape.Cascading;
            this.diagram1.MeasureUnit = MindFusion.Diagramming.MeasureUnit.Pixel;
            this.diagram1.RouteLinks = true;
            this.diagram1.RoutingOptions.GridSize = 16F;
            this.diagram1.RoutingOptions.NodeVicinitySize = 48F;
            this.diagram1.SelectAfterCreate = false;
            this.diagram1.SnapToAnchor = MindFusion.Diagramming.SnapToAnchor.OnCreateOrModify;
            this.diagram1.TableRowHeight = 14F;
            this.diagram1.TouchThreshold = 0F;
            this.diagram1.Clicked += new System.EventHandler<MindFusion.Diagramming.DiagramEventArgs>(this.diagram1_Clicked);
            this.diagram1.LinkClicked += new System.EventHandler<MindFusion.Diagramming.LinkEventArgs>(this.diagram1_LinkClicked);
            this.diagram1.NodeClicked += new System.EventHandler<MindFusion.Diagramming.NodeEventArgs>(this.diagram1_NodeClicked);
            this.diagram1.NodeCreating += new System.EventHandler<MindFusion.Diagramming.NodeValidationEventArgs>(this.diagram1_NodeCreating);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stergeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 28);
            // 
            // stergeToolStripMenuItem
            // 
            this.stergeToolStripMenuItem.Name = "stergeToolStripMenuItem";
            this.stergeToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.stergeToolStripMenuItem.Text = "Sterge";
            this.stergeToolStripMenuItem.Click += new System.EventHandler(this.stergeToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stergeTotToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(145, 28);
            // 
            // stergeTotToolStripMenuItem
            // 
            this.stergeTotToolStripMenuItem.Name = "stergeTotToolStripMenuItem";
            this.stergeTotToolStripMenuItem.Size = new System.Drawing.Size(144, 24);
            this.stergeTotToolStripMenuItem.Text = "Sterge tot";
            this.stergeTotToolStripMenuItem.Click += new System.EventHandler(this.stergeTotToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generareRezultatToolStripMenuItem,
            this.conectareLaServerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1131, 31);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // generareRezultatToolStripMenuItem
            // 
            this.generareRezultatToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.generareRezultatToolStripMenuItem.Name = "generareRezultatToolStripMenuItem";
            this.generareRezultatToolStripMenuItem.Size = new System.Drawing.Size(155, 27);
            this.generareRezultatToolStripMenuItem.Text = "Generare rezultat";
            this.generareRezultatToolStripMenuItem.Click += new System.EventHandler(this.generareRezultatToolStripMenuItem_Click);
            // 
            // conectareLaServerToolStripMenuItem
            // 
            this.conectareLaServerToolStripMenuItem.Enabled = false;
            this.conectareLaServerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.conectareLaServerToolStripMenuItem.Name = "conectareLaServerToolStripMenuItem";
            this.conectareLaServerToolStripMenuItem.Size = new System.Drawing.Size(168, 27);
            this.conectareLaServerToolStripMenuItem.Text = "Conectare la server";
            this.conectareLaServerToolStripMenuItem.Visible = false;
            this.conectareLaServerToolStripMenuItem.Click += new System.EventHandler(this.conectareLaServerToolStripMenuItem_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1131, 603);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.diagramView1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private MindFusion.Diagramming.WinForms.DiagramView diagramView1;
        private MindFusion.Diagramming.Diagram diagram1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stergeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem stergeTotToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generareRezultatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conectareLaServerToolStripMenuItem;
    }
}