
using System.Reflection;
using System.Reflection.Metadata;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Core.Services
{
    public class ProjectFileService
    {
        private readonly IAssemblyRepository _assemblyRepository;

        public ProjectFileService(IAssemblyRepository assemblyRepository)
        {
            _assemblyRepository = assemblyRepository;
        }
        public async Task CreateAsync(ProjectFile assemblyFile)
        {
            var projectFile = (await _assemblyRepository.GetAsync())
                .FirstOrDefault();
            if (projectFile == null)
            {
                _assemblyRepository.Create(assemblyFile);
            }
            else
            {
                assemblyFile.Data = assemblyFile.Data;
                assemblyFile.FileName = assemblyFile.FileName;
                assemblyFile.ProjectName = assemblyFile.ProjectName;
                assemblyFile.Size = assemblyFile.Size;
                _assemblyRepository.Update(assemblyFile);
            }
            await _assemblyRepository.SaveAsync();
        }

        public async Task<ProjectFile> GetAsync()
        {
            return (await _assemblyRepository.GetAsync()).FirstOrDefault();
        }
        public async Task DeleteAsync()
        {
            var projectFile = await GetAsync();
            if (projectFile != null)
            {
                _assemblyRepository.Delete(projectFile.Id);
            }
            await _assemblyRepository.SaveAsync();
        }

    }

}


