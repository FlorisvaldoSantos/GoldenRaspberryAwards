using System;
using System.Collections.Generic;

namespace Domain
{
    public class AwardProducer
    {
        public List<ProducerDTO> Min { get; set; } = new List<ProducerDTO>();
        public List<ProducerDTO> Max { get; set; } = new List<ProducerDTO>();

    }
}
