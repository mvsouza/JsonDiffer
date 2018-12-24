using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Infrastructure.Repositories;
using Xunit;

namespace JsonDiffer.UnitTest.Infrastructure.Repository
{
    public class DiffRepositoryTests
    {
        private DiffRepository _repository;

        public DiffRepositoryTests()
        {
            _repository = new DiffRepository();
        }
        [Fact]
        public void Should_add_diff_and_get_clone_by_id()
        {
            var id = "teste";
            var newDiff = new DiffJson(id);
            _repository.Add(newDiff);
            var clonedDiff = _repository.GetById(id);
            Assert.Equal(newDiff, clonedDiff);
            Assert.False(newDiff==clonedDiff);
        }

        [Fact]
        public void Should_update_sides_diff()
        {
            var id = "teste";
            var json = "json";
            var newDiff = new DiffJson(id);
            _repository.Add(newDiff);
            _repository.Update(new DiffJson(id) { Left = json, Right = json });
            var diffSaved = _repository.GetById(id);
            Assert.NotEqual(newDiff, diffSaved);
            Assert.Equal(json, diffSaved.Right);
            Assert.Equal(json, diffSaved.Left);
        }

        [Fact]
        public void Should_clone_before_adding_diff()
        {
            var id = "teste";
            var json = "json";
            var newDiff = new DiffJson(id);
            _repository.Add(newDiff);
            newDiff.Left = json;
            newDiff.Right = json;
            var diffSaved = _repository.GetById(id);
            Assert.Null(diffSaved.Right);
            Assert.Null(diffSaved.Left);
        }

        [Fact]
        public void Shouldnt_find_any_diff()
        {
            Assert.Null(_repository.GetById("null_diff"));
        }

        [Fact]
        public void Should_clone_null_DiffJson()
        {
            DiffJson json = null;
            Assert.Null(json.Clone());
        }

    }
}
