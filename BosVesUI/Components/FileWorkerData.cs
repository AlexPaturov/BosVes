// Получаю список файлов в одной из статически заданных
// директорий и в поддиректориях.

namespace app03.Data
{
    public class FileWorkerData
    {
        private readonly IWebHostEnvironment _environment;
        // add logging
        private readonly string? _fullLogsPath = "\\logs\\";
        private readonly string? _projLogPath = "\\logs\\"; // путь к файлам лога относительно 
        //private readonly string? _webRootPath = "\\logs";
        //private readonly string? _enotherPath = "\\?";
        private List<string> _listPathes;

        public FileWorkerData(IWebHostEnvironment environment)
        {
            _environment = environment;
            _fullLogsPath = _environment.ContentRootPath + _fullLogsPath;
            // _webRootPath = _environment.WebRootPath + _webRootPath;
            _listPathes = new List<string>();
        }

        // получаю список файлов по пути хранения логов в .
        public List<string> GetListLogFiles() 
        {
            if(!string.IsNullOrEmpty(_fullLogsPath))
                ProcessDirectory(_fullLogsPath);

            return (_listPathes.Count > 0) ? new List<string>(_listPathes) : _listPathes;
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public void ProcessFile(string path)
        {
            _listPathes.Add(_projLogPath + Path.GetFileName(path));
            //Console.WriteLine("Processed file '{0}'.", path);
        }
    }
}
