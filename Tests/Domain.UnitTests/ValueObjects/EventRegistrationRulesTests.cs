using EventsCore.Domain.Exceptions.ValueObjects;
using EventsCore.Domain.ValueObjects;
using Xunit;

namespace EventsCore.Domain.UnitTests.ValueObjects
{
    public class EventRegistrationRulesTests
    {
        [Fact]
        public void RegistrationRules_Given_Valid_Values_Is_Valid()
        {
            // Arrange
            uint maxRegs = 10;
            uint minRegs = 1;
            uint standbyRegs = 5;

            // Act
            var regRulesNoMinNoStandby = new EventRegistrationRules(maxRegs);
            var regRulesWithMinNoStandby = new EventRegistrationRules(maxRegs, minRegs);
            var regRulesWithMinAndStandby = new EventRegistrationRules(maxRegs, minRegs, standbyRegs);            

            // Assert
            Assert.Equal(maxRegs, regRulesNoMinNoStandby.MaxRegistrations);
            Assert.Equal((uint)1, regRulesNoMinNoStandby.MinRegistrations);
            Assert.Equal((uint)0, regRulesNoMinNoStandby.MaxStandbyRegistrations);
            Assert.Equal(maxRegs, regRulesWithMinNoStandby.MaxRegistrations);
            Assert.Equal(minRegs, regRulesWithMinNoStandby.MinRegistrations);
            Assert.Equal((uint)0, regRulesWithMinNoStandby.MaxStandbyRegistrations);
            Assert.Equal(maxRegs, regRulesWithMinAndStandby.MaxRegistrations);
            Assert.Equal(minRegs, regRulesWithMinAndStandby.MinRegistrations);
            Assert.Equal(standbyRegs, regRulesWithMinAndStandby.MaxStandbyRegistrations);
        }

        [Fact]
        public void Should_Throw_EventRegistrationRuleInvalidException_For_MaxRegistrations_Zero()
        {
            // Arrange
            uint maxRegs = 0;            

            // Act/Assert
            Assert.Throws<EventRegistrationRulesArgumentException>(() => new EventRegistrationRules(maxRegs));
        }

        [Fact]
        public void Should_Throw_EventRegistrationRuleInvalidException_For_MaxRegs_Less_Than_MinRegs()
        {
            // Arrange
            uint maxRegs = 1;
            uint minRegs = 10;

            // Act/Assert
            Assert.Throws<EventRegistrationRulesArgumentException>(() => new EventRegistrationRules(maxRegs, minRegs));
        }
    }
}
