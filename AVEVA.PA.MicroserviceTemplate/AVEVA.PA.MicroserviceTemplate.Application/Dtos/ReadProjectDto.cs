namespace AVEVA.PA.MicroserviceTemplate.Application.Dtos
{
    public class ReadProjectDto : ProjectDto
    {
        public int ProjectID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastAccessedDate { get; set; }
        public string? FilePath { get; set; }
        public int? DeployedProfileID { get; set; }
        public int? PrismServerID { get; set; }
        public DateTime? LastDeployDate { get; set; }
        public string? LastDeployUser { get; set; }
        public int? PollingInterval { get; set; }
        public DateTime? LOCKEDDATE { get; set; }
        public string? LOCKEDUSER { get; set; }
        public string? LASTMODIFIEDUSER { get; set; }
        public int STATUSID { get; set; }
        public DateTime STATUSLASTUPDATE { get; set; }
        public int ARCHIVERTSID { get; set; }
        public int PROJECTTYPEID { get; set; }
        public int ALIASRTSID { get; set; }
        public int ParentTemplateID { get; set; }
        public int RATING { get; set; }
        public string? CREATEDUSERNAME { get; set; }
        public int TransientInterval { get; set; }
        public int CRITICALITY { get; set; }
        public int? TACID { get; set; }
        public int? TemplateInstanceID { get; set; }
        public int? PAGId { get; set; }
        public int InstanceId { get; set; }
        public int SignalMinPercentValidValue { get; set; }
        public int SignalMinPercentValidSource { get; set; }
        public int AutoFilterEnableValue { get; set; }
        public int AutoFilterEnableSource { get; set; }
        public int PollingIntervalSource { get; set; }
        public int CriticalitySource { get; set; }
        public string? ProjectGUID { get; set; }
        public int? CASETOTAL { get; set; }
        public int? CASECLOSED { get; set; }
        public int? MINCASEPRIORITY { get; set; }
        public int ProcessingMode { get; set; }
    }
}
