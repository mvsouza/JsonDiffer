using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using JsonDiffer.Application.Command;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Domain.Interfaces;
using JsonDiffer.Domain.ValueObject;
using Moq;
using Xunit;

namespace JsonDiffer.UnitTest.Application
{
    public class DiffCommandHandlerTests
    {
        private string _id = "teste";
        private string _json = "{\"some\":\"json\"}";
        private Mock<IDifferResult> _differResult;
        private Mock<IDiffer> _differ;
        private Mock<IDiffRepository> _repostirory;

        public DiffCommandHandlerTests()
        {
            _differResult = new Mock<IDifferResult>();
            _differ = new Mock<IDiffer>();
            _repostirory = new Mock<IDiffRepository>();
        }
        [Fact]
        public async System.Threading.Tasks.Task Should_return_that_jsons_are_equalAsync()
        {
            _differResult.Setup(d => d.AreEqual).Returns(true);
            _differResult.Setup(d => d.Id).Returns(_id);
            _differ.Setup(d => d.Diff()).Returns(_differResult.Object);
            _repostirory.Setup(r => r.GetById(It.Is<string>(i => i == _id))).Returns(_differ.Object);
            var handler = new DiffCommandHandler(_repostirory.Object);
            var result = await handler.Handle(new DiffCommand(_id), default(CancellationToken));
            Assert.True(result.AreEqual);
        }
    }
}
