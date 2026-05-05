namespace RailFenceCipher;

/// <summary>
/// Главная форма приложения Rail Fence Cipher.
/// Обеспечивает: шифрование, дешифрование, копирование результата
/// и визуализацию матрицы-забора в виде цветной «сетки».
/// </summary>
public partial class Form1 : Form
{
    // Цвета для рельсов в визуализации (до 20 рельсов)
    private static readonly Color[] RailColors =
    [
        Color.FromArgb(50,  220, 100),  // rail 0 — зелёный
        Color.FromArgb(80,  180, 255),  // rail 1 — голубой
        Color.FromArgb(255, 180,  50),  // rail 2 — жёлтый
        Color.FromArgb(255,  90,  90),  // rail 3 — красный
        Color.FromArgb(200, 100, 255),  // rail 4 — фиолетовый
        Color.FromArgb(100, 230, 220),  // rail 5 — бирюзовый
        Color.FromArgb(255, 140,  60),  // rail 6 — оранжевый
        Color.FromArgb(160, 255, 120),  // rail 7 — светло-зелёный
        Color.FromArgb(255, 100, 180),  // rail 8 — розовый
        Color.FromArgb(120, 200, 255),  // rail 9
        Color.FromArgb(255, 220, 100),  // rail 10
        Color.FromArgb(180, 255, 200),  // rail 11
        Color.FromArgb(255, 160, 160),  // rail 12
        Color.FromArgb(160, 160, 255),  // rail 13
        Color.FromArgb(200, 255, 180),  // rail 14
        Color.FromArgb(255, 200, 140),  // rail 15
        Color.FromArgb(140, 220, 255),  // rail 16
        Color.FromArgb(255, 140, 220),  // rail 17
        Color.FromArgb(180, 255, 240),  // rail 18
        Color.FromArgb(255, 240, 140),  // rail 19
    ];

    public Form1()
    {
        InitializeComponent();
        WireEvents();
    }

    // ─────────────────────────────────────────────────────────────────
    //  Подключение событий
    // ─────────────────────────────────────────────────────────────────
    private void WireEvents()
    {
        btnEncrypt.Click += BtnEncrypt_Click;
        btnDecrypt.Click += BtnDecrypt_Click;
        btnClear.Click   += BtnClear_Click;
        btnCopy.Click    += BtnCopy_Click;

        // Hover-эффекты для кнопок
        AttachHoverEffect(btnEncrypt, Color.FromArgb(0, 80,  180), Color.FromArgb(0,  102, 204));
        AttachHoverEffect(btnDecrypt, Color.FromArgb(0, 110,  60), Color.FromArgb(0,  140,  80));
        AttachHoverEffect(btnClear,   Color.FromArgb(120, 120, 130), Color.FromArgb(160, 160, 168));
        AttachHoverEffect(btnCopy,    Color.FromArgb(60,  60,  70),  Color.FromArgb(90,  90, 100));

        // Чистим статус при вводе
        txtInput.TextChanged += (_, _) => SetStatus("Готов к работе", false);
        nudRails.ValueChanged += (_, _) => SetStatus("Готов к работе", false);
    }

    // ─────────────────────────────────────────────────────────────────
    //  Обработчики кнопок
    // ─────────────────────────────────────────────────────────────────
    private void BtnEncrypt_Click(object? sender, EventArgs e)
    {
        if (!TryGetInput(out string text, out int rails)) return;
        try
        {
            string encrypted = RailFenceCipherLogic.Encrypt(text, rails);
            txtResult.Text = encrypted;
            DrawFence(text, rails);
            SetStatus($"✔  Зашифровано: {text.Length} символов, {rails} рельса(-ов)", false);
        }
        catch (Exception ex) { ShowError(ex.Message); }
    }

    private void BtnDecrypt_Click(object? sender, EventArgs e)
    {
        if (!TryGetInput(out string text, out int rails)) return;
        try
        {
            string decrypted = RailFenceCipherLogic.Decrypt(text, rails);
            txtResult.Text = decrypted;
            DrawFence(text, rails);   // показываем забор входного (зашифрованного) текста
            SetStatus($"✔  Расшифровано: {text.Length} символов, {rails} рельса(-ов)", false);
        }
        catch (Exception ex) { ShowError(ex.Message); }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        txtInput.Clear();
        txtResult.Clear();
        rtbFence.Clear();
        SetStatus("Поля очищены", false);
        txtInput.Focus();
    }

    private void BtnCopy_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtResult.Text))
        {
            SetStatus("⚠  Нечего копировать — поле результата пусто", true);
            return;
        }
        Clipboard.SetText(txtResult.Text);
        SetStatus("✔  Результат скопирован в буфер обмена", false);
    }

    // ─────────────────────────────────────────────────────────────────
    //  Валидация ввода
    // ─────────────────────────────────────────────────────────────────
    private bool TryGetInput(out string text, out int rails)
    {
        text  = txtInput.Text;
        rails = (int)nudRails.Value;

        if (string.IsNullOrEmpty(text))
        {
            SetStatus("⚠  Введите текст перед выполнением операции", true);
            txtInput.Focus();
            text = string.Empty;
            return false;
        }
        return true;
    }

    // ─────────────────────────────────────────────────────────────────
    //  Визуализация матрицы-забора (цветная)
    // ─────────────────────────────────────────────────────────────────
    private void DrawFence(string text, int rails)
    {
        rtbFence.Clear();
        rtbFence.SuspendLayout();

        int effectiveRails = (rails >= text.Length) ? text.Length : rails;
        char[][] fence = RailFenceCipherLogic.BuildFence(text, effectiveRails);

        // Заголовок
        AppendColored(rtbFence, "  Визуализация Rail Fence", Color.White, bold: true);
        AppendColored(rtbFence, $"   [ {effectiveRails} рельса(-ов), {text.Length} символов ]\n\n",
                      Color.FromArgb(160, 160, 170));

        // Строка-индекс позиций (каждые 5)
        AppendColored(rtbFence, "Позиция:  ", Color.FromArgb(120, 120, 130));
        for (int i = 0; i < text.Length; i++)
        {
            string marker = (i % 5 == 0) ? $"{i,-2}" : "  ";
            AppendColored(rtbFence, marker, Color.FromArgb(100, 100, 110));
        }
        AppendColored(rtbFence, "\n", Color.White);

        // Строки рельсов
        for (int r = 0; r < effectiveRails; r++)
        {
            Color railColor = RailColors[r % RailColors.Length];

            // Метка рельса
            string railLabel = $"Rail {r,2}:  ";
            AppendColored(rtbFence, railLabel, railColor, bold: true);

            // Ячейки
            for (int i = 0; i < text.Length; i++)
            {
                char ch = fence[r][i];
                if (ch == '\0')
                {
                    // Пустая ячейка — серая точка
                    AppendColored(rtbFence, "· ", Color.FromArgb(55, 55, 65));
                }
                else
                {
                    // Символ — цвет рельса
                    AppendColored(rtbFence, $"{ch} ", railColor, bold: true);
                }
            }
            AppendColored(rtbFence, "\n", Color.White);
        }

        // Итоговая строка «считанных» символов (результат шифрования)
        AppendColored(rtbFence, "\nРезультат:  ", Color.FromArgb(160, 160, 170));
        for (int r = 0; r < effectiveRails; r++)
        {
            Color c = RailColors[r % RailColors.Length];
            foreach (char ch in fence[r])
                if (ch != '\0')
                    AppendColored(rtbFence, ch.ToString(), c, bold: true);
        }
        AppendColored(rtbFence, "\n", Color.White);

        rtbFence.ResumeLayout();
    }

    /// <summary>Добавляет цветной текст в RichTextBox.</summary>
    private static void AppendColored(RichTextBox rtb, string text,
                                      Color color, bool bold = false)
    {
        int start = rtb.TextLength;
        rtb.AppendText(text);
        rtb.Select(start, text.Length);
        rtb.SelectionColor = color;
        rtb.SelectionFont  = new Font(rtb.Font,
                                      bold ? FontStyle.Bold : FontStyle.Regular);
        rtb.SelectionLength = 0;
    }

    // ─────────────────────────────────────────────────────────────────
    //  Вспомогательные методы
    // ─────────────────────────────────────────────────────────────────
    private void SetStatus(string msg, bool isWarning)
    {
        lblStatus.Text      = msg;
        lblStatus.ForeColor = isWarning
            ? Color.FromArgb(200, 60, 60)
            : Color.FromArgb(60, 120, 60);
    }

    private void ShowError(string msg)
    {
        SetStatus($"✖  Ошибка: {msg}", true);
        MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private static void AttachHoverEffect(Button btn, Color hoverColor, Color normalColor)
    {
        btn.MouseEnter += (_, _) => btn.BackColor = hoverColor;
        btn.MouseLeave += (_, _) => btn.BackColor = normalColor;
    }
}
