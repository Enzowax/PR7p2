namespace RailFenceCipher;

/// <summary>
/// Реализация шифра Билайна (Rail Fence / Zigzag cipher).
/// Текст записывается зигзагом по заданному числу «рельсов»,
/// затем читается построчно.
/// </summary>
public static class RailFenceCipherLogic
{
    /// <summary>
    /// Шифрует текст методом Rail Fence.
    /// </summary>
    /// <param name="text">Исходный текст.</param>
    /// <param name="rails">Количество строк (рельсов). Должно быть >= 2.</param>
    /// <returns>Зашифрованная строка.</returns>
    /// <exception cref="ArgumentNullException">Если text равен null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если rails меньше 2.</exception>
    public static string Encrypt(string text, int rails)
    {
        if (text is null) throw new ArgumentNullException(nameof(text));
        if (rails < 2) throw new ArgumentOutOfRangeException(nameof(rails), "Число рельсов должно быть не менее 2.");
        if (text.Length == 0) return string.Empty;
        if (rails >= text.Length) return text;

        var fence = BuildFence(text, rails);
        return string.Concat(fence.Select(row => new string(row.Where(c => c != '\0').ToArray())));
    }

    /// <summary>
    /// Дешифрует текст, зашифрованный методом Rail Fence.
    /// </summary>
    /// <param name="cipher">Зашифрованный текст.</param>
    /// <param name="rails">Количество строк (рельсов). Должно совпадать с использованным при шифровании.</param>
    /// <returns>Исходная строка.</returns>
    /// <exception cref="ArgumentNullException">Если cipher равен null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если rails меньше 2.</exception>
    public static string Decrypt(string cipher, int rails)
    {
        if (cipher is null) throw new ArgumentNullException(nameof(cipher));
        if (rails < 2) throw new ArgumentOutOfRangeException(nameof(rails), "Число рельсов должно быть не менее 2.");
        if (cipher.Length == 0) return string.Empty;
        if (rails >= cipher.Length) return cipher;

        int len = cipher.Length;
        // Определяем, какой символ в какую строку попадает
        int[] rowIndex = GetRowIndices(len, rails);

        // Вычисляем длины каждой строки
        int[] rowLengths = new int[rails];
        foreach (int r in rowIndex) rowLengths[r]++;

        // Распределяем символы шифртекста по строкам
        char[][] fence = new char[rails][];
        int pos = 0;
        for (int r = 0; r < rails; r++)
        {
            fence[r] = cipher.Skip(pos).Take(rowLengths[r]).ToArray();
            pos += rowLengths[r];
        }

        // Читаем зигзагом
        int[] rowPos = new int[rails];
        char[] result = new char[len];
        for (int i = 0; i < len; i++)
        {
            int row = rowIndex[i];
            result[i] = fence[row][rowPos[row]++];
        }
        return new string(result);
    }

    /// <summary>
    /// Строит матрицу-забор для визуализации заполнения.
    /// Незаполненные ячейки содержат '\0'.
    /// </summary>
    /// <param name="text">Исходный текст.</param>
    /// <param name="rails">Количество рельсов.</param>
    /// <returns>Двумерный массив char[rails][text.Length].</returns>
    public static char[][] BuildFence(string text, int rails)
    {
        if (text is null) throw new ArgumentNullException(nameof(text));
        if (rails < 2) throw new ArgumentOutOfRangeException(nameof(rails));

        char[][] fence = new char[rails][];
        for (int r = 0; r < rails; r++)
            fence[r] = new char[text.Length];

        int[] indices = GetRowIndices(text.Length, rails);
        for (int i = 0; i < text.Length; i++)
            fence[indices[i]][i] = text[i];

        return fence;
    }

    // Возвращает номер рельса для каждой позиции зигзага
    private static int[] GetRowIndices(int length, int rails)
    {
        int[] indices = new int[length];
        int row = 0;
        int step = 1;          // +1 = вниз, -1 = вверх
        for (int i = 0; i < length; i++)
        {
            indices[i] = row;
            if (row == rails - 1) step = -1;
            else if (row == 0)    step = 1;
            row += step;
        }
        return indices;
    }
}
