using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Infrastructure.Repositories;
using Xunit;

namespace JsonDiffer.UnitTest.Infrastructure.Repository
{
    public class DiffRepositoryTests
    {
        private List<DiffJson> _list;
        private DiffRepository _repository;

        public DiffRepositoryTests()
        {
            _list = new List<DiffJson>();
            _repository = new DiffRepository(_list);
        }
        [Fact]
        public void Should_add_diff_and_get_clone()
        {
            var id = "teste";
            var newDiff = new DiffJson(id);
            _list.Add(newDiff);
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
            _list.Add(newDiff);
            var updateDiff = new DiffJson(id) { Left = json + "L", Right = json + "R" };
            _repository.Update(updateDiff);
            Assert.Equal(newDiff, updateDiff);
        }

        [Fact]
        public void Should_clone_before_adding_diff()
        {
            var id = "teste";
            var newDiff = new DiffJson(id);
            _repository.Add(newDiff);
            var clonedDiff = _list.First(d => d.Id == id);
            Assert.Equal(newDiff, clonedDiff);
            Assert.False(newDiff == clonedDiff);
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
