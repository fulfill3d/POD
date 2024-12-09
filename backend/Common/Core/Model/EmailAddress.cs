namespace POD.Common.Core.Model
{
    public class EmailAddress
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public EmailAddress(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}