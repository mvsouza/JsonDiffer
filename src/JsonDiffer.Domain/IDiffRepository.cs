using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Entities;

namespace JsonDiffer.Domain
{
    public interface IDiffRepository
    {
        void Add(DiffJson diffJson);
        DiffJson GetById(string id);
        void Update(DiffJson diffJson);
    }
}
