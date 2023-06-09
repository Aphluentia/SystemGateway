﻿using PublicAPI.Models.DatabaseDtos;
using PublicAPI.Models.Dtos;
using PublicAPI.Models.Dtos.Modules;
using PublicAPI.Models.Entities;
using PublicAPI.Models.Enums;
using PublicAPI.Services.Base;
using Newtonsoft.Json;
using System.Text.Json;

namespace PublicAPI.Operations
{
    public class UpdateModuleDataOperation : ApplicationBase<UpdateModuleDataInputDto, UpdateModuleDataOutputDto>
    {
        private readonly IServiceAggregator _ServiceAggregator;
        public UpdateModuleDataOperation(ServiceAggregator ServiceAggregator)
        {
            this._ServiceAggregator = ServiceAggregator;
        }
        public async Task<OutputMessage<UpdateModuleDataOutputDto>> Init(UpdateModuleDataInputDto input)
        {
            return await base.Init(input);
        }

        public async override Task<OutputMessage<UpdateModuleDataOutputDto>> Run(UpdateModuleDataInputDto input)
        {
            var (success, moduleString) = await _ServiceAggregator.DatabaseProvider.Get($"/Modules/{input.Id}");
            if (!success || string.IsNullOrEmpty(moduleString))
                return OutputMessage<UpdateModuleDataOutputDto>.GetOutputMessage().AddError(ApplicationErrors.FailedToCallDatabase);
            var moduleDto = JsonConvert.DeserializeObject<ModuleDto>(moduleString);
            var Module = new Module
            {
                ModuleType = moduleDto.ModuleType,
                Data = input.Data,
                Id = moduleDto.Id
            };
            (success, var _) = await _ServiceAggregator.DatabaseProvider.Put($"/Modules/{input.Id}", Module);
            if (!success)
                return OutputMessage<UpdateModuleDataOutputDto>.GetOutputMessage().AddError(ApplicationErrors.FailedToCallDatabase);
            return OutputMessage<UpdateModuleDataOutputDto>.GetOutputMessage(new UpdateModuleDataOutputDto { Module = Module });

        }

        public override (bool, Error?) ValidateInput(UpdateModuleDataInputDto input)
        {
            return (false, null);
        }
    }
}
