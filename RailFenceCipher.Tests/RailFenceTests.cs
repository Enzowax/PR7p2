namespace RailFenceCipher.Tests;

/// <summary>
/// Автоматизированные тесты для модулей шифрования и дешифрования
/// шифра Билайна (Rail Fence / Zigzag).
///
/// Тестовые сценарии (TDD — составлены до разработки приложения):
///
/// === ПОЗИТИВНЫЕ СЦЕНАРИИ ===
/// TC-01: Шифрование с 2 рельсами — базовый случай
/// TC-02: Шифрование с 3 рельсами
/// TC-03: Шифрование с 4 рельсами
/// TC-04: Обратимость: Decrypt(Encrypt(text)) == text, 2 рельса
/// TC-05: Обратимость: Decrypt(Encrypt(text)) == text, 3 рельса
/// TC-06: Обратимость: Decrypt(Encrypt(text)) == text, 4 рельса
/// TC-07: Пустая строка → пустая строка (шифрование)
/// TC-08: Пустая строка → пустая строка (дешифрование)
/// TC-09: Число рельсов >= длины текста → текст без изменений
/// TC-10: Текст из одного символа
/// TC-11: Текст с пробелами и знаками препинания
/// TC-12: Текст на кириллице
/// TC-13: Текст смешанного регистра
///
/// === НЕГАТИВНЫЕ СЦЕНАРИИ ===
/// TC-14: null-текст при шифровании → ArgumentNullException
/// TC-15: null-текст при дешифровании → ArgumentNullException
/// TC-16: rails = 1 при шифровании → ArgumentOutOfRangeException
/// TC-17: rails = 0 при шифровании → ArgumentOutOfRangeException
/// TC-18: rails = -1 при шифровании → ArgumentOutOfRangeException
/// TC-19: rails = 1 при дешифровании → ArgumentOutOfRangeException
///
/// === ВИЗУАЛИЗАЦИЯ ===
/// TC-20: BuildFence возвращает корректную матрицу (проверка по ячейкам)
/// TC-21: BuildFence — null-текст → ArgumentNullException
/// TC-22: BuildFence — rails < 2 → ArgumentOutOfRangeException
/// </summary>
[TestClass]
public class RailFenceTests
{
    // ─── TC-01 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-01: Шифрование с 2 рельсами — базовый случай")]
    public void Encrypt_TwoRails_ReturnsExpected()
    {
        // WEAREDISCOVEREDFLEEAATONCE → WECRLTEERDSOEEFEAABORADICVNE (2 rails)
        // Из примеров: "WEAREDISCOVEREDFLEEAATONCE" → "WECRLTEERDSOEEFEAABORADICVNE" при 3 рельсах
        // Для 2 рельсов: "HELLO" → "HLOEL"
        string result = RailFenceCipherLogic.Encrypt("HELLO", 2);
        Assert.AreEqual("HLOEL", result);
    }

    // ─── TC-02 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-02: Шифрование с 3 рельсами")]
    public void Encrypt_ThreeRails_ReturnsExpected()
    {
        // Rail 0 (idx 0,4,8,12,16,20,24): W,E,C,R,L,A,C
        // Rail 1 (idx 1,3,5,7,9,11,13,15,17,19,21,23,25): E,R,D,S,O,E,E,F,E,A,T,N,E
        // Rail 2 (idx 2,6,10,14,18,22): A,I,V,D,E,O
        string result = RailFenceCipherLogic.Encrypt("WEAREDISCOVEREDFLEEAATONCE", 3);
        Assert.AreEqual("WECRLACERDSOEEFEATNEAIVDEO", result);
    }

    // ─── TC-03 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-03: Шифрование с 4 рельсами")]
    public void Encrypt_FourRails_ReturnsExpected()
    {
        string plain = "THEQUICKBROWNFOX";
        string encrypted = RailFenceCipherLogic.Encrypt(plain, 4);
        // Результат должен содержать те же символы, но в другом порядке
        Assert.AreEqual(plain.Length, encrypted.Length);
        CollectionAssert.AreEquivalent(plain.ToCharArray(), encrypted.ToCharArray());
        Assert.AreNotEqual(plain, encrypted);
    }

    // ─── TC-04 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-04: Обратимость — 2 рельса")]
    public void EncryptDecrypt_TwoRails_IsReversible()
    {
        string original = "HELLOWORLD";
        string encrypted = RailFenceCipherLogic.Encrypt(original, 2);
        string decrypted = RailFenceCipherLogic.Decrypt(encrypted, 2);
        Assert.AreEqual(original, decrypted);
        Assert.AreNotEqual(original, encrypted);
    }

    // ─── TC-05 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-05: Обратимость — 3 рельса")]
    public void EncryptDecrypt_ThreeRails_IsReversible()
    {
        string original = "WEAREDISCOVEREDFLEEAATONCE";
        string encrypted = RailFenceCipherLogic.Encrypt(original, 3);
        string decrypted = RailFenceCipherLogic.Decrypt(encrypted, 3);
        Assert.AreEqual(original, decrypted);
    }

    // ─── TC-06 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-06: Обратимость — 4 рельса")]
    public void EncryptDecrypt_FourRails_IsReversible()
    {
        string original = "THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG";
        string encrypted = RailFenceCipherLogic.Encrypt(original, 4);
        string decrypted = RailFenceCipherLogic.Decrypt(encrypted, 4);
        Assert.AreEqual(original, decrypted);
    }

    // ─── TC-07 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-07: Пустая строка → пустая строка (шифрование)")]
    public void Encrypt_EmptyString_ReturnsEmpty()
    {
        string result = RailFenceCipherLogic.Encrypt(string.Empty, 3);
        Assert.AreEqual(string.Empty, result);
    }

    // ─── TC-08 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-08: Пустая строка → пустая строка (дешифрование)")]
    public void Decrypt_EmptyString_ReturnsEmpty()
    {
        string result = RailFenceCipherLogic.Decrypt(string.Empty, 3);
        Assert.AreEqual(string.Empty, result);
    }

    // ─── TC-09 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-09: Число рельсов >= длины текста → текст без изменений")]
    public void Encrypt_RailsGeLength_ReturnsOriginal()
    {
        string text = "AB";
        Assert.AreEqual(text, RailFenceCipherLogic.Encrypt(text, 2));  // rails == len
        Assert.AreEqual(text, RailFenceCipherLogic.Encrypt(text, 5));  // rails > len
    }

    // ─── TC-10 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-10: Текст из одного символа")]
    public void Encrypt_SingleChar_ReturnsSameChar()
    {
        string result = RailFenceCipherLogic.Encrypt("X", 3);
        Assert.AreEqual("X", result);
    }

    // ─── TC-11 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-11: Пробелы и знаки препинания сохраняются")]
    public void EncryptDecrypt_WithSpacesAndPunctuation_IsReversible()
    {
        string original = "Hello, World! 123";
        string encrypted = RailFenceCipherLogic.Encrypt(original, 3);
        string decrypted = RailFenceCipherLogic.Decrypt(encrypted, 3);
        Assert.AreEqual(original, decrypted);
    }

    // ─── TC-12 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-12: Кириллица — шифрование и дешифрование")]
    public void EncryptDecrypt_Cyrillic_IsReversible()
    {
        string original = "ПРИВЕТ МИР";
        string encrypted = RailFenceCipherLogic.Encrypt(original, 3);
        string decrypted = RailFenceCipherLogic.Decrypt(encrypted, 3);
        Assert.AreEqual(original, decrypted);
    }

    // ─── TC-13 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-13: Смешанный регистр сохраняется")]
    public void EncryptDecrypt_MixedCase_IsReversible()
    {
        string original = "HelloWorld";
        string encrypted = RailFenceCipherLogic.Encrypt(original, 2);
        string decrypted = RailFenceCipherLogic.Decrypt(encrypted, 2);
        Assert.AreEqual(original, decrypted);
    }

    // ─── TC-14 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-14: null-текст при шифровании → ArgumentNullException")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Encrypt_NullText_ThrowsArgumentNullException()
    {
        RailFenceCipherLogic.Encrypt(null!, 3);
    }

    // ─── TC-15 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-15: null-текст при дешифровании → ArgumentNullException")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Decrypt_NullText_ThrowsArgumentNullException()
    {
        RailFenceCipherLogic.Decrypt(null!, 3);
    }

    // ─── TC-16 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-16: rails = 1 при шифровании → ArgumentOutOfRangeException")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Encrypt_OneRail_ThrowsArgumentOutOfRangeException()
    {
        RailFenceCipherLogic.Encrypt("HELLO", 1);
    }

    // ─── TC-17 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-17: rails = 0 при шифровании → ArgumentOutOfRangeException")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Encrypt_ZeroRails_ThrowsArgumentOutOfRangeException()
    {
        RailFenceCipherLogic.Encrypt("HELLO", 0);
    }

    // ─── TC-18 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-18: rails = -1 при шифровании → ArgumentOutOfRangeException")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Encrypt_NegativeRails_ThrowsArgumentOutOfRangeException()
    {
        RailFenceCipherLogic.Encrypt("HELLO", -1);
    }

    // ─── TC-19 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-19: rails = 1 при дешифровании → ArgumentOutOfRangeException")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Decrypt_OneRail_ThrowsArgumentOutOfRangeException()
    {
        RailFenceCipherLogic.Decrypt("HELLO", 1);
    }

    // ─── TC-20 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-20: BuildFence строит корректную матрицу")]
    public void BuildFence_ReturnsCorrectMatrix()
    {
        // "HELLO" с 2 рельсами: H на 0, E на 1, L на 0, L на 1, O на 0
        char[][] fence = RailFenceCipherLogic.BuildFence("HELLO", 2);
        Assert.AreEqual(2, fence.Length);
        Assert.AreEqual('H', fence[0][0]);
        Assert.AreEqual('L', fence[0][2]);
        Assert.AreEqual('O', fence[0][4]);
        Assert.AreEqual('E', fence[1][1]);
        Assert.AreEqual('L', fence[1][3]);
    }

    // ─── TC-21 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-21: BuildFence — null → ArgumentNullException")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void BuildFence_NullText_ThrowsArgumentNullException()
    {
        RailFenceCipherLogic.BuildFence(null!, 2);
    }

    // ─── TC-22 ────────────────────────────────────────────────────────────────
    [TestMethod]
    [Description("TC-22: BuildFence — rails < 2 → ArgumentOutOfRangeException")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void BuildFence_InvalidRails_ThrowsArgumentOutOfRangeException()
    {
        RailFenceCipherLogic.BuildFence("HELLO", 1);
    }
}
