namespace CodeManager
{
    partial class frmCodeManager
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ssMessageBar = new System.Windows.Forms.StatusStrip();
            this.spcFormQuery = new System.Windows.Forms.SplitContainer();
            this.gbQueryCondition = new System.Windows.Forms.GroupBox();
            this.txbQueryCategoryName = new System.Windows.Forms.TextBox();
            this.lsbQueryCondition = new System.Windows.Forms.ListBox();
            this.lbQuery_01 = new System.Windows.Forms.Label();
            this.dgvCode = new System.Windows.Forms.DataGridView();
            this.tbcForm = new System.Windows.Forms.TabControl();
            this.tbcpQuery = new System.Windows.Forms.TabPage();
            this.tbcpEditor = new System.Windows.Forms.TabPage();
            this.spcFormEditor = new System.Windows.Forms.SplitContainer();
            this.gbEditorCondition = new System.Windows.Forms.GroupBox();
            this.lsbEditorCodeType = new System.Windows.Forms.ListBox();
            this.lbEditor_08 = new System.Windows.Forms.Label();
            this.txbEditorCodeListName = new System.Windows.Forms.TextBox();
            this.lsbEditorCodeList = new System.Windows.Forms.ListBox();
            this.lbEditor_02 = new System.Windows.Forms.Label();
            this.txbEditorCategoryName = new System.Windows.Forms.TextBox();
            this.lsbEditorCategoryID = new System.Windows.Forms.ListBox();
            this.lbEditor_01 = new System.Windows.Forms.Label();
            this.gbEditorEdit = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbEditor_03 = new System.Windows.Forms.Label();
            this.txbEditorCodeCauseEnglish = new System.Windows.Forms.TextBox();
            this.cmdEditorCodeType = new System.Windows.Forms.ComboBox();
            this.txbEditorCodeCause = new System.Windows.Forms.TextBox();
            this.txbEditorCodeID = new System.Windows.Forms.TextBox();
            this.lbEditor_07 = new System.Windows.Forms.Label();
            this.lbEditor_06 = new System.Windows.Forms.Label();
            this.lbEditor_04 = new System.Windows.Forms.Label();
            this.txbEditorCodeName = new System.Windows.Forms.TextBox();
            this.lbEditor_05 = new System.Windows.Forms.Label();
            this.btnEditorDelete = new System.Windows.Forms.Button();
            this.btnEditorSave = new System.Windows.Forms.Button();
            this.btnEditorCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spcFormQuery)).BeginInit();
            this.spcFormQuery.Panel1.SuspendLayout();
            this.spcFormQuery.Panel2.SuspendLayout();
            this.spcFormQuery.SuspendLayout();
            this.gbQueryCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCode)).BeginInit();
            this.tbcForm.SuspendLayout();
            this.tbcpQuery.SuspendLayout();
            this.tbcpEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcFormEditor)).BeginInit();
            this.spcFormEditor.Panel1.SuspendLayout();
            this.spcFormEditor.Panel2.SuspendLayout();
            this.spcFormEditor.SuspendLayout();
            this.gbEditorCondition.SuspendLayout();
            this.gbEditorEdit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMessageBar
            // 
            this.ssMessageBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ssMessageBar.Location = new System.Drawing.Point(0, 0);
            this.ssMessageBar.Name = "ssMessageBar";
            this.ssMessageBar.Size = new System.Drawing.Size(784, 22);
            this.ssMessageBar.TabIndex = 0;
            this.ssMessageBar.Text = "statusStrip1";
            // 
            // spcFormQuery
            // 
            this.spcFormQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcFormQuery.Location = new System.Drawing.Point(3, 3);
            this.spcFormQuery.Name = "spcFormQuery";
            // 
            // spcFormQuery.Panel1
            // 
            this.spcFormQuery.Panel1.Controls.Add(this.gbQueryCondition);
            // 
            // spcFormQuery.Panel2
            // 
            this.spcFormQuery.Panel2.Controls.Add(this.dgvCode);
            this.spcFormQuery.Size = new System.Drawing.Size(770, 508);
            this.spcFormQuery.SplitterDistance = 181;
            this.spcFormQuery.TabIndex = 1;
            // 
            // gbQueryCondition
            // 
            this.gbQueryCondition.Controls.Add(this.txbQueryCategoryName);
            this.gbQueryCondition.Controls.Add(this.lsbQueryCondition);
            this.gbQueryCondition.Controls.Add(this.lbQuery_01);
            this.gbQueryCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbQueryCondition.Location = new System.Drawing.Point(0, 0);
            this.gbQueryCondition.Name = "gbQueryCondition";
            this.gbQueryCondition.Size = new System.Drawing.Size(181, 508);
            this.gbQueryCondition.TabIndex = 0;
            this.gbQueryCondition.TabStop = false;
            this.gbQueryCondition.Text = "Condition";
            // 
            // txbQueryCategoryName
            // 
            this.txbQueryCategoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbQueryCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbQueryCategoryName.Location = new System.Drawing.Point(8, 123);
            this.txbQueryCategoryName.Multiline = true;
            this.txbQueryCategoryName.Name = "txbQueryCategoryName";
            this.txbQueryCategoryName.ReadOnly = true;
            this.txbQueryCategoryName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbQueryCategoryName.Size = new System.Drawing.Size(167, 72);
            this.txbQueryCategoryName.TabIndex = 4;
            // 
            // lsbQueryCondition
            // 
            this.lsbQueryCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbQueryCondition.FormattingEnabled = true;
            this.lsbQueryCondition.ItemHeight = 12;
            this.lsbQueryCondition.Location = new System.Drawing.Point(8, 41);
            this.lsbQueryCondition.Name = "lsbQueryCondition";
            this.lsbQueryCondition.Size = new System.Drawing.Size(167, 76);
            this.lsbQueryCondition.TabIndex = 3;
            this.lsbQueryCondition.Click += new System.EventHandler(this.lsbQueryCondition_Click);
            this.lsbQueryCondition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbQueryCondition_KeyDown);
            // 
            // lbQuery_01
            // 
            this.lbQuery_01.Location = new System.Drawing.Point(6, 18);
            this.lbQuery_01.Name = "lbQuery_01";
            this.lbQuery_01.Size = new System.Drawing.Size(169, 20);
            this.lbQuery_01.TabIndex = 2;
            this.lbQuery_01.Text = "Category ID :";
            this.lbQuery_01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dgvCode
            // 
            this.dgvCode.AllowUserToAddRows = false;
            this.dgvCode.AllowUserToDeleteRows = false;
            this.dgvCode.AllowUserToOrderColumns = true;
            this.dgvCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCode.Location = new System.Drawing.Point(0, 0);
            this.dgvCode.Name = "dgvCode";
            this.dgvCode.ReadOnly = true;
            this.dgvCode.RowTemplate.Height = 24;
            this.dgvCode.Size = new System.Drawing.Size(585, 508);
            this.dgvCode.TabIndex = 0;
            // 
            // tbcForm
            // 
            this.tbcForm.Controls.Add(this.tbcpQuery);
            this.tbcForm.Controls.Add(this.tbcpEditor);
            this.tbcForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcForm.Location = new System.Drawing.Point(0, 22);
            this.tbcForm.Name = "tbcForm";
            this.tbcForm.SelectedIndex = 0;
            this.tbcForm.Size = new System.Drawing.Size(784, 540);
            this.tbcForm.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbcForm.TabIndex = 2;
            // 
            // tbcpQuery
            // 
            this.tbcpQuery.Controls.Add(this.spcFormQuery);
            this.tbcpQuery.Location = new System.Drawing.Point(4, 22);
            this.tbcpQuery.Name = "tbcpQuery";
            this.tbcpQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tbcpQuery.Size = new System.Drawing.Size(776, 514);
            this.tbcpQuery.TabIndex = 0;
            this.tbcpQuery.Text = "Query";
            this.tbcpQuery.UseVisualStyleBackColor = true;
            // 
            // tbcpEditor
            // 
            this.tbcpEditor.Controls.Add(this.spcFormEditor);
            this.tbcpEditor.Location = new System.Drawing.Point(4, 22);
            this.tbcpEditor.Name = "tbcpEditor";
            this.tbcpEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tbcpEditor.Size = new System.Drawing.Size(776, 514);
            this.tbcpEditor.TabIndex = 1;
            this.tbcpEditor.Text = "Editor";
            this.tbcpEditor.UseVisualStyleBackColor = true;
            // 
            // spcFormEditor
            // 
            this.spcFormEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcFormEditor.Location = new System.Drawing.Point(3, 3);
            this.spcFormEditor.Name = "spcFormEditor";
            // 
            // spcFormEditor.Panel1
            // 
            this.spcFormEditor.Panel1.Controls.Add(this.gbEditorCondition);
            // 
            // spcFormEditor.Panel2
            // 
            this.spcFormEditor.Panel2.Controls.Add(this.gbEditorEdit);
            this.spcFormEditor.Size = new System.Drawing.Size(770, 508);
            this.spcFormEditor.SplitterDistance = 181;
            this.spcFormEditor.TabIndex = 2;
            // 
            // gbEditorCondition
            // 
            this.gbEditorCondition.Controls.Add(this.lsbEditorCodeType);
            this.gbEditorCondition.Controls.Add(this.lbEditor_08);
            this.gbEditorCondition.Controls.Add(this.txbEditorCodeListName);
            this.gbEditorCondition.Controls.Add(this.lsbEditorCodeList);
            this.gbEditorCondition.Controls.Add(this.lbEditor_02);
            this.gbEditorCondition.Controls.Add(this.txbEditorCategoryName);
            this.gbEditorCondition.Controls.Add(this.lsbEditorCategoryID);
            this.gbEditorCondition.Controls.Add(this.lbEditor_01);
            this.gbEditorCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEditorCondition.Location = new System.Drawing.Point(0, 0);
            this.gbEditorCondition.Name = "gbEditorCondition";
            this.gbEditorCondition.Size = new System.Drawing.Size(181, 508);
            this.gbEditorCondition.TabIndex = 0;
            this.gbEditorCondition.TabStop = false;
            this.gbEditorCondition.Text = "Condition";
            // 
            // lsbEditorCodeType
            // 
            this.lsbEditorCodeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbEditorCodeType.FormattingEnabled = true;
            this.lsbEditorCodeType.ItemHeight = 12;
            this.lsbEditorCodeType.Location = new System.Drawing.Point(8, 199);
            this.lsbEditorCodeType.Name = "lsbEditorCodeType";
            this.lsbEditorCodeType.Size = new System.Drawing.Size(167, 76);
            this.lsbEditorCodeType.TabIndex = 9;
            this.lsbEditorCodeType.Click += new System.EventHandler(this.lsbQueryCondition_Click);
            this.lsbEditorCodeType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbQueryCondition_KeyDown);
            // 
            // lbEditor_08
            // 
            this.lbEditor_08.Location = new System.Drawing.Point(6, 176);
            this.lbEditor_08.Name = "lbEditor_08";
            this.lbEditor_08.Size = new System.Drawing.Size(169, 20);
            this.lbEditor_08.TabIndex = 8;
            this.lbEditor_08.Text = "Code Type :";
            this.lbEditor_08.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbEditorCodeListName
            // 
            this.txbEditorCodeListName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbEditorCodeListName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbEditorCodeListName.Location = new System.Drawing.Point(8, 443);
            this.txbEditorCodeListName.Multiline = true;
            this.txbEditorCodeListName.Name = "txbEditorCodeListName";
            this.txbEditorCodeListName.ReadOnly = true;
            this.txbEditorCodeListName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbEditorCodeListName.Size = new System.Drawing.Size(167, 59);
            this.txbEditorCodeListName.TabIndex = 7;
            // 
            // lsbEditorCodeList
            // 
            this.lsbEditorCodeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbEditorCodeList.FormattingEnabled = true;
            this.lsbEditorCodeList.ItemHeight = 12;
            this.lsbEditorCodeList.Location = new System.Drawing.Point(8, 301);
            this.lsbEditorCodeList.Name = "lsbEditorCodeList";
            this.lsbEditorCodeList.Size = new System.Drawing.Size(167, 136);
            this.lsbEditorCodeList.TabIndex = 6;
            this.lsbEditorCodeList.Click += new System.EventHandler(this.lsbQueryCondition_Click);
            this.lsbEditorCodeList.SelectedIndexChanged += new System.EventHandler(this.lsbEditorCodeList_SelectedIndexChanged);
            this.lsbEditorCodeList.DoubleClick += new System.EventHandler(this.lsbEditorCodeList_DoubleClick);
            this.lsbEditorCodeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbQueryCondition_KeyDown);
            // 
            // lbEditor_02
            // 
            this.lbEditor_02.Location = new System.Drawing.Point(6, 278);
            this.lbEditor_02.Name = "lbEditor_02";
            this.lbEditor_02.Size = new System.Drawing.Size(169, 20);
            this.lbEditor_02.TabIndex = 5;
            this.lbEditor_02.Text = "Code List :";
            this.lbEditor_02.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbEditorCategoryName
            // 
            this.txbEditorCategoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbEditorCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbEditorCategoryName.Location = new System.Drawing.Point(8, 123);
            this.txbEditorCategoryName.Multiline = true;
            this.txbEditorCategoryName.Name = "txbEditorCategoryName";
            this.txbEditorCategoryName.ReadOnly = true;
            this.txbEditorCategoryName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbEditorCategoryName.Size = new System.Drawing.Size(167, 50);
            this.txbEditorCategoryName.TabIndex = 4;
            // 
            // lsbEditorCategoryID
            // 
            this.lsbEditorCategoryID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbEditorCategoryID.FormattingEnabled = true;
            this.lsbEditorCategoryID.ItemHeight = 12;
            this.lsbEditorCategoryID.Location = new System.Drawing.Point(8, 41);
            this.lsbEditorCategoryID.Name = "lsbEditorCategoryID";
            this.lsbEditorCategoryID.Size = new System.Drawing.Size(167, 76);
            this.lsbEditorCategoryID.TabIndex = 3;
            this.lsbEditorCategoryID.Click += new System.EventHandler(this.lsbQueryCondition_Click);
            this.lsbEditorCategoryID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbQueryCondition_KeyDown);
            // 
            // lbEditor_01
            // 
            this.lbEditor_01.Location = new System.Drawing.Point(6, 18);
            this.lbEditor_01.Name = "lbEditor_01";
            this.lbEditor_01.Size = new System.Drawing.Size(169, 20);
            this.lbEditor_01.TabIndex = 2;
            this.lbEditor_01.Text = "Category ID :";
            this.lbEditor_01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // gbEditorEdit
            // 
            this.gbEditorEdit.Controls.Add(this.tableLayoutPanel1);
            this.gbEditorEdit.Controls.Add(this.btnEditorDelete);
            this.gbEditorEdit.Controls.Add(this.btnEditorSave);
            this.gbEditorEdit.Controls.Add(this.btnEditorCancel);
            this.gbEditorEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEditorEdit.Location = new System.Drawing.Point(0, 0);
            this.gbEditorEdit.Name = "gbEditorEdit";
            this.gbEditorEdit.Size = new System.Drawing.Size(585, 508);
            this.gbEditorEdit.TabIndex = 20;
            this.gbEditorEdit.TabStop = false;
            this.gbEditorEdit.Text = "Editor";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.16327F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.83673F));
            this.tableLayoutPanel1.Controls.Add(this.lbEditor_03, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbEditorCodeCauseEnglish, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmdEditorCodeType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbEditorCodeCause, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbEditorCodeID, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbEditor_07, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbEditor_06, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbEditor_04, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbEditorCodeName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbEditor_05, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(18, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 293);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // lbEditor_03
            // 
            this.lbEditor_03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEditor_03.Location = new System.Drawing.Point(3, 0);
            this.lbEditor_03.Name = "lbEditor_03";
            this.lbEditor_03.Size = new System.Drawing.Size(149, 30);
            this.lbEditor_03.TabIndex = 6;
            this.lbEditor_03.Text = "Code Type :";
            this.lbEditor_03.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbEditorCodeCauseEnglish
            // 
            this.txbEditorCodeCauseEnglish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbEditorCodeCauseEnglish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEditorCodeCauseEnglish.Location = new System.Drawing.Point(158, 193);
            this.txbEditorCodeCauseEnglish.Multiline = true;
            this.txbEditorCodeCauseEnglish.Name = "txbEditorCodeCauseEnglish";
            this.txbEditorCodeCauseEnglish.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbEditorCodeCauseEnglish.Size = new System.Drawing.Size(309, 97);
            this.txbEditorCodeCauseEnglish.TabIndex = 15;
            // 
            // cmdEditorCodeType
            // 
            this.cmdEditorCodeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdEditorCodeType.FormattingEnabled = true;
            this.cmdEditorCodeType.Location = new System.Drawing.Point(158, 3);
            this.cmdEditorCodeType.Name = "cmdEditorCodeType";
            this.cmdEditorCodeType.Size = new System.Drawing.Size(309, 20);
            this.cmdEditorCodeType.TabIndex = 7;
            // 
            // txbEditorCodeCause
            // 
            this.txbEditorCodeCause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbEditorCodeCause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEditorCodeCause.Location = new System.Drawing.Point(158, 93);
            this.txbEditorCodeCause.Multiline = true;
            this.txbEditorCodeCause.Name = "txbEditorCodeCause";
            this.txbEditorCodeCause.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbEditorCodeCause.Size = new System.Drawing.Size(309, 94);
            this.txbEditorCodeCause.TabIndex = 12;
            // 
            // txbEditorCodeID
            // 
            this.txbEditorCodeID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEditorCodeID.Location = new System.Drawing.Point(158, 63);
            this.txbEditorCodeID.Name = "txbEditorCodeID";
            this.txbEditorCodeID.Size = new System.Drawing.Size(309, 22);
            this.txbEditorCodeID.TabIndex = 14;
            // 
            // lbEditor_07
            // 
            this.lbEditor_07.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEditor_07.Location = new System.Drawing.Point(3, 190);
            this.lbEditor_07.Name = "lbEditor_07";
            this.lbEditor_07.Size = new System.Drawing.Size(149, 103);
            this.lbEditor_07.TabIndex = 10;
            this.lbEditor_07.Text = "Code Cause English :";
            this.lbEditor_07.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbEditor_06
            // 
            this.lbEditor_06.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEditor_06.Location = new System.Drawing.Point(3, 90);
            this.lbEditor_06.Name = "lbEditor_06";
            this.lbEditor_06.Size = new System.Drawing.Size(149, 100);
            this.lbEditor_06.TabIndex = 11;
            this.lbEditor_06.Text = "Code Cause :";
            this.lbEditor_06.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbEditor_04
            // 
            this.lbEditor_04.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEditor_04.Location = new System.Drawing.Point(3, 30);
            this.lbEditor_04.Name = "lbEditor_04";
            this.lbEditor_04.Size = new System.Drawing.Size(149, 30);
            this.lbEditor_04.TabIndex = 8;
            this.lbEditor_04.Text = "Code Name :";
            this.lbEditor_04.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbEditorCodeName
            // 
            this.txbEditorCodeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEditorCodeName.Location = new System.Drawing.Point(158, 33);
            this.txbEditorCodeName.Name = "txbEditorCodeName";
            this.txbEditorCodeName.Size = new System.Drawing.Size(309, 22);
            this.txbEditorCodeName.TabIndex = 13;
            // 
            // lbEditor_05
            // 
            this.lbEditor_05.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEditor_05.Location = new System.Drawing.Point(3, 60);
            this.lbEditor_05.Name = "lbEditor_05";
            this.lbEditor_05.Size = new System.Drawing.Size(149, 30);
            this.lbEditor_05.TabIndex = 9;
            this.lbEditor_05.Text = "Code ID :";
            this.lbEditor_05.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEditorDelete
            // 
            this.btnEditorDelete.Location = new System.Drawing.Point(413, 320);
            this.btnEditorDelete.Name = "btnEditorDelete";
            this.btnEditorDelete.Size = new System.Drawing.Size(75, 23);
            this.btnEditorDelete.TabIndex = 19;
            this.btnEditorDelete.Text = "Delete";
            this.btnEditorDelete.UseVisualStyleBackColor = true;
            this.btnEditorDelete.Click += new System.EventHandler(this.btnEditorDelete_Click);
            // 
            // btnEditorSave
            // 
            this.btnEditorSave.Location = new System.Drawing.Point(251, 320);
            this.btnEditorSave.Name = "btnEditorSave";
            this.btnEditorSave.Size = new System.Drawing.Size(75, 23);
            this.btnEditorSave.TabIndex = 17;
            this.btnEditorSave.Text = "Save";
            this.btnEditorSave.UseVisualStyleBackColor = true;
            this.btnEditorSave.Click += new System.EventHandler(this.btnEditorSave_Click);
            // 
            // btnEditorCancel
            // 
            this.btnEditorCancel.Location = new System.Drawing.Point(332, 320);
            this.btnEditorCancel.Name = "btnEditorCancel";
            this.btnEditorCancel.Size = new System.Drawing.Size(75, 23);
            this.btnEditorCancel.TabIndex = 18;
            this.btnEditorCancel.Text = "Cancel";
            this.btnEditorCancel.UseVisualStyleBackColor = true;
            this.btnEditorCancel.Click += new System.EventHandler(this.btnEditorCancel_Click);
            // 
            // frmCodeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tbcForm);
            this.Controls.Add(this.ssMessageBar);
            this.Name = "frmCodeManager";
            this.Text = "Code Manager";
            this.Load += new System.EventHandler(this.frmCodeManager_Load);
            this.spcFormQuery.Panel1.ResumeLayout(false);
            this.spcFormQuery.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcFormQuery)).EndInit();
            this.spcFormQuery.ResumeLayout(false);
            this.gbQueryCondition.ResumeLayout(false);
            this.gbQueryCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCode)).EndInit();
            this.tbcForm.ResumeLayout(false);
            this.tbcpQuery.ResumeLayout(false);
            this.tbcpEditor.ResumeLayout(false);
            this.spcFormEditor.Panel1.ResumeLayout(false);
            this.spcFormEditor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcFormEditor)).EndInit();
            this.spcFormEditor.ResumeLayout(false);
            this.gbEditorCondition.ResumeLayout(false);
            this.gbEditorCondition.PerformLayout();
            this.gbEditorEdit.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMessageBar;
        private System.Windows.Forms.SplitContainer spcFormQuery;
        private System.Windows.Forms.GroupBox gbQueryCondition;
        private System.Windows.Forms.Label lbQuery_01;
        private System.Windows.Forms.ListBox lsbQueryCondition;
        private System.Windows.Forms.TabControl tbcForm;
        private System.Windows.Forms.TabPage tbcpQuery;
        private System.Windows.Forms.TabPage tbcpEditor;
        private System.Windows.Forms.TextBox txbQueryCategoryName;
        private System.Windows.Forms.DataGridView dgvCode;
        private System.Windows.Forms.SplitContainer spcFormEditor;
        private System.Windows.Forms.GroupBox gbEditorCondition;
        private System.Windows.Forms.TextBox txbEditorCategoryName;
        private System.Windows.Forms.ListBox lsbEditorCategoryID;
        private System.Windows.Forms.Label lbEditor_01;
        private System.Windows.Forms.ListBox lsbEditorCodeList;
        private System.Windows.Forms.Label lbEditor_02;
        private System.Windows.Forms.TextBox txbEditorCodeListName;
        private System.Windows.Forms.GroupBox gbEditorEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbEditor_03;
        private System.Windows.Forms.TextBox txbEditorCodeCauseEnglish;
        private System.Windows.Forms.ComboBox cmdEditorCodeType;
        private System.Windows.Forms.TextBox txbEditorCodeCause;
        private System.Windows.Forms.TextBox txbEditorCodeID;
        private System.Windows.Forms.Label lbEditor_07;
        private System.Windows.Forms.Label lbEditor_06;
        private System.Windows.Forms.Label lbEditor_04;
        private System.Windows.Forms.TextBox txbEditorCodeName;
        private System.Windows.Forms.Label lbEditor_05;
        private System.Windows.Forms.Button btnEditorDelete;
        private System.Windows.Forms.Button btnEditorSave;
        private System.Windows.Forms.Button btnEditorCancel;
        private System.Windows.Forms.ListBox lsbEditorCodeType;
        private System.Windows.Forms.Label lbEditor_08;
    }
}

