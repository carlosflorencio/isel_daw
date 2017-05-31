namespace API.TransferModels.InputModels
{
    public class TeacherDTO
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}