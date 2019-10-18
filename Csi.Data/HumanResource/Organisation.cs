namespace Csi.Data.HumanResource
{
    public enum GroupType 
    {
        ORGANISATION,
        DEPARTMENT,
        GROUP
    }

    public class Group
    {
        public string Name { get; set; }
        protected GroupType GroupType { get; set; }

        public Group()
        {
            this.GroupType = GroupType.GROUP;
        }
    }

    public class Organisation : Group
    {
        public Organisation()
        {
            this.GroupType = GroupType.ORGANISATION;
        }

        public Organisation(string name) : this()
        {
            this.Name = name;
        }
    }
}