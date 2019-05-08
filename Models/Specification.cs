namespace TechnoraApi.Models
{
    public class Specification
    {
        #region Properties
        public int Id { get; set; }

        public string Description { get; set; }
        #endregion

        #region Constructors
        public Specification(string description)
        {
            Description = description;
        }
        #endregion
    }
}