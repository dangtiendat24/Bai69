using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // Phần a: Tính số dòng, số kí tự, số từ trong file Program.cs
        string filePath = "Program.cs";
        char charToCount = 'a';

        Console.WriteLine($"Số dòng: {CountLines(filePath)}");
        Console.WriteLine($"Số kí tự '{charToCount}': {CountChar(filePath, charToCount)}");
        Console.WriteLine($"Số từ: {CountWords(filePath)}");

        // Phần b: Đọc file UTF-8 và ghi sang file UTF-8 khác
        string readUtf8Path = "vd1_read_utf8.txt";
        string writeUtf8Path = "vd1_ghi_utf8.txt";
        CopyFileUtf8(readUtf8Path, writeUtf8Path);

        // Phần c: Đọc file UTF-16 và ghi sang file UTF-16 khác
        string readUtf16Path = "vd1_read_utf16.txt";
        string writeUtf16Path = "vd1_ghi_utf16.txt";
        CopyFileUtf16(readUtf16Path, writeUtf16Path);

        // Phần d: Ghi và đọc file nhị phân
        double[,] arrayToWrite = {
            { 1.1, 2.2, 3.3 },
            { 4.4, 5.5, 6.6 }
        };

        string binaryFilePath = "a2d.dat";

        WriteDoubleArrayToBinaryFile(binaryFilePath, arrayToWrite);
        double[,] arrayRead = ReadDoubleArrayFromBinaryFile(binaryFilePath, 2, 3);

        // Hiển thị mảng đọc từ file nhị phân
        for (int i = 0; i < arrayRead.GetLength(0); i++)
        {
            for (int j = 0; j < arrayRead.GetLength(1); j++)
            {
                Console.Write(arrayRead[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // Phần a
    static int CountLines(string filePath)
    {
        return File.ReadAllLines(filePath).Length;
    }

    static int CountChar(string filePath, char charToCount)
    {
        int count = 0;
        foreach (char c in File.ReadAllText(filePath))
        {
            if (c == charToCount)
            {
                count++;
            }
        }
        return count;
    }

    static int CountWords(string filePath)
    {
        string text = File.ReadAllText(filePath);
        string[] words = Regex.Split(text, @"\s+");
        return words.Length;
    }

    // Phần b
    static void CopyFileUtf8(string readPath, string writePath)
    {
        string content = File.ReadAllText(readPath, Encoding.UTF8);
        File.WriteAllText(writePath, content, Encoding.UTF8);
    }

    // Phần c
    static void CopyFileUtf16(string readPath, string writePath)
    {
        string content = File.ReadAllText(readPath, Encoding.Unicode);
        File.WriteAllText(writePath, content, Encoding.Unicode);
    }

    // Phần d
    static void WriteDoubleArrayToBinaryFile(string filePath, double[,] array)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    writer.Write(array[i, j]);
                }
            }
        }
    }

    static double[,] ReadDoubleArrayFromBinaryFile(string filePath, int rows, int cols)
    {
        double[,] array = new double[rows, cols];

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = reader.ReadDouble();
                }
            }
        }

        return array;
    }
}
