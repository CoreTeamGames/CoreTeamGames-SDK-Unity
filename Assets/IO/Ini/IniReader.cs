using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreTeamGamesSDK.Parsers.Ini
{
    public static class IniReader
    {
        static string _exe = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public static string Read(string pathToFile, string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? _exe, Key, "", RetVal, 255, pathToFile);
            return RetVal.ToString();
        }

        public static bool KeyExists(string Key, string pathToFile, string Section = null)
        {
            return Read(Key, pathToFile, Section) != "";
        }
    }
}