using System.IO;

namespace WPF.RandomText
{
    public class TextSelector
    {
        private string filePath;

        public TextSelector(string filePath)
        {
            this.filePath = filePath;
        }

        public string SelectText(string mode)
        {
            if (mode == "line")
            {
                string[] lines = File.ReadAllLines(filePath);
                Random rnd = new();
                int randomLineNumber = rnd.Next(0, lines.Length);
                return lines[randomLineNumber];
            }
            else
            {
                string text = File.ReadAllText(filePath);
                string[]? parts = null;
                if (mode == "semicolon")
                {
                    parts = text.Split(';', '\n');
                }
                else if (mode == "comma")
                {
                    parts = text.Split(',', '\n');
                }
                else if (mode == "pipe")
                {
                    parts = text.Split('|', '\n');
                }
                else if (mode == "backslash")
                {
                    parts = text.Split('\\', '\n');
                }
                else if (mode == "slash")
                {
                    parts = text.Split('/', '\n');
                }
                else if (mode == "exclamation")
                {
                    parts = text.Split('!', '\n');
                }
                else
                {
                    return "��Ч��ģʽѡ����ѡ�� 'line'��'semicolon'��'comma'��'pipe'��'backslash'��'slash' �� 'exclamation'��";
                }
                
                // ȥ���հײ���
                parts = Array.FindAll(parts, p => !string.IsNullOrWhiteSpace(p));

                Random rnd = new();
                int randomPartIndex = rnd.Next(0, parts.Length);
                return parts[randomPartIndex];
            }
        }
    }

    class UUU
    {
        static void Main()
        {
            string filePath = "your_file_path_here.txt"; // �滻Ϊ����ļ�·��

            TextSelector textSelector = new(filePath);

            // ѡ����ģʽ
            string selectedLine = textSelector.SelectText("line");
            Console.WriteLine("Selected Line: " + selectedLine);

            // ѡ��ֺ�ģʽ
            string selectedSemicolonPart = textSelector.SelectText("semicolon");
            Console.WriteLine("Selected Semicolon Part: " + selectedSemicolonPart);

            // ѡ�񶺺�ģʽ
            string selectedCommaPart = textSelector.SelectText("comma");
            Console.WriteLine("Selected Comma Part: " + selectedCommaPart);
        }
    }
}
