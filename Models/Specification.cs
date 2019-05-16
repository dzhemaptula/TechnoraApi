namespace TechnoraApi.Models
{
    public class Specification
    {
        #region Properties
        public int Id { get; set; }

        public string Description { get; set; }
        public string Type { get; set; }
        #endregion

        #region Constructors
        public Specification(string description)
        {
            Description = description;
        }

        public Specification(string description, string type) : this(description)
        {

            Type = type;
        }
    }
    #endregion
}