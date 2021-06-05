using CompData.Configurations.Constants.Enums;
using CompData.Models.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class SaveRegulationDetailViewModel
    {
        public int RegId { get; set; }
        public bool IsVersion { get; set; }
        public List<ExistingTagGroupViewModel> ExistingTagGroups { get; set; }
        public List<ExistingRelatedLink> ExistingRelatedLinks { get; set; }
        public List<SectionDetailViewModel> Sections { get; set; }
        
    }

    public class SectionDetailViewModel 
    {
        public int? SecId { get; set; }
        public string Title { get; set; }
        public int Seq { get; set; }
        public int? parentId { get; set; }
        public List<DescriptionViewModel> Descriptions { get; set; }
        public List<SectionDetailViewModel> Children { get; set; }
    }

    public class DescriptionViewModel
    {
        public int? DescId { get; set; }
        public string Description { get; set; }
        public int Seq { get; set; }
    }

    public class ExistingTagGroupViewModel
    {
        public enum TagOperationType
        {
            None = 0,
            New = 1,
            Deleted = 2
        }
        public int Id { get; set; }
        public string TagGroupKey { get; set; }
        public string Tag { get; set; }
        public int RegId { get; set; }
        public int SecId { get; set; }
        public int DescId { get; set; }
        public TagType TagType { get; set; }
        public TagOperationType OperationType { get; set; }
    }

    public class ExistingRelatedLink
    {
        public enum RegLinkOperationType
        {
            None = 0,
            New = 1,
            Deleted = 2
        }
        public int Id { get; set; }
        public int LinkedRegId { get; set; }
        public RegLinkOperationType OperationType { get; set; }
    }


}
