using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IFilantrope.Models
{
    /// <summary>
    /// Ad model
    /// </summary>
    public class Ad
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public virtual ApplicationUser Author { get; set; }
    }
}