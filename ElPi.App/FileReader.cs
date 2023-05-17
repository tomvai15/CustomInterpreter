namespace ElPi.App
{
    public static class FileReader
    {
        public static string ReadFile(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}
