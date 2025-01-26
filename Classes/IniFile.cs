using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TDF.Net.Classes
{
    public class IniFile
    {
        private readonly string path;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder value, int size, string filePath);

        public IniFile(string iniPath)
        {
            path = iniPath;
        }

        // Method to read a specific key within a section
        public string Read(string section, string key, string defaultValue = "")
        {
            var result = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, result, 255, path);
            return result.ToString();
        }

        public List<string> ReadSectionValues(string sectionName)
        {
            List<string> values = new List<string>();
            bool isInSection = false;

            foreach (var line in File.ReadLines(path))
            {
                string trimmedLine = line.Trim();

                // Check if we've reached the target section
                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    isInSection = trimmedLine.Equals($"[{sectionName}]", StringComparison.OrdinalIgnoreCase);
                    continue;
                }

                // If we're inside the section, add each non-empty line as a value
                if (isInSection && !string.IsNullOrWhiteSpace(trimmedLine))
                {
                    values.Add(trimmedLine);
                }
            }

            return values;
        }

    }
}
