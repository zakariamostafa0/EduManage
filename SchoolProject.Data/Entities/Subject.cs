namespace SchoolProject.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            StudSubjects = new HashSet<StudentSubject>();
            DeptSubjects = new HashSet<DepartmentSubject>();
            InstSubjects = new HashSet<InstructorSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public DateTime Period { get; set; }

        [InverseProperty(nameof(StudentSubject.Subject))]
        public virtual ICollection<StudentSubject> StudSubjects { get; set; }

        [InverseProperty(nameof(DepartmentSubject.Subject))]
        public virtual ICollection<DepartmentSubject> DeptSubjects { get; set; }

        [InverseProperty(nameof(InstructorSubject.Subject))]
        public virtual ICollection<InstructorSubject> InstSubjects { get; set; }
    }
}
