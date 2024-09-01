using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreTeamGamesSDK.Parsers.Ini
{
    public static class IniWriter
    {
        static string _exe = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public static void Write(string path, string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? _exe, Key, Value, path);
        }

        public static void DeleteKey(string path, string Key, string Section = null)
        {
            Write(path, Key, null, Section ?? _exe);
        }

        public static void DeleteSection(string path, string Section = null)
        {
            Write(path, null, null, Section ?? _exe);
        }
    }
}