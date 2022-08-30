namespace CardStorageService.Controllers.Models
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Email { get; set; }

        public bool Locked { get; set; }

        public string Name { get; set; }

    }
}
