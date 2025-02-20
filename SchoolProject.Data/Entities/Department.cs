namespace SchoolProject.Data.Entities
{
    public class Department
    {
        public Department()
        {
            Instructors = new HashSet<Instructor>();
            Students = new HashSet<Student>();
            DeptSubjects = new HashSet<DepartmentSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }

        [StringLength(500)]
        public string? DName { get; set; }

        public int? InsManager { get; set; }
        [ForeignKey(nameof(InsManager))]
        [InverseProperty(nameof(Entities.Instructor.DepartmentManager))]
        public virtual Instructor? Instructor { get; set; }

        [InverseProperty(nameof(Student.Department))]
        public virtual ICollection<Student> Students { get; set; }

        [InverseProperty(nameof(DepartmentSubject.Department))]
        public virtual ICollection<DepartmentSubject> DeptSubjects { get; set; }

        [InverseProperty(nameof(Entities.Instructor.Department))]
        public virtual ICollection<Instructor> Instructors { get; set; }

    }
}
