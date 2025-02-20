namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudId { get; set; }
        [Key]
        public int SubId { get; set; }

        public int? Grade { get; set; }

        [ForeignKey(nameof(StudId))]
        [InverseProperty(nameof(Entities.Student.StudentSubjects))]
        public virtual Student? Student { get; set; }

        [ForeignKey(nameof(SubId))]
        [InverseProperty(nameof(Entities.Subject.StudSubjects))]
        public virtual Subject? Subject { get; set; }

    }
}
