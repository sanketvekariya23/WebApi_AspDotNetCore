namespace Login_Registor.Providers
{
    public class ConfigProvider
    {
        public static string BaseDirectory => @"D:/Zoopr";
        public static string BaseFolderName => "Zoopr";
        public static string EncryptionKey => "b14ca5898a4e4133bbce2ea2315a1916";
        public static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
            }
        }
    }
}
