﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobPosting.Models
{
    public class PostingTemplate
    {
        public int ID { get; set; }

        [StringLength(255, ErrorMessage = "Template Name cannot be more than 255 characters")]
        [Index("IX_Unique_Name", IsUnique = true)]
        public string templateName { get; set; }

        public int pstNumPosition { get; set; }

        public decimal pstFTE { get; set; }

        public decimal pstSalary { get; set; }

        public Int16 pstCompensationType { get; set; }

        public string pstJobDescription { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime pstOpenDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime pstEndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pstJobStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pstJobEndDate { get; set; }

        public int PositionID { get; set; }

        public string RequirementIDs { get; set; }

        public string SkillIDs { get; set; }

        public string LocationIDs { get; set; }

        public string dayIDs { get; set; }
    }
}