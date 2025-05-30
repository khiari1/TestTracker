using System.IO;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Core.Services
{
    public class FileManager
    {
        private readonly IFileRepository _fileRepository;

        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void Save(string directory, string folder,int? objectId, string fileName,  string? userId, MemoryStream stream)
        {
            var directoryResult =  CreateDirectoryIfNotExiste(directory, folder, objectId?.ToString());

            var path = Path.Combine(directoryResult, fileName);

            var extension = Path.GetExtension(fileName);

            var bytes = WriteFile(path, stream);
                
            _fileRepository.Create(new Attachement()
            {
                FileName = fileName,
                Folder = folder,
                ObjectId = objectId,
                Data = stream.ToArray(),
                Date = DateTime.UtcNow,
                UserId = userId,
                FileSize = bytes.Length,
                Extension = extension,
            });

            _fileRepository.Save();
        }

        public void Save(string directory, string fileName, MemoryStream stream) =>
            Save(directory, null ,null, fileName,  null, stream);

        public void SaveAvatar(string directory, string fileName, MemoryStream stream) =>
            Save(directory, "Avatar", null, fileName,  null, stream);
        public static byte[] WriteFile(string path,Stream stream)
        {
            byte[] bytes = new byte[stream.Length];

            using var fileToCreate = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            stream.Seek(0, SeekOrigin.Begin);

            stream.CopyTo(fileToCreate);

            stream.Write(bytes);

            return bytes;
        }

        public void Delete(string folder, int objectId, string fileName)
        {
            var file = _fileRepository.GetFile(fileName, folder, objectId);
            if (file == null)
                throw new Exception($"Can not find file with name {fileName}");
            _fileRepository.Delete(file.Id);
            _fileRepository.Save();
        }

        public Attachement? GetFile(string folder, int objectId, string fileName)
        {
            return _fileRepository.GetFile(fileName, folder, objectId);
        }

        public IEnumerable<Attachement> GetFiles(string folder, int objectId)
        {
            return _fileRepository.GetFiles(folder, objectId);
        }

        public string CreateDirectoryIfNotExiste(params string[] folder)
        {
            var folders = folder.Where(x => x != null).ToArray();

            var directory = Path.Combine(folders);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
