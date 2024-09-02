using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request.Ui
{
    public class ManagerPointReplacementUi
    {
        public int[] Regions { get; set; }
        public Guid FromManager { get; set; }
        public Guid ToManager { get; set; }

        public void ValidateReplace()
        {
            if (Regions == null || Regions.Length == 0)
                throw new AkkordException("regions_required");

            if (FromManager == null || FromManager == Guid.Empty)
                throw new AkkordException("from_manager_required");

            if (ToManager == null || ToManager == Guid.Empty)
                throw new AkkordException("from_manager_required");
        }
    }
}
