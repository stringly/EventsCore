using EventsCore.Domain.Exceptions.EventAggregate;

namespace EventsCore.Domain.Entities.EventAggregate
{
    public class Registrant
    {
        private Registrant() { }
        public Registrant(int userId, string userName, string email, string contact)
        {
            if (userId == 0)
            {
                throw new RegistrantInvalidException("Cannot create RegisteredUser: invalid parameter value", nameof(userId));
            }
            else if (string.IsNullOrEmpty(userName))
            {
                throw new RegistrantInvalidException("Cannot create RegisteredUser: invalid parameter value", nameof(userName));
            }
            else if (string.IsNullOrEmpty(email))
            {
                throw new RegistrantInvalidException("Cannot create RegisteredUser: invalid parameter value", nameof(email));
            }
            UserId = userId;
            UserName = userName;
            Email = email;
            Contact = contact;
        }
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Contact { get; private set; }        
    }
}
