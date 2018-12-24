using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JsonDiffer.Application.Command;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Entities;
using Moq;
using Xunit;

namespace JsonDiffer.UnitTest.Application
{
    public class PushLeftJsonCommandHandlerTests
    {
        private string _id = "teste";
        private string _json = "jsonfile";
        private Expression<Action<IDiffRepository>> _add = r => r.Add(It.IsAny<DiffJson>());
        private Expression<Action<IDiffRepository>> _update = r => r.Update(It.IsAny<DiffJson>());
        private Mock<IDiffRepository> _repository;

        public PushLeftJsonCommandHandlerTests()
        {
            _repository = new Mock<IDiffRepository>();
        }
        [Fact]
        public async Task Should_add_file_to_repositoryAsync()
        {
            
            var handler = new PushLeftJsonCommandHandler(_repository.Object);
            _repository.Setup(r => r.GetById(It.Is<string>(i => i == _id))).Returns<DiffJson>(null);
            await handler.Handle(new PushLeftJsonCommand(_id, _json),default(CancellationToken));
            _repository.Verify(_add, Times.Once);
        }
        [Fact]
        public async Task Should_add_file_to_existend_idAsync()
        {
            var existemDiff = new DiffJson(_id) { Left = null };
            _repository.Setup(r => r.GetById(It.Is<string>(i => i==_id))).Returns(existemDiff);
            var handler = new PushLeftJsonCommandHandler(_repository.Object);
            await handler.Handle(new PushLeftJsonCommand(_id, _json), default(CancellationToken));
            _repository.Verify(_update, Times.Once);
            Assert.Equal(_json, existemDiff.Left);
        }
        [Fact]
        public async Task Shouldnt_add_file_to_existend_filled_left_diffAsync()
        {
            var leftOldContent = "alreadyfilled";
            var existemDiff = new DiffJson(_id) { Left = leftOldContent };
            _repository.Setup(r => r.GetById(It.Is<string>(i => i == _id))).Returns(existemDiff);
            var handler = new PushLeftJsonCommandHandler(_repository.Object);
            await handler.Handle(new PushLeftJsonCommand(_id, _json), default(CancellationToken));
            _repository.Verify(_update, Times.Never);
            _repository.Verify(_add, Times.Never);
            Assert.Equal(leftOldContent, existemDiff.Left);
        }
    }
}
