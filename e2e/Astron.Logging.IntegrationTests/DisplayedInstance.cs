using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Logging.IntegrationTests
{
    public class DisplayedInstance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Property { get; set; }

        public DisplayedInstance(int id, string name, short property)
        {
            Id = id;
            Name = name;
            Property = property;
        }
    }
}
