namespace RailFenceCipher;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        // ── Controls ──────────────────────────────────────────────────
        lblTitle        = new Label();
        grpInput        = new GroupBox();
        lblInputText    = new Label();
        txtInput        = new TextBox();
        lblRails        = new Label();
        nudRails        = new NumericUpDown();
        pnlButtons      = new Panel();
        btnEncrypt      = new Button();
        btnDecrypt      = new Button();
        btnClear        = new Button();
        grpResult       = new GroupBox();
        txtResult       = new TextBox();
        btnCopy         = new Button();
        grpFence        = new GroupBox();
        rtbFence        = new RichTextBox();
        lblStatus       = new Label();
        toolTip1        = new ToolTip(components);

        // ── Begin layout ──────────────────────────────────────────────
        SuspendLayout();
        grpInput.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudRails).BeginInit();
        pnlButtons.SuspendLayout();
        grpResult.SuspendLayout();
        grpFence.SuspendLayout();

        // ── Form ──────────────────────────────────────────────────────
        ClientSize     = new Size(880, 700);
        Text           = "Шифр Билайна (Rail Fence) — Вариант 13";
        MinimumSize    = new Size(740, 620);
        StartPosition  = FormStartPosition.CenterScreen;
        Font           = new Font("Segoe UI", 9.5f);
        BackColor      = Color.FromArgb(245, 248, 252);
        AutoScaleMode  = AutoScaleMode.Font;
        AutoScaleDimensions = new SizeF(7F, 15F);
        Name           = "Form1";

        // ── lblTitle ──────────────────────────────────────────────────
        lblTitle.Text      = "Шифр Билайна (Rail Fence Cipher)";
        lblTitle.Font      = new Font("Segoe UI", 14f, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(30, 80, 160);
        lblTitle.Location  = new Point(16, 12);
        lblTitle.Size      = new Size(500, 28);
        lblTitle.AutoSize  = false;

        // ── grpInput ──────────────────────────────────────────────────
        grpInput.Text     = "Ввод";
        grpInput.Location = new Point(12, 48);
        grpInput.Size     = new Size(856, 130);
        grpInput.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpInput.ForeColor = Color.FromArgb(30, 80, 160);
        grpInput.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);

        lblInputText.Text      = "Исходный текст:";
        lblInputText.Location  = new Point(12, 24);
        lblInputText.Size      = new Size(130, 20);
        lblInputText.Font      = new Font("Segoe UI", 9f);
        lblInputText.ForeColor = Color.FromArgb(50, 50, 50);

        txtInput.Multiline    = true;
        txtInput.ScrollBars   = ScrollBars.Vertical;
        txtInput.Location     = new Point(12, 44);
        txtInput.Size         = new Size(830, 68);
        txtInput.Anchor       = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtInput.Font         = new Font("Consolas", 10f);
        txtInput.BorderStyle  = BorderStyle.FixedSingle;
        txtInput.BackColor    = Color.White;
        toolTip1.SetToolTip(txtInput, "Введите текст для шифрования или дешифрования");

        grpInput.Controls.Add(lblInputText);
        grpInput.Controls.Add(txtInput);

        // ── pnlButtons ────────────────────────────────────────────────
        pnlButtons.Location  = new Point(12, 186);
        pnlButtons.Size      = new Size(856, 50);
        pnlButtons.Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlButtons.BackColor = Color.Transparent;

        lblRails.Text      = "Рельсов:";
        lblRails.Location  = new Point(0, 14);
        lblRails.Size      = new Size(68, 20);
        lblRails.Font      = new Font("Segoe UI", 9.5f);
        lblRails.ForeColor = Color.FromArgb(50, 50, 50);

        nudRails.Minimum  = 2;
        nudRails.Maximum  = 20;
        nudRails.Value    = 3;
        nudRails.Location = new Point(72, 10);
        nudRails.Size     = new Size(58, 26);
        nudRails.Font     = new Font("Segoe UI", 9.5f);
        toolTip1.SetToolTip(nudRails, "Число рельсов: от 2 до 20");

        // Encrypt button — синий
        btnEncrypt.Text      = "⇒  Зашифровать";
        btnEncrypt.Location  = new Point(148, 8);
        btnEncrypt.Size      = new Size(148, 34);
        btnEncrypt.BackColor = Color.FromArgb(0, 102, 204);
        btnEncrypt.ForeColor = Color.White;
        btnEncrypt.FlatStyle = FlatStyle.Flat;
        btnEncrypt.FlatAppearance.BorderSize = 0;
        btnEncrypt.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        btnEncrypt.Cursor    = Cursors.Hand;
        toolTip1.SetToolTip(btnEncrypt, "Зашифровать введённый текст (Rail Fence)");

        // Decrypt button — зелёный
        btnDecrypt.Text      = "⇐  Расшифровать";
        btnDecrypt.Location  = new Point(306, 8);
        btnDecrypt.Size      = new Size(148, 34);
        btnDecrypt.BackColor = Color.FromArgb(0, 140, 80);
        btnDecrypt.ForeColor = Color.White;
        btnDecrypt.FlatStyle = FlatStyle.Flat;
        btnDecrypt.FlatAppearance.BorderSize = 0;
        btnDecrypt.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        btnDecrypt.Cursor    = Cursors.Hand;
        toolTip1.SetToolTip(btnDecrypt, "Расшифровать текст из поля «Исходный текст»");

        // Clear button — серый
        btnClear.Text      = "✕  Очистить";
        btnClear.Location  = new Point(464, 8);
        btnClear.Size      = new Size(120, 34);
        btnClear.BackColor = Color.FromArgb(160, 160, 168);
        btnClear.ForeColor = Color.White;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.FlatAppearance.BorderSize = 0;
        btnClear.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        btnClear.Cursor    = Cursors.Hand;
        toolTip1.SetToolTip(btnClear, "Очистить все поля");

        pnlButtons.Controls.Add(lblRails);
        pnlButtons.Controls.Add(nudRails);
        pnlButtons.Controls.Add(btnEncrypt);
        pnlButtons.Controls.Add(btnDecrypt);
        pnlButtons.Controls.Add(btnClear);

        // ── grpResult ─────────────────────────────────────────────────
        grpResult.Text     = "Результат";
        grpResult.Location = new Point(12, 244);
        grpResult.Size     = new Size(856, 110);
        grpResult.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpResult.ForeColor = Color.FromArgb(30, 80, 160);
        grpResult.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);

        txtResult.Multiline   = true;
        txtResult.ScrollBars  = ScrollBars.Vertical;
        txtResult.ReadOnly    = true;
        txtResult.Location    = new Point(12, 22);
        txtResult.Size        = new Size(728, 68);
        txtResult.Anchor      = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtResult.Font        = new Font("Consolas", 10f);
        txtResult.BorderStyle = BorderStyle.FixedSingle;
        txtResult.BackColor   = Color.FromArgb(240, 248, 240);

        btnCopy.Text      = "📋 Копировать";
        btnCopy.Location  = new Point(748, 22);
        btnCopy.Size      = new Size(96, 34);
        btnCopy.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        btnCopy.BackColor = Color.FromArgb(90, 90, 100);
        btnCopy.ForeColor = Color.White;
        btnCopy.FlatStyle = FlatStyle.Flat;
        btnCopy.FlatAppearance.BorderSize = 0;
        btnCopy.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
        btnCopy.Cursor    = Cursors.Hand;
        toolTip1.SetToolTip(btnCopy, "Скопировать результат в буфер обмена");

        grpResult.Controls.Add(txtResult);
        grpResult.Controls.Add(btnCopy);

        // ── grpFence ──────────────────────────────────────────────────
        grpFence.Text     = "Визуализация забора (Rail Fence matrix)";
        grpFence.Location = new Point(12, 362);
        grpFence.Size     = new Size(856, 288);
        grpFence.Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpFence.ForeColor = Color.FromArgb(30, 80, 160);
        grpFence.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);

        rtbFence.ReadOnly    = true;
        rtbFence.ScrollBars  = RichTextBoxScrollBars.Both;
        rtbFence.Location    = new Point(12, 22);
        rtbFence.Size        = new Size(830, 242);
        rtbFence.Anchor      = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        rtbFence.Font        = new Font("Consolas", 11f);
        rtbFence.BackColor   = Color.FromArgb(20, 20, 30);
        rtbFence.ForeColor   = Color.LimeGreen;
        rtbFence.BorderStyle = BorderStyle.None;
        rtbFence.WordWrap    = false;

        grpFence.Controls.Add(rtbFence);

        // ── lblStatus ─────────────────────────────────────────────────
        lblStatus.Text      = "Готов к работе";
        lblStatus.Location  = new Point(12, 658);
        lblStatus.Size      = new Size(856, 20);
        lblStatus.Anchor    = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblStatus.Font      = new Font("Segoe UI", 8.5f);
        lblStatus.ForeColor = Color.FromArgb(100, 100, 110);

        // ── Add controls to Form ──────────────────────────────────────
        Controls.Add(lblTitle);
        Controls.Add(grpInput);
        Controls.Add(pnlButtons);
        Controls.Add(grpResult);
        Controls.Add(grpFence);
        Controls.Add(lblStatus);

        // ── End layout ────────────────────────────────────────────────
        grpFence.ResumeLayout(false);
        grpResult.ResumeLayout(false);
        pnlButtons.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudRails).EndInit();
        grpInput.ResumeLayout(false);
        grpInput.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    // ── Fields ────────────────────────────────────────────────────────
    private Label           lblTitle;
    private GroupBox        grpInput;
    private Label           lblInputText;
    private TextBox         txtInput;
    private Label           lblRails;
    private NumericUpDown   nudRails;
    private Panel           pnlButtons;
    private Button          btnEncrypt;
    private Button          btnDecrypt;
    private Button          btnClear;
    private GroupBox        grpResult;
    private TextBox         txtResult;
    private Button          btnCopy;
    private GroupBox        grpFence;
    private RichTextBox     rtbFence;
    private Label           lblStatus;
    private ToolTip         toolTip1;
}
