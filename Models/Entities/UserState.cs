using Common.Enums;

namespace Models.Entities
{
    public class UserState
    {
        public int Id { get; set; }

        public StateCodesEnum Code { get; set; }

        public string Description { get; set; }
    }
}