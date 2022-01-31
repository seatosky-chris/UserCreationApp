﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public class CompanyDefaultModel
    {

        /// <summary>
        /// A randomly generated uuid that is unique to this entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The priority to give this selection if multiple options match the user.
        /// The lower the number, the higher the priority (e.g. 1 is the highest).
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// The Company name to fill into the Company field in AD.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// A list of ITG locations to apply this Company to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.Locations"/>
        public List<int> Locations { get; set; } = new List<int>();
    }
}
