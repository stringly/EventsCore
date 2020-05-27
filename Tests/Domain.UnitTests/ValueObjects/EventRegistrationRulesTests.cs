using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities.ValueObjects 
{ 
    public class EventRegistrationRulesTests
    {
        [Fact]
        public void RegistrationRules_Given_Valid_Values_Is_Valid()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Should_Throw_EventRegistrationRuleInvalidException_For_MaxRegistrations_Zero()
        {
            // Arrange
                        

            // Act/Assert
        }

        [Fact]
        public void Should_Throw_EventRegistrationRuleInvalidException_For_MaxRegs_Less_Than_MinRegs()
        {
            // Arrange

            // Act/Assert
        }
    }
}
