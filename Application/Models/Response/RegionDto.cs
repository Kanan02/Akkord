using Application.Entities;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class RegionDto : IListFilterDto<Region, RegionDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public RegionDto SetDto(Region entity)
        {

            Id = entity.Id;
            Name = entity?.Name;
            return this;
        }

        public RegionDto() { }

        public RegionDto(Region region) => SetDto(region);

        public RegionDto(Region region,bool @checked) : this(region) => Checked = @checked;

        

        

    }
}
