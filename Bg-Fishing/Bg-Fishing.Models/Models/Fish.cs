using Bg_Fishing.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Models
{
    public class Fish
    {
        private string name;
        private string info;

        public Fish(string name, FishType fishType)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Lakes = new HashSet<Lake>();
        }

        public string Id { get; private set; }

        [Required]
        [Index(IsUnique = true)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                // TODO: Validate

                this.name = value;
            }
        }

        public string Info
        {
            get
            {
                return this.info;
            }

            set
            {
                // TODO: Validate

                this.info = value;
            }
        }

        public FishType FishType { get; set; }

        public virtual ICollection<Lake> Lakes { get; set; }
    }
}
