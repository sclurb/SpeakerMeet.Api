﻿using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public class CommunityRandomSpecification : Specification<Community>
    {
        public CommunityRandomSpecification()
        {
            Take = 4;
            Query
                .OrderBy(x => Guid.NewGuid());
        }

        public new int Take { get; internal set; }
    }
}