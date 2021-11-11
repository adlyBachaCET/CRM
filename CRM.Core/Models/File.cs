using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class File
    {
        public int IdFile { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string Format { get; set; }

        public string FileName { get; set; }
        public int Lvl { get; set; }
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }

        public int IdParent { get; set; }
        public int VersionParent { get; set; }
        public Status StatusParent { get; set; }
        public File File1 { get; set; }
        public ICollection<File> File1Nav { get; set; }

        public virtual ICollection<BuFile> BuFile { get; set; }
        public virtual ICollection<SharedFiles> SharedFiles { get; set; }
        public virtual ICollection<FavouriteFiles> FavouriteFiles { get; set; }
        public virtual ICollection<VisitFileTracking> VisitFileTracking { get; set; }

        public virtual ICollection<ProductFile> ProductFile { get; set; }

    }
}
