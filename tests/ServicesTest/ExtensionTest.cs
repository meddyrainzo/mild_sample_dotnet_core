using System;
using Models.Exceptions;
using Services;
using Xunit;

namespace ServicesTest
{
    public class ExtensionTest
    {
        [Fact]
        public void CheckIfNullWorks()
        {
            //Given
            string value = null;

            //When
            Action action = () => value.CheckNull();
            var exception = Assert.Throws<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Message);
        }

        [Fact]
        public void CheckIfIntIsInvalidWorks()
        {
            // Given
            int value = 0;

            // When
            Action action = () => value.CheckInvalid();
            var exception = Assert.Throws<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }
    }
}