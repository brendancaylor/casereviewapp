﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseReview.DataLayer.Models
{
    public interface IBaseDomainModel
    {
        Guid ID { get; set; }
        int DisplayOrder { get; set; }
        bool IsActive { get; set; }
    }
}
