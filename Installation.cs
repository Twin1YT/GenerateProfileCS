using System.Collections.Generic;

namespace GetItems
{
    public class InstallationList
    {
        public string InstallLocation { get; set; }
        public string NamespaceId { get; set; }
        public string ItemId { get; set; }
        public string ArtifactId { get; set; }
        public string AppVersion { get; set; }
        public string AppName { get; set; }
    }

    public class Installation
    {
        public List<InstallationList> InstallationList { get; set; }
    }
}