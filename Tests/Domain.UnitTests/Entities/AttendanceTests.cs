using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.Attendance;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class AttendanceTests
    {
        [Fact]
        public void Given_Minimum_Valid_Values_Attendance_Is_Valid()
        {
            // Arrange
            int userId = 1;

            // Act
            var entity = new Attendance(userId);

            // Assert
            Assert.Equal(userId, entity.UserId);
            Assert.Equal(0.0, entity.Score);
            Assert.Equal(AttendanceStatus.Present, entity.Status);
            Assert.Null(entity.ModuleId);
        }
        [Fact]
        public void Given_All_Valid_Values_Attendance_Is_Valid()
        {
            // Arrange
            int validUserId = 1;
            int validModuleId = 1;
            AttendanceStatus validStatus = AttendanceStatus.Excused;
            double validScore = 100.00;

            // Act
            var entity = new Attendance(validUserId, validModuleId, validStatus, validScore);

            // Assert
            Assert.Equal(validUserId, entity.UserId);
            Assert.Equal(validScore, entity.Score);
            Assert.Equal(validStatus, entity.Status);
            Assert.Equal(validModuleId, entity.ModuleId);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Given_Invalid_UserId_Throws_AttendanceArgumentException(int value)
        {
            // Arrange
            int invalidUserId = value;

            // Act/Assert
            Assert.Throws<AttendanceArgumentException>(() => new Attendance(invalidUserId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Given_Invalid_ModuleId_Throws_AttendanceArgumentException(int value)
        {
            // Arrange
            int validUserId = 1;
            int invalidModuleId = value;

            // Act/Assert
            Assert.Throws<AttendanceArgumentException>(() => new Attendance(validUserId, invalidModuleId));
        }
        [Fact]
        public void Can_Update_Status_Present()
        {
            // Arrange
            int validUserId = 1;
            AttendanceStatus validStatus = AttendanceStatus.Absent;
            var entity = new Attendance(validUserId, null, validStatus);
            Assert.Equal(AttendanceStatus.Absent, entity.Status);

            // Act
            entity.UpdateStatusPresent();

            // Assert
            Assert.Equal(AttendanceStatus.Present, entity.Status);
        }
        [Fact]
        public void Can_Update_Status_Absent()
        {
            // Arrange
            int validUserId = 1;            
            var entity = new Attendance(validUserId);
            Assert.Equal(AttendanceStatus.Present, entity.Status);

            // Act
            entity.UpdateStatusAbsent();

            // Assert
            Assert.Equal(AttendanceStatus.Absent, entity.Status);
        }
        [Fact]
        public void Can_Update_Status_Excused()
        {
            // Arrange
            int validUserId = 1;
            var entity = new Attendance(validUserId);
            Assert.Equal(AttendanceStatus.Present, entity.Status);

            // Act
            entity.UpdateStatusExcused();

            // Assert
            Assert.Equal(AttendanceStatus.Excused, entity.Status);
        }
        [Fact]
        public void Can_Update_Status_Pass()
        {
            // Arrange
            int validUserId = 1;
            var entity = new Attendance(validUserId);
            Assert.Equal(AttendanceStatus.Present, entity.Status);

            // Act
            entity.UpdateStatusPass();

            // Assert
            Assert.Equal(AttendanceStatus.Pass, entity.Status);
        }
        [Fact]
        public void Can_Update_Status_Fail()
        {
            // Arrange
            int validUserId = 1;
            var entity = new Attendance(validUserId);
            Assert.Equal(AttendanceStatus.Present, entity.Status);

            // Act
            entity.UpdateStatusFail();

            // Assert
            Assert.Equal(AttendanceStatus.Fail, entity.Status);
        }
    }
}
