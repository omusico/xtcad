﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace CAD
{
    public class IniFile
    {
        public string path;     //INI文件名
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //声明读写INI文件的API函数     
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        //类的构造函数，传递INI文件名
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        //读INI文件         
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(256);
            int i = GetPrivateProfileString(Section, Key, "", temp, 256, this.path);
            return temp.ToString();
        }

        public static IniFile getIniFile(string path)
        {
            string inipath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, path);
            if (!File.Exists(inipath))
            {
                FileStream fs = File.Create(inipath);
                fs.Close();
            }
            return new IniFile(inipath);
        }
    }
}
