using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Common
{
    /// <summary>
    /// Abstract base class for Entity classes
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The Id for the Entity
        /// </summary>
        public virtual int Id { get; protected set;}
    }
}
