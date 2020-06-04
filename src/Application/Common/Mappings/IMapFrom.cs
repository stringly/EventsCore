using AutoMapper;

namespace EventsCore.Application.Common.Mappings
{
    /// <summary>
    /// Defines a mapping 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Sets the mapping profile.
        /// </summary>
        /// <param name="profile"></param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
