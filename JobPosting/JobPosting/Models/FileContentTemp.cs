﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobPosting.Models
{
    public class FileContentTemp
    {
        [Key, ForeignKey("aFileTemp")]
        public int FileContentTempID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        [StringLength(256)]
        [ScaffoldColumn(false)]
        public string MimeType { get; set; }

        public virtual aFileTemp aFileTemp { get; set; }
    }
}