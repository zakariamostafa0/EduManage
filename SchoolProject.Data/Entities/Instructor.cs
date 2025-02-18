namespace SchoolProject.Data.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InstSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public string? Image { get; set; }


        public int? SupervisorId { get; set; }
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructors))]
        public Instructor? SuperVisor { get; set; }


        [InverseProperty(nameof(SuperVisor))]
        public virtual ICollection<Instructor> Instructors { get; set; }


        public int DID { get; set; }
        [ForeignKey(nameof(DID))]
        [InverseProperty(nameof(Entities.Department.Instructors))]
        public Department? Department { get; set; }


        [InverseProperty(nameof(Entities.Department.Instructor))]
        public Department? DepartmentManager { get; set; }


        [InverseProperty(nameof(InstructorSubject.Instructor))]
        public virtual ICollection<InstructorSubject> InstSubjects { get; set; }

    }
}
