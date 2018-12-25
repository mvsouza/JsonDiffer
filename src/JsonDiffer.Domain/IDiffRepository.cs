using System;
using System.Collections.Generic;
using System.Text;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Domain.Interfaces;

namespace JsonDiffer.Domain
{
    public interface IDiffRepository
    {
        void Add(IDiffer diff);
        IDiffer GetById(string id);
        void Update(IDiffer diff);
    }
}
