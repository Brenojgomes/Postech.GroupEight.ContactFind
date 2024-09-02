namespace Postech.GroupEight.ContactFind.Core.Entities
{
    public class ContactEntity : EntityBase
    {
        public string AreaCode { get; set; }
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
