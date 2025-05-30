namespace Tsi.Erp.TestTracker.Domain.Stores
{


    public class GraphMembersResponse
    {
        public List<Members> Value { get; set; }
    }

    public class Members
    {
        public string DisplayName { get; set; }
        public Dictionary<string, string> Properties { get; set; }

        public bool HasProjectAccess(string projectName)
        {
            if (Properties.TryGetValue("Project", out var projectValue))
            {
                return projectValue.Equals(projectName, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }

}
