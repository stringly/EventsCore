using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsCore.Application.Common.Models
{
    /// <summary>
    /// Class that collects a request's results
    /// </summary>
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }
        /// <summary>
        /// Indicates whether the request succeeded
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// Contains any validation error messages
        /// </summary>
        public string[] Errors { get; set; }
        /// <summary>
        /// Success result
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(true, new string[] {});
        }
        /// <summary>
        /// Failure result
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
