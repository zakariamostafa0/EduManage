namespace SchoolProject.Data.Entities
{
    public class Student
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudID { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(500)]
        public string Phone { get; set; }
        public int? DID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty(nameof(Department.Students))]
        public virtual Department Department { get; set; }

        [InverseProperty(nameof(StudentSubject.Student))]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
