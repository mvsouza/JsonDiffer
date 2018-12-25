using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Domain.Interfaces;

namespace JsonDiffer.Infrastructure.Repositories
{
    public class DiffRepository : IDiffRepository
    {
        private readonly ICollection<DiffJson> _diffs;

        public DiffRepository(ICollection<DiffJson> diff)
        {
            _diffs = diff;
        }
        public void Add(IDiffer diffJson)
        {
            _diffs.Add(diffJson.Clone());
        }

        public IDiffer GetById(string id)
        {
            var diff = _diffs.FirstOrDefault(d => d.Id == id);
            return diff==null ? null: diff.Clone();
        }

        public void Update(IDiffer diff)
        {
            var diffToUpdate = _diffs.FirstOrDefault(d => d.Id == diff.Id);
            diffToUpdate.Left = diff.Left;
            diffToUpdate.Right = diff.Right;
        }
    }
    public static class DiffJsonExtension
    {
        public static DiffJson Clone(this IDiffer diffJson)
        {
            if (diffJson == null)
                return null;
            return new DiffJson(diffJson.Id) { Left = diffJson.Left, Right = diffJson.Right };
        }
    }    

}
