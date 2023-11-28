using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class StarSystemDto : ExtendedBaseDto, IConvertibleFromDto<StarSystemDto, Domain.DataRunnerV2.StarSystem>
{
    public Domain.DataRunnerV2.StarSystem ConvertFromDto()
    {
        Domain.DataRunnerV2.StarSystem starSystem = new Domain.DataRunnerV2.StarSystem();
        starSystem.Id = this.Id;
        starSystem.Name = this.Name!;
        starSystem.Code = this.Code!;
        starSystem.IsAvailable = this.IsAvailable;
        starSystem.IsVisible = this.IsVisible;
        starSystem.IsDefault = this.IsDefault;
        starSystem.DateAdded = this.DateAdded;
        starSystem.DateModified = this.DateModified;

        return starSystem;
    }
}
