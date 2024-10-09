namespace TestAPI_TEE.Model
{
    public class GuestEntity: CommonEntity
    {
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string[] PhoneNumbers { get; set; }
    }
}
