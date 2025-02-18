namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject
    {
        [Key]
        public int DepId { get; set; }
        [Key]
        public int SubId { get; set; }

        [ForeignKey(nameof(DepId))]
        [InverseProperty(nameof(Entities.Department.DeptSubjects))]
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(SubId))]
        [InverseProperty(nameof(Entities.Subject.DeptSubjects))]
        public virtual Subject Subject { get; set; }
    }
}
