using FluentAssertions;
namespace SchoolService.XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Culculate_3_sum_2_should_be_5_with_fluentValidation()
        {
            //Arrange
            var a = 3;
            var b = 2;
            var expected = 5;

            //Act
            var result = a + b;

            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Culculate_3_sum_2_should_be_5_with_fluentValidation1()
        {
            //Arrange
            var a = 3;
            var b = 2;
            var expected = 5;

            //Act
            var result = a + b;

            //Assert
            result.Should().Be(expected);
        }
    }
}