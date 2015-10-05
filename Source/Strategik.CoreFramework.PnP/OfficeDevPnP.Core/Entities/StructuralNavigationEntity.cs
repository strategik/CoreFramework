namespace OfficeDevPnP.Core.Entities
{
    public class StructuralNavigationEntity
    {
        public StructuralNavigationEntity()
        {
            MaxDynamicItems = 20;
            ShowSubsites = true;
            ShowPages = false;
        }

        public bool ManagedNavigation { get; set; }

        public bool ShowSubsites { get; set; }

        public bool ShowPages { get; set; }

        public int MaxDynamicItems { get; set; }

        public bool ShowSiblings { get; set; }
    }
}