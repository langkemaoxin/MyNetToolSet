using System;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MyToolSet
{
    public class FileHelperExtension
    {
        public static string GetLength(double lengthOfDocument)
        {
            if (lengthOfDocument < 1024)
                return Math.Round(lengthOfDocument, 2).ToString(CultureInfo.InvariantCulture) + 'B';

            if (lengthOfDocument > 1024 && lengthOfDocument <= Math.Pow(1024, 2))
                return Math.Round((lengthOfDocument / 1024.0), 2).ToString(CultureInfo.InvariantCulture) + "KB";

            if (lengthOfDocument > Math.Pow(1024, 2) && lengthOfDocument <= Math.Pow(1024, 3))
                return Math.Round((lengthOfDocument / 1024.0 / 1024.0), 2).ToString(CultureInfo.InvariantCulture) + "M";

            return Math.Round((lengthOfDocument / 1024.0 / 1024.0 / 1024.0), 2).ToString(CultureInfo.InvariantCulture) + "GB";
        }

        public static string GetFileBelongs(FileInfo fileInfo)
        {
            try
            {
                FileSecurity fileSecurity = fileInfo.GetAccessControl();
                IdentityReference identityReference = fileSecurity.GetOwner(typeof(NTAccount));
                return identityReference.Value;
            }
            catch (Exception ex)
            {
                return "<UnKnow>";
            }
        }

        public static string GetFileBelongs(string fileInfo)
        {
            if (string.IsNullOrWhiteSpace(fileInfo))
            {
                return "<UnKnow>";
            }

            return GetFileBelongs(new FileInfo(fileInfo));
        }

     
    }
}