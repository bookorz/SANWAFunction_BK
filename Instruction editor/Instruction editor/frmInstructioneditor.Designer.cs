namespace Instruction_editor
{
    partial class frmInstructionEditor
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
            this.ssFormMessage = new System.Windows.Forms.StatusStrip();
            this.tbcForm = new System.Windows.Forms.TabControl();
            this.tapInstruction_Create = new System.Windows.Forms.TabPage();
            this.spctInstructionForm = new System.Windows.Forms.SplitContainer();
            this.gbCreateForm = new System.Windows.Forms.GroupBox();
            this.tlpInstruction = new System.Windows.Forms.TableLayoutPanel();
            this.lsbCreateCommandType = new System.Windows.Forms.ListBox();
            this.lbCreate_01 = new System.Windows.Forms.Label();
            this.lsbCreateEqpType = new System.Windows.Forms.ListBox();
            this.lbCreate_02 = new System.Windows.Forms.Label();
            this.lsbCreateEqpSupplier = new System.Windows.Forms.ListBox();
            this.lbCreate_03 = new System.Windows.Forms.Label();
            this.gbInstructionEditor = new System.Windows.Forms.GroupBox();
            this.spctEditor = new System.Windows.Forms.SplitContainer();
            this.tlpEditorLeft = new System.Windows.Forms.TableLayoutPanel();
            this.lbCreate_12 = new System.Windows.Forms.Label();
            this.lsbCreateCommand = new System.Windows.Forms.ListBox();
            this.lbCreate_04 = new System.Windows.Forms.Label();
            this.lsbCreateCommandParameter = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCommandAction = new System.Windows.Forms.ComboBox();
            this.tlpEditorRight = new System.Windows.Forms.TableLayoutPanel();
            this.palEditorData = new System.Windows.Forms.Panel();
            this.txbEditorParameterData = new System.Windows.Forms.TextBox();
            this.lbCreate_05 = new System.Windows.Forms.Label();
            this.nudParameterOrder = new System.Windows.Forms.NumericUpDown();
            this.lbCreate_06 = new System.Windows.Forms.Label();
            this.txbParameterID = new System.Windows.Forms.TextBox();
            this.lbCreate_07 = new System.Windows.Forms.Label();
            this.txbParameterDescription = new System.Windows.Forms.TextBox();
            this.rdbEditorParameterData = new System.Windows.Forms.RadioButton();
            this.rdbEditorParameterValue = new System.Windows.Forms.RadioButton();
            this.palEditorValue = new System.Windows.Forms.Panel();
            this.tlpValueSet = new System.Windows.Forms.TableLayoutPanel();
            this.lbCreate_09 = new System.Windows.Forms.Label();
            this.nudMAXValue = new System.Windows.Forms.NumericUpDown();
            this.nudMINValue = new System.Windows.Forms.NumericUpDown();
            this.lbCreate_08 = new System.Windows.Forms.Label();
            this.nudDefaultValue = new System.Windows.Forms.NumericUpDown();
            this.lbCreate_10 = new System.Windows.Forms.Label();
            this.nudValueLength = new System.Windows.Forms.NumericUpDown();
            this.lbCreate_11 = new System.Windows.Forms.Label();
            this.chbIsFill = new System.Windows.Forms.CheckBox();
            this.palEditorButton = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInstructionDelete = new System.Windows.Forms.Button();
            this.btnInstructionSave = new System.Windows.Forms.Button();
            this.tapInstruction_Script = new System.Windows.Forms.TabPage();
            this.tbcForm.SuspendLayout();
            this.tapInstruction_Create.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spctInstructionForm)).BeginInit();
            this.spctInstructionForm.Panel1.SuspendLayout();
            this.spctInstructionForm.Panel2.SuspendLayout();
            this.spctInstructionForm.SuspendLayout();
            this.gbCreateForm.SuspendLayout();
            this.tlpInstruction.SuspendLayout();
            this.gbInstructionEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spctEditor)).BeginInit();
            this.spctEditor.Panel1.SuspendLayout();
            this.spctEditor.Panel2.SuspendLayout();
            this.spctEditor.SuspendLayout();
            this.tlpEditorLeft.SuspendLayout();
            this.tlpEditorRight.SuspendLayout();
            this.palEditorData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudParameterOrder)).BeginInit();
            this.palEditorValue.SuspendLayout();
            this.tlpValueSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMAXValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMINValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValueLength)).BeginInit();
            this.palEditorButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssFormMessage
            // 
            this.ssFormMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.ssFormMessage.Location = new System.Drawing.Point(0, 0);
            this.ssFormMessage.Name = "ssFormMessage";
            this.ssFormMessage.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.ssFormMessage.Size = new System.Drawing.Size(784, 22);
            this.ssFormMessage.TabIndex = 0;
            this.ssFormMessage.Text = "statusStrip1";
            // 
            // tbcForm
            // 
            this.tbcForm.Controls.Add(this.tapInstruction_Create);
            this.tbcForm.Controls.Add(this.tapInstruction_Script);
            this.tbcForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcForm.Location = new System.Drawing.Point(0, 22);
            this.tbcForm.Name = "tbcForm";
            this.tbcForm.SelectedIndex = 0;
            this.tbcForm.Size = new System.Drawing.Size(784, 540);
            this.tbcForm.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbcForm.TabIndex = 1;
            // 
            // tapInstruction_Create
            // 
            this.tapInstruction_Create.Controls.Add(this.spctInstructionForm);
            this.tapInstruction_Create.Location = new System.Drawing.Point(4, 22);
            this.tapInstruction_Create.Name = "tapInstruction_Create";
            this.tapInstruction_Create.Padding = new System.Windows.Forms.Padding(3);
            this.tapInstruction_Create.Size = new System.Drawing.Size(776, 514);
            this.tapInstruction_Create.TabIndex = 0;
            this.tapInstruction_Create.Text = "Instruction Create";
            this.tapInstruction_Create.UseVisualStyleBackColor = true;
            // 
            // spctInstructionForm
            // 
            this.spctInstructionForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spctInstructionForm.Location = new System.Drawing.Point(3, 3);
            this.spctInstructionForm.Name = "spctInstructionForm";
            // 
            // spctInstructionForm.Panel1
            // 
            this.spctInstructionForm.Panel1.Controls.Add(this.gbCreateForm);
            // 
            // spctInstructionForm.Panel2
            // 
            this.spctInstructionForm.Panel2.Controls.Add(this.gbInstructionEditor);
            this.spctInstructionForm.Size = new System.Drawing.Size(770, 508);
            this.spctInstructionForm.SplitterDistance = 155;
            this.spctInstructionForm.TabIndex = 0;
            // 
            // gbCreateForm
            // 
            this.gbCreateForm.Controls.Add(this.tlpInstruction);
            this.gbCreateForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCreateForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbCreateForm.Location = new System.Drawing.Point(0, 0);
            this.gbCreateForm.Name = "gbCreateForm";
            this.gbCreateForm.Size = new System.Drawing.Size(155, 508);
            this.gbCreateForm.TabIndex = 0;
            this.gbCreateForm.TabStop = false;
            this.gbCreateForm.Text = "Condition";
            // 
            // tlpInstruction
            // 
            this.tlpInstruction.ColumnCount = 1;
            this.tlpInstruction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInstruction.Controls.Add(this.lsbCreateCommandType, 0, 5);
            this.tlpInstruction.Controls.Add(this.lbCreate_01, 0, 0);
            this.tlpInstruction.Controls.Add(this.lsbCreateEqpType, 0, 1);
            this.tlpInstruction.Controls.Add(this.lbCreate_02, 0, 2);
            this.tlpInstruction.Controls.Add(this.lsbCreateEqpSupplier, 0, 3);
            this.tlpInstruction.Controls.Add(this.lbCreate_03, 0, 4);
            this.tlpInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInstruction.Location = new System.Drawing.Point(3, 18);
            this.tlpInstruction.Name = "tlpInstruction";
            this.tlpInstruction.RowCount = 6;
            this.tlpInstruction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInstruction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpInstruction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInstruction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpInstruction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInstruction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInstruction.Size = new System.Drawing.Size(149, 487);
            this.tlpInstruction.TabIndex = 0;
            // 
            // lsbCreateCommandType
            // 
            this.lsbCreateCommandType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbCreateCommandType.FormattingEnabled = true;
            this.lsbCreateCommandType.ItemHeight = 12;
            this.lsbCreateCommandType.Location = new System.Drawing.Point(3, 275);
            this.lsbCreateCommandType.Name = "lsbCreateCommandType";
            this.lsbCreateCommandType.Size = new System.Drawing.Size(143, 209);
            this.lsbCreateCommandType.TabIndex = 3;
            this.lsbCreateCommandType.Click += new System.EventHandler(this.lsbCreateEqpType_Click);
            this.lsbCreateCommandType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbCreateEqpType_KeyDown);
            // 
            // lbCreate_01
            // 
            this.lbCreate_01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_01.Location = new System.Drawing.Point(3, 0);
            this.lbCreate_01.Name = "lbCreate_01";
            this.lbCreate_01.Size = new System.Drawing.Size(143, 20);
            this.lbCreate_01.TabIndex = 7;
            this.lbCreate_01.Text = "Equipment Type :";
            this.lbCreate_01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lsbCreateEqpType
            // 
            this.lsbCreateEqpType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbCreateEqpType.FormattingEnabled = true;
            this.lsbCreateEqpType.ItemHeight = 12;
            this.lsbCreateEqpType.Location = new System.Drawing.Point(3, 23);
            this.lsbCreateEqpType.Name = "lsbCreateEqpType";
            this.lsbCreateEqpType.Size = new System.Drawing.Size(143, 100);
            this.lsbCreateEqpType.TabIndex = 1;
            this.lsbCreateEqpType.Click += new System.EventHandler(this.lsbCreateEqpType_Click);
            this.lsbCreateEqpType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbCreateEqpType_KeyDown);
            // 
            // lbCreate_02
            // 
            this.lbCreate_02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_02.Location = new System.Drawing.Point(3, 126);
            this.lbCreate_02.Name = "lbCreate_02";
            this.lbCreate_02.Size = new System.Drawing.Size(143, 20);
            this.lbCreate_02.TabIndex = 9;
            this.lbCreate_02.Text = "Equipment Supplier :";
            this.lbCreate_02.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lsbCreateEqpSupplier
            // 
            this.lsbCreateEqpSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbCreateEqpSupplier.FormattingEnabled = true;
            this.lsbCreateEqpSupplier.ItemHeight = 12;
            this.lsbCreateEqpSupplier.Location = new System.Drawing.Point(3, 149);
            this.lsbCreateEqpSupplier.Name = "lsbCreateEqpSupplier";
            this.lsbCreateEqpSupplier.Size = new System.Drawing.Size(143, 100);
            this.lsbCreateEqpSupplier.TabIndex = 2;
            this.lsbCreateEqpSupplier.Click += new System.EventHandler(this.lsbCreateEqpType_Click);
            this.lsbCreateEqpSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbCreateEqpType_KeyDown);
            // 
            // lbCreate_03
            // 
            this.lbCreate_03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_03.Location = new System.Drawing.Point(3, 252);
            this.lbCreate_03.Name = "lbCreate_03";
            this.lbCreate_03.Size = new System.Drawing.Size(143, 20);
            this.lbCreate_03.TabIndex = 11;
            this.lbCreate_03.Text = "Command Type :";
            this.lbCreate_03.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // gbInstructionEditor
            // 
            this.gbInstructionEditor.Controls.Add(this.spctEditor);
            this.gbInstructionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbInstructionEditor.Location = new System.Drawing.Point(0, 0);
            this.gbInstructionEditor.Name = "gbInstructionEditor";
            this.gbInstructionEditor.Size = new System.Drawing.Size(611, 508);
            this.gbInstructionEditor.TabIndex = 0;
            this.gbInstructionEditor.TabStop = false;
            this.gbInstructionEditor.Text = "Editor";
            // 
            // spctEditor
            // 
            this.spctEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spctEditor.Location = new System.Drawing.Point(3, 18);
            this.spctEditor.Name = "spctEditor";
            // 
            // spctEditor.Panel1
            // 
            this.spctEditor.Panel1.Controls.Add(this.tlpEditorLeft);
            // 
            // spctEditor.Panel2
            // 
            this.spctEditor.Panel2.Controls.Add(this.tlpEditorRight);
            this.spctEditor.Size = new System.Drawing.Size(605, 487);
            this.spctEditor.SplitterDistance = 194;
            this.spctEditor.TabIndex = 0;
            // 
            // tlpEditorLeft
            // 
            this.tlpEditorLeft.ColumnCount = 1;
            this.tlpEditorLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEditorLeft.Controls.Add(this.lbCreate_12, 0, 0);
            this.tlpEditorLeft.Controls.Add(this.lsbCreateCommand, 0, 1);
            this.tlpEditorLeft.Controls.Add(this.lbCreate_04, 0, 2);
            this.tlpEditorLeft.Controls.Add(this.lsbCreateCommandParameter, 0, 3);
            this.tlpEditorLeft.Controls.Add(this.label1, 0, 4);
            this.tlpEditorLeft.Controls.Add(this.cmbCommandAction, 0, 5);
            this.tlpEditorLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditorLeft.Location = new System.Drawing.Point(0, 0);
            this.tlpEditorLeft.Name = "tlpEditorLeft";
            this.tlpEditorLeft.RowCount = 6;
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpEditorLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorLeft.Size = new System.Drawing.Size(194, 487);
            this.tlpEditorLeft.TabIndex = 0;
            // 
            // lbCreate_12
            // 
            this.lbCreate_12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_12.Location = new System.Drawing.Point(3, 0);
            this.lbCreate_12.Name = "lbCreate_12";
            this.lbCreate_12.Size = new System.Drawing.Size(188, 20);
            this.lbCreate_12.TabIndex = 8;
            this.lbCreate_12.Text = "Command :";
            this.lbCreate_12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lsbCreateCommand
            // 
            this.lsbCreateCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbCreateCommand.FormattingEnabled = true;
            this.lsbCreateCommand.ItemHeight = 12;
            this.lsbCreateCommand.Location = new System.Drawing.Point(3, 23);
            this.lsbCreateCommand.Name = "lsbCreateCommand";
            this.lsbCreateCommand.Size = new System.Drawing.Size(188, 192);
            this.lsbCreateCommand.TabIndex = 4;
            this.lsbCreateCommand.Click += new System.EventHandler(this.lsbCreateCommand_Click);
            this.lsbCreateCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbCreateEqpType_KeyDown);
            // 
            // lbCreate_04
            // 
            this.lbCreate_04.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_04.Location = new System.Drawing.Point(3, 218);
            this.lbCreate_04.Name = "lbCreate_04";
            this.lbCreate_04.Size = new System.Drawing.Size(188, 20);
            this.lbCreate_04.TabIndex = 10;
            this.lbCreate_04.Text = "Command Paraneter :";
            this.lbCreate_04.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lsbCreateCommandParameter
            // 
            this.lsbCreateCommandParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbCreateCommandParameter.FormattingEnabled = true;
            this.lsbCreateCommandParameter.ItemHeight = 12;
            this.lsbCreateCommandParameter.Location = new System.Drawing.Point(3, 241);
            this.lsbCreateCommandParameter.Name = "lsbCreateCommandParameter";
            this.lsbCreateCommandParameter.Size = new System.Drawing.Size(188, 192);
            this.lsbCreateCommandParameter.TabIndex = 5;
            this.lsbCreateCommandParameter.DoubleClick += new System.EventHandler(this.lsbCreateCommandParameter_DoubleClick);
            this.lsbCreateCommandParameter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbCreateEqpType_KeyDown);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 436);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Action function :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cmbCommandAction
            // 
            this.cmbCommandAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCommandAction.FormattingEnabled = true;
            this.cmbCommandAction.Location = new System.Drawing.Point(3, 459);
            this.cmbCommandAction.Name = "cmbCommandAction";
            this.cmbCommandAction.Size = new System.Drawing.Size(188, 20);
            this.cmbCommandAction.TabIndex = 6;
            // 
            // tlpEditorRight
            // 
            this.tlpEditorRight.ColumnCount = 1;
            this.tlpEditorRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEditorRight.Controls.Add(this.palEditorData, 0, 8);
            this.tlpEditorRight.Controls.Add(this.lbCreate_05, 0, 0);
            this.tlpEditorRight.Controls.Add(this.nudParameterOrder, 0, 1);
            this.tlpEditorRight.Controls.Add(this.lbCreate_06, 0, 2);
            this.tlpEditorRight.Controls.Add(this.txbParameterID, 0, 3);
            this.tlpEditorRight.Controls.Add(this.lbCreate_07, 0, 4);
            this.tlpEditorRight.Controls.Add(this.txbParameterDescription, 0, 5);
            this.tlpEditorRight.Controls.Add(this.rdbEditorParameterData, 0, 7);
            this.tlpEditorRight.Controls.Add(this.rdbEditorParameterValue, 0, 10);
            this.tlpEditorRight.Controls.Add(this.palEditorValue, 0, 11);
            this.tlpEditorRight.Controls.Add(this.palEditorButton, 0, 12);
            this.tlpEditorRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditorRight.Location = new System.Drawing.Point(0, 0);
            this.tlpEditorRight.Name = "tlpEditorRight";
            this.tlpEditorRight.RowCount = 13;
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpEditorRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEditorRight.Size = new System.Drawing.Size(407, 487);
            this.tlpEditorRight.TabIndex = 0;
            // 
            // palEditorData
            // 
            this.palEditorData.Controls.Add(this.txbEditorParameterData);
            this.palEditorData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palEditorData.Enabled = false;
            this.palEditorData.Location = new System.Drawing.Point(3, 202);
            this.palEditorData.Name = "palEditorData";
            this.palEditorData.Size = new System.Drawing.Size(401, 60);
            this.palEditorData.TabIndex = 26;
            // 
            // txbEditorParameterData
            // 
            this.txbEditorParameterData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEditorParameterData.Location = new System.Drawing.Point(0, 0);
            this.txbEditorParameterData.Multiline = true;
            this.txbEditorParameterData.Name = "txbEditorParameterData";
            this.txbEditorParameterData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbEditorParameterData.Size = new System.Drawing.Size(401, 60);
            this.txbEditorParameterData.TabIndex = 9;
            // 
            // lbCreate_05
            // 
            this.lbCreate_05.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_05.Location = new System.Drawing.Point(3, 0);
            this.lbCreate_05.Name = "lbCreate_05";
            this.lbCreate_05.Size = new System.Drawing.Size(401, 20);
            this.lbCreate_05.TabIndex = 7;
            this.lbCreate_05.Text = "Parameter order :";
            this.lbCreate_05.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudParameterOrder
            // 
            this.nudParameterOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudParameterOrder.Enabled = false;
            this.nudParameterOrder.Location = new System.Drawing.Point(3, 23);
            this.nudParameterOrder.Name = "nudParameterOrder";
            this.nudParameterOrder.Size = new System.Drawing.Size(401, 22);
            this.nudParameterOrder.TabIndex = 8;
            // 
            // lbCreate_06
            // 
            this.lbCreate_06.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_06.Location = new System.Drawing.Point(3, 45);
            this.lbCreate_06.Name = "lbCreate_06";
            this.lbCreate_06.Size = new System.Drawing.Size(401, 20);
            this.lbCreate_06.TabIndex = 9;
            this.lbCreate_06.Text = "Parameter ID :";
            this.lbCreate_06.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbParameterID
            // 
            this.txbParameterID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbParameterID.Location = new System.Drawing.Point(3, 68);
            this.txbParameterID.Name = "txbParameterID";
            this.txbParameterID.Size = new System.Drawing.Size(401, 22);
            this.txbParameterID.TabIndex = 7;
            // 
            // lbCreate_07
            // 
            this.lbCreate_07.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_07.Location = new System.Drawing.Point(3, 90);
            this.lbCreate_07.Name = "lbCreate_07";
            this.lbCreate_07.Size = new System.Drawing.Size(401, 20);
            this.lbCreate_07.TabIndex = 11;
            this.lbCreate_07.Text = "Parameter description :";
            this.lbCreate_07.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbParameterDescription
            // 
            this.txbParameterDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbParameterDescription.Location = new System.Drawing.Point(3, 113);
            this.txbParameterDescription.Multiline = true;
            this.txbParameterDescription.Name = "txbParameterDescription";
            this.txbParameterDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbParameterDescription.Size = new System.Drawing.Size(401, 60);
            this.txbParameterDescription.TabIndex = 8;
            // 
            // rdbEditorParameterData
            // 
            this.rdbEditorParameterData.AutoSize = true;
            this.rdbEditorParameterData.Location = new System.Drawing.Point(3, 182);
            this.rdbEditorParameterData.Name = "rdbEditorParameterData";
            this.rdbEditorParameterData.Size = new System.Drawing.Size(93, 14);
            this.rdbEditorParameterData.TabIndex = 25;
            this.rdbEditorParameterData.TabStop = true;
            this.rdbEditorParameterData.Text = "Parameter Data";
            this.rdbEditorParameterData.UseVisualStyleBackColor = true;
            this.rdbEditorParameterData.CheckedChanged += new System.EventHandler(this.rdbEditorParameterData_CheckedChanged);
            // 
            // rdbEditorParameterValue
            // 
            this.rdbEditorParameterValue.AutoSize = true;
            this.rdbEditorParameterValue.Location = new System.Drawing.Point(3, 271);
            this.rdbEditorParameterValue.Name = "rdbEditorParameterValue";
            this.rdbEditorParameterValue.Size = new System.Drawing.Size(97, 14);
            this.rdbEditorParameterValue.TabIndex = 27;
            this.rdbEditorParameterValue.TabStop = true;
            this.rdbEditorParameterValue.Text = "Parameter value";
            this.rdbEditorParameterValue.UseVisualStyleBackColor = true;
            this.rdbEditorParameterValue.CheckedChanged += new System.EventHandler(this.rdbEditorParameterData_CheckedChanged);
            // 
            // palEditorValue
            // 
            this.palEditorValue.Controls.Add(this.tlpValueSet);
            this.palEditorValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palEditorValue.Enabled = false;
            this.palEditorValue.Location = new System.Drawing.Point(3, 291);
            this.palEditorValue.Name = "palEditorValue";
            this.palEditorValue.Size = new System.Drawing.Size(401, 126);
            this.palEditorValue.TabIndex = 28;
            // 
            // tlpValueSet
            // 
            this.tlpValueSet.ColumnCount = 2;
            this.tlpValueSet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpValueSet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpValueSet.Controls.Add(this.lbCreate_09, 0, 0);
            this.tlpValueSet.Controls.Add(this.nudMAXValue, 1, 1);
            this.tlpValueSet.Controls.Add(this.nudMINValue, 0, 1);
            this.tlpValueSet.Controls.Add(this.lbCreate_08, 1, 0);
            this.tlpValueSet.Controls.Add(this.nudDefaultValue, 1, 3);
            this.tlpValueSet.Controls.Add(this.lbCreate_10, 0, 2);
            this.tlpValueSet.Controls.Add(this.nudValueLength, 0, 3);
            this.tlpValueSet.Controls.Add(this.lbCreate_11, 1, 2);
            this.tlpValueSet.Controls.Add(this.chbIsFill, 0, 4);
            this.tlpValueSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpValueSet.Location = new System.Drawing.Point(0, 0);
            this.tlpValueSet.Name = "tlpValueSet";
            this.tlpValueSet.RowCount = 5;
            this.tlpValueSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpValueSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpValueSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpValueSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpValueSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpValueSet.Size = new System.Drawing.Size(401, 126);
            this.tlpValueSet.TabIndex = 23;
            // 
            // lbCreate_09
            // 
            this.lbCreate_09.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_09.Location = new System.Drawing.Point(3, 0);
            this.lbCreate_09.Name = "lbCreate_09";
            this.lbCreate_09.Size = new System.Drawing.Size(194, 20);
            this.lbCreate_09.TabIndex = 16;
            this.lbCreate_09.Text = "MIN value :";
            this.lbCreate_09.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudMAXValue
            // 
            this.nudMAXValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudMAXValue.Location = new System.Drawing.Point(203, 23);
            this.nudMAXValue.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudMAXValue.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.nudMAXValue.Name = "nudMAXValue";
            this.nudMAXValue.Size = new System.Drawing.Size(195, 22);
            this.nudMAXValue.TabIndex = 11;
            this.nudMAXValue.Enter += new System.EventHandler(this.nudMINValue_Enter);
            // 
            // nudMINValue
            // 
            this.nudMINValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudMINValue.Location = new System.Drawing.Point(3, 23);
            this.nudMINValue.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudMINValue.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.nudMINValue.Name = "nudMINValue";
            this.nudMINValue.Size = new System.Drawing.Size(194, 22);
            this.nudMINValue.TabIndex = 10;
            this.nudMINValue.Enter += new System.EventHandler(this.nudMINValue_Enter);
            // 
            // lbCreate_08
            // 
            this.lbCreate_08.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_08.Location = new System.Drawing.Point(203, 0);
            this.lbCreate_08.Name = "lbCreate_08";
            this.lbCreate_08.Size = new System.Drawing.Size(195, 20);
            this.lbCreate_08.TabIndex = 12;
            this.lbCreate_08.Text = "MAX value :";
            this.lbCreate_08.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudDefaultValue
            // 
            this.nudDefaultValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudDefaultValue.Location = new System.Drawing.Point(203, 73);
            this.nudDefaultValue.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudDefaultValue.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.nudDefaultValue.Name = "nudDefaultValue";
            this.nudDefaultValue.Size = new System.Drawing.Size(195, 22);
            this.nudDefaultValue.TabIndex = 13;
            this.nudDefaultValue.Enter += new System.EventHandler(this.nudMINValue_Enter);
            // 
            // lbCreate_10
            // 
            this.lbCreate_10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_10.Location = new System.Drawing.Point(3, 50);
            this.lbCreate_10.Name = "lbCreate_10";
            this.lbCreate_10.Size = new System.Drawing.Size(194, 20);
            this.lbCreate_10.TabIndex = 14;
            this.lbCreate_10.Text = "Value length :";
            this.lbCreate_10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudValueLength
            // 
            this.nudValueLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudValueLength.Location = new System.Drawing.Point(3, 73);
            this.nudValueLength.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudValueLength.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.nudValueLength.Name = "nudValueLength";
            this.nudValueLength.Size = new System.Drawing.Size(194, 22);
            this.nudValueLength.TabIndex = 12;
            this.nudValueLength.Enter += new System.EventHandler(this.nudMINValue_Enter);
            // 
            // lbCreate_11
            // 
            this.lbCreate_11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreate_11.Location = new System.Drawing.Point(203, 50);
            this.lbCreate_11.Name = "lbCreate_11";
            this.lbCreate_11.Size = new System.Drawing.Size(195, 20);
            this.lbCreate_11.TabIndex = 18;
            this.lbCreate_11.Text = "Default value :";
            this.lbCreate_11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // chbIsFill
            // 
            this.chbIsFill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chbIsFill.AutoSize = true;
            this.chbIsFill.Enabled = false;
            this.chbIsFill.Location = new System.Drawing.Point(3, 103);
            this.chbIsFill.Name = "chbIsFill";
            this.chbIsFill.Size = new System.Drawing.Size(194, 16);
            this.chbIsFill.TabIndex = 14;
            this.chbIsFill.Text = "Is Fill";
            this.chbIsFill.UseVisualStyleBackColor = true;
            // 
            // palEditorButton
            // 
            this.palEditorButton.Controls.Add(this.button1);
            this.palEditorButton.Controls.Add(this.btnInstructionDelete);
            this.palEditorButton.Controls.Add(this.btnInstructionSave);
            this.palEditorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palEditorButton.Location = new System.Drawing.Point(3, 423);
            this.palEditorButton.Name = "palEditorButton";
            this.palEditorButton.Size = new System.Drawing.Size(401, 61);
            this.palEditorButton.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(5, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 25);
            this.button1.TabIndex = 17;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInstructionDelete
            // 
            this.btnInstructionDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstructionDelete.Location = new System.Drawing.Point(278, 33);
            this.btnInstructionDelete.Name = "btnInstructionDelete";
            this.btnInstructionDelete.Size = new System.Drawing.Size(120, 25);
            this.btnInstructionDelete.TabIndex = 16;
            this.btnInstructionDelete.Text = "Delete";
            this.btnInstructionDelete.UseVisualStyleBackColor = true;
            // 
            // btnInstructionSave
            // 
            this.btnInstructionSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstructionSave.Location = new System.Drawing.Point(152, 34);
            this.btnInstructionSave.Name = "btnInstructionSave";
            this.btnInstructionSave.Size = new System.Drawing.Size(120, 25);
            this.btnInstructionSave.TabIndex = 15;
            this.btnInstructionSave.Text = "Save";
            this.btnInstructionSave.UseVisualStyleBackColor = true;
            this.btnInstructionSave.Click += new System.EventHandler(this.btnInstructionSave_Click);
            // 
            // tapInstruction_Script
            // 
            this.tapInstruction_Script.Location = new System.Drawing.Point(4, 22);
            this.tapInstruction_Script.Name = "tapInstruction_Script";
            this.tapInstruction_Script.Padding = new System.Windows.Forms.Padding(3);
            this.tapInstruction_Script.Size = new System.Drawing.Size(776, 514);
            this.tapInstruction_Script.TabIndex = 1;
            this.tapInstruction_Script.Text = "Script Create";
            this.tapInstruction_Script.UseVisualStyleBackColor = true;
            // 
            // frmInstructionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tbcForm);
            this.Controls.Add(this.ssFormMessage);
            this.Name = "frmInstructionEditor";
            this.Text = "Sorter Instruction editor";
            this.Load += new System.EventHandler(this.frmInstructionEditor_Load);
            this.tbcForm.ResumeLayout(false);
            this.tapInstruction_Create.ResumeLayout(false);
            this.spctInstructionForm.Panel1.ResumeLayout(false);
            this.spctInstructionForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spctInstructionForm)).EndInit();
            this.spctInstructionForm.ResumeLayout(false);
            this.gbCreateForm.ResumeLayout(false);
            this.tlpInstruction.ResumeLayout(false);
            this.gbInstructionEditor.ResumeLayout(false);
            this.spctEditor.Panel1.ResumeLayout(false);
            this.spctEditor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spctEditor)).EndInit();
            this.spctEditor.ResumeLayout(false);
            this.tlpEditorLeft.ResumeLayout(false);
            this.tlpEditorRight.ResumeLayout(false);
            this.tlpEditorRight.PerformLayout();
            this.palEditorData.ResumeLayout(false);
            this.palEditorData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudParameterOrder)).EndInit();
            this.palEditorValue.ResumeLayout(false);
            this.tlpValueSet.ResumeLayout(false);
            this.tlpValueSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMAXValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMINValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValueLength)).EndInit();
            this.palEditorButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssFormMessage;
        private System.Windows.Forms.TabControl tbcForm;
        private System.Windows.Forms.TabPage tapInstruction_Create;
        private System.Windows.Forms.TabPage tapInstruction_Script;
        private System.Windows.Forms.SplitContainer spctInstructionForm;
        private System.Windows.Forms.GroupBox gbCreateForm;
        private System.Windows.Forms.GroupBox gbInstructionEditor;
        private System.Windows.Forms.SplitContainer spctEditor;
        private System.Windows.Forms.TableLayoutPanel tlpEditorLeft;
        private System.Windows.Forms.Label lbCreate_12;
        private System.Windows.Forms.ListBox lsbCreateCommand;
        private System.Windows.Forms.Label lbCreate_04;
        private System.Windows.Forms.ListBox lsbCreateCommandParameter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCommandAction;
        private System.Windows.Forms.TableLayoutPanel tlpEditorRight;
        private System.Windows.Forms.Panel palEditorData;
        private System.Windows.Forms.TextBox txbEditorParameterData;
        private System.Windows.Forms.Label lbCreate_05;
        private System.Windows.Forms.NumericUpDown nudParameterOrder;
        private System.Windows.Forms.Label lbCreate_06;
        private System.Windows.Forms.Label lbCreate_07;
        private System.Windows.Forms.TextBox txbParameterDescription;
        private System.Windows.Forms.RadioButton rdbEditorParameterData;
        private System.Windows.Forms.RadioButton rdbEditorParameterValue;
        private System.Windows.Forms.Panel palEditorValue;
        private System.Windows.Forms.TableLayoutPanel tlpValueSet;
        private System.Windows.Forms.Label lbCreate_08;
        private System.Windows.Forms.Label lbCreate_09;
        private System.Windows.Forms.NumericUpDown nudMAXValue;
        private System.Windows.Forms.NumericUpDown nudMINValue;
        private System.Windows.Forms.NumericUpDown nudDefaultValue;
        private System.Windows.Forms.Label lbCreate_10;
        private System.Windows.Forms.NumericUpDown nudValueLength;
        private System.Windows.Forms.Label lbCreate_11;
        private System.Windows.Forms.CheckBox chbIsFill;
        private System.Windows.Forms.Panel palEditorButton;
        private System.Windows.Forms.Button btnInstructionDelete;
        private System.Windows.Forms.Button btnInstructionSave;
        private System.Windows.Forms.TableLayoutPanel tlpInstruction;
        private System.Windows.Forms.Label lbCreate_01;
        private System.Windows.Forms.ListBox lsbCreateEqpType;
        private System.Windows.Forms.Label lbCreate_02;
        private System.Windows.Forms.ListBox lsbCreateEqpSupplier;
        private System.Windows.Forms.Label lbCreate_03;
        private System.Windows.Forms.ListBox lsbCreateCommandType;
        private System.Windows.Forms.TextBox txbParameterID;
        private System.Windows.Forms.Button button1;
    }
}

