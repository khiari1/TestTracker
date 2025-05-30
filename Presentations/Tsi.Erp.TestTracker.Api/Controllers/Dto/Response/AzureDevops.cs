namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class AzureDevops
    {
        public int OrganisationId { get; set; }
        public string Pat { get; set; }
    }
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }

    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }

    public class TestProject
    {
        public string ProjectName { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public int Size { get; set; }
    }

}
