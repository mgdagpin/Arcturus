using Arcturus.Domain;
using Arcturus.Domain.Entities;
using TasqR;

namespace Arcturus.Commands.UsersCmds
{
    public class NewUserCmd : ITasq<User>
    {
        public NewUserCmd(string firstName, string lastName, string middleName, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public Gender Gender { get; }
    }
}
