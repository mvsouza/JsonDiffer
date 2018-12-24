using JsonDiffer.Application.Command;
using JsonDiffer.Application.Validation;
using Xunit;

namespace JsonDiffer.UnitTest.Application.Validation
{
    public class PushLeftJsonCommandValidationTests
    {
        [Fact]
        public void Should_return_invalid_when_json_is_empty()
        {
            var validation = new PushLeftJsonCommandValidation();
            var validationObj = new PushLeftJsonCommand("ID","");
            var result = validation.Validate(validationObj);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void Should_return_invalid_when_id_is_empty()
        {
            var validation = new PushLeftJsonCommandValidation();
            var validationObj = new PushLeftJsonCommand("", "json");
            var result = validation.Validate(validationObj);
            Assert.False(result.IsValid);
        }
    }
}
