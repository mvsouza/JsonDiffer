using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Entities;

namespace JsonDiffer.Infrastructure.Repositories
{
    public class DiffRepository : IDiffRepository
    {
        private ICollection<DiffJson> _diffs;

        public DiffRepository(ICollection<DiffJson> diff)
        {
            _diffs = diff;
        }
        public void Add(DiffJson diffJson)
        {
            _diffs.Add(diffJson.Clone());
        }

        public DiffJson GetById(string id)
        {
            var diff = _diffs.FirstOrDefault(d => d.Id == id);
            return diff==null ? null: diff.Clone();
        }

        public void Update(DiffJson diffJson)
        {
            var diff = _diffs.FirstOrDefault(d => d.Id == diffJson.Id);
            diff.Left = diffJson.Left;
            diff.Right = diffJson.Right;
        }
    }
    public static class DiffJsonExtension
    {
        public static DiffJson Clone(this DiffJson diffJson)
        {
            if (diffJson == null)
                return null;
            return new DiffJson(diffJson.Id) { Left = diffJson.Left, Right = diffJson.Right };
        }
    }    

}
